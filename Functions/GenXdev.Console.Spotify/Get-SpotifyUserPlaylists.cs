// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Get-SpotifyUserPlaylists.cs
// Original author           : René Vaessen / GenXdev
// Version                   : 2.3.2026
// ################################################################################
// Copyright (c)  René Vaessen / GenXdev
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ################################################################################



using System.Management.Automation;
using System.Text.RegularExpressions;

namespace GenXdev.Console.Spotify
{
    /// <summary>
    /// <para type="synopsis">
    /// Returns a collection of Spotify playlists owned by the current user.
    /// </para>
    ///
    /// <para type="description">
    /// Retrieves all playlists owned by the current Spotify user, with optional
    /// filtering. Results are cached for 12 hours to improve performance unless Force is
    /// specified.
    /// </para>
    ///
    /// <para type="description">
    /// PARAMETERS
    /// </para>
    ///
    /// <para type="description">
    /// -Filter &lt;String[]&gt;<br/>
    /// Specifies a wildcard pattern to filter playlists by name. Accepts pipeline input.
    /// Multiple patterns can be provided.<br/>
    /// - <b>Aliases</b>: Uri, Id, Name<br/>
    /// - <b>Position</b>: 0<br/>
    /// - <b>Default</b>: "*"<br/>
    /// </para>
    ///
    /// <para type="description">
    /// -Force &lt;SwitchParameter&gt;<br/>
    /// Forces retrieval of fresh data from Spotify API instead of using cached results.<br/>
    /// - <b>Position</b>: Named<br/>
    /// - <b>Default</b>: False<br/>
    /// </para>
    ///
    /// <example>
    /// <para>Get-SpotifyUserPlaylists -Filter "Rock*" -Force</para>
    /// <para>Retrieves playlists matching "Rock*" pattern, forcing fresh data from API.</para>
    /// <code>
    /// Get-SpotifyUserPlaylists -Filter "Rock*" -Force
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>"*Metal*" | gupl</para>
    /// <para>Pipes a filter pattern to get playlists matching "*Metal*".</para>
    /// <code>
    /// "*Metal*" | gupl
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "SpotifyUserPlaylists")]
    [Alias("gupl")]
    [OutputType(typeof(PSObject))]
    public class GetSpotifyUserPlaylistsCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// Specifies a wildcard pattern to filter playlists by name. Accepts pipeline input.
        /// Multiple patterns can be provided.
        /// </summary>
        [Parameter(
            Mandatory = false,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Wildcard pattern to filter playlists by name")]
        [Alias("Uri", "Id", "Name")]
        public string[] Filter { get; set; } = new string[] { "*" };

        /// <summary>
        /// Forces retrieval of fresh data from Spotify API instead of using cached results.
        /// </summary>
        [Parameter(
            Mandatory = false,
            HelpMessage = "Force fresh data retrieval instead of using cache")]
        public SwitchParameter Force { get; set; }

        // Static cache to mimic PowerShell global variable
        private static List<PSObject> spotifyPlaylistCache = null;

        // Store apiToken and filePath from BeginProcessing
        private PSObject apiToken;
        private string filePath;

        /// <summary>
        /// Initialization logic - get API token and determine cache file path
        /// </summary>
        protected override void BeginProcessing()
        {
            // Get Spotify API authentication token
            var tokenResult = InvokeCommand.InvokeScript("GenXdev.Console\\Get-SpotifyApiToken");
            if (tokenResult.Count > 0)
            {
                apiToken = tokenResult[0] as PSObject;
            }

            // Determine cache file location
            filePath = Path.Combine(GetGenXdevAppDataPath(), "Spotify.Playlists.json");

            WriteVerbose($"Cache file: {filePath}");
        }

        /// <summary>
        /// Main processing logic - handle caching and filtering
        /// </summary>
        protected override void ProcessRecord()
        {
            // Check if we should try using cached data
            if (!Force.ToBool())
            {
                // Check if static cache is empty
                if (spotifyPlaylistCache == null)
                {
                    WriteVerbose("Global cache empty, checking file cache");

                    // Create file info object for cache file
                    var playlistCacheFile = new FileInfo(filePath);

                    // Check if cache file exists and is less than 12 hours old
                    if (playlistCacheFile.Exists &&
                        (DateTime.Now - playlistCacheFile.LastWriteTime < TimeSpan.FromHours(12)))
                    {
                        WriteVerbose("Loading playlists from cache file");

                        // Load cached data from file
                        var cachedData = ReadJsonWithRetry(filePath);
                        if (cachedData is object[] psObjectArray)
                        {
                            spotifyPlaylistCache = new List<PSObject>(psObjectArray.Cast<PSObject>());
                        }
                    }
                }
            }

            // Check if we need to fetch fresh data
            if (Force.ToBool() || spotifyPlaylistCache == null || spotifyPlaylistCache.Count == 0)
            {
                WriteVerbose("Retrieving fresh playlist data from Spotify API");

                // Get playlists from Spotify API
                var playlistsResult = InvokeCommand.InvokeScript("[GenXdev.Helpers.Spotify]::GetUserPlaylists($args[0], '*')", apiToken);
                if (playlistsResult.Count > 0 && playlistsResult[0].BaseObject is List<PSObject> list)
                {
                    spotifyPlaylistCache = list;
                }

                // Save to cache file
                string jsonString = ConvertToJson(spotifyPlaylistCache, 100);
                File.WriteAllText(filePath, jsonString);
            }

            // Filter and return playlists matching pattern
            if (spotifyPlaylistCache != null)
            {
                foreach (var playlist in spotifyPlaylistCache)
                {
                    // Get the Name property
                    var nameProperty = playlist.Properties["Name"];
                    if (nameProperty != null)
                    {
                        string name = nameProperty.Value?.ToString();
                        if (!string.IsNullOrEmpty(name))
                        {
                            // Check if name matches any filter pattern (mimicking PowerShell -like behavior)
                            bool matches = false;
                            if (Filter.Length == 1)
                            {
                                matches = Like(name, Filter[0]);
                            }
                            // If Filter has multiple elements, no match (matching original behavior)

                            if (matches)
                            {
                                WriteObject(playlist);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Cleanup logic - empty as in original
        /// </summary>
        protected override void EndProcessing()
        {
            // No cleanup needed
        }

        /// <summary>
        /// Implements PowerShell-like wildcard matching using regex
        /// </summary>
        /// <param name="input">The string to test</param>
        /// <param name="pattern">The wildcard pattern</param>
        /// <returns>True if the input matches the pattern</returns>
        private bool Like(string input, string pattern)
        {
            // Escape regex special chars except * and ?
            string escaped = Regex.Escape(pattern);
            // Replace escaped * with .* and escaped ? with .
            string regexPattern = "^" + escaped.Replace(@"\*", ".*").Replace(@"\?", ".") + "$";
            return Regex.IsMatch(input, regexPattern, RegexOptions.IgnoreCase);
        }
    }
}
// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Get-SpotifyPlaylistIdsByName.cs
// Original author           : René Vaessen / GenXdev
// Version                   : 3.3.2026
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

namespace GenXdev.Console.Spotify
{
    /// <summary>
    /// <para type="synopsis">
    /// Retrieves Spotify playlist IDs by their names.
    /// </para>
    ///
    /// <para type="description">
    /// This function searches for Spotify playlists by name and returns their
    /// corresponding IDs. It follows a three-step process:
    /// 1. Searches in the current session's playlists
    /// 2. Checks a local cache file if no results found
    /// 3. Forces a fresh fetch if cache is outdated or missing
    ///
    /// The function returns all playlist IDs that match the provided names.
    /// </para>
    ///
    /// <para type="description">
    /// PARAMETERS
    /// </para>
    ///
    /// <para type="description">
    /// -PlaylistName &lt;string[]&gt;<br/>
    /// An array of playlist names to search for. The function will match these names
    /// against your Spotify playlists and return the IDs of matching playlists.<br/>
    /// - <b>Aliases</b>: Name<br/>
    /// - <b>Position</b>: 0<br/>
    /// - <b>Mandatory</b>: true<br/>
    /// - <b>ValueFromPipeline</b>: true<br/>
    /// - <b>ValueFromPipelineByPropertyName</b>: true<br/>
    /// </para>
    ///
    /// <example>
    /// <para>Get playlist IDs for specific names</para>
    /// <para>This example retrieves the IDs for playlists named "My Favorites" and "Workout Mix".</para>
    /// <code>
    /// Get-SpotifyPlaylistIdsByName -PlaylistName "My Favorites", "Workout Mix"
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Get playlist ID using pipeline input</para>
    /// <para>This example demonstrates using pipeline input to get a playlist ID.</para>
    /// <code>
    /// "Chill Vibes" | Get-SpotifyPlaylistIdsByName
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "SpotifyPlaylistIdsByName")]
    [OutputType(typeof(string))]
    public class GetSpotifyPlaylistIdsByNameCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// An array of playlist names to search for. The function will match these names
        /// against your Spotify playlists and return the IDs of matching playlists.
        /// </summary>
        [Parameter(
            ParameterSetName = "ByName",
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "One or more Spotify playlist names to search for")]
        [Alias("Name")]
        public string[] PlaylistName { get; set; }

        /// <summary>
        /// Begin processing - initialization logic
        /// </summary>
        protected override void BeginProcessing()
        {
            // Log the start of playlist lookup with provided names
            WriteVerbose($"Starting playlist ID lookup for: {string.Join(", ", PlaylistName)}");
        }

        /// <summary>
        /// Process record - main cmdlet logic
        /// </summary>
        protected override void ProcessRecord()
        {
            // Attempt to find playlists in current session
            var getPlaylistsScript = ScriptBlock.Create("param($filter) GenXdev.Console\\Get-SpotifyUserPlaylists -Filter $filter");
            var results = getPlaylistsScript.Invoke(PlaylistName).Cast<PSObject>().ToArray();

            // Handle case when no playlists found in current session
            if (results.Length == 0)
            {
                WriteVerbose("No playlists found in session, checking local cache...");

                // Construct path to cache file
                string filePath = ExpandPath(Path.Combine(GetGenXdevAppDataPath(), "Spotify.Playlists.json"));

                // Ensure directory exists
                string directoryPath = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Get cache file info
                var playlistCache = new FileInfo(filePath);

                // Refresh if cache is outdated (>12 hours) or missing
                if (!playlistCache.Exists ||
                    (DateTime.Now - playlistCache.LastWriteTime >= TimeSpan.FromHours(12)))
                {
                    WriteVerbose("Cache missing or expired, forcing playlist refresh...");
                    var forceGetPlaylistsScript = ScriptBlock.Create("param($filter) GenXdev.Console\\Get-SpotifyUserPlaylists -Force -Filter $filter");
                    results = forceGetPlaylistsScript.Invoke(PlaylistName).Cast<PSObject>().ToArray();
                }
            }

            // Throw error if no matching playlists found
            if (results.Length == 0)
            {
                var errorRecord = new ErrorRecord(
                    new Exception("Playlist not found"),
                    "PlaylistNotFound",
                    ErrorCategory.ObjectNotFound,
                    null);
                ThrowTerminatingError(errorRecord);
                return;
            }

            // Return the IDs of matching playlists
            foreach (var result in results)
            {
                WriteObject(result.Properties["Id"].Value);
            }
        }

        /// <summary>
        /// End processing - cleanup logic
        /// </summary>
        protected override void EndProcessing()
        {
            // No cleanup needed
        }
    }
}
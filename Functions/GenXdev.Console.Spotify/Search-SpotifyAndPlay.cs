// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Search-SpotifyAndPlay.cs
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
    /// Performs a Spotify search and plays the first found item.
    /// </para>
    ///
    /// <para type="description">
    /// Searches Spotify using the provided query string and automatically plays the first
    /// matching item on the currently active Spotify device. Can search for different
    /// types of content including tracks, albums, artists, playlists, shows and episodes.
    /// </para>
    ///
    /// <para type="description">
    /// PARAMETERS
    /// </para>
    ///
    /// <para type="description">
    /// -Queries &lt;string[]&gt;<br/>
    /// One or more search phrases to look for on Spotify. Each query will be processed
    /// in sequence.<br/>
    /// - <b>Aliases</b>: q, Name, Text, Query<br/>
    /// - <b>Position</b>: 0<br/>
    /// - <b>Mandatory</b>: true<br/>
    /// </para>
    ///
    /// <para type="description">
    /// -SearchType &lt;SpotifyAPI.Web.SearchRequest.Types&gt;<br/>
    /// The type of content to search for. Valid options are:
    /// Track (default), Album, Artist, Playlist, Show, Episode, All<br/>
    /// - <b>Aliases</b>: t<br/>
    /// - <b>Position</b>: 1<br/>
    /// - <b>Default</b>: Track<br/>
    /// </para>
    ///
    /// <example>
    /// <para>Search for and play a track by Rage Against the Machine</para>
    /// <para>This example searches for tracks by Rage Against the Machine and plays the first result.</para>
    /// <code>
    /// Search-SpotifyAndPlay -Queries "Rage against the machine" -SearchType Track
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Search for and play a track using the alias</para>
    /// <para>This example uses the fmp alias to search for and play a Dire Straits track.</para>
    /// <code>
    /// fmp "Dire Straits You and your friend"
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet("Search", "SpotifyAndPlay")]
    [Alias("smp", "fmp")]
    [OutputType(typeof(object))]
    public class SearchSpotifyAndPlayCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// One or more search phrases to look for on Spotify
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "One or more search phrases to look for on Spotify")]
        [Alias("q", "Name", "Text", "Query")]
        public string[] Queries { get; set; }

        /// <summary>
        /// The type of content to search for
        /// </summary>
        [Parameter(
            Position = 1,
            Mandatory = false,
            HelpMessage = "The type of content to search for")]
        [Alias("t")]
        public SpotifyAPI.Web.SearchRequest.Types SearchType { get; set; } = SpotifyAPI.Web.SearchRequest.Types.Track;

        /// <summary>
        /// Begin processing - initialization logic
        /// </summary>
        protected override void BeginProcessing()
        {
            WriteVerbose($"Search type bit mask: {SearchType}");
        }

        /// <summary>
        /// Process record - main cmdlet logic
        /// </summary>
        protected override void ProcessRecord()
        {
            foreach (var query in Queries)
            {
                WriteVerbose($"Searching Spotify for: {query}");

                try
                {
                    // Get the Spotify API token by invoking the PowerShell function
                    var tokenResults = InvokeCommand.InvokeScript("GenXdev.Console\\Get-SpotifyApiToken");
                    var token = tokenResults.Count > 0 ? (string)tokenResults[0].BaseObject : null;

                    if (token != null)
                    {
                        // Call the SearchAndPlay method from the GenXdev.Helpers assembly
                        var rawResults = GenXdev.Helpers.Spotify.SearchAndPlay(token, query, SearchType);

                        if (rawResults != null)
                        {
                            var results = rawResults as IEnumerable<object> ?? new[] { rawResults };

                            foreach (var item in results)
                            {
                                if (item != null)
                                {
                                    WriteObject(item);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    // Silently continue on errors to match -ErrorAction SilentlyContinue behavior
                }
            }
        }

        /// <summary>
        /// End processing - cleanup logic
        /// </summary>
        protected override void EndProcessing()
        {
            // Empty as in original PowerShell function
        }
    }
}
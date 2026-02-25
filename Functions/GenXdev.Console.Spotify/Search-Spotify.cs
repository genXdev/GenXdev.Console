// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Search-Spotify.cs
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
    /// Performs a Spotify search and returns matching items.
    /// </para>
    ///
    /// <para type="description">
    /// Searches Spotify's catalog for items matching the provided search query. Can
    /// search for tracks, albums, artists, playlists, shows, and episodes. Results are
    /// returned through the pipeline.
    /// </para>
    ///
    /// <para type="description">
    /// PARAMETERS
    /// </para>
    ///
    /// <para type="description">
    /// -Queries &lt;string[]&gt;<br/>
    /// One or more search phrases to look up in Spotify's catalog. Multiple queries will<br/>
    /// be processed sequentially.<br/>
    /// - <b>Aliases</b>: q, Name, Text, Query<br/>
    /// - <b>Position</b>: 0<br/>
    /// - <b>Mandatory</b>: true<br/>
    /// </para>
    ///
    /// <para type="description">
    /// -SearchType &lt;SpotifyAPI.Web.SearchRequest.Types&gt;<br/>
    /// The type(s) of items to search for. Valid values are: Album, Artist, Playlist,<br/>
    /// Track, Show, Episode, or All. Default is Track.<br/>
    /// - <b>Aliases</b>: t<br/>
    /// - <b>Position</b>: 1<br/>
    /// - <b>Default</b>: Track<br/>
    /// </para>
    ///
    /// <example>
    /// <para>Search for tracks by Rage Against the Machine</para>
    /// <para>Searches Spotify for tracks matching the query.</para>
    /// <code>
    /// Search-Spotify -Queries "Rage against the machine" -SearchType Track
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Using alias and positional parameter</para>
    /// <para>Demonstrates using the 'fm' alias with positional parameters.</para>
    /// <code>
    /// fm "Dire Straits"
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Search, "Spotify")]
    [Alias("sm", "fm")]
    [OutputType(typeof(object))]
    public class SearchSpotifyCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// One or more search phrases to look up in Spotify's catalog. Multiple queries will
        /// be processed sequentially.
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The query to perform")]
        [Alias("q", "Name", "Text", "Query")]
        public string[] Queries { get; set; }

        /// <summary>
        /// The type(s) of items to search for. Valid values are: Album, Artist, Playlist,
        /// Track, Show, Episode, or All. Default is Track.
        /// </summary>
        [Parameter(
            Position = 1,
            Mandatory = false,
            HelpMessage = "Type of content to search for")]
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
            // Get the Spotify API token by invoking the PowerShell function
            var tokenResults = InvokeCommand.InvokeScript("GenXdev.Console\\Get-SpotifyApiToken");

            // Assume the function returns a single token string
            if (tokenResults.Count == 0)
            {
                throw new InvalidOperationException("Failed to retrieve Spotify API token.");
            }

            var token = (string)tokenResults[0].BaseObject;

            // Process each query
            foreach (var query in Queries)
            {
                WriteVerbose($"Searching Spotify for: {query}");

                // Call the helper class to perform the search
                var results = GenXdev.Helpers.Spotify.Search(token, query, SearchType);

                // Output the search results, matching the PowerShell pipeline behavior
                WriteObject(results);
            }
        }
    }
}
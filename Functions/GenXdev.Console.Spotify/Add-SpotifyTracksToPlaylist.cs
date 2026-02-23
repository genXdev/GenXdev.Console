// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Add-SpotifyTracksToPlaylist.cs
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

namespace GenXdev.Console.Spotify
{
    /// <summary>
    /// <para type="synopsis">
    /// Adds tracks to a Spotify playlist.
    /// </para>
    ///
    /// <para type="description">
    /// Adds one or more tracks to either a named Spotify playlist or a playlist
    /// specified by its ID. Supports pipeline input for track URIs.
    /// </para>
    ///
    /// <para type="description">
    /// PARAMETERS
    /// </para>
    ///
    /// <para type="description">
    /// -PlaylistName &lt;string[]&gt;<br/>
    /// The name(s) of the Spotify playlist(s) to add tracks to.<br/>
    /// - <b>Aliases</b>: Name<br/>
    /// - <b>Position</b>: 0<br/>
    /// </para>
    ///
    /// <para type="description">
    /// -PlaylistId &lt;string[]&gt;<br/>
    /// The Spotify playlist ID(s) to add tracks to.<br/>
    /// - <b>Position</b>: 0<br/>
    /// </para>
    ///
    /// <para type="description">
    /// -Uri &lt;string[]&gt;<br/>
    /// The Spotify track URIs that should be added to the playlist.<br/>
    /// - <b>Position</b>: 1<br/>
    /// </para>
    ///
    /// <example>
    /// <para>Add tracks to a playlist by name</para>
    /// <para>Adds a track to the playlist named "My Favorites".</para>
    /// <code>
    /// Add-SpotifyTracksToPlaylist -PlaylistName "My Favorites" `
    ///     -Uri "spotify:track:1234567890"
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Add tracks using pipeline</para>
    /// <para>Adds tracks to a playlist using pipeline input.</para>
    /// <code>
    /// "spotify:track:1234567890" | Add-SpotifyTracksToPlaylist "My Playlist"
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "SpotifyTracksToPlaylist")]
    [Alias("addtoplaylist")]
    [OutputType(typeof(void))]
    public class AddSpotifyTracksToPlaylistCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// The name(s) of the Spotify playlist(s) to add tracks to.
        /// </summary>
        [Parameter(
            ParameterSetName = "ByName",
            Mandatory = true,
            Position = 0,
            HelpMessage = "The Spotify playlist to add tracks to")]
        [Alias("Name")]
        public string[] PlaylistName { get; set; }

        /// <summary>
        /// The Spotify playlist ID(s) to add tracks to.
        /// </summary>
        [Parameter(
            ParameterSetName = "ById",
            Mandatory = true,
            Position = 0,
            HelpMessage = "The Spotify playlist to add tracks to")]
        public string[] PlaylistId { get; set; }

        /// <summary>
        /// The Spotify track URIs that should be added to the playlist.
        /// </summary>
        [Parameter(
            Mandatory = false,
            Position = 1,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Spotify tracks that should be added to the playlist")]
        public string[] Uri { get; set; } = new string[0];

        private string apiToken;
        private string[] effectivePlaylistIds;

        /// <summary>
        /// Begin processing - retrieve API token and convert playlist names to IDs
        /// </summary>
        protected override void BeginProcessing()
        {
            // Get the Spotify API authentication token
            var scriptBlock = ScriptBlock.Create("GenXdev.Console\\Get-SpotifyApiToken");
            var result = scriptBlock.Invoke();
            apiToken = result[0].ToString();
            WriteVerbose("Retrieved Spotify API token");

            // If playlist names were provided, convert them to playlist IDs
            if (PlaylistName != null && PlaylistName.Length > 0)
            {
                WriteVerbose("Converting playlist names to IDs");
                var namesParam = PlaylistName;
                var scriptBlock2 = ScriptBlock.Create("param($names) GenXdev.Console\\Get-SpotifyPlaylistIdsByName -PlaylistName $names");
                var result2 = scriptBlock2.Invoke(namesParam);
                effectivePlaylistIds = ((IEnumerable<object>)result2).Select(o => o.ToString()).ToArray();
                WriteVerbose($"Found {effectivePlaylistIds.Length} matching playlists");
            }
            else
            {
                effectivePlaylistIds = PlaylistId;
            }
        }

        /// <summary>
        /// Process record - add tracks to playlists
        /// </summary>
        protected override void ProcessRecord()
        {
            // Add provided tracks to each specified playlist
            foreach (var id in effectivePlaylistIds)
            {
                WriteVerbose($"Adding {Uri.Length} tracks to playlist {id}");
                GenXdev.Helpers.Spotify.AddToPlaylist(apiToken, id, Uri);
            }
        }

        /// <summary>
        /// End processing - no cleanup needed
        /// </summary>
        protected override void EndProcessing()
        {
        }
    }
}
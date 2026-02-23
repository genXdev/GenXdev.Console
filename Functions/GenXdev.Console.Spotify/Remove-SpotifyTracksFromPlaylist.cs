// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Remove-SpotifyTracksFromPlaylist.cs
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
    /// Removes tracks from a Spotify playlist.
    /// </para>
    ///
    /// <para type="description">
    /// Removes one or more tracks from either a named Spotify playlist or a playlist
    /// specified by its ID. Supports pipeline input for track URIs.
    /// </para>
    ///
    /// <para type="description">
    /// PARAMETERS
    /// </para>
    ///
    /// <para type="description">
    /// -PlaylistName &lt;string[]&gt;<br/>
    /// The name(s) of the Spotify playlist(s) to remove tracks from.<br/>
    /// - <b>Position</b>: 0<br/>
    /// </para>
    ///
    /// <para type="description">
    /// -PlaylistId &lt;string[]&gt;<br/>
    /// The Spotify playlist ID(s) to remove tracks from.<br/>
    /// - <b>Position</b>: 0<br/>
    /// </para>
    ///
    /// <para type="description">
    /// -Uri &lt;string[]&gt;<br/>
    /// The Spotify track URIs that should be removed from the playlist.<br/>
    /// - <b>Position</b>: 1<br/>
    /// - <b>Default</b>: @()<br/>
    /// </para>
    ///
    /// <example>
    /// <para>Remove tracks from a playlist by name</para>
    /// <para>Removes a track from the playlist named "My Playlist".</para>
    /// <code>
    /// Remove-SpotifyTracksFromPlaylist -PlaylistName "My Playlist" -Uri "spotify:track:1234567890"
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Remove tracks using pipeline</para>
    /// <para>Pipes track URIs to remove from the playlist.</para>
    /// <code>
    /// "spotify:track:1234567890" | Remove-SpotifyTracksFromPlaylist "My Playlist"
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "SpotifyTracksFromPlaylist")]
    [Alias("removefromplaylist")]
    [OutputType(typeof(void))]
    public class RemoveSpotifyTracksFromPlaylistCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// The name(s) of the Spotify playlist(s) to remove tracks from.
        /// </summary>
        [Parameter(
            ParameterSetName = "ByName",
            Mandatory = true,
            Position = 0,
            HelpMessage = "The Spotify playlist to delete tracks from")]
        public string[] PlaylistName { get; set; }

        /// <summary>
        /// The Spotify playlist ID(s) to remove tracks from.
        /// </summary>
        [Parameter(
            ParameterSetName = "ById",
            Mandatory = true,
            Position = 0,
            HelpMessage = "The Spotify playlist to delete tracks from")]
        public string[] PlaylistId { get; set; }

        /// <summary>
        /// The Spotify track URIs that should be removed from the playlist.
        /// </summary>
        [Parameter(
            Mandatory = false,
            Position = 1,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Spotify tracks that should be removed from the playlist")]
        public string[] Uri { get; set; } = new string[0];

        private object apiToken;
        private string[] playlistIds;

        /// <summary>
        /// Begin processing - retrieve API token and convert playlist names to IDs
        /// </summary>
        protected override void BeginProcessing()
        {
            // Get authentication token for spotify api
            var scriptBlock = ScriptBlock.Create("GenXdev.Console\\Get-SpotifyApiToken");
            var tokenResult = scriptBlock.Invoke();
            apiToken = tokenResult.FirstOrDefault();

            WriteVerbose("Retrieved Spotify API token");

            // If playlist names were provided, convert them to playlist ids
            if (PlaylistName != null && PlaylistName.Length > 0)
            {
                WriteVerbose("Converting playlist names to IDs");
                var idsScriptBlock = ScriptBlock.Create("param($names) GenXdev.Console\\Get-SpotifyPlaylistIdsByName -PlaylistName $names");
                var idsResult = idsScriptBlock.Invoke(PlaylistName);
                playlistIds = idsResult.Select(o => o.ToString()).ToArray();
                WriteVerbose($"Found {playlistIds.Length} matching playlists");
            }
            else
            {
                playlistIds = PlaylistId;
            }
        }

        /// <summary>
        /// Process record - remove tracks from playlists
        /// </summary>
        protected override void ProcessRecord()
        {
            // Process each playlist id and remove the specified tracks
            foreach (var id in playlistIds)
            {
                WriteVerbose($"Preparing to remove tracks from playlist with ID: {id}");

                // Use shouldprocess to get confirmation before removing tracks
                if (ShouldProcess($"Playlist ID: {id}", "Remove tracks"))
                {
                    WriteVerbose($"Removing tracks from playlist with ID: {id}");
                    // Call the static method using ScriptBlock
                    var removeScriptBlock = ScriptBlock.Create("param($token, $id, $uris) [GenXdev.Helpers.Spotify]::RemoveFromPlaylist($token, $id, $uris)");
                    removeScriptBlock.Invoke(apiToken, id, Uri);
                }
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
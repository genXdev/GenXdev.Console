// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Get-SpotifyPlaylistTrack.cs
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
    /// Returns all tracks from a Spotify playlist.
    /// </para>
    ///
    /// <para type="description">
    /// Retrieves all tracks from a Spotify playlist by either playlist name or ID. When
    /// using playlist name, it will fetch the first matching playlist's ID and then
    /// retrieve its tracks using the Spotify API.
    /// </para>
    ///
    /// <para type="description">
    /// PARAMETERS
    /// </para>
    ///
    /// <para type="description">
    /// -PlaylistName &lt;string&gt;<br/>
    /// The name of the Spotify playlist to retrieve tracks from. Will match the first<br/>
    /// playlist found with this name.<br/>
    /// - <b>Aliases</b>: Name<br/>
    /// - <b>Position</b>: 0<br/>
    /// </para>
    ///
    /// <para type="description">
    /// -PlaylistId &lt;string&gt;<br/>
    /// The unique Spotify ID of the playlist to retrieve tracks from.<br/>
    /// - <b>Aliases</b>: Id<br/>
    /// - <b>Position</b>: 0<br/>
    /// </para>
    ///
    /// <example>
    /// <para>Get tracks from a playlist by name</para>
    /// <para>Get-SpotifyPlaylistTrack -PlaylistName "My Favorite Songs"</para>
    /// <code>
    /// Get-SpotifyPlaylistTrack -PlaylistName "My Favorite Songs"
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Get tracks from a playlist by ID</para>
    /// <para>Get-SpotifyPlaylistTrack -PlaylistId "37i9dQZF1DXcBWIGoYBM5M"</para>
    /// <code>
    /// Get-SpotifyPlaylistTrack -PlaylistId "37i9dQZF1DXcBWIGoYBM5M"
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Use pipeline input</para>
    /// <para>"My Workout Mix" | Get-SpotifyPlaylistTrack</para>
    /// <code>
    /// "My Workout Mix" | Get-SpotifyPlaylistTrack
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "SpotifyPlaylistTrack", DefaultParameterSetName = "ByName")]
    [Alias("getplaylist")]
    [OutputType(typeof(System.Collections.Generic.List<SpotifyAPI.Web.PlaylistTrack<SpotifyAPI.Web.IPlayableItem>>))]
    public class GetSpotifyPlaylistTrackCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// The name of the Spotify playlist to retrieve tracks from
        /// </summary>
        [Parameter(
            ParameterSetName = "ByName",
            Mandatory = true,
            Position = 0,
            HelpMessage = "The Spotify playlist to return tracks for",
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        [Alias("Name")]
        public string PlaylistName { get; set; }

        /// <summary>
        /// The unique Spotify ID of the playlist to retrieve tracks from
        /// </summary>
        [Parameter(
            ParameterSetName = "ById",
            Mandatory = true,
            Position = 0,
            HelpMessage = "The Spotify playlist to return tracks for",
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        [Alias("Id")]
        public string PlaylistId { get; set; }

        private string apiToken;
        private string effectivePlaylistId;

        /// <summary>
        /// Begin processing - get spotify api authentication token and resolve playlist id
        /// </summary>
        protected override void BeginProcessing()
        {
            // Get spotify api authentication token
            var scriptBlock = ScriptBlock.Create("GenXdev.Console\\Get-SpotifyApiToken");
            var result = scriptBlock.Invoke();
            apiToken = result[0].ToString();

            WriteVerbose("Retrieved Spotify API token");

            // If playlist name provided, get its ID
            if (!string.IsNullOrWhiteSpace(PlaylistName))
            {
                WriteVerbose($"Looking up playlist ID for name: {PlaylistName}");

                var namesParam = new[] { PlaylistName };
                var scriptBlock2 = ScriptBlock.Create("param($names) @(GenXdev.Console\\Get-SpotifyPlaylistIdsByName -PlaylistName $names) | Microsoft.PowerShell.Utility\\Select-Object -First 1");
                var result2 = scriptBlock2.Invoke(namesParam);
                effectivePlaylistId = result2[0].ToString();

                WriteVerbose($"Found playlist ID: {effectivePlaylistId}");
            }
            else
            {
                effectivePlaylistId = PlaylistId;
            }
        }

        /// <summary>
        /// Process record - retrieve tracks for playlist
        /// </summary>
        protected override void ProcessRecord()
        {
            WriteVerbose($"Retrieving tracks for playlist ID: {effectivePlaylistId}");

            // Call spotify api to get playlist tracks
            var scriptBlock = ScriptBlock.Create("param($apiToken, $playlistId) [GenXdev.Helpers.Spotify]::GetPlaylistTracks($apiToken, $playlistId)");
            var result = scriptBlock.Invoke(apiToken, effectivePlaylistId);

            WriteObject(result[0]);
        }

        /// <summary>
        /// End processing - no cleanup required
        /// </summary>
        protected override void EndProcessing()
        {
            // No cleanup required
        }
    }
}
// ###############################################################################
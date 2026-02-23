// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Set-SpotifyPlaylistOrder.cs
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
    /// Reorders tracks within a Spotify playlist by moving a range of items to a new position.
    /// </para>
    ///
    /// <para type="description">
    /// Allows repositioning of one or more tracks within a Spotify playlist by specifying the start position of items to move and their destination position. Requires valid Spotify API authentication.
    /// </para>
    ///
    /// <para type="description">
    /// PARAMETERS
    /// </para>
    ///
    /// <para type="description">
    /// -PlaylistId &lt;String&gt;<br/>
    /// The unique identifier of the Spotify playlist to modify.<br/>
    /// - <b>Position</b>: 0<br/>
    /// - <b>Mandatory</b>: true<br/>
    /// </para>
    ///
    /// <para type="description">
    /// -RangeStart &lt;Int32&gt;<br/>
    /// The zero-based position index of the first track to be moved.<br/>
    /// - <b>Position</b>: 1<br/>
    /// - <b>Mandatory</b>: true<br/>
    /// </para>
    ///
    /// <para type="description">
    /// -InsertBefore &lt;Int32&gt;<br/>
    /// The zero-based position index where the moved tracks should be inserted. For example, to move items to the end of a 10-track playlist, use 10. To move items to the start, use 0.<br/>
    /// - <b>Position</b>: 2<br/>
    /// - <b>Mandatory</b>: true<br/>
    /// </para>
    ///
    /// <para type="description">
    /// -RangeLength &lt;Nullable&lt;Int32&gt;&gt;<br/>
    /// Optional. The number of consecutive tracks to move, starting from RangeStart. Defaults to 1 if not specified.<br/>
    /// - <b>Position</b>: 3<br/>
    /// - <b>Mandatory</b>: false<br/>
    /// - <b>Default</b>: null<br/>
    /// </para>
    ///
    /// <example>
    /// <para>Reorder tracks in a Spotify playlist</para>
    /// <para>This example moves 2 tracks starting from position 5 to the beginning of the playlist.</para>
    /// <code>
    /// Set-SpotifyPlaylistOrder -PlaylistId "2v3iNvBX8Ay1Gt2uXtUKUT" -RangeStart 5 -InsertBefore 0 -RangeLength 2
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Move a single track to the start of the playlist</para>
    /// <para>This example moves the track at position 9 to the beginning of the playlist.</para>
    /// <code>
    /// Set-SpotifyPlaylistOrder "2v3iNvBX8Ay1Gt2uXtUKUT" 9 0
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "SpotifyPlaylistOrder")]
    [OutputType(typeof(void))]
    public class SetSpotifyPlaylistOrderCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// The unique identifier of the Spotify playlist to modify
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 0,
            HelpMessage = "The Spotify playlist identifier to modify")]
        [ValidateNotNullOrEmpty]
        public string PlaylistId { get; set; }

        /// <summary>
        /// The position of the first track to move
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 1,
            HelpMessage = "The position of the first track to move")]
        public int RangeStart { get; set; }

        /// <summary>
        /// The position where tracks should be inserted
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 2,
            HelpMessage = "The position where tracks should be inserted")]
        public int InsertBefore { get; set; }

        /// <summary>
        /// Number of consecutive tracks to move (defaults to 1)
        /// </summary>
        [Parameter(
            Mandatory = false,
            Position = 3,
            HelpMessage = "Number of consecutive tracks to move (defaults to 1)")]
        public int? RangeLength { get; set; }

        private string apiToken;

        /// <summary>
        /// Initialize the cmdlet by retrieving the Spotify API token
        /// </summary>
        protected override void BeginProcessing()
        {
            // Retrieve the current spotify api authentication token
            var scriptBlock = ScriptBlock.Create("GenXdev.Console\\Get-SpotifyApiToken");
            var results = scriptBlock.Invoke();
            apiToken = results[0].BaseObject as string;
        }

        /// <summary>
        /// Process record - reorder the playlist tracks
        /// </summary>
        protected override void ProcessRecord()
        {
            // Calculate the actual number of tracks to move
            int tracksToMove = RangeLength ?? 1;

            // Prepare descriptive message for should process and verbose output
            string operationDescription = $"Moving {tracksToMove} tracks from position {RangeStart} to position {InsertBefore}";
            string targetDescription = $"Spotify playlist {PlaylistId}";

            // Output verbose information about the reordering operation
            WriteVerbose($"Reordering {targetDescription} - {operationDescription}");

            // Check if the action should be performed based on whatif/confirm
            if (ShouldProcess(targetDescription, operationDescription))
            {
                // Call the spotify api to reorder the playlist tracks
                GenXdev.Helpers.Spotify.ReorderPlaylist(
                    apiToken,
                    PlaylistId,
                    RangeStart,
                    InsertBefore,
                    RangeLength);
            }
        }

        /// <summary>
        /// Clean up resources if needed
        /// </summary>
        protected override void EndProcessing()
        {
            // No cleanup needed
        }
    }
}
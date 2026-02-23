// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Set-SpotifyPlaylistDetails.cs
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
    /// Sets the main properties of a Spotify playlist.
    /// </para>
    ///
    /// <para type="description">
    /// Modifies playlist properties including name, description, visibility and
    /// collaboration settings using the Spotify API.
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
    /// -Name &lt;String&gt;<br/>
    /// The new name to set for the playlist.<br/>
    /// - <b>Position</b>: 1<br/>
    /// - <b>Mandatory</b>: true<br/>
    /// </para>
    ///
    /// <para type="description">
    /// -Description &lt;String&gt;<br/>
    /// Optional description text for the playlist.<br/>
    /// - <b>Position</b>: 2<br/>
    /// - <b>Default</b>: ""<br/>
    /// </para>
    ///
    /// <para type="description">
    /// -Public &lt;SwitchParameter&gt;<br/>
    /// Switch to make the playlist visible to all users.<br/>
    /// </para>
    ///
    /// <para type="description">
    /// -Collabrative &lt;SwitchParameter&gt;<br/>
    /// Switch to allow other users to modify the playlist.<br/>
    /// </para>
    ///
    /// <para type="description">
    /// -Private &lt;SwitchParameter&gt;<br/>
    /// Switch to make the playlist only visible to you.<br/>
    /// </para>
    ///
    /// <para type="description">
    /// -NoCollabrations &lt;SwitchParameter&gt;<br/>
    /// Switch to prevent other users from modifying the playlist.<br/>
    /// </para>
    ///
    /// <example>
    /// <para>Set playlist details with public visibility</para>
    /// <para>Updates a Spotify playlist with a new name, description, and makes it public.</para>
    /// <code>
    /// Set-SpotifyPlaylistDetails -PlaylistId "1234567890" -Name "My Playlist" -Description "My favorite songs" -Public
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Set playlist to private with no collaborations</para>
    /// <para>Changes a playlist to private and prevents others from modifying it.</para>
    /// <code>
    /// Set-SpotifyPlaylistDetails "1234567890" "Weekend Mix" -Private -NoCollabrations
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "SpotifyPlaylistDetails")]
    [Alias("spld")]
    public class SetSpotifyPlaylistDetailsCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// The unique identifier of the Spotify playlist to modify
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 0,
            HelpMessage = "The Spotify playlist to make changes to")]
        public string PlaylistId { get; set; }

        /// <summary>
        /// The name for the new playlist
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 1,
            HelpMessage = "The name for the new playlist")]
        public string Name { get; set; }

        /// <summary>
        /// The description for the new playlist
        /// </summary>
        [Parameter(
            Mandatory = false,
            Position = 2,
            HelpMessage = "The description for the new playlist")]
        public string Description { get; set; } = "";

        /// <summary>
        /// Make this a public playlist
        /// </summary>
        [Parameter(
            Mandatory = false,
            HelpMessage = "Make this a public playlist")]
        public SwitchParameter Public { get; set; }

        /// <summary>
        /// Allow others to make changes to this playlist
        /// </summary>
        [Parameter(
            Mandatory = false,
            HelpMessage = "Allow others to make changes to this playlist")]
        public SwitchParameter Collabrative { get; set; }

        /// <summary>
        /// Make the playlist private
        /// </summary>
        [Parameter(
            Mandatory = false,
            HelpMessage = "Make the playlist private")]
        public SwitchParameter Private { get; set; }

        /// <summary>
        /// Disallow others to make changes
        /// </summary>
        [Parameter(
            Mandatory = false,
            HelpMessage = "Disallow others to make changes")]
        public SwitchParameter NoCollabrations { get; set; }

        private string apiToken;
        private bool? publicFlag;
        private bool? collabFlag;

        /// <summary>
        /// Initialize authentication and flags
        /// </summary>
        protected override void BeginProcessing()
        {
            // Get the spotify api authentication token
            var getTokenScript = ScriptBlock.Create("GenXdev.Console\\Get-SpotifyApiToken");
            var tokenResult = getTokenScript.Invoke();
            apiToken = ((PSObject)tokenResult[0]).BaseObject.ToString();

            WriteVerbose("Retrieved Spotify API token");

            // Initialize visibility and collaboration flags
            publicFlag = null;
            collabFlag = null;

            // Set playlist visibility based on switches
            if (Public.ToBool())
            {
                publicFlag = true;
                WriteVerbose("Setting playlist to public");
            }

            // Set collaboration permission based on switches
            if (Collabrative.ToBool())
            {
                collabFlag = true;
                WriteVerbose("Enabling playlist collaboration");
            }

            // Override public flag if private switch is used
            if (Private.ToBool())
            {
                publicFlag = false;
                WriteVerbose("Setting playlist to private");
            }

            // Override collab flag if no collaborations switch is used
            if (NoCollabrations.ToBool())
            {
                collabFlag = false;
                WriteVerbose("Disabling playlist collaboration");
            }
        }

        /// <summary>
        /// Update the playlist details
        /// </summary>
        protected override void ProcessRecord()
        {
            WriteVerbose($"Updating playlist {PlaylistId} with new settings");

            // Call spotify api to update playlist details only if ShouldProcess confirms the action
            if (ShouldProcess($"Spotify playlist '{PlaylistId}'", "Update playlist details"))
            {
                GenXdev.Helpers.Spotify.ChangePlaylistDetails(
                    PlaylistId,
                    apiToken,
                    Name,
                    publicFlag,
                    collabFlag,
                    Description);
            }
        }

        /// <summary>
        /// Cleanup if needed
        /// </summary>
        protected override void EndProcessing()
        {
        }
    }
}
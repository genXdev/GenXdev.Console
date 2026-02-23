// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Remove-SpotifyTracksFromLiked.cs
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
    /// Removes tracks from the user's Spotify Library (Liked Songs).
    /// </para>
    ///
    /// <para type="description">
    /// Removes one or more tracks from the user's Spotify Liked Songs collection.
    /// If no track ID is provided, removes the currently playing track.
    /// </para>
    ///
    /// <para type="description">
    /// PARAMETERS
    /// </para>
    ///
    /// <para type="description">
    /// -TrackId &lt;string[]&gt;<br/>
    /// One or more Spotify track IDs to remove from the Liked Songs collection.
    /// If omitted, the currently playing track will be removed.<br/>
    /// - <b>Aliases</b>: Id<br/>
    /// - <b>Position</b>: 0<br/>
    /// - <b>Default</b>: @()<br/>
    /// </para>
    ///
    /// <example>
    /// <para>Remove a specific track from liked songs</para>
    /// <para>This example removes a track with the specified ID from the user's liked songs.</para>
    /// <code>
    /// Remove-SpotifyTracksFromLiked -TrackId "1234567890abcdef"
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Use the alias to remove a track</para>
    /// <para>This example uses the 'dislike' alias to remove a track from liked songs.</para>
    /// <code>
    /// dislike "1234567890abcdef"
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Remove the currently playing track</para>
    /// <para>This example removes the track that is currently playing from liked songs.</para>
    /// <code>
    /// Remove-SpotifyTracksFromLiked
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "SpotifyTracksFromLiked", SupportsShouldProcess = true)]
    [Alias("dislike")]
    [OutputType(typeof(PSObject))]
    public class RemoveSpotifyTracksFromLikedCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// The Spotify track IDs to remove from Liked Songs
        /// </summary>
        [Parameter(
            Mandatory = false,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Spotify track IDs to remove from Liked Songs")]
        [Alias("Id")]
        public string[] TrackId { get; set; } = new string[0];

        private string apiToken;

        /// <summary>
        /// Initialize the cmdlet by retrieving the Spotify API token
        /// </summary>
        protected override void BeginProcessing()
        {
            // Retrieve spotify api authentication token for subsequent requests
            var scriptBlock = ScriptBlock.Create("GenXdev.Console\\Get-SpotifyApiToken");
            var results = scriptBlock.Invoke();
            apiToken = results[0].BaseObject as string;
            WriteVerbose("Retrieved Spotify API token");
        }

        /// <summary>
        /// Process each record and remove tracks from liked songs
        /// </summary>
        protected override void ProcessRecord()
        {
            if (TrackId.Length == 0)
            {
                // If no track specified, get the currently playing track
                WriteVerbose("No track ID provided, checking currently playing track");
                var getCurrentScript = ScriptBlock.Create("GenXdev.Console\\Get-SpotifyCurrentlyPlaying");
                var currentResults = getCurrentScript.Invoke();
                var currentlyPlaying = currentResults[0] as PSObject;

                if (currentlyPlaying == null ||
                    currentlyPlaying.Properties["CurrentlyPlayingType"].Value.ToString() != "track")
                {
                    WriteWarning("There are no tracks playing at this time");
                    return;
                }

                // Remove the currently playing track from liked songs
                WriteVerbose("Removing currently playing track from Liked Songs");
                var item = currentlyPlaying.Properties["Item"].Value as PSObject;
                var trackId = item.Properties["Id"].Value as string;

                // Remove the track directly using the API
                GenXdev.Helpers.Spotify.RemoveFromLiked(apiToken, new[] { trackId });

                // Return the track that was removed
                WriteObject(item);
            }
            else
            {
                // Remove the specified tracks from liked songs
                WriteVerbose($"Removing {TrackId.Length} track(s) from Liked Songs");

                if (ShouldProcess(
                    $"Remove {TrackId.Length} track(s) from Liked Songs",
                    "Are you sure you want to remove these tracks from your Liked Songs?",
                    "Removing tracks from Liked Songs"))
                {
                    // Remove the specified tracks from liked songs using the spotify api
                    GenXdev.Helpers.Spotify.RemoveFromLiked(apiToken, TrackId);
                }
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
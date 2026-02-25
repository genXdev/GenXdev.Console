// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Add-SpotifyTracksToLiked.cs
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
    /// Adds tracks to the user's Spotify liked songs library.
    /// </para>
    ///
    /// <para type="description">
    /// This function allows you to add one or more tracks to your Spotify liked songs
    /// library. If no track IDs are provided, it will attempt to like the currently
    /// playing track.
    /// </para>
    ///
    /// <para type="description">
    /// PARAMETERS
    /// </para>
    ///
    /// <para type="description">
    /// -TrackId &lt;string[]&gt;<br/>
    /// An array of Spotify track IDs that should be added to your liked songs. If not
    /// provided, the currently playing track will be liked instead.<br/>
    /// - <b>Aliases</b>: Id<br/>
    /// - <b>Position</b>: 0<br/>
    /// - <b>Default</b>: @()<br/>
    /// </para>
    ///
    /// <example>
    /// <para>Add a specific track to liked songs</para>
    /// <para>This example adds a track with the specified ID to the user's liked songs.</para>
    /// <code>
    /// Add-SpotifyTracksToLiked -TrackId "spotify:track:123456789"
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Use the alias to like a track</para>
    /// <para>This example uses the 'like' alias to add a track to liked songs.</para>
    /// <code>
    /// like "spotify:track:123456789"
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Like the currently playing track</para>
    /// <para>This example likes the track that is currently playing on Spotify.</para>
    /// <code>
    /// Get-SpotifyCurrentlyPlaying | Add-SpotifyTracksToLiked
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "SpotifyTracksToLiked")]
    [Alias("like")]
    [OutputType(typeof(PSObject))]
    public class AddSpotifyTracksToLikedCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// The Spotify track IDs to add to liked songs
        /// </summary>
        [Parameter(
            Mandatory = false,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Spotify track IDs to add to liked songs")]
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
        /// Process each record and add tracks to liked songs
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

                // Add the currently playing track to liked songs
                WriteVerbose("Adding currently playing track to liked songs");
                var item = currentlyPlaying.Properties["Item"].Value as PSObject;
                var trackId = item.Properties["Id"].Value as string;
                GenXdev.Helpers.Spotify.AddToLiked(apiToken, new[] { trackId });

                // Return the track that was liked
                WriteObject(item);
            }
            else
            {
                // Add the specified tracks to liked songs
                WriteVerbose($"Adding {TrackId.Length} track(s) to liked songs");
                GenXdev.Helpers.Spotify.AddToLiked(apiToken, TrackId);
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
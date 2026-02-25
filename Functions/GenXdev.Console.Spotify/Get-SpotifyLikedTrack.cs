// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Get-SpotifyLikedTrack.cs
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
    /// Retrieves all tracks saved in the user's Spotify Library.
    /// </para>
    ///
    /// <para type="description">
    /// This function authenticates with Spotify using an API token and retrieves all
    /// tracks that the user has saved (liked) in their Spotify library. The tracks are
    /// returned as collection of track objects containing metadata like title, artist,
    /// and album information.
    /// </para>
    ///
    /// <example>
    /// <para>Get-SpotifyLikedTrack</para>
    /// <para>Retrieves all liked tracks from the user's Spotify library.</para>
    /// <code>
    /// Get-SpotifyLikedTrack
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>liked</para>
    /// <para>Uses the alias 'liked' to retrieve all liked tracks from the user's Spotify library.</para>
    /// <code>
    /// liked
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "SpotifyLikedTrack")]
    [Alias("liked")]
    [OutputType(typeof(List<SpotifyAPI.Web.SavedTrack>))]
    public class GetSpotifyLikedTrackCommand : PSGenXdevCmdlet
    {
        private string apiToken;

        /// <summary>
        /// Begin processing - retrieve authentication token for spotify api access
        /// </summary>
        protected override void BeginProcessing()
        {
            // Retrieve authentication token for spotify api access
            var scriptBlock = ScriptBlock.Create("GenXdev.Console\\Get-SpotifyApiToken");

            var results = scriptBlock.Invoke();

            apiToken = (string)results[0].BaseObject;

            // Log api authentication attempt
            WriteVerbose("Retrieved Spotify API authentication token");
        }

        /// <summary>
        /// Process record - fetch all tracks from user's spotify library using helper class
        /// </summary>
        protected override void ProcessRecord()
        {
            // Fetch all tracks from user's spotify library using helper class
            WriteVerbose("Retrieving saved tracks from Spotify library...");

            var tracks = GenXdev.Helpers.Spotify.GetLibraryTracks(apiToken);

            WriteObject(tracks);
        }

        /// <summary>
        /// End processing - no cleanup needed
        /// </summary>
        protected override void EndProcessing()
        {
        }
    }
}
// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Get-SpotifyCurrentlyPlaying.cs
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
    /// Returns information about the currently playing track on Spotify.
    /// </para>
    ///
    /// <para type="description">
    /// Retrieves detailed information about the track currently playing on Spotify,
    /// including track name, artist, album, and playback status using the Spotify Web
    /// API.
    /// </para>
    ///
    /// <example>
    /// <para>Example: Get currently playing track information</para>
    /// <para>Get-SpotifyCurrentlyPlaying</para>
    /// <para>Returns the full currently playing track information object.</para>
    /// <code>
    /// Get-SpotifyCurrentlyPlaying
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Example: Use alias to get currently playing track</para>
    /// <para>gcp</para>
    /// <para>Uses the alias to get the currently playing track information.</para>
    /// <code>
    /// gcp
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Example: Get just the track name</para>
    /// <para>(Get-SpotifyCurrentlyPlaying).Item.Name</para>
    /// <para>Returns just the name of the currently playing track.</para>
    /// <code>
    /// (Get-SpotifyCurrentlyPlaying).Item.Name
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "SpotifyCurrentlyPlaying")]
    [Alias("gcp")]
    [OutputType(typeof(SpotifyAPI.Web.CurrentlyPlaying))]
    public class GetSpotifyCurrentlyPlayingCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// Begin processing - retrieve Spotify API authentication token
        /// </summary>
        protected override void BeginProcessing()
        {
            WriteVerbose("Getting Spotify API authentication token");
        }

        /// <summary>
        /// Process record - query Spotify API for currently playing track
        /// </summary>
        protected override void ProcessRecord()
        {
            // Retrieve the Spotify API authentication token
            var apiTokenResult = InvokeCommand.InvokeScript("GenXdev.Console\\Get-SpotifyApiToken");
            var apiToken = (string)apiTokenResult[0].BaseObject;

            // Query the Spotify API for currently playing track information
            WriteVerbose("Querying Spotify API for currently playing track");
            var result = GenXdev.Helpers.Spotify.GetCurrentlyPlaying(apiToken);

            WriteObject(result);
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
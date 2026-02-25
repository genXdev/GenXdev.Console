// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Get-SpotifyTrackAudioFeatures.cs
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
using SpotifyAPI.Web;

namespace GenXdev.Console.Spotify
{
    /// <summary>
    /// <para type="synopsis">
    /// Retrieves audio feature information for one or more Spotify tracks.
    /// </para>
    ///
    /// <para type="description">
    /// This function connects to the Spotify API to fetch detailed audio features for the
    /// specified tracks. Audio features include characteristics like danceability,
    /// energy, key, loudness, mode, speechiness, acousticness, instrumentalness,
    /// liveness, valence, tempo, and time signature.
    /// </para>
    ///
    /// <para type="description">
    /// PARAMETERS
    /// </para>
    ///
    /// <para type="description">
    /// -TrackId &lt;string[]&gt;<br/>
    /// One or more Spotify track IDs to analyze. These must be valid Spotify track
    /// identifiers.<br/>
    /// - <b>Aliases</b>: Id<br/>
    /// - <b>Position</b>: 0<br/>
    /// - <b>Mandatory</b>: true<br/>
    /// - <b>ValueFromPipeline</b>: true<br/>
    /// - <b>ValueFromPipelineByPropertyName</b>: true<br/>
    /// </para>
    ///
    /// <example>
    /// <para>Get audio features for a specific track</para>
    /// <para>This example retrieves audio features for a single Spotify track.</para>
    /// <code>
    /// Get-SpotifyTrackAudioFeatures -TrackId "1301WleyT98MSxVHPZCA6M"
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Get audio features using the alias</para>
    /// <para>This example uses the audiofeatures alias to retrieve audio features.</para>
    /// <code>
    /// audiofeatures "1301WleyT98MSxVHPZCA6M", "6rqhFgbbKwnb9MLmUQDhG6"
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "SpotifyTrackAudioFeatures")]
    [Alias("audiofeatures")]
    [OutputType(typeof(List<TrackAudioFeatures>))]
    public class GetSpotifyTrackAudioFeaturesCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// One or more Spotify track IDs to analyze. These must be valid Spotify track
        /// identifiers.
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Spotify track to return audio features for")]
        [Alias("Id")]
        public string[] TrackId { get; set; }

        private string apiToken;

        /// <summary>
        /// Begin processing - acquire Spotify API authentication token
        /// </summary>
        protected override void BeginProcessing()
        {
            // Obtain the spotify api authentication token for subsequent requests
            WriteVerbose("Acquiring Spotify API authentication token");
            var tokenResult = InvokeCommand.InvokeScript("GenXdev.Console\\Get-SpotifyApiToken");
            apiToken = tokenResult[0].ToString();
        }

        /// <summary>
        /// Process record - fetch audio features for the specified tracks
        /// </summary>
        protected override void ProcessRecord()
        {
            // Fetch audio features for the specified tracks using the spotify api
            WriteVerbose($"Retrieving audio features for {TrackId.Length} tracks");
            var result = GenXdev.Helpers.Spotify.GetSeveralAudioFeatures(apiToken, TrackId);
            WriteObject(result);
        }

        /// <summary>
        /// End processing - no cleanup needed
        /// </summary>
        protected override void EndProcessing()
        {
            // No cleanup required
        }
    }
}
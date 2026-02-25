// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Get-SpotifyTrackById.cs
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
    /// Retrieves detailed track information from Spotify using a track ID.
    /// </para>
    ///
    /// <para type="description">
    /// Uses the Spotify Web API to fetch comprehensive track information including
    /// artist, album, duration, popularity, and other metadata for a specific track
    /// identified by its Spotify track ID.
    /// </para>
    ///
    /// <para type="description">
    /// PARAMETERS
    /// </para>
    ///
    /// <para type="description">
    /// -TrackId &lt;string&gt;<br/>
    /// The unique Spotify track identifier. This is typically a 22-character string<br/>
    /// that can be found in the Spotify track URL or through the Spotify client.<br/>
    /// - <b>Aliases</b>: Id<br/>
    /// - <b>Position</b>: 0<br/>
    /// - <b>Default</b>: (none)<br/>
    /// </para>
    ///
    /// <example>
    /// <para>Get track details for "Mr. Brightside" by The Killers</para>
    /// <para>This example retrieves full track details for the specified Spotify track ID.</para>
    /// <code>
    /// Get-SpotifyTrackById -TrackId "3n3Ppam7vgaVa1iaRUc9Lp"
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Use alias for track lookup</para>
    /// <para>This example demonstrates using the 'gettrack' alias.</para>
    /// <code>
    /// gettrack "3n3Ppam7vgaVa1iaRUc9Lp"
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "SpotifyTrackById")]
    [OutputType(typeof(SpotifyAPI.Web.FullTrack))]
    [Alias("gettrack")]
    public class GetSpotifyTrackByIdCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// The unique Spotify track identifier
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Spotify track ID to lookup track information for"
        )]
        [Alias("Id")]
        public string TrackId { get; set; }

        private PSObject apiToken;

        /// <summary>
        /// Begin processing - acquire authentication token
        /// </summary>
        protected override void BeginProcessing()
        {
            // Get authentication token for spotify api access
            WriteVerbose("Acquiring Spotify API authentication token");
            var apiTokenScript = ScriptBlock.Create("GenXdev.Console\\Get-SpotifyApiToken");
            var apiTokenResult = apiTokenScript.Invoke();
            apiToken = apiTokenResult[0] as PSObject;
        }

        /// <summary>
        /// Process record - fetch track information
        /// </summary>
        protected override void ProcessRecord()
        {
            // Fetch track information using the spotify api helper class
            WriteVerbose($"Retrieving track information for ID: {TrackId}");
            var trackScript = ScriptBlock.Create($"param($apiToken, $trackId); [GenXdev.Helpers.Spotify]::GetTrackById($apiToken, $trackId)");
            var result = trackScript.Invoke(apiToken, TrackId);
            WriteObject(result[0]);
        }

        /// <summary>
        /// End processing - cleanup if needed
        /// </summary>
        protected override void EndProcessing()
        {
            // No cleanup needed
        }
    }
}
// ###############################################################################
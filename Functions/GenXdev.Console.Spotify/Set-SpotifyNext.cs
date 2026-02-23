// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Set-SpotifyNext.cs
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
    /// Skips to next track on Spotify.
    /// </para>
    ///
    /// <para type="description">
    /// Advances playback to the next track in the current playlist or album on the
    /// currently active Spotify device. This cmdlet requires a valid Spotify API token
    /// and an active playback session.
    /// </para>
    ///
    /// <example>
    /// <para>Skip to the next track</para>
    /// <para>This example advances playback to the next track on Spotify.</para>
    /// <code>
    /// Set-SpotifyNext
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Use the 'next' alias to skip to the next track</para>
    /// <para>This example uses the 'next' alias to advance to the next track.</para>
    /// <code>
    /// next
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Use the 'skip' alias to skip to the next track</para>
    /// <para>This example uses the 'skip' alias to advance to the next track.</para>
    /// <code>
    /// skip
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "SpotifyNext", SupportsShouldProcess = true)]
    [Alias("next", "skip")]
    [OutputType(typeof(void))]
    public class SetSpotifyNextCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// Begin processing - initialization logic
        /// </summary>
        protected override void BeginProcessing()
        {
            WriteVerbose("Starting Set-SpotifyNext operation");
        }

        /// <summary>
        /// Process record - main cmdlet logic
        /// </summary>
        protected override void ProcessRecord()
        {
            // Retrieve the current Spotify API token
            WriteVerbose("Retrieving Spotify API token");
            var tokenResults = InvokeCommand.InvokeScript("GenXdev.Console\\Get-SpotifyApiToken");
            var token = tokenResults[0];

            // Skip to the next track using the Spotify API
            if (ShouldProcess("Spotify", "Skip to next track"))
            {
                WriteVerbose("Sending next track command to Spotify");
                InvokeCommand.InvokeScript($"[GenXdev.Helpers.Spotify]::Next('{token}')");
            }
        }

        /// <summary>
        /// End processing - cleanup logic
        /// </summary>
        protected override void EndProcessing()
        {
            // No cleanup needed
        }
    }
}
// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Set-SpotifyPrevious.cs
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
    /// Skips to the previous track in Spotify playback.
    /// </para>
    ///
    /// <para type="description">
    /// Controls Spotify playback by skipping to the previous track on the currently
    /// active device. Requires valid Spotify API authentication token.
    /// </para>
    ///
    /// <example>
    /// <para>Skip to the previous track in Spotify playback</para>
    /// <code>
    /// Set-SpotifyPrevious
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Skip to the previous track using the 'previous' alias</para>
    /// <code>
    /// previous
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Skip to the previous track using the 'prev' alias</para>
    /// <code>
    /// prev
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "SpotifyPrevious")]
    [Alias("previous", "prev")]
    public class SetSpotifyPreviousCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// Begin processing - output information about the operation
        /// </summary>
        protected override void BeginProcessing()
        {
            WriteVerbose("Attempting to skip to previous track in Spotify");
        }

        /// <summary>
        /// Process record - retrieve token and skip to previous track
        /// </summary>
        protected override void ProcessRecord()
        {
            // Retrieve the current spotify api authentication token
            var tokenResults = InvokeCommand.InvokeScript("GenXdev.Console\\Get-SpotifyApiToken");

            // Extract the token from the results
            var token = tokenResults[0].BaseObject.ToString();

            // Check if should process is supported and confirmed
            if (ShouldProcess("Spotify", "Skip to previous track"))
            {
                // Call spotify api to skip to previous track
                InvokeCommand.InvokeScript($"[GenXdev.Helpers.Spotify]::Previous('{token}')");
            }
        }

        /// <summary>
        /// End processing - no cleanup needed
        /// </summary>
        protected override void EndProcessing()
        {
        }
    }
}
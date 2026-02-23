// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Set-SpotifyStop.cs
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
    /// Stops Spotify playback on the active device.
    /// </para>
    ///
    /// <para type="description">
    /// Stops the currently playing music on the active Spotify device by calling the
    /// Spotify Web API. Requires a valid authentication token.
    /// </para>
    ///
    /// <example>
    /// <para>Set-SpotifyStop</para>
    /// <para>Stops Spotify playback.</para>
    /// <code>
    /// Set-SpotifyStop
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>stop</para>
    /// <para>Stops Spotify playback using the alias.</para>
    /// <code>
    /// stop
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Stop-Music</para>
    /// <para>Stops Spotify playback (example alias usage).</para>
    /// <code>
    /// Stop-Music
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "SpotifyStop")]
    [Alias("stop")]
    public class SetSpotifyStopCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// Begin processing - initialization logic
        /// </summary>
        protected override void BeginProcessing()
        {
            WriteVerbose("Attempting to stop Spotify playback on active device");
        }

        /// <summary>
        /// Process record - main cmdlet logic
        /// </summary>
        protected override void ProcessRecord()
        {
            // Retrieve the current spotify api authentication token
            WriteVerbose("Retrieving Spotify API token");
            var tokenResults = InvokeCommand.InvokeScript("GenXdev.Console\\Get-SpotifyApiToken");
            if (tokenResults.Count == 0)
            {
                // Handle error if no token retrieved
                WriteError(new ErrorRecord(
                    new Exception("Failed to retrieve Spotify API token"),
                    "TokenRetrievalFailed",
                    ErrorCategory.AuthenticationError,
                    null));
                return;
            }
            var token = tokenResults[0].BaseObject.ToString();

            // Check if we should proceed with stopping playback
            if (ShouldProcess("Spotify", "Stop playback"))
            {
                // Call spotify api to stop playback
                WriteVerbose("Sending stop command to Spotify");
                InvokeCommand.InvokeScript($"[GenXdev.Helpers.Spotify]::Stop('{token}')");
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
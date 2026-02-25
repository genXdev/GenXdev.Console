// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Set-SpotifyPause.cs
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
    /// Pauses Spotify playback
    /// </para>
    ///
    /// <para type="description">
    /// Controls Spotify playback by pausing the currently playing track on the active
    /// device. If playback is already paused, this command will resume playback.
    /// </para>
    ///
    /// <example>
    /// <para>Pauses or resumes Spotify playback</para>
    /// <para>This example demonstrates how to pause or resume Spotify playback using the Set-SpotifyPause cmdlet.</para>
    /// <code>
    /// Set-SpotifyPause
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Using the alias to pause music</para>
    /// <para>This example shows how to use the pausemusic alias to control Spotify playback.</para>
    /// <code>
    /// pausemusic
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "SpotifyPause", SupportsShouldProcess = true)]
    [Alias("togglepausemusic", "pausemusic")]
    [OutputType(typeof(void))]
    public class SetSpotifyPauseCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// Begin processing - initialization logic
        /// </summary>
        protected override void BeginProcessing()
        {
            // Output verbose information about the action
            WriteVerbose("Attempting to pause/resume Spotify playback");
        }

        /// <summary>
        /// Process record - main cmdlet logic
        /// </summary>
        protected override void ProcessRecord()
        {
            // Check if we should proceed with the operation
            if (ShouldProcess("Spotify", "Pause/Resume playback"))
            {
                // Call spotify api to toggle pause state using the current auth token
                InvokeCommand.InvokeScript("[GenXdev.Helpers.Spotify]::Pause((GenXdev.Console\\Get-SpotifyApiToken))");
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
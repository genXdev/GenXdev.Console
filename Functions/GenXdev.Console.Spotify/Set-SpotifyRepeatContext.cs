// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Set-SpotifyRepeatContext.cs
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
    /// Enables playlist repeat mode for Spotify playback.
    /// </para>
    ///
    /// <para type="description">
    /// Sets the current Spotify context (playlist, album, etc.) to repeat mode on the
    /// active device. This affects the entire playback queue rather than a single track.
    /// </para>
    ///
    /// <example>
    /// <para>Set-SpotifyRepeatContext</para>
    /// <para>Enables repeat mode for the current Spotify context.</para>
    /// <code>
    /// Set-SpotifyRepeatContext
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>repeat</para>
    /// <para>Enables repeat mode using the alias.</para>
    /// <code>
    /// repeat
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "SpotifyRepeatContext")]
    [Alias("repeat")]
    public class SetSpotifyRepeatContextCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// Begin processing - initialization logic
        /// </summary>
        protected override void BeginProcessing()
        {
            WriteVerbose("Enabling repeat mode for current Spotify context");
        }

        /// <summary>
        /// Process record - main cmdlet logic
        /// </summary>
        protected override void ProcessRecord()
        {
            // Get the current api token for authentication
            var tokenResult = InvokeCommand.InvokeScript("GenXdev.Console\\Get-SpotifyApiToken");

            var token = tokenResult[0].BaseObject as string;

            // Enable repeat mode for the current context using the spotify helper
            // Only if ShouldProcess confirms the action
            if (ShouldProcess("Current Spotify context", "Enable repeat mode"))
            {
                WriteVerbose("Setting repeat mode to context");
                GenXdev.Helpers.Spotify.RepeatContext(token);
            }
        }

        /// <summary>
        /// End processing - cleanup logic
        /// </summary>
        protected override void EndProcessing()
        {
        }
    }
}
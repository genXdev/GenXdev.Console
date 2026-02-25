// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Set-SpotifyShuffleOn.cs
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
    /// Enables Spotify song-shuffle mode.
    /// </para>
    ///
    /// <para type="description">
    /// Enables shuffle mode on the currently active Spotify playback device. This
    /// cmdlet requires a valid Spotify API token and an active playback session.
    /// </para>
    ///
    /// <example>
    /// <para>Enable shuffle mode on the active Spotify device</para>
    /// <para>This example enables shuffle mode for the currently playing Spotify session.</para>
    /// <code>
    /// Set-SpotifyShuffleOn
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Use the shuffle alias to enable shuffle mode</para>
    /// <para>This example demonstrates using the 'shuffle' alias to enable shuffle mode.</para>
    /// <code>
    /// shuffle
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "SpotifyShuffleOn")]
    [Alias("shuffle", "shuffleon")]
    [OutputType(typeof(void))]
    public class SetSpotifyShuffleOnCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// Begin processing - initialization logic
        /// </summary>
        protected override void BeginProcessing()
        {
            WriteVerbose("Enabling shuffle mode on active Spotify device");
        }

        /// <summary>
        /// Process record - main cmdlet logic
        /// </summary>
        protected override void ProcessRecord()
        {
            // retrieve the current spotify api token
            var tokenResult = InvokeCommand.InvokeScript("GenXdev.Console\\Get-SpotifyApiToken");

            // extract the token value from the result
            var token = tokenResult[0].BaseObject.ToString();

            // check if we should proceed with enabling shuffle mode
            if (ShouldProcess("active Spotify device", "Enable shuffle mode"))
            {
                // enable shuffle mode using the spotify api
                InvokeCommand.InvokeScript($"[GenXdev.Helpers.Spotify]::ShuffleOn('{token}')");
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
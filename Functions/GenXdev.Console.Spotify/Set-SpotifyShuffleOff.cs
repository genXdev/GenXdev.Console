// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Set-SpotifyShuffleOff.cs
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
    /// Disables Spotify song-shuffle mode on the active device.
    /// </para>
    ///
    /// <para type="description">
    /// Disables the shuffle playback mode on the currently active Spotify device. This
    /// function uses the Spotify Web API to modify the shuffle state of the playback.
    /// </para>
    ///
    /// <example>
    /// <para>Disable shuffle mode</para>
    /// <para>Disables shuffle playback on the active Spotify device.</para>
    /// <code>
    /// Set-SpotifyShuffleOff
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Disable shuffle mode using alias</para>
    /// <para>Disables shuffle playback using the noshuffle alias.</para>
    /// <code>
    /// noshuffle
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Disable shuffle mode using another alias</para>
    /// <para>Disables shuffle playback using the shuffleoff alias.</para>
    /// <code>
    /// shuffleoff
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "SpotifyShuffleOff")]
    [OutputType(typeof(void))]
    public class SetSpotifyShuffleOffCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// Begin processing - initialization logic
        /// </summary>
        protected override void BeginProcessing()
        {
            WriteVerbose("Disabling shuffle mode on active Spotify device...");
        }

        /// <summary>
        /// Process record - main cmdlet logic
        /// </summary>
        protected override void ProcessRecord()
        {
            if (ShouldProcess("Spotify", "Disable shuffle mode"))
            {
                InvokeCommand.InvokeScript("[GenXdev.Helpers.Spotify]::ShuffleOff((GenXdev.Console\\Get-SpotifyApiToken))");
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
// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Set-SpotifyRepeatSong.cs
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
    /// Enables song repeat mode in Spotify.
    /// </para>
    ///
    /// <para type="description">
    /// Sets the repeat mode to 'track' for the currently active Spotify device using the
    /// Spotify Web API. This will make the current song play repeatedly until repeat
    /// mode is disabled.
    /// </para>
    ///
    /// <example>
    /// <para>Set-SpotifyRepeatSong</para>
    /// <para>Enables repeat mode for the current song in Spotify.</para>
    /// <code>
    /// Set-SpotifyRepeatSong
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>repeatsong</para>
    /// <para>Uses the alias to enable repeat mode.</para>
    /// <code>
    /// repeatsong
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "SpotifyRepeatSong")]
    [Alias("repeatsong")]
    [OutputType(typeof(void))]
    public class SetSpotifyRepeatSongCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// Begin processing - initialization logic
        /// </summary>
        protected override void BeginProcessing()
        {
            WriteVerbose("Attempting to enable song repeat mode in Spotify...");
        }

        /// <summary>
        /// Process record - main cmdlet logic
        /// </summary>
        protected override void ProcessRecord()
        {
            // Get the current spotify api authentication token
            var tokenResult = InvokeCommand.InvokeScript("GenXdev.Console\\Get-SpotifyApiToken");

            var token = ((PSObject)tokenResult[0]).BaseObject as string;

            // Only proceed if ShouldProcess returns true
            if (ShouldProcess("Spotify", "Set repeat mode to 'track'"))
            {
                // Send api request to enable repeat mode for the current song
                GenXdev.Helpers.Spotify.RepeatSong(token);
            }
        }

        /// <summary>
        /// End processing - cleanup logic
        /// </summary>
        protected override void EndProcessing()
        {
            WriteVerbose("Spotify repeat mode has been set to 'track'");
        }
    }
}
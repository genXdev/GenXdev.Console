// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Set-SpotifyRepeatOff.cs
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
    /// Disables Spotify repeat mode for the currently active device.
    /// </para>
    ///
    /// <para type="description">
    /// This function disables the repeat mode on the currently active Spotify device
    /// using the Spotify Web API. It requires a valid Spotify API token which is
    /// automatically retrieved using Get-SpotifyApiToken.
    /// </para>
    ///
    /// <example>
    /// <para>Set-SpotifyRepeatOff</para>
    /// <para>Disables repeat mode on the active Spotify device.</para>
    /// <code>
    /// Set-SpotifyRepeatOff
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>norepeat</para>
    /// <para>Disables repeat mode using the alias.</para>
    /// <code>
    /// norepeat
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>repeatoff</para>
    /// <para>Disables repeat mode using another alias.</para>
    /// <code>
    /// repeatoff
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "SpotifyRepeatOff")]
    [Alias("norepeat", "repeatoff")]
    public class SetSpotifyRepeatOffCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// Begin processing - initialization logic
        /// </summary>
        protected override void BeginProcessing()
        {
            WriteVerbose("Disabling Spotify repeat mode on active device...");
        }

        /// <summary>
        /// Process record - main cmdlet logic
        /// </summary>
        protected override void ProcessRecord()
        {
            // Get the current spotify api authentication token
            var tokenResult = InvokeCommand.InvokeScript("GenXdev.Console\\Get-SpotifyApiToken");

            var token = tokenResult[0].BaseObject as string;

            // Use ShouldProcess to confirm the operation
            if (ShouldProcess("Spotify active device", "Turn off repeat mode"))
            {
                // Call the spotify api to disable repeat mode
                GenXdev.Helpers.Spotify.RepeatOff(token);
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
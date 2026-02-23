// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Set-SpotifyStart.cs
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
    /// Starts Spotify playback on the currently active device.
    /// </para>
    ///
    /// <para type="description">
    /// This cmdlet initiates playback on the device that is currently active in
    /// Spotify. It uses the Spotify API token to authenticate the request and control
    /// playback.
    /// </para>
    ///
    /// <example>
    /// <para>Start Spotify playback</para>
    /// <para>This example starts playback on the currently active Spotify device.</para>
    /// <code>
    /// Set-SpotifyStart
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Start Spotify playback using alias</para>
    /// <para>This example uses the 'play' alias to start Spotify playback.</para>
    /// <code>
    /// play
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "SpotifyStart", SupportsShouldProcess = true)]
    [Alias("play", "startmusic")]
    public class SetSpotifyStartCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// Begin processing - output verbose information about starting playback
        /// </summary>
        protected override void BeginProcessing()
        {
            WriteVerbose("Initiating Spotify playback on active device");
        }

        /// <summary>
        /// Process record - retrieve token and start playback if confirmed
        /// </summary>
        protected override void ProcessRecord()
        {
            // Retrieve the current spotify api token for authentication
            var tokenResult = InvokeCommand.InvokeScript("GenXdev.Console\\Get-SpotifyApiToken");

            if (tokenResult.Count > 0)
            {
                var token = tokenResult[0].BaseObject.ToString();

                // Check if we should proceed with starting playback
                if (ShouldProcess("active Spotify device", "Start playback"))
                {
                    // Use the spotify helper class to start playback
                    GenXdev.Helpers.Spotify.Start(token);
                }
            }
        }
    }
}
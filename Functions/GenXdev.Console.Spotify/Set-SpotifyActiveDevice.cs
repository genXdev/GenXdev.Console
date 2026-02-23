// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Set-SpotifyActiveDevice.cs
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
using SpotifyAPI.Web;

namespace GenXdev.Console.Spotify
{
    /// <summary>
    /// <para type="synopsis">
    /// Sets the active Spotify playback device.
    /// </para>
    ///
    /// <para type="description">
    /// Transfers playback to the specified Spotify device using the Spotify Web API.
    /// This cmdlet requires an authenticated Spotify session and a valid device ID.
    /// The device ID can be obtained using the Get-SpotifyDevice cmdlet.
    /// </para>
    ///
    /// <para type="description">
    /// PARAMETERS
    /// </para>
    ///
    /// <para type="description">
    /// -DeviceId &lt;string&gt;<br/>
    /// The Spotify device ID to transfer playback to. This is a unique identifier
    /// assigned by Spotify to each playback device (speakers, computers, phones, etc.).
    /// Use Get-SpotifyDevice to get a list of available device IDs.<br/>
    /// - <b>Aliases</b>: Id<br/>
    /// - <b>Position</b>: 0<br/>
    /// - <b>Mandatory</b>: true<br/>
    /// - <b>ValueFromPipeline</b>: true<br/>
    /// - <b>ValueFromPipelineByPropertyName</b>: true<br/>
    /// </para>
    ///
    /// <example>
    /// <para>Set-SpotifyActiveDevice -DeviceId "1234567890abcdef"</para>
    /// <para>Transfers playback to the device with ID "1234567890abcdef"</para>
    /// <code>
    /// Set-SpotifyActiveDevice -DeviceId "1234567890abcdef"
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>"1234567890abcdef" | Set-SpotifyActiveDevice</para>
    /// <para>Same as above but using pipeline input</para>
    /// <code>
    /// "1234567890abcdef" | Set-SpotifyActiveDevice
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "SpotifyActiveDevice")]
    [OutputType(typeof(List<Device>))]
    public class SetSpotifyActiveDeviceCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// The Spotify device ID to transfer playback to
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Spotify deviceId to transfer playback to")]
        [Alias("Id")]
        public string DeviceId { get; set; }

        private string apiToken;

        /// <summary>
        /// Begin processing - retrieve authentication token
        /// </summary>
        protected override void BeginProcessing()
        {
            // Retrieve authentication token from spotify api for subsequent requests
            var tokenResult = InvokeCommand.InvokeScript("GenXdev.Console\\Get-SpotifyApiToken");

            if (tokenResult.Count > 0)
            {
                apiToken = tokenResult[0].BaseObject.ToString();
            }

            WriteVerbose("Retrieved Spotify API token for authentication");
        }

        /// <summary>
        /// Process record - transfer playback to the specified device
        /// </summary>
        protected override void ProcessRecord()
        {
            // Use spotify api to transfer playback to the specified device
            WriteVerbose($"Attempting to transfer playback to device ID: {DeviceId}");

            if (ShouldProcess($"device {DeviceId}", "Transfer Spotify playback"))
            {
                var scriptBlock = ScriptBlock.Create("[GenXdev.Helpers.Spotify]::SetActiveDevice($args[0], $args[1])");
                scriptBlock.Invoke(apiToken, DeviceId);
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
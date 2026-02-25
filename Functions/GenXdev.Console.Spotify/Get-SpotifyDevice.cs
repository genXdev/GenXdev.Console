// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Get-SpotifyDevice.cs
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
using SpotifyAPI.Web;

namespace GenXdev.Console.Spotify
{
    /// <summary>
    /// <para type="synopsis">
    /// Returns all currently available Spotify devices for current user.
    /// </para>
    ///
    /// <para type="description">
    /// Retrieves a list of all Spotify devices that are currently available for the
    /// authenticated user. This includes any active or recently active devices such as
    /// smartphones, computers, speakers, and other Spotify-enabled devices.
    /// </para>
    ///
    /// <example>
    /// <para>Get-SpotifyDevice</para>
    /// <para>This command returns all available Spotify devices for the current user.</para>
    /// <code>
    /// Get-SpotifyDevice
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "SpotifyDevice")]
    [OutputType(typeof(List<Device>))]
    public class GetSpotifyDeviceCommand : PSGenXdevCmdlet
    {
        private object apiToken;

        /// <summary>
        /// Begin processing - retrieve the Spotify API authentication token
        /// </summary>
        protected override void BeginProcessing()
        {
            WriteVerbose("Retrieving Spotify API authentication token...");

            var tokenResults = InvokeCommand.InvokeScript("GenXdev.Console\\Get-SpotifyApiToken");
            this.apiToken = tokenResults[0].BaseObject;
        }

        /// <summary>
        /// Process record - query Spotify API for available devices
        /// </summary>
        protected override void ProcessRecord()
        {
            WriteVerbose("Querying Spotify API for available devices...");

            var result = GenXdev.Helpers.Spotify.GetDevices((string)this.apiToken);
            WriteObject(result);
        }

        /// <summary>
        /// End processing - no cleanup needed
        /// </summary>
        protected override void EndProcessing()
        {
        }
    }
}
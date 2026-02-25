// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Get-SpotifyActiveDevice.cs
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
    /// Returns all currently active Spotify devices for the current user.
    /// </para>
    ///
    /// <para type="description">
    /// Retrieves a list of Spotify devices that are currently marked as active for the
    /// authenticated user's account. This includes devices like phones, computers,
    /// speakers, etc. that are currently available to play music.
    /// </para>
    ///
    /// <example>
    /// <para>Example: Get active Spotify devices</para>
    /// <para>Get-SpotifyActiveDevice</para>
    /// <para>Returns all active Spotify devices for the current user, displaying properties like name, type, and volume.</para>
    /// <code>
    /// Get-SpotifyActiveDevice
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "SpotifyActiveDevice")]
    [OutputType(typeof(SpotifyAPI.Web.Device))]
    public class GetSpotifyActiveDeviceCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// Begin processing - retrieve Spotify API authentication token
        /// </summary>
        protected override void BeginProcessing()
        {
            WriteVerbose("Retrieving Spotify API authentication token");
        }

        /// <summary>
        /// Process record - fetch and filter active Spotify devices
        /// </summary>
        protected override void ProcessRecord()
        {
            // Retrieve the Spotify API authentication token
            var apiTokenResult = InvokeCommand.InvokeScript("GenXdev.Console\\Get-SpotifyApiToken");
            var apiToken = (string)apiTokenResult[0].BaseObject;

            // Fetch active Spotify devices
            WriteVerbose("Fetching active Spotify devices");
            var devices = GenXdev.Helpers.Spotify.GetDevices(apiToken);

            // Filter and output only active devices
            foreach (var device in devices.Where(d => d.IsActive))
            {
                WriteObject(device);
            }
        }

        /// <summary>
        /// End processing - cleanup if needed
        /// </summary>
        protected override void EndProcessing()
        {
            // No cleanup required
        }
    }
}
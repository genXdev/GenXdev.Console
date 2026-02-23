// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Get-SpotifyApiToken.cs
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
    /// Retrieves or generates a valid Spotify API authentication token.
    /// </para>
    ///
    /// <para type="description">
    /// This function manages Spotify API authentication by either retrieving a cached
    /// token or obtaining a new one. It also ensures proper firewall access and
    /// validates the token's functionality.
    /// </para>
    ///
    /// <example>
    /// <para>Example</para>
    /// <para>$token = Get-SpotifyApiToken</para>
    /// <code>
    /// $token = Get-SpotifyApiToken
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "SpotifyApiToken")]
    [OutputType(typeof(string))]
    public class GetSpotifyApiTokenCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// Begin processing - initialization logic
        /// </summary>
        protected override void BeginProcessing()
        {
            // define firewall rule settings
            string ruleName = "Allow Current PowerShell";

            WriteVerbose($"Checking firewall rule: {ruleName}");
        }

        /// <summary>
        /// Process record - main cmdlet logic
        /// </summary>
        protected override void ProcessRecord()
        {
            // define firewall rule settings
            string ruleName = "Allow Current PowerShell";
            var pshomeResult = InvokeCommand.InvokeScript("$PSHOME");
            string programPath = Path.Combine(pshomeResult[0].ToString(), "pwsh.exe");
            string remoteAddress = "192.168.1.1";

            // verify if firewall rule exists
            var existingRule = InvokeCommand.InvokeScript(
                $"Get-NetFirewallRule -DisplayName '{ruleName}' -ErrorAction SilentlyContinue");

            // create firewall rule if it doesn't exist
            if (existingRule.Count == 0)
            {
                WriteVerbose("Creating new firewall rule");
                InvokeCommand.InvokeScript(
                    $"New-NetFirewallRule -DisplayName '{ruleName}' -Direction Inbound -Program '{programPath}' -Action Allow -RemoteAddress '{remoteAddress}'");

                Host.UI.WriteLine($"Firewall rule '{ruleName}' created.");
            }

            // path to cached authentication token
            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "GenXdev.PowerShell",
                "Spotify_Auth.json");

            string apiToken;

            // attempt to load existing token
            if (File.Exists(path))
            {
                WriteVerbose($"Loading existing token from {path}");
                apiToken = File.ReadAllText(path);
            }
            else
            {
                WriteVerbose("No existing token found, connecting to Spotify");
                var connectResult = InvokeCommand.InvokeScript("GenXdev.Console\\Connect-SpotifyApiToken");
                apiToken = connectResult[0].ToString();
                InvokeCommand.InvokeScript($"GenXdev.Console\\Set-SpotifyApiToken '{apiToken}'");
            }

            // validate token by attempting to list devices
            try
            {
                WriteVerbose("Validating token by retrieving devices");
                InvokeCommand.InvokeScript($"[GenXdev.Helpers.Spotify]::GetDevices('{apiToken}')");
            }
            catch
            {
                WriteVerbose("Token validation failed, obtaining new token");
                var connectResult = InvokeCommand.InvokeScript("GenXdev.Console\\Connect-SpotifyApiToken");
                apiToken = connectResult[0].ToString();
                InvokeCommand.InvokeScript($"GenXdev.Console\\Set-SpotifyApiToken '{apiToken}'");
            }

            // return the valid token
            WriteObject(apiToken);
        }

        /// <summary>
        /// End processing - cleanup logic
        /// </summary>
        protected override void EndProcessing()
        {
        }
    }
}
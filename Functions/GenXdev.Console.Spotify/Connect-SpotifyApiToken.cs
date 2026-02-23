// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Connect-SpotifyApiToken.cs
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



/* ###############################################################################
Part of PowerShell module : GenXdev.Console.Spotify
Original cmdlet filename  : Connect-SpotifyApiToken.cs
Original author           : René Vaessen / GenXdev
Version                   : 2.3.2026
###############################################################################
Copyright (c)  René Vaessen / GenXdev

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
############################################################################### */
using System.Management.Automation;

namespace GenXdev.Console.Spotify
{
    /// <summary>
    /// <para type="synopsis">
    /// Authenticates with Spotify API using OAuth flow to obtain an access token.
    /// </para>
    ///
    /// <para type="description">
    /// Opens a browser window in application mode to handle the Spotify OAuth
    /// authentication flow. Once authenticated, retrieves and stores the access token for
    /// subsequent API calls. The browser window automatically closes after successful
    /// authentication. Uses port 5642 for the OAuth callback listener.
    /// </para>
    ///
    /// <example>
    /// <para>Authenticate with Spotify and obtain access token</para>
    /// <para>Connect-SpotifyApiToken</para>
    /// <code>
    /// Connect-SpotifyApiToken
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommunications.Connect, "SpotifyApiToken")]
    [OutputType(typeof(string))]
    public class ConnectSpotifyApiTokenCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// Begin processing - inform user that authentication flow is starting
        /// </summary>
        protected override void BeginProcessing()
        {
            WriteVerbose("Starting Spotify OAuth authentication flow on port 5642");
        }

        /// <summary>
        /// Process record - main authentication logic
        /// </summary>
        protected override void ProcessRecord()
        {
            // construct oauth url using helper class method
            var urlResult = InvokeCommand.InvokeScript("[GenXdev.Helpers.Spotify]::RequestAuthenticationUri(5642)");
            string url = urlResult[0].ToString();

            // launch minimal browser window for authentication
            var processResult = InvokeCommand.InvokeScript($"GenXdev.Webbrowser\\Open-Webbrowser -PassThru -ApplicationMode -NewWindow -Width 10 -Height 8 -Centered -Monitor 0 -Url '{url}'");
            dynamic process = processResult[0];

            // attempt to minimize the browser window
            try
            {
                var windowHelperResult = InvokeCommand.InvokeScript($"GenXdev.Windows\\Get-Window -ProcessId {process.Id}");
                dynamic windowHelper = windowHelperResult[0];
                InvokeCommand.InvokeScript($"$null = {windowHelper}.Minimize()");
            }
            catch
            {
                // silently continue if window manipulation fails
            }

            // wait for oauth callback and retrieve token
            WriteVerbose("Waiting for OAuth callback on port 5642");
            var authTokenResult = InvokeCommand.InvokeScript("[GenXdev.Helpers.Spotify]::RequestAuthenticationTokenUsingOAuth(5642)");
            string authToken = authTokenResult[0].ToString();

            // cleanup: close browser window if still running
            if (process != null && !(bool)process.HasExited)
            {
                WriteVerbose("Closing authentication browser window");
                InvokeCommand.InvokeScript($"$null = {process}.CloseMainWindow()");
            }

            // return authentication token
            WriteObject(authToken);
        }
    }
}
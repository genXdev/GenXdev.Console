// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Set-SpotifyApiToken.cs
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
    /// Caches a Spotify API token for later use in the local configuration.
    /// </para>
    ///
    /// <para type="description">
    /// This function stores a provided Spotify API token in a local JSON file for
    /// subsequent use by other Spotify-related commands. The token is saved in a
    /// dedicated configuration directory under GenXdev.Local.
    /// </para>
    ///
    /// <para type="description">
    /// PARAMETERS
    /// </para>
    ///
    /// <para type="description">
    /// -ApiToken &lt;String&gt;<br/>
    /// The Spotify API authentication token to be cached locally.<br/>
    /// - <b>Position</b>: 0<br/>
    /// - <b>Mandatory</b>: true<br/>
    /// </para>
    ///
    /// <example>
    /// <para>Set a Spotify API token</para>
    /// <para>Stores the provided token for use by other Spotify cmdlets.</para>
    /// <code>
    /// Set-SpotifyApiToken -ApiToken "YOUR-SPOTIFY-API-TOKEN"
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "SpotifyApiToken")]
    [OutputType(typeof(void))]
    public class SetSpotifyApiTokenCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// The Spotify API token to cache locally
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 0,
            HelpMessage = "The Spotify API token to cache locally"
        )]
        public string ApiToken { get; set; }

        /// <summary>
        /// Begin processing - initialization logic
        /// </summary>
        protected override void BeginProcessing()
        {
        }

        /// <summary>
        /// Process record - main cmdlet logic
        /// </summary>
        protected override void ProcessRecord()
        {
            // Define the storage location for the api token
            string path = ExpandPath(Path.Combine(GetGenXdevAppDataPath(), "Spotify_Auth.json"), CreateDirectory: true);

            WriteVerbose("Storing Spotify API token in: " + path);

            // Save the trimmed api token to the json file
            WriteVerbose("Writing API token to file");

            if (ShouldProcess(path, "Save Spotify API Token"))
            {
                System.IO.File.WriteAllText(path, ApiToken.Trim('\r', '\n', '\t', ' '));
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
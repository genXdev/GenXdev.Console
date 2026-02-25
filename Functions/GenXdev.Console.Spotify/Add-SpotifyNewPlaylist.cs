// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Add-SpotifyNewPlaylist.cs
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
    /// Creates a new Spotify playlist with customizable settings.
    /// </para>
    ///
    /// <para type="description">
    /// Creates a new Spotify playlist with the specified name, description, and privacy
    /// settings. The function authenticates with Spotify, creates the playlist, and
    /// updates the local playlist cache.
    /// </para>
    ///
    /// <para type="description">
    /// PARAMETERS
    /// </para>
    ///
    /// <para type="description">
    /// -Name &lt;string&gt;<br/>
    /// The name for the new playlist. This will be visible to users who can access the
    /// playlist.<br/>
    /// - <b>Position</b>: 0<br/>
    /// - <b>Default</b>: (null)<br/>
    /// </para>
    ///
    /// <para type="description">
    /// -Description &lt;string&gt;<br/>
    /// An optional description for the playlist that provides additional context about
    /// its contents or purpose.<br/>
    /// - <b>Position</b>: 1<br/>
    /// - <b>Default</b>: ""<br/>
    /// </para>
    ///
    /// <para type="description">
    /// -Public &lt;SwitchParameter&gt;<br/>
    /// When specified, makes the playlist publicly visible to other Spotify users.<br/>
    /// - <b>Default</b>: False<br/>
    /// </para>
    ///
    /// <para type="description">
    /// -Collabrative &lt;SwitchParameter&gt;<br/>
    /// When specified, allows other users to modify the playlist contents.<br/>
    /// - <b>Default</b>: False<br/>
    /// </para>
    ///
    /// <example>
    /// <para>Example 1: Create a public playlist with description</para>
    /// <para>Creates a new public playlist named "My Awesome Mix" with a description.</para>
    /// <code>
    /// Add-SpotifyNewPlaylist -Name "My Awesome Mix" -Description "Best songs of 2023" -Public
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Example 2: Create a collaborative playlist using alias</para>
    /// <para>Creates a new collaborative playlist named "Road Trip Songs" using the alias.</para>
    /// <code>
    /// newplaylist "Road Trip Songs" -Collabrative
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "SpotifyNewPlaylist")]
    [OutputType(typeof(SpotifyAPI.Web.FullPlaylist))]
    [Alias("newplaylist")]
    public class AddSpotifyNewPlaylistCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// The name for the new playlist
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 0,
            HelpMessage = "The name for the new playlist")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// The description for the new playlist
        /// </summary>
        [Parameter(
            Mandatory = false,
            Position = 1,
            HelpMessage = "The description for the new playlist")]
        public string Description { get; set; } = "";

        /// <summary>
        /// Make this a public playlist
        /// </summary>
        [Parameter(
            Mandatory = false,
            HelpMessage = "Make this a public playlist")]
        public SwitchParameter Public { get; set; }

        /// <summary>
        /// Allow others to make changes
        /// </summary>
        [Parameter(
            Mandatory = false,
            HelpMessage = "Allow others to make changes")]
        public SwitchParameter Collabrative { get; set; }

        private object apiToken;

        /// <summary>
        /// Begin processing - retrieve Spotify API authentication token
        /// </summary>
        protected override void BeginProcessing()
        {
            // Retrieve spotify api authentication token
            WriteVerbose("Retrieving Spotify API authentication token");

            // Use InvokeCommand to call the PowerShell function
            var results = InvokeCommand.InvokeScript("GenXdev.Console\\Get-SpotifyApiToken");
            if (results.Count > 0)
            {
                apiToken = results[0].BaseObject?.ToString();
            }
        }

        /// <summary>
        /// Process record - create new playlist and update cache
        /// </summary>
        protected override void ProcessRecord()
        {
            // Create new playlist using spotify api helper
            WriteVerbose($"Creating new Spotify playlist '{Name}'");

            // Call the static method directly
            var playlistResult = GenXdev.Helpers.Spotify.NewPlaylist(
                apiToken as string,
                Name,
                Public.ToBool(),
                Collabrative.ToBool(),
                Description
            );

            // Update local playlist cache if it exists
            var cacheVariable = SessionState.PSVariable.Get("SpotifyPlaylistCache");
            bool cacheExists = cacheVariable != null && cacheVariable.Value is System.Collections.Generic.List<object>;

            if (cacheExists)
            {
                WriteVerbose("Updating local playlist cache");

                // Insert the result at position 0 in the cache
                var cacheList = (System.Collections.Generic.List<object>)cacheVariable.Value;
                cacheList.Insert(0, playlistResult);

                // Save updated cache to json file
                string filePath = Path.Combine(GetGenXdevAppDataPath(), "Spotify.Playlists.json");

                string json = ConvertToJson(cacheList, 100);
                File.WriteAllText(filePath, json);
            }

            // Return the newly created playlist
            WriteObject(playlistResult);
        }

        /// <summary>
        /// End processing - cleanup if needed
        /// </summary>
        protected override void EndProcessing()
        {
            // No cleanup needed
        }
    }
}
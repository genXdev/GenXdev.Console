// ################################################################################
// Part of PowerShell module : GenXdev.Console.Spotify
// Original cmdlet filename  : Move-SpotifyLikedTracksToPlaylist.cs
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
    /// Moves all liked tracks from your Spotify library to specified playlist(s)
    /// </para>
    ///
    /// <para type="description">
    /// This cmdlet retrieves all tracks from your Spotify liked songs library and moves
    /// them to one or more specified playlists. After successfully adding the tracks to
    /// the target playlist(s), it removes them from your liked songs.
    /// </para>
    ///
    /// <para type="description">
    /// PARAMETERS
    /// </para>
    ///
    /// <para type="description">
    /// -PlaylistName &lt;string[]&gt;<br/>
    /// The name(s) of the Spotify playlist(s) where liked tracks should be moved to.<br/>
    /// Multiple playlist names can be specified.<br/>
    /// - <b>Aliases</b>: Name<br/>
    /// - <b>Position</b>: 0<br/>
    /// - <b>ParameterSet</b>: ByName<br/>
    /// </para>
    ///
    /// <para type="description">
    /// -PlaylistId &lt;string[]&gt;<br/>
    /// The Spotify ID(s) of the playlist(s) where liked tracks should be moved to.<br/>
    /// Multiple playlist IDs can be specified.<br/>
    /// - <b>Aliases</b>: Id<br/>
    /// - <b>Position</b>: 0<br/>
    /// - <b>ParameterSet</b>: ById<br/>
    /// </para>
    ///
    /// <example>
    /// <para>Example 1: Move liked tracks to a playlist by name</para>
    /// <para>Moves all liked tracks to the specified playlist.</para>
    /// <code>
    /// Move-SpotifyLikedTracksToPlaylist -PlaylistName "My Archived Likes"
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Example 2: Use alias for shorter command</para>
    /// <para>Uses the alias 'moveliked' for the cmdlet.</para>
    /// <code>
    /// moveliked "My Archived Likes"
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Move, "SpotifyLikedTracksToPlaylist")]
    [Alias("moveliked")]
    [OutputType(typeof(PSObject))]
    public class MoveSpotifyLikedTracksToPlaylistCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// The name(s) of the Spotify playlist(s) where liked tracks should be moved to.
        /// Multiple playlist names can be specified.
        /// </summary>
        [Parameter(
            ParameterSetName = "ByName",
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Spotify playlist where all liked tracks should move to")]
        [Alias("Name")]
        public string[] PlaylistName { get; set; }

        /// <summary>
        /// The Spotify ID(s) of the playlist(s) where liked tracks should be moved to.
        /// Multiple playlist IDs can be specified.
        /// </summary>
        [Parameter(
            ParameterSetName = "ById",
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Spotify playlist ID where all liked tracks should move to")]
        [Alias("Id")]
        public string[] PlaylistId { get; set; }

        private bool alreadyProcessed = false;

        /// <summary>
        /// Begin processing - convert playlist names to IDs if names were provided
        /// </summary>
        protected override void BeginProcessing()
        {
            // Convert playlist names to IDs if names were provided
            if (PlaylistName != null && PlaylistName.Length > 0)
            {
                WriteVerbose("Converting playlist names to IDs");

                var scriptBlock = ScriptBlock.Create(
                    "param($playlistName) GenXdev.Console\\Get-SpotifyPlaylistIdsByName -PlaylistName $playlistName");

                var result = scriptBlock.Invoke(PlaylistName);
                PlaylistId = (string[])result[0].BaseObject;
            }
        }

        /// <summary>
        /// Process record - main cmdlet logic for moving liked tracks
        /// </summary>
        protected override void ProcessRecord()
        {
            // Ensure processing only happens once for array parameters
            if (alreadyProcessed)
            {
                return;
            }

            alreadyProcessed = true;

            // Exit if no valid playlist IDs were found
            if (PlaylistId == null || PlaylistId.Length == 0)
            {
                WriteVerbose("No valid playlist IDs found, exiting");
                return;
            }

            // Retrieve all liked tracks from the user's library
            WriteVerbose("Retrieving liked tracks from library");

            var getLikedScript = ScriptBlock.Create("GenXdev.Console\\Get-SpotifyLikedTrack");
            var likedTracksResult = getLikedScript.Invoke();
            var likedTracks = (PSObject)likedTracksResult[0].BaseObject;

            // Track whether we successfully added tracks to at least one playlist
            bool done = false;

            // Get the track array from the liked tracks object
            var trackProperty = likedTracks.Properties["Track"];
            if (trackProperty == null || trackProperty.Value == null)
            {
                WriteVerbose("No tracks found in liked tracks");
                return;
            }

            var trackArray = (PSObject[])trackProperty.Value;

            // Attempt to add tracks to each specified playlist
            foreach (var id in PlaylistId)
            {
                WriteVerbose($"Adding tracks to playlist with ID: {id}");

                // Extract URIs from the track objects
                var uris = trackArray.Select(t => t.Properties["Uri"].Value?.ToString()).Where(u => u != null).ToArray();

                var addScript = ScriptBlock.Create(
                    "param($playlistId, $uris) GenXdev.Console\\Add-SpotifyTracksToPlaylist -PlaylistId $playlistId -Uri $uris");

                addScript.Invoke(id, uris);
                done = true;
            }

            // If tracks were added successfully, remove them from liked songs
            if (done)
            {
                WriteVerbose("Removing tracks from liked songs");

                // Extract IDs from the track objects
                var trackIds = trackArray.Select(t => t.Properties["Id"].Value?.ToString()).Where(id => id != null).ToArray();

                var removeScript = ScriptBlock.Create(
                    "param($trackIds) GenXdev.Console\\Remove-SpotifyTracksFromLiked -TrackId $trackIds");

                removeScript.Invoke(trackIds);

                // Return the tracks that were moved
                WriteObject(likedTracks);
            }
        }

        /// <summary>
        /// End processing - cleanup logic (empty in this case)
        /// </summary>
        protected override void EndProcessing()
        {
            // No cleanup needed
        }
    }
}
// ################################################################################
// Part of PowerShell module : GenXdev.Console.Vlc
// Original cmdlet filename  : Start-VlcMediaPlayerPreviousInPlaylist.cs
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

namespace GenXdev.Console.Vlc
{
    /// <summary>
    /// <para type="synopsis">
    /// Moves to the previous item in the VLC Media Player playlist.
    /// </para>
    ///
    /// <para type="description">
    /// This function sends the 'p' key command to VLC Media Player to navigate to the
    /// previous item in the current playlist. The function supports WhatIf operations
    /// and will restore focus after sending the command.
    /// </para>
    ///
    /// <example>
    /// <para>Moves to the previous item in the VLC Media Player playlist.</para>
    /// <code>
    /// Start-VlcMediaPlayerPreviousInPlaylist
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Using alias vlcprev</para>
    /// <code>
    /// vlcprev
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Using alias vlcback</para>
    /// <code>
    /// vlcback
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsLifecycle.Start, "VlcMediaPlayerPreviousInPlaylist", SupportsShouldProcess = true)]
    [Alias("vlcprev", "vlcback")]
    public class StartVlcMediaPlayerPreviousInPlaylistCommand : PSGenXdevCmdlet
    {
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
            // Check if the user wants to proceed with the operation
            if (ShouldProcess("VLC Media Player", "Go to previous item in playlist"))
            {
                // Send the 'p' key to VLC media player to go to previous playlist item
                InvokeCommand.InvokeScript("GenXdev.Console\\Open-VlcMediaPlayer -KeysToSend 'p' -RestoreFocus");
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
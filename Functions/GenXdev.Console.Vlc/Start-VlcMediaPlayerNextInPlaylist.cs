// ################################################################################
// Part of PowerShell module : GenXdev.Console.Vlc
// Original cmdlet filename  : Start-VlcMediaPlayerNextInPlaylist.cs
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



/*
<#
.SYNOPSIS
Advances VLC Media Player to the next item in the current playlist.

.DESCRIPTION
This function sends the 'n' keystroke to VLC Media Player to skip to the next
track or media item in the currently loaded playlist. It focuses the VLC window,
sends the next command, and restores focus to the PowerShell console. The
function includes ShouldProcess support for confirmation prompts when needed.

.EXAMPLE
Start-VlcMediaPlayerNextInPlaylist

.EXAMPLE
vlcnext
#>
*/
using System.Management.Automation;

namespace GenXdev.Console.Vlc
{
    /// <summary>
    /// <para type="synopsis">
    /// Advances VLC Media Player to the next item in the current playlist.
    /// </para>
    ///
    /// <para type="description">
    /// This cmdlet sends the 'n' keystroke to VLC Media Player to skip to the next
    /// track or media item in the currently loaded playlist. It focuses the VLC window,
    /// sends the next command, and restores focus to the PowerShell console. The
    /// cmdlet includes ShouldProcess support for confirmation prompts when needed.
    /// </para>
    ///
    /// <para type="description">
    /// PARAMETERS
    /// </para>
    ///
    /// <example>
    /// <para>Example advancing to next playlist item</para>
    /// <para>Advances VLC Media Player to the next item in the playlist.</para>
    /// <code>
    /// Start-VlcMediaPlayerNextInPlaylist
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Example using alias</para>
    /// <para>Uses the vlcnext alias to advance to the next playlist item.</para>
    /// <code>
    /// vlcnext
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsLifecycle.Start, "VlcMediaPlayerNextInPlaylist")]
    [Alias("vlcnext")]
    [OutputType(typeof(void))]
    public class StartVlcMediaPlayerNextInPlaylistCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// Begin processing - initialization logic
        /// </summary>
        protected override void BeginProcessing()
        {
            // Check if shouldprocess is enabled and user wants to proceed
            WriteVerbose("Preparing to advance VLC Media Player to next playlist item");
        }

        /// <summary>
        /// Process record - main cmdlet logic
        /// </summary>
        protected override void ProcessRecord()
        {
            // Verify user confirmation before sending next command to vlc
            if (ShouldProcess("VLC Media Player", "Next in playlist"))
            {
                // Send the 'n' key to vlc to advance to next playlist item
                // Restore focus ensures powershell regains control after command
                WriteVerbose("Sending next command to VLC Media Player");

                try
                {
                    InvokeCommand.InvokeScript("GenXdev.Console\\Open-VlcMediaPlayer -KeysToSend 'n' -RestoreFocus");
                }
                catch (Exception ex)
                {
                    WriteError(new ErrorRecord(
                        ex,
                        "VlcCommandFailed",
                        ErrorCategory.OperationStopped,
                        null));
                    return;
                }

                WriteVerbose("Successfully sent next command to VLC Media Player");
            }
        }

        /// <summary>
        /// End processing - cleanup logic
        /// </summary>
        protected override void EndProcessing()
        {
            // Empty implementation
        }
    }
}
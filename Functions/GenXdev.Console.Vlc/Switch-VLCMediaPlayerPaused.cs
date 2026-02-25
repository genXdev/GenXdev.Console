// ################################################################################
// Part of PowerShell module : GenXdev.Console.Vlc
// Original cmdlet filename  : Switch-VLCMediaPlayerPaused.cs
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
    /// Toggles the pause/play state of the VLC Media Player.
    /// </para>
    ///
    /// <para type="description">
    /// This function sends a space key to VLC Media Player to toggle between paused
    /// and playing states. It automatically restores focus to the original window
    /// after sending the key command.
    /// </para>
    ///
    /// <example>
    /// <para>Toggles the pause/play state of VLC Media Player.</para>
    /// <code>
    /// Switch-VLCMediaPlayerPaused
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Uses the alias to toggle the pause/play state.</para>
    /// <code>
    /// vlcpause
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Uses the alternate alias to toggle the pause/play state.</para>
    /// <code>
    /// vlcplay
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Switch, "VLCMediaPlayerPaused")]
    [Alias("vlcpause", "vlcplay")]
    [OutputType(typeof(void))]
    public class SwitchVLCMediaPlayerPausedCommand : PSGenXdevCmdlet
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
            // Send space key to vlc media player to toggle pause/play state
            WriteVerbose("Toggling VLC Media Player pause/play state");

            // Send the space key command and restore focus to original window
            var scriptBlock = ScriptBlock.Create("GenXdev.Console\\Open-VlcMediaPlayer -KeysToSend ' ' -RestoreFocus");
            scriptBlock.Invoke();
        }

        /// <summary>
        /// End processing - cleanup logic
        /// </summary>
        protected override void EndProcessing()
        {
        }
    }
}
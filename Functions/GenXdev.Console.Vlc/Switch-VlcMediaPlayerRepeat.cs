// ################################################################################
// Part of PowerShell module : GenXdev.Console.Vlc
// Original cmdlet filename  : Switch-VlcMediaPlayerRepeat.cs
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

namespace GenXdev.Console.Vlc
{
    /// <summary>
    /// <para type="synopsis">
    /// Toggles the repeat mode in VLC Media Player.
    /// </para>
    ///
    /// <para type="description">
    /// This cmdlet sends the 'r' key command to VLC Media Player to toggle between
    /// different repeat modes (no repeat, repeat current, repeat all). The cmdlet
    /// opens VLC if it's not already running and restores focus to the previous
    /// window after sending the command.
    /// </para>
    ///
    /// <example>
    /// <para>Toggle VLC repeat mode</para>
    /// <para>Detailed explanation: This example demonstrates how to toggle the repeat mode in VLC Media Player using the Switch-VlcMediaPlayerRepeat cmdlet.</para>
    /// <code>
    /// Switch-VlcMediaPlayerRepeat
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Toggle VLC repeat mode using alias</para>
    /// <para>Detailed explanation: This example shows how to use the alias 'vlcrepeat' to toggle the repeat mode in VLC Media Player.</para>
    /// <code>
    /// vlcrepeat
    /// </code>
    /// </example>
    ///
    /// </summary>
    [Cmdlet(VerbsCommon.Switch, "VlcMediaPlayerRepeat")]
    [Alias("vlcrepeat")]
    [OutputType(typeof(void))]
    public class SwitchVlcMediaPlayerRepeatCommand : PSGenXdevCmdlet
    {

        /// <summary>
        /// Process record - main cmdlet logic
        /// </summary>
        protected override void ProcessRecord()
        {

            // Send the repeat toggle key ('r') to VLC media player
            InvokeCommand.InvokeScript("GenXdev.Console\\Open-VlcMediaPlayer -KeysToSend 'r' -RestoreFocus");

        }
    }
}
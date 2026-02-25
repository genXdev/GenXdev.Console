// ################################################################################
// Part of PowerShell module : GenXdev.Console.Vlc
// Original cmdlet filename  : Switch-VlcMediaPlayerMute.cs
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
    /// Toggles the mute state of the VLC Media Player.
    /// </para>
    ///
    /// <para type="description">
    /// This cmdlet sends the 'm' key to VLC Media Player to toggle between muted
    /// and unmuted audio states. The cmdlet focuses VLC, sends the mute/unmute
    /// command, and then restores focus to the previously active window.
    /// </para>
    ///
    /// <para type="description">
    /// PARAMETERS
    /// </para>
    ///
    /// <example>
    /// <para>Toggles the mute state of VLC Media Player using the full cmdlet name.</para>
    /// <code>
    /// Switch-VlcMediaPlayerMute
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Toggles the mute state of VLC Media Player using the short alias.</para>
    /// <code>
    /// vlcmute
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Switch, "VlcMediaPlayerMute")]
    [Alias("vlcmute", "vlcunmute")]
    [OutputType(typeof(void))]
    public class SwitchVlcMediaPlayerMuteCommand : PSGenXdevCmdlet
    {

        /// <summary>
        /// Process record - main cmdlet logic
        /// </summary>
        protected override void ProcessRecord()
        {

            // Invoke the PowerShell cmdlet to send keys to VLC Media Player
            InvokeCommand.InvokeScript("GenXdev.Console\\Open-VlcMediaPlayer -KeysToSend 'm' -RestoreFocus");
        }
    }
}
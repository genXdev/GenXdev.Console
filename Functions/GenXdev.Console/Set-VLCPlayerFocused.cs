// ################################################################################
// Part of PowerShell module : GenXdev.Console
// Original cmdlet filename  : Set-VLCPlayerFocused.cs
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

namespace GenXdev.Console
{
    /// <summary>
    /// <para type="synopsis">
    /// Sets focus to the VLC media player window.
    /// </para>
    ///
    /// <para type="description">
    /// Locates a running instance of VLC media player and brings its window to the
    /// foreground, making it the active window. If VLC is not running, silently
    /// continues without error. Uses Windows API calls to manipulate window focus.
    /// </para>
    ///
    /// <example>
    /// <para>Set-VLCPlayerFocused</para>
    /// <para>Brings the VLC player window to front and gives it focus</para>
    /// <code>
    /// Set-VLCPlayerFocused
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>vlcf</para>
    /// <para>Same operation using the short alias</para>
    /// <code>
    /// vlcf
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "VLCPlayerFocused")]
    [Alias("showvlc", "vlcf", "fvlc")]
    [OutputType(typeof(void))]
    public class SetVLCPlayerFocusedCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// Begin processing - initialization logic
        /// </summary>
        protected override void BeginProcessing()
        {
            WriteVerbose("Starting Set-VLCPlayerFocused operation");
        }

        /// <summary>
        /// Process record - main cmdlet logic
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                // Find vlc window by process name, returns null if not found
                WriteVerbose("Attempting to locate VLC player window");

                // Execute the same PowerShell logic to maintain exact compatibility
                var scriptBlock = ScriptBlock.Create(@"
                    $window = GenXdev.Windows\Get-Window -ProcessName vlc
                    if ($window) {
                        $window.Focus()
                    }
                ");

                // Only proceed if ShouldProcess confirms
                if (ShouldProcess("VLC media player window", "Set as foreground window"))
                {
                    WriteVerbose("Setting VLC window as foreground window");

                    // Execute the script block
                    scriptBlock.Invoke();
                }
            }
            catch (Exception ex)
            {
                // Silently continue if window not found or other errors occur
                WriteVerbose("Failed to set VLC window focus: " + ex.Message);
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
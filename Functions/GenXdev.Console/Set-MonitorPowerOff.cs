// ################################################################################
// Part of PowerShell module : GenXdev.Console
// Original cmdlet filename  : Set-MonitorPowerOff.cs
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

namespace GenXdev.Console
{
    /// <summary>
    /// <para type="synopsis">
    /// Turns off power to all connected monitors.
    /// </para>
    ///
    /// <para type="description">
    /// Uses Windows API calls to send a power-off signal to all connected monitors. This
    /// is equivalent to pressing the physical power button on your monitors. The monitors
    /// will enter power saving mode but can be awakened by moving the mouse or pressing
    /// a key.
    /// </para>
    ///
    /// <example>
    /// <para>Set-MonitorPowerOff</para>
    /// <para>Turns off all connected monitors.</para>
    /// <code>
    /// Set-MonitorPowerOff
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>poweroff</para>
    /// <para>Turns off all connected monitors using the alias.</para>
    /// <code>
    /// poweroff
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "MonitorPowerOff")]
    [Alias("poweroff")]
    public class SetMonitorPowerOffCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// Begin processing - initialization logic
        /// </summary>
        protected override void BeginProcessing()
        {
            WriteVerbose("Preparing to turn off monitor(s)...");
        }

        /// <summary>
        /// Process record - main cmdlet logic
        /// </summary>
        protected override void ProcessRecord()
        {
            // Wait briefly to allow any pending screen operations to complete
            System.Threading.Thread.Sleep(2000);

            // Check if we should proceed with turning off the monitors
            if (ShouldProcess("All Monitors", "Turn Off"))
            {
                // Invoke windows power management api to trigger monitor power-off
                GenXdev.Helpers.WindowObj.SleepMonitor();
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
// ################################################################################
// Part of PowerShell module : GenXdev.Console
// Original cmdlet filename  : Set-MonitorPowerOn.cs
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
using GenXdev.Helpers;

namespace GenXdev.Console
{
    /// <summary>
    /// <para type="synopsis">
    /// Turns the monitor power on.
    /// </para>
    ///
    /// <para type="description">
    /// Uses the Windows API through GenXdev.Helpers.WindowObj to wake up the monitor
    /// from sleep/power off state. This is useful for automation scripts that need to
    /// ensure the monitor is powered on.
    /// </para>
    ///
    /// <example>
    /// <para>Set-MonitorPowerOn</para>
    /// <para>Turns the monitor power on.</para>
    /// <code>
    /// Set-MonitorPowerOn
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>wake-monitor</para>
    /// <para>Turns the monitor power on using an alias.</para>
    /// <code>
    /// wake-monitor
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "MonitorPowerOn")]
    [Alias("wakemonitor", "monitoroff")]
    [OutputType(typeof(void))]
    public class SetMonitorPowerOnCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// Begin processing - initialization logic
        /// </summary>
        protected override void BeginProcessing()
        {
            WriteVerbose("Attempting to wake monitor from sleep/power off state");
        }

        /// <summary>
        /// Process record - main cmdlet logic
        /// </summary>
        protected override void ProcessRecord()
        {
            // only proceed if ShouldProcess approves the action
            if (ShouldProcess("Monitor", "Power On"))
            {
                // call the windows api through our helper class to wake the monitor
                WindowObj.WakeMonitor();
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
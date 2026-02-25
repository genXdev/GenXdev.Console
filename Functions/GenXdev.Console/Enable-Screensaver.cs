// ################################################################################
// Part of PowerShell module : GenXdev.Console
// Original cmdlet filename  : Enable-Screensaver.cs
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



/*
<#
.SYNOPSIS
Starts the configured Windows screensaver.

.DESCRIPTION
Activates the Windows system screensaver by executing the default screensaver
executable (scrnsave.scr) with the /s switch to start it immediately.

.EXAMPLE
Enable-Screensaver

.NOTES
This function requires the Windows screensaver to be properly configured in the
system settings. The screensaver executable must exist at the default Windows
System32 location.
#>
*/
using System.Management.Automation;

namespace GenXdev.Console
{
    /// <summary>
    /// Cmdlet to start the configured Windows screensaver.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Enable, "Screensaver")]
    [OutputType(typeof(void))]
    public class EnableScreensaverCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// Begin processing - write verbose message
        /// </summary>
        protected override void BeginProcessing()
        {
            WriteVerbose("Starting Windows screensaver activation");
        }

        /// <summary>
        /// Process record - execute the screensaver
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                // Execute the windows screensaver executable with the start switch using PowerShell
                InvokeCommand.InvokeScript($@"
                    Microsoft.PowerShell.Management\Start-Process -FilePath '{System.IO.Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "system32", "scrnsave.scr")}' -ArgumentList '/s'
                ");
            }
            catch (Exception ex)
            {
                WriteError(new ErrorRecord(
                    ex,
                    "ScreensaverExecutionFailed",
                    ErrorCategory.OperationStopped,
                    null));
            }
        }

        /// <summary>
        /// End processing - empty implementation
        /// </summary>
        protected override void EndProcessing()
        {
            // Empty implementation matching original PowerShell function
        }
    }
}
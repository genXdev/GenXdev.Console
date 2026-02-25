<##############################################################################
Part of PowerShell module : GenXdev.Console
Original cmdlet filename  : UtcNow.ps1
Original author           : René Vaessen / GenXdev
Version                   : 3.3.2026
################################################################################
Copyright (c)  René Vaessen / GenXdev

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
################################################################################>
###############################################################################
<#
.SYNOPSIS
Gets the current UTC (Coordinated Universal Time) date and time.

.DESCRIPTION
Returns the current UTC date and time as a System.DateTime object. This function
provides a standardized way to retrieve UTC time across different systems and
time zones. The returned DateTime object can be used for timestamp
 synchronization, logging, and cross-timezone operations.

.EXAMPLE
$currentUtc = UtcNow
Returns the current UTC time as a DateTime object.

.EXAMPLE
$timestamp = (UtcNow).ToString("yyyy-MM-dd HH:mm:ss")
Gets the current UTC time and formats it as a string.
#>
function UtcNow {

    [CmdletBinding()]
    [OutputType([System.DateTime])]
    param()

    begin {

        # log function execution for debugging purposes
        Microsoft.PowerShell.Utility\Write-Verbose 'Starting UtcNow function execution'
    }


    process {

        # retrieve the current utc time using .net datetime for precision
        Microsoft.PowerShell.Utility\Write-Verbose 'Retrieving current UTC datetime'
        [DateTime]::UtcNow
    }

    end {
    }
}
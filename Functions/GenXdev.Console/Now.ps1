<##############################################################################
Part of PowerShell module : GenXdev.Console
Original cmdlet filename  : Now.ps1
Original author           : René Vaessen / GenXdev
Version                   : 2.3.2026
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
Returns the current system date and time as a DateTime object.

.DESCRIPTION
Provides a simple way to get the current system date and time without any
parameters. Returns a standard .NET DateTime object that can be used for
datetime calculations, formatting, and comparisons.

.EXAMPLE
Now
Returns the current system date and time as a DateTime object

.EXAMPLE
$timestamp = Now
Stores the current date and time in a variable for later use
#>
function Now {

    [CmdletBinding()]
    [OutputType([System.DateTime])]
    param()

    begin {

        # log function start with verbose output
        Microsoft.PowerShell.Utility\Write-Verbose 'Starting Now function to retrieve system date and time'
    }


    process {

        # return the current system datetime using .NET DateTime.Now property
        # this provides high-precision timestamp including date and time
        return [DateTime]::Now
    }

    end {
    }
}
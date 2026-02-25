<##############################################################################
Part of PowerShell module : GenXdev.Console
Original cmdlet filename  : SayTime.ps1
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
Announces the current time using text-to-speech.

.DESCRIPTION
This function gets the current time and uses the system's text-to-speech engine
to verbally announce it in hours and minutes format. It's useful for
accessibility purposes or when you need an audible time announcement.

.EXAMPLE
SayTime
Speaks the current time, e.g. "The time is 14 hours and 30 minutes"
#>
function SayTime {

    [CmdletBinding()]
    param(
        [switch] $Wait
    )

    begin {

        # get the current system time
        $date = Microsoft.PowerShell.Utility\Get-Date

        # extract hours (0-23) and minutes (0-59) from current time
        $hours = $date.Hour
        $minutes = $date.Minute

        # log the current time for troubleshooting
        Microsoft.PowerShell.Utility\Write-Verbose "Processing time announcement for $hours`:$minutes"
    }


    process {

        # construct the speech text in a clear, consistent format
        $speechText = "The time is $($hours.ToString('0')) hours and " +
        "$($minutes.ToString('0')) minutes"

        # log the text that will be spoken
        Microsoft.PowerShell.Utility\Write-Verbose "Speaking: $speechText"

        # convert the text to speech using system TTS engine
        GenXdev.Console\Start-TextToSpeech $speechText -Wait:$Wait
    }

    end {
    }
}
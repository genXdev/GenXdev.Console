﻿###############################################################################
<#
.SYNOPSIS
Navigates up five directory levels in the file system hierarchy.

.DESCRIPTION
Changes the current working directory by moving up five parent directory levels
and displays the contents of the resulting directory. This function provides a
convenient shorthand alias '......' for quick navigation.

.EXAMPLE
Set-LocationParent5

.EXAMPLE
......
#>
function Set-LocationParent5 {

    [CmdletBinding(SupportsShouldProcess)]
    [Alias('......')]

    param (
        ########################################################################
    )

    begin {

        Microsoft.PowerShell.Utility\Write-Verbose "Starting directory navigation up 5 levels from: $PWD"
    }


    process {

        # navigate up five levels
        for ($i = 1; $i -le 5; $i++) {

            # check if we can move up before attempting
            $parent = Microsoft.PowerShell.Management\Split-Path -Path (Microsoft.PowerShell.Management\Get-Location) -Parent
            if ($null -eq $parent) {
                Microsoft.PowerShell.Utility\Write-Verbose 'Cannot go up further - at root level'
                break
            }

            # prepare target description for ShouldProcess
            $target = "from '$(Microsoft.PowerShell.Management\Get-Location)' to '$parent' (level $i of 5)"

            # only navigate if ShouldProcess returns true
            if ($PSCmdlet.ShouldProcess($target, 'Change location')) {
                Microsoft.PowerShell.Management\Set-Location -LiteralPath $parent
            }
            else {
                # exit the loop if user declined
                break
            }
        }

        # show contents of the new current directory if not in WhatIf mode
        if (-not $WhatIfPreference -and (Microsoft.PowerShell.Management\Get-Location).Provider.Name -eq 'FileSystem') {
            Microsoft.PowerShell.Management\Get-ChildItem
        }
    }

    end {

        Microsoft.PowerShell.Utility\Write-Verbose "Completed navigation. New location: $PWD"
    }
}
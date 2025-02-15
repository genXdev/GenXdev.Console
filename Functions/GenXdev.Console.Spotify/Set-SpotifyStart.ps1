################################################################################
<#
.SYNOPSIS
Starts Spotify playback on the currently active device.

.DESCRIPTION
This function initiates playback on the device that is currently active in
Spotify. It uses the Spotify API token to authenticate the request and control
playback.

.EXAMPLE
Set-SpotifyStart

.EXAMPLE
play
#>
function Set-SpotifyStart {

    [CmdletBinding()]
    [Alias("play", "Start-Music")]
    param()

    begin {

        # output verbose information about starting playback
        Write-Verbose "Initiating Spotify playback on active device"
    }

    process {

        # retrieve the current spotify api token for authentication
        $token = Get-SpotifyApiToken

        # use the spotify helper class to start playback
        [GenXdev.Helpers.Spotify]::Start($token)
    }

    end {
    }
}
################################################################################

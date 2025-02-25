################################################################################
<#
.SYNOPSIS
Caches a Spotify API token for later use in the local configuration.

.DESCRIPTION
This function stores a provided Spotify API token in a local JSON file for
subsequent use by other Spotify-related commands. The token is saved in a
dedicated configuration directory under GenXdev.Local.

.PARAMETER ApiToken
The Spotify API authentication token to be cached locally.

.EXAMPLE
Set-SpotifyApiToken -ApiToken "YOUR-SPOTIFY-API-TOKEN"
#>
function Set-SpotifyApiToken {

    [CmdletBinding()]
    param(
        ########################################################################
        [parameter(
            Mandatory = $true,
            Position = 0,
            HelpMessage = "The Spotify API token to cache locally"
        )]
        [string] $ApiToken
        ########################################################################
    )

    begin {

        # define the storage location for the api token
        $dir = "$PSScriptRoot\..\..\..\GenXdev.Local"
        $path = "$dir\Spotify_Auth.json"

        Write-Verbose "Storing Spotify API token in: $path"
    }

    process {

        # ensure the storage directory exists
        if (![IO.Directory]::Exists($dir)) {

            Write-Verbose "Creating directory: $dir"
            [IO.Directory]::CreateDirectory($dir)
        }

        # save the trimmed api token to the json file
        Write-Verbose "Writing API token to file"
        [IO.File]::WriteAllText($path, $ApiToken.Trim("`r`n`t "))
    }

    end {
    }
}
################################################################################

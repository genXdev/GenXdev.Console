###############################################################################
<#
.SYNOPSIS
Retrieves lyrics for Spotify tracks from Musixmatch.com

.DESCRIPTION
Searches for and displays song lyrics by either:
- Using a Spotify track ID
- Searching for tracks by name/artist
- Getting lyrics for currently playing track
If lyrics aren't found on Musixmatch, opens a Google search as fallback.

.PARAMETER TrackId
The Spotify track ID to look up lyrics for. If omitted, uses currently playing
track or allows searching by name.

.PARAMETER Queries
Search terms to find a track. Can include artist name and/or song title.
Results will be shown for selection.

.EXAMPLE
Get-SpotifyLyrics -TrackId "1301WleyT98MSxVHPZCA6M"

.EXAMPLE
lyrics "bohemian rhapsody queen"
        ###############################################################################>
function Get-SpotifyLyrics {

    [CmdLetBinding(DefaultParameterSetName = "")]
    [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("PSUseSingularNouns", "Get-SpotifyLyrics")]
    [Alias("lyrics")]

    param(
        ########################################################################
        [parameter(
            Mandatory = $false,
            ValueFromPipelineByPropertyName,
            ParameterSetName = "",
            HelpMessage = "Spotify track ID to lookup lyrics for"
        )]
        [Alias("Id")]
        [string] $TrackId = $null,

        ########################################################################
        [parameter(
            Mandatory = $false,
            Position = 0,
            ValueFromRemainingArguments,
            ValueFromPipeline,
            ValueFromPipelineByPropertyName,
            HelpMessage = "Search terms to find a track"
        )]
        [Alias("q", "Value", "Name", "Text", "Query")]
        [string[]] $Queries = $null
        ########################################################################
    )

    begin {

        if (@(Microsoft.PowerShell.Core\Get-Module GenXdev.Queries -ErrorAction SilentlyContinue).Count -eq 0) {

            $null = PowerShellGet\Install-Module GenXdev.Queries -SkipPublisherCheck
            $null = Microsoft.PowerShell.Core\Import-Module GenXdev.Queries
        }

        # handle track search if queries provided
        if ($null -ne $Queries) {

            Microsoft.PowerShell.Utility\Write-Verbose "Searching Spotify for tracks matching query: $Queries"

            # search spotify and build list of track names with artists
            $results = GenXdev.Console\Search-Spotify -SearchType Track -Queries $Queries
            $new = [System.Collections.Generic.List[string]]::new()

            foreach ($track in $results.Tracks.Items) {
                $null = $new.Add("$($track.Artists[0].Name) - $($track.Name)")
            }

            $Queries = $new
            if ($new.Count -eq 0) {
                Microsoft.PowerShell.Utility\Write-Warning "No tracks found matching search terms"
            }
        }
        else {
            # use track ID if provided
            if ([String]::IsNullOrWhiteSpace($TrackId) -eq $false) {

                Microsoft.PowerShell.Utility\Write-Verbose "Looking up track by ID: $TrackId"
                $track = GenXdev.Console\Get-SpotifyTrackById -TrackId $TrackId

                if ($null -ne $track) {
                    $Queries = @("$($track.Artists[0].Name) - $($track.Name)")
                }
            }
            else {
                # get currently playing track
                Microsoft.PowerShell.Utility\Write-Verbose "Getting currently playing track"
                $current = GenXdev.Console\Get-SpotifyCurrentlyPlaying

                if ($null -ne $current) {
                    $Queries = @("$($current.Item.Artists[0].Name) - " +
                        "$($current.Item.Name)")
                }
            }
        }

        if ($null -eq $Queries) {
            throw "No song playing and no search terms provided"
        }

         if (@(Microsoft.PowerShell.Core\Get-Module GenXdev.Queries -ErrorAction SilentlyContinue).Count -eq 0) {

                $null = PowerShellGet\Install-Module GenXdev.Queries -SkipPublisherCheck
                $null = Microsoft.PowerShell.Core\Import-Module GenXdev.Queries
            }
    }


process {

        foreach ($query in $Queries) {

            Microsoft.PowerShell.Utility\Write-Verbose "Searching Musixmatch for lyrics: $query"

            # encode query for URL
            $q = [Uri]::EscapeUriString($query)
            [string] $html = ""

            # attempt to get search results page
            try {
                $html = Microsoft.PowerShell.Utility\Invoke-WebRequest `
                    -Uri "https://www.musixmatch.com/search/$q" `
                    -ErrorAction SilentlyContinue
            }
            catch {
                Microsoft.PowerShell.Utility\Write-Warning "No results found for '$query'"
                GenXdev.Queries\Open-GoogleQuery "lyrics $query"
                continue
            }

            # extract best match URL from search results
            [int] $idx = $html.IndexOf("Best Result")
            if ($idx -lt 0) {
                Microsoft.PowerShell.Utility\Write-Warning "No results found for '$query'"
                GenXdev.Queries\Open-GoogleQuery "lyrics $query"
                continue
            }

            $idx = $html.IndexOf('<a class="title" href="', $idx)
            if ($idx -lt 0) {
                Microsoft.PowerShell.Utility\Write-Warning "No results found for '$query'"
                GenXdev.Queries\Open-GoogleQuery "lyrics $query"
                continue
            }

            $idx += '<a class="title" href="'.Length
            [int] $idx2 = $html.IndexOf('"', $idx)

            if ($idx2 -lt 0) {
                Microsoft.PowerShell.Utility\Write-Warning "No results found for '$query'"
                GenXdev.Queries\Open-GoogleQuery "lyrics $query"
                continue
            }

            # get lyrics page
            $url = "https://www.musixmatch.com$($html.Substring($idx, $idx2-$idx))"
            Microsoft.PowerShell.Utility\Write-Verbose "Fetching lyrics from: $url"

            try {
                $html = Microsoft.PowerShell.Utility\Invoke-WebRequest -Uri $url -ErrorAction SilentlyContinue
            }
            catch {
                Microsoft.PowerShell.Utility\Write-Warning "Failed to get lyrics for '$query'"
                GenXdev.Queries\Open-GoogleQuery "lyrics $query"
                continue
            }

            # extract lyrics from page
            $idx = $html.IndexOf('"body":"')
            if ($idx -lt 0) {
                Microsoft.PowerShell.Utility\Write-Warning "No lyrics found for '$query'"
                GenXdev.Queries\Open-GoogleQuery "lyrics $query"
                continue
            }

            $idx += '"body":"'.Length
            $idx2 = $html.IndexOf('","language":', $idx)

            if ($idx2 -lt 0) {
                Microsoft.PowerShell.Utility\Write-Warning "No lyrics found for '$query'"
                GenXdev.Queries\Open-GoogleQuery "lyrics $query"
                continue
            }

            # parse and clean up lyrics
            $result = "`"$($html.Substring($idx, $idx2-$idx))`"" |
            Microsoft.PowerShell.Utility\ConvertFrom-Json

            if ([String]::IsNullOrWhiteSpace($result)) {
                Microsoft.PowerShell.Utility\Write-Warning "Empty lyrics found for '$query'"
                GenXdev.Queries\Open-GoogleQuery "lyrics $query"
            }

            $result.Replace("???", "'")
        }
    }
}
        ###############################################################################
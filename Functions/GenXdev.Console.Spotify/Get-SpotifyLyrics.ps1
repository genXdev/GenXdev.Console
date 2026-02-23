<##############################################################################
Part of PowerShell module : GenXdev.Console.Spotify
Original cmdlet filename  : Get-SpotifyLyrics.ps1
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
#>
function Get-SpotifyLyrics {

    [CmdLetBinding(DefaultParameterSetName = '')]
    [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSUseSingularNouns', 'Get-SpotifyLyrics')]
    [Alias('lyrics')]

    param(
        ########################################################################
        [parameter(
            Mandatory = $false,
            ValueFromPipelineByPropertyName,
            ParameterSetName = '',
            HelpMessage = 'Spotify track ID to lookup lyrics for'
        )]
        [Alias('Id')]
        [string] $TrackId = $null,
        ###############################################################################

        ###############################################################################
        [Alias('incognito','inprivate')]
        [parameter(Mandatory = $false, HelpMessage = 'Open browser in private/incognito mode')]
        [switch] $Private,
        ###############################################################################
        [parameter(Mandatory = $false, HelpMessage = 'Force browser launch or override restrictions')]
        [switch] $Force,
        ###############################################################################

        ###############################################################################
        [Alias('e')]
        [parameter(Mandatory = $false, HelpMessage = 'Use Microsoft Edge browser')]
        [switch] $Edge,
        ###############################################################################

        ###############################################################################
        [Alias('ch')]
        [parameter(Mandatory = $false, HelpMessage = 'Use Google Chrome browser')]
        [switch] $Chrome,
        ###############################################################################

        ###############################################################################
        [Alias('c')]
        [parameter(Mandatory = $false, HelpMessage = 'Use Chromium browser')]
        [switch] $Chromium,
        ###############################################################################

        ###############################################################################
        [Alias('ff')]
        [parameter(Mandatory = $false, HelpMessage = 'Use Mozilla Firefox browser')]
        [switch] $Firefox,
        ###############################################################################

        ###############################################################################
        [Alias('m','mon')]
        [parameter(Mandatory = $false, HelpMessage = 'Target monitor for browser window')]
        [int] $Monitor = -1,
        ###############################################################################

        ###############################################################################
        [Alias('fs','f')]
        [parameter(Mandatory = $false, HelpMessage = 'Open browser in full screen mode')]
        [switch] $FullScreen,
        ###############################################################################
        [parameter(Mandatory = $false, HelpMessage = 'Width of browser window in pixels')]
        [int] $Width = -1,
        ###############################################################################
        [parameter(Mandatory = $false, HelpMessage = 'Height of browser window in pixels')]
        [int] $Height = -1,
        ###############################################################################
        [parameter(Mandatory = $false, HelpMessage = 'Left position of browser window')]
        [int] $Left,
        ###############################################################################
        [parameter(Mandatory = $false, HelpMessage = 'Right position of browser window')]
        [int] $Right,
        ###############################################################################
        [parameter(Mandatory = $false, HelpMessage = 'Top position of browser window')]
        [int] $Top,
        ###############################################################################
        [parameter(Mandatory = $false, HelpMessage = 'Bottom position of browser window')]
        [int] $Bottom,
        ###############################################################################
        [parameter(Mandatory = $false, HelpMessage = 'Center browser window on screen')]
        [switch] $Centered,
        ###############################################################################

        ###############################################################################
        [Alias('a','app','appmode')]
        [parameter(Mandatory = $false, HelpMessage = 'Open browser in application mode')]
        [switch] $ApplicationMode,
        ###############################################################################

        ###############################################################################
        [Alias('de','ne','NoExtensions')]
        [parameter(Mandatory = $false, HelpMessage = 'Disable browser extensions')]
        [switch] $NoBrowserExtensions,
        ###############################################################################

        ###############################################################################
        [Alias('allowpopups')]
        [parameter(Mandatory = $false, HelpMessage = 'Disable popup blocker in browser')]
        [switch] $DisablePopupBlocker,
        ###############################################################################

        ###############################################################################
        [Alias('lang','locale')]
        [parameter(Mandatory = $false, HelpMessage = 'Set Accept-Language HTTP header')]
        [string] $AcceptLang,
        ###############################################################################

        ###############################################################################
        [Alias('Escape')]
        [parameter(Mandatory = $false, HelpMessage = 'Send Escape key after page load')]
        [switch] $SendKeyEscape,
        ###############################################################################

        ###############################################################################
        [Alias('HoldKeyboardFocus')]
        [parameter(Mandatory = $false, HelpMessage = 'Hold keyboard focus after sending keys')]
        [switch] $SendKeyHoldKeyboardFocus,
        ###############################################################################

        ###############################################################################
        [Alias('UseShiftEnter')]
        [parameter(Mandatory = $false, HelpMessage = 'Use Shift+Enter when sending keys')]
        [switch] $SendKeyUseShiftEnter,
        ###############################################################################

        ###############################################################################
        [Alias('DelayMilliSeconds')]
        [parameter(Mandatory = $false, HelpMessage = 'Delay in milliseconds between sending keys')]
        [int] $SendKeyDelayMilliSeconds,
        ###############################################################################

        ###############################################################################
        [Alias('fw','focus')]
        [parameter(Mandatory = $false, HelpMessage = 'Focus browser window after launch')]
        [switch] $FocusWindow,
        ###############################################################################

        ###############################################################################
        [Alias('fg')]
        [parameter(Mandatory = $false, HelpMessage = 'Set browser window to foreground')]
        [switch] $SetForeground,
        ###############################################################################
        [parameter(Mandatory = $false, HelpMessage = 'Maximize browser window')]
        [switch] $Maximize,
        ###############################################################################

        ###############################################################################
        [Alias('rf','bg')]
        [parameter(Mandatory = $false, HelpMessage = 'Restore focus to previous window after closing browser')]
        [switch] $RestoreFocus,
        ###############################################################################

        ###############################################################################
        [Alias('nw','new')]
        [parameter(Mandatory = $false, HelpMessage = 'Open link in a new browser window')]
        [switch] $NewWindow,
        ###############################################################################
        [parameter(Mandatory = $false, HelpMessage = 'Return the URL after opening')]
        [switch] $ReturnURL,
        ###############################################################################
        [parameter(Mandatory = $false, HelpMessage = 'Return only the URL, do not open browser')]
        [switch] $ReturnOnlyURL,
        ###############################################################################

        ###############################################################################
        [Alias('nb')]
        [parameter(Mandatory = $false, HelpMessage = 'Open browser window without borders')]
        [switch] $NoBorders,
        ###############################################################################
        [parameter(Mandatory = $false, HelpMessage = 'Use session-only mode for browser')]
        [switch] $SessionOnly,
        ###############################################################################
        [parameter(Mandatory = $false, HelpMessage = 'Clear browser session before opening')]
        [switch] $ClearSession,
        ###############################################################################

        ###############################################################################
        [Alias('FromPreferences')]
        [parameter(Mandatory = $false, HelpMessage = 'Skip using browser session')]
        [switch] $SkipSession,
        ###############################################################################
        ###############################################################################
        [Alias('sbs')]
        [parameter(Mandatory = $false, HelpMessage = 'Open browser side by side with other windows')]
        [switch] $SideBySide
        ###############################################################################
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
                Microsoft.PowerShell.Utility\Write-Warning 'No tracks found matching search terms'
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
                Microsoft.PowerShell.Utility\Write-Verbose 'Getting currently playing track'
                $current = GenXdev.Console\Get-SpotifyCurrentlyPlaying

                if ($null -ne $current) {
                    $Queries = @("$($current.Item.Artists[0].Name) - " +
                        "$($current.Item.Name)")
                }
            }
        }

        if ($null -eq $Queries) {
            throw 'No song playing and no search terms provided'
        }

        if (@(Microsoft.PowerShell.Core\Get-Module GenXdev.Queries -ErrorAction SilentlyContinue).Count -eq 0) {

            $null = PowerShellGet\Install-Module GenXdev.Queries -SkipPublisherCheck
            $null = Microsoft.PowerShell.Core\Import-Module GenXdev.Queries
        }

        $params = GenXdev.FileSystem\Copy-IdenticalParamValues `
            -FunctionName 'GenXdev.Queries\Open-GoogleQuery' `
            -BoundParameters $PSBoundParameters `
            -DefaultValues (Microsoft.PowerShell.Utility\Get-Variable -Scope Local -ErrorAction SilentlyContinue)
    }


    process {

        foreach ($query in $Queries) {
            Microsoft.PowerShell.Utility\Write-Verbose "Searching Musixmatch for lyrics: $query"

            # encode query for URL
            $q = [Uri]::EscapeUriString($query)
            [string] $html = ''

            # attempt to get search results page
            try {
                $html = Microsoft.PowerShell.Utility\Invoke-WebRequest `
                    -Uri "https://www.musixmatch.com/search/$q" `
                    -ErrorAction SilentlyContinue
            }
            catch {
                Microsoft.PowerShell.Utility\Write-Warning "No results found for '$query'"
                GenXdev.Queries\Open-GoogleQuery @params -Queries "lyrics $query"
                continue
            }

            # extract best match URL from search results
            [int] $idx = $html.IndexOf('Best Result')
            if ($idx -lt 0) {
                Microsoft.PowerShell.Utility\Write-Warning "No results found for '$query'"
                GenXdev.Queries\Open-GoogleQuery @params -Queries "lyrics $query"
                continue
            }

            $idx = $html.IndexOf('<a class="title" href="', $idx)
            if ($idx -lt 0) {
                Microsoft.PowerShell.Utility\Write-Warning "No results found for '$query'"
                GenXdev.Queries\Open-GoogleQuery @params -Queries "lyrics $query"
                continue
            }

            $idx += '<a class="title" href="'.Length
            [int] $idx2 = $html.IndexOf('"', $idx)

            if ($idx2 -lt 0) {
                Microsoft.PowerShell.Utility\Write-Warning "No results found for '$query'"
                GenXdev.Queries\Open-GoogleQuery @params -Queries "lyrics $query"
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
                GenXdev.Queries\Open-GoogleQuery @params -Queries "lyrics $query"
                continue
            }

            # extract lyrics from page
            $idx = $html.IndexOf('"body":"')
            if ($idx -lt 0) {
                Microsoft.PowerShell.Utility\Write-Warning "No lyrics found for '$query'"
                GenXdev.Queries\Open-GoogleQuery @params -Queries "lyrics $query"
                continue
            }

            $idx += '"body":"'.Length
            $idx2 = $html.IndexOf('","language":', $idx)

            if ($idx2 -lt 0) {
                Microsoft.PowerShell.Utility\Write-Warning "No lyrics found for '$query'"
                GenXdev.Queries\Open-GoogleQuery @params -Queries "lyrics $query"
                continue
            }

            # parse and clean up lyrics
            $result = "`"$($html.Substring($idx, $idx2-$idx))`"" |
                Microsoft.PowerShell.Utility\ConvertFrom-Json

            if ([String]::IsNullOrWhiteSpace($result)) {
                Microsoft.PowerShell.Utility\Write-Warning "Empty lyrics found for '$query'"
                GenXdev.Queries\Open-GoogleQuery @params -Queries "lyrics $query"
            }

            $result.Replace('???', "'")
        }
    }
}
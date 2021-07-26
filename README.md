<hr/>

<img src="powershell.jpg" alt="GenXdev" width="50%"/>

<hr/>

### NAME
    GenXdev.Console
### SYNOPSIS
    A Windows PowerShell module for enhancing the commandline experience
[![GenXdev.Console](https://img.shields.io/powershellgallery/v/GenXdev.Console.svg?style=flat-square&label=GenXdev.Console)](https://www.powershellgallery.com/packages/GenXdev.Console/) [![License](https://img.shields.io/github/license/renevaessen/GenXdev.Console?style=flat-square)](./LICENSE)

### FEATURES

    * ✅ Text-to-speech
    * ✅ Spotify control
    * ✅ Microsoft Shell helpers
    * ✅ after installation, use 'cmds' to see full list of installed GenXdev cmdslets

### DEPENDENCIES
[![WinOS - Windows-10](https://img.shields.io/badge/WinOS-Windows--10--10.0.19041--SP0-brightgreen)](https://www.microsoft.com/en-us/windows/get-windows-10) [![GenXdev.Helpers](https://img.shields.io/powershellgallery/v/GenXdev.Helpers.svg?style=flat-square&label=GenXdev.Helpers)](https://www.powershellgallery.com/packages/GenXdev.Helpers/) [![GenXdev.Webbrowser](https://img.shields.io/powershellgallery/v/GenXdev.Webbrowser.svg?style=flat-square&label=GenXdev.Webbrowser)](https://www.powershellgallery.com/packages/GenXdev.Webbrowser/)
### INSTALLATION
````PowerShell
Install-Module "GenXdev.Console" -Force
Import-Module "GenXdev.Console"
````
### UPDATE
````PowerShell
Update-Module
````

<br/><hr/><hr/><br/>

# Cmdlet Index
| Command&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | aliases&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | Description |
| --- | --- | --- |
| [Add-SpotifyNewPlaylist](#Add-SpotifyNewPlaylist) |  | Creates a new Spotify playlist |
| [Add-SpotifyTracksToPlaylist](#Add-SpotifyTracksToPlaylist) |  | Adds tracks to a Spotify playlist |
| [Connect-SpotifyApiToken](#Connect-SpotifyApiToken) |  | Uses Spotify Open-Auth to request an access token |
| [Enable-Screensaver](#Enable-Screensaver) |  | Starts the configured Windows screensaver |
| [Get-GenXDevCmdLets](#Get-GenXDevCmdLets) |  | Retreives a list of all installed GenXdev modules and their Cmdlets and corresponding aliases |
| [Get-SpotifyActiveDevice](#Get-SpotifyActiveDevice) |  | Returns all currently active Spotify devices for current user |
| [Get-SpotifyApiToken](#Get-SpotifyApiToken) |  | Returns a ApiToken for Spotify |
| [Get-SpotifyCurrentlyPlaying](#Get-SpotifyCurrentlyPlaying) | gcp | Returns the currently on Spotify playing track |
| [Get-SpotifyDevices](#Get-SpotifyDevices) |  | Returns all currently available Spotify devices for current user |
| [Get-SpotifyLyrics](#Get-SpotifyLyrics) | lyrics | Searches for lyrics of a track |
| [Get-SpotifyPlaylistTracks](#Get-SpotifyPlaylistTracks) |  | Returns all tracks of a Spotify playlist |
| [Get-SpotifyTrackAudioFeatures](#Get-SpotifyTrackAudioFeatures) |  | Returns Spotify track audio feature information |
| [Get-SpotifyTrackById](#Get-SpotifyTrackById) |  | Returns full Spotify track information by given TrackId |
| [Get-SpotifyUserPlaylists](#Get-SpotifyUserPlaylists) | upl | Returns a fully populated collection of Spotify playlists owned by current user |
| [Invoke-Fasti](#Invoke-Fasti) | Fasti | Will extract all archive files (zip, 7z, tar, etc) found in current directory and then DELETE them.Each archive file is extracted into their own directory with the same name as the file |
| [Invoke-Repeated](#Invoke-Repeated) | rpt | Pauses playback on the device that is active on Spotify |
| [New-MicrosoftShellTab](#New-MicrosoftShellTab) | x | Opens a new Windows Terminal tab and closes current by default |
| [Remove-SpotifyTracksFromPlaylist](#Remove-SpotifyTracksFromPlaylist) |  | Removes tracks from a Spotify playlist |
| [Search-Spotify](#Search-Spotify) | fm, sm | Performs a Spotify search and returns the search results |
| [Search-SpotifyAndEnqueue](#Search-SpotifyAndEnqueue) | fmq, smq | Performs a Spotify search and adds the first item to the queue |
| [Search-SpotifyAndPlay](#Search-SpotifyAndPlay) | fmp, smp | Performs a Spotify search and plays the first found item on the active device |
| [Set-LocationParent](#Set-LocationParent) | .. | Provides the .. alias to go one directory up |
| [Set-LocationParent2](#Set-LocationParent2) | ... | Provides the ... alias to go two directory up |
| [Set-LocationParent3](#Set-LocationParent3) | .... | Provides the .... alias to go three directory up |
| [Set-LocationParent4](#Set-LocationParent4) | ..... | Provides the ..... alias to go four directory up |
| [Set-LocationParent5](#Set-LocationParent5) | ...... | Provides the ...... alias to go five directory up |
| [Set-MonitorPowerOff](#Set-MonitorPowerOff) |  | Turns the monitor power off |
| [Set-MonitorPowerOn](#Set-MonitorPowerOn) |  | Turns the monitor power on |
| [Set-SpotifyActiveDevice](#Set-SpotifyActiveDevice) |  | Transfers playback to provided Spotify device |
| [Set-SpotifyApiToken](#Set-SpotifyApiToken) |  | Caches an Spotify API-token for later use |
| [Set-SpotifyNext](#Set-SpotifyNext) | next, skip | Skips to next track on the device that is active on Spotify |
| [Set-SpotifyPause](#Set-SpotifyPause) | pausemusic, Resume-Music | Pauses playback on the device that is active on Spotify |
| [Set-SpotifyPlaylistDetails](#Set-SpotifyPlaylistDetails) | spld | Sets the main properties of a Spotify playlist |
| [Set-SpotifyPlaylistOrder](#Set-SpotifyPlaylistOrder) |  | Reorders a range of tracks inside a Spotify playlist |
| [Set-SpotifyPrevious](#Set-SpotifyPrevious) | prev, previous | Skips to previous track on the device that is active on Spotify |
| [Set-SpotifyRepeatContext](#Set-SpotifyRepeatContext) | repeat | Enables playlist-repeat on the device that is active on Spotify |
| [Set-SpotifyRepeatOff](#Set-SpotifyRepeatOff) | norepeat, repeatoff | Disables repeat on the device that is active on Spotify |
| [Set-SpotifyRepeatSong](#Set-SpotifyRepeatSong) | repeatsong | Enables song-repeat on the device that is active on Spotify |
| [Set-SpotifyShuffleOff](#Set-SpotifyShuffleOff) | noshuffle, shuffleoff | Disables song-shuffle on the device that is active on Spotify |
| [Set-SpotifyShuffleOn](#Set-SpotifyShuffleOn) | shuffle, shuffleon | Enables song-shuffle on the device that is active on Spotify |
| [Set-SpotifyStart](#Set-SpotifyStart) | play, Start-Music | Starts playback on the device that is active on Spotify |
| [Set-SpotifyStop](#Set-SpotifyStop) | stop, Stop-Music | Stops playback on the device that is active on Spotify |
| [Show-GenXDevCmdLets](#Show-GenXDevCmdLets) | cmds | Shows a list of all installed GenXdev modules and their Cmdlets and corresponding aliases |
| [Show-Verb](#Show-Verb) |  | Shows a short alphabetical list of all PowerShell verbs |
| [Start-TextToSpeech](#Start-TextToSpeech) | say | Will use the text-to-speech engine to speak out text |
| [Stop-TextToSpeech](#Stop-TextToSpeech) | sst | Will stop the text-to-speech engine from saying anything else |

<br/><hr/><hr/><br/>


# Cmdlets

##	Add-SpotifyNewPlaylist
````PowerShell
Add-SpotifyNewPlaylist
````

### SYNOPSIS
    Creates a new Spotify playlist

### SYNTAX
````PowerShell
Add-SpotifyNewPlaylist [-Name] <String> [[-Description] <String>] 
[-Public] [-Collabrative] [<CommonParameters>]
````

### DESCRIPTION
    Creates a new Spotify playlist

### PARAMETERS
    -Name <String>
        The name for the new playlist
        Required?                    true
        Position?                    1
        Default value                
        Accept pipeline input?       false
        Accept wildcard characters?  false
    -Description <String>
        The description for the new playlist
        Required?                    false
        Position?                    2
        Default value                
        Accept pipeline input?       false
        Accept wildcard characters?  false
    -Public [<SwitchParameter>]
        Make this a public playlist
        Required?                    false
        Position?                    named
        Default value                False
        Accept pipeline input?       false
        Accept wildcard characters?  false
    -Collabrative [<SwitchParameter>]
        Allow others to make changes
        Required?                    false
        Position?                    named
        Default value                False
        Accept pipeline input?       false
        Accept wildcard characters?  false
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Add-SpotifyTracksToPlaylist
````PowerShell
Add-SpotifyTracksToPlaylist
````

### SYNOPSIS
    Adds tracks to a Spotify playlist

### SYNTAX
````PowerShell
Add-SpotifyTracksToPlaylist [-PlaylistId] <String> [-Uri] <String[]> 
[<CommonParameters>]
````

### DESCRIPTION
    Adds tracks to a Spotify playlist

### PARAMETERS
    -PlaylistId <String>
        The Spotify playlist to add tracks to
        Required?                    true
        Position?                    1
        Default value                
        Accept pipeline input?       false
        Accept wildcard characters?  false
    -Uri <String[]>
        The Spotify track Uris of the songs that should be added to the 
        playlist"
        Required?                    true
        Position?                    2
        Default value                
        Accept pipeline input?       true (ByValue, ByPropertyName)
        Accept wildcard characters?  false
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Connect-SpotifyApiToken
````PowerShell
Connect-SpotifyApiToken
````

### SYNOPSIS
    Uses Spotify Open-Auth to request an access token

### SYNTAX
````PowerShell
Connect-SpotifyApiToken [<CommonParameters>]
````

### DESCRIPTION
    Uses Spotify Open-Auth to request an access token

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Enable-Screensaver
````PowerShell
Enable-Screensaver
````

### SYNOPSIS
    Starts the configured Windows screensaver

### SYNTAX
````PowerShell
Enable-Screensaver [<CommonParameters>]
````

### DESCRIPTION
    Starts the configured Windows screensaver

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Get-GenXDevCmdLets
````PowerShell
Get-GenXDevCmdLets
````

### SYNOPSIS
    Retreives a list of all installed GenXdev modules and their Cmdlets and 
    corresponding aliases

### SYNTAX
````PowerShell
Get-GenXDevCmdLets [[-Filter] <String>] [-ModuleName <String[]>] 
[<CommonParameters>]
````

### DESCRIPTION
    Retreives a list of all installed GenXdev modules and their Cmdlets and 
    corresponding aliases

### PARAMETERS
    -Filter <String>
        Allows you to search for cmdLets by providing searchstrings, with or 
        without wildcards
        Required?                    false
        Position?                    1
        Default value                *
        Accept pipeline input?       false
        Accept wildcard characters?  false
    -ModuleName <String[]>
        Required?                    false
        Position?                    named
        Default value                @("GenXdev.*")
        Accept pipeline input?       false
        Accept wildcard characters?  false
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Get-SpotifyActiveDevice
````PowerShell
Get-SpotifyActiveDevice
````

### SYNOPSIS
    Returns all currently active Spotify devices for current user

### SYNTAX
````PowerShell
Get-SpotifyActiveDevice [<CommonParameters>]
````

### DESCRIPTION
    Returns all currently active Spotify devices for current user

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Get-SpotifyApiToken
````PowerShell
Get-SpotifyApiToken
````

### SYNOPSIS
    Returns a ApiToken for Spotify

### SYNTAX
````PowerShell
Get-SpotifyApiToken [<CommonParameters>]
````

### DESCRIPTION
    Returns a ApiToken for Spotify

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Get-SpotifyCurrentlyPlaying
````PowerShell
Get-SpotifyCurrentlyPlaying          --> gcp
````

### SYNOPSIS
    Returns the currently on Spotify playing track

### SYNTAX
````PowerShell
Get-SpotifyCurrentlyPlaying [<CommonParameters>]
````

### DESCRIPTION
    Returns the currently on Spotify playing track

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Get-SpotifyDevices
````PowerShell
Get-SpotifyDevices
````

### SYNOPSIS
    Returns all currently available Spotify devices for current user

### SYNTAX
````PowerShell
Get-SpotifyDevices [<CommonParameters>]
````

### DESCRIPTION
    Returns all currently available Spotify devices for current user

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Get-SpotifyLyrics
````PowerShell
Get-SpotifyLyrics                    --> lyrics
````

### SYNOPSIS
    Searches for lyrics of a track

### SYNTAX
````PowerShell
Get-SpotifyLyrics [-TrackId <String>] [[-Queries] <String[]>] 
[<CommonParameters>]
````

### DESCRIPTION
    Searches for lyrics of a track

### PARAMETERS
    -TrackId <String>
        Optional: Spotify id of track to lookup lyrics for
        Required?                    false
        Position?                    named
        Default value                
        Accept pipeline input?       true (ByPropertyName)
        Accept wildcard characters?  false
    -Queries <String[]>
        Optional: A query to find a track to lookup lyrics for
        Required?                    false
        Position?                    1
        Default value                
        Accept pipeline input?       true (ByValue, ByPropertyName)
        Accept wildcard characters?  false
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Get-SpotifyPlaylistTracks
````PowerShell
Get-SpotifyPlaylistTracks
````

### SYNOPSIS
    Returns all tracks of a Spotify playlist

### SYNTAX
````PowerShell
Get-SpotifyPlaylistTracks [-PlaylistId] <String> [<CommonParameters>]
````

### DESCRIPTION
    Returns all tracks of a Spotify playlist

### PARAMETERS
    -PlaylistId <String>
        The Spotify playlist to return tracks for
        Required?                    true
        Position?                    1
        Default value                
        Accept pipeline input?       true (ByValue, ByPropertyName)
        Accept wildcard characters?  false
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Get-SpotifyTrackAudioFeatures
````PowerShell
Get-SpotifyTrackAudioFeatures
````

### SYNOPSIS
    Returns Spotify track audio feature information

### SYNTAX
````PowerShell
Get-SpotifyTrackAudioFeatures [-Id] <String[]> [<CommonParameters>]
````

### DESCRIPTION
    Returns Spotify track audio feature information

### PARAMETERS
    -Id <String[]>
        The Spotify track to return audio features for
        Required?                    true
        Position?                    1
        Default value                
        Accept pipeline input?       true (ByValue, ByPropertyName)
        Accept wildcard characters?  false
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Get-SpotifyTrackById
````PowerShell
Get-SpotifyTrackById
````

### SYNOPSIS
    Returns full Spotify track information by given TrackId

### SYNTAX
````PowerShell
Get-SpotifyTrackById [-TrackId] <String> [<CommonParameters>]
````

### DESCRIPTION
    Returns full Spotify track information by given TrackId

### PARAMETERS
    -TrackId <String>
        The Spotify track id of the track to lookup
        Required?                    true
        Position?                    1
        Default value                
        Accept pipeline input?       true (ByValue, ByPropertyName)
        Accept wildcard characters?  false
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Get-SpotifyUserPlaylists
````PowerShell
Get-SpotifyUserPlaylists             --> upl
````

### SYNOPSIS
    Returns a fully populated collection of Spotify playlists owned by current 
    user

### SYNTAX
````PowerShell
Get-SpotifyUserPlaylists [[-Filter] <String[]>] [<CommonParameters>]
````

### DESCRIPTION
    Returns a fully populated collection of Spotify playlists owned by current 
    user

### PARAMETERS
    -Filter <String[]>
        Required?                    false
        Position?                    1
        Default value                @("*")
        Accept pipeline input?       true (ByValue, ByPropertyName)
        Accept wildcard characters?  false
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Invoke-Fasti
````PowerShell
Invoke-Fasti                         --> Fasti
````

### SYNOPSIS
    Will extract all archive files (zip, 7z, tar, etc) found in current 
    directory and then DELETE them

### SYNTAX
````PowerShell
Invoke-Fasti [<CommonParameters>]
````

### DESCRIPTION
    Will extract all archive files (zip, 7z, tar, etc) found in current 
    directory and then DELETE them.
    Each archive file is extracted into their own directory with the same name 
    as the file

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

### NOTES
````PowerShell
    You need 7z installed
-------------------------- EXAMPLE 1 --------------------------
PS D:\downloads>Invoke-Fasti
````

<br/><hr/><hr/><br/>

##	Invoke-Repeated
````PowerShell
Invoke-Repeated                      --> rpt
````

### SYNOPSIS
    Pauses Spotify playback

### SYNTAX
````PowerShell
Invoke-Repeated [-script] <String> [<CommonParameters>]
````

### DESCRIPTION
    Pauses playback on the device that is active on Spotify

### PARAMETERS
    -script <String>
        Required?                    true
        Position?                    1
        Default value                
        Accept pipeline input?       false
        Accept wildcard characters?  false
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	New-MicrosoftShellTab
````PowerShell
New-MicrosoftShellTab                --> x
````

### SYNOPSIS
    Opens a new Windows Terminal tab

### SYNTAX
````PowerShell
New-MicrosoftShellTab [-DontCloseThisTab] [<CommonParameters>]
````

### DESCRIPTION
    Opens a new Windows Terminal tab and closes current by default

### PARAMETERS
    -DontCloseThisTab [<SwitchParameter>]
        Keeps current tab open
        Required?                    false
        Position?                    named
        Default value                False
        Accept pipeline input?       false
        Accept wildcard characters?  false
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Remove-SpotifyTracksFromPlaylist
````PowerShell
Remove-SpotifyTracksFromPlaylist
````

### SYNOPSIS
    Removes tracks from a Spotify playlist

### SYNTAX
````PowerShell
Remove-SpotifyTracksFromPlaylist [-PlaylistId] <String> [-Uri] <String[]> 
[<CommonParameters>]
````

### DESCRIPTION
    Removes tracks from a Spotify playlist

### PARAMETERS
    -PlaylistId <String>
        The Spotify playlist to delete tracks from
        Required?                    true
        Position?                    1
        Default value                
        Accept pipeline input?       false
        Accept wildcard characters?  false
    -Uri <String[]>
        The Spotify track Uris of the songs that should be removed from the 
        playlist
        Required?                    true
        Position?                    2
        Default value                
        Accept pipeline input?       true (ByValue, ByPropertyName)
        Accept wildcard characters?  false
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Search-Spotify
````PowerShell
Search-Spotify                       --> fm, sm
````

### SYNOPSIS
    Performs a Spotify search and returns the search results

### SYNTAX
````PowerShell
Search-Spotify [-Queries] <String[]> [-SearchType <String[]>] 
[<CommonParameters>]
````

### DESCRIPTION
    Performs a Spotify search and returns the search results

### PARAMETERS
    -Queries <String[]>
        The search phrase
        Required?                    true
        Position?                    1
        Default value                
        Accept pipeline input?       true (ByValue, ByPropertyName)
        Accept wildcard characters?  false
    -SearchType <String[]>
        Optionally, the type of item to search for
        Required?                    false
        Position?                    named
        Default value                @("Track")
        Accept pipeline input?       false
        Accept wildcard characters?  false
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Search-SpotifyAndEnqueue
````PowerShell
Search-SpotifyAndEnqueue             --> fmq, smq
````

### SYNOPSIS
    Performs a Spotify search and adds the first item to the queue

### SYNTAX
````PowerShell
Search-SpotifyAndEnqueue [-Queries] <String[]> [-SearchType <String[]>] 
[<CommonParameters>]
````

### DESCRIPTION
    Performs a Spotify search and adds the first item to the queue

### PARAMETERS
    -Queries <String[]>
        The search phrase
        Required?                    true
        Position?                    1
        Default value                
        Accept pipeline input?       true (ByValue, ByPropertyName)
        Accept wildcard characters?  false
    -SearchType <String[]>
        Optionally, the type of item to search for
        Required?                    false
        Position?                    named
        Default value                @("Track")
        Accept pipeline input?       false
        Accept wildcard characters?  false
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Search-SpotifyAndPlay
````PowerShell
Search-SpotifyAndPlay                --> fmp, smp
````

### SYNOPSIS
    Performs a Spotify search and plays the first found item

### SYNTAX
````PowerShell
Search-SpotifyAndPlay [-Queries] <String[]> [-SearchType <String[]>] 
[<CommonParameters>]
````

### DESCRIPTION
    Performs a Spotify search and plays the first found item on the active 
    device

### PARAMETERS
    -Queries <String[]>
        The search phrase
        Required?                    true
        Position?                    1
        Default value                
        Accept pipeline input?       true (ByValue, ByPropertyName)
        Accept wildcard characters?  false
    -SearchType <String[]>
        Optionally, the type of item to search for
        Required?                    false
        Position?                    named
        Default value                @("Track")
        Accept pipeline input?       false
        Accept wildcard characters?  false
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Set-LocationParent
````PowerShell
Set-LocationParent                   --> ..
````

### SYNOPSIS
    Provides the .. alias to go one directory up

### SYNTAX
````PowerShell
Set-LocationParent [<CommonParameters>]
````

### DESCRIPTION
    Provides the .. alias to go one directory up

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Set-LocationParent2
````PowerShell
Set-LocationParent2                  --> ...
````

### SYNOPSIS
    Provides the ... alias to go two directory up

### SYNTAX
````PowerShell
Set-LocationParent2 [<CommonParameters>]
````

### DESCRIPTION
    Provides the ... alias to go two directory up

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Set-LocationParent3
````PowerShell
Set-LocationParent3                  --> ....
````

### SYNOPSIS
    Provides the .... alias to go three directory up

### SYNTAX
````PowerShell
Set-LocationParent3 [<CommonParameters>]
````

### DESCRIPTION
    Provides the .... alias to go three directory up

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Set-LocationParent4
````PowerShell
Set-LocationParent4                  --> .....
````

### SYNOPSIS
    Provides the ..... alias to go four directory up

### SYNTAX
````PowerShell
Set-LocationParent4 [<CommonParameters>]
````

### DESCRIPTION
    Provides the ..... alias to go four directory up

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Set-LocationParent5
````PowerShell
Set-LocationParent5                  --> ......
````

### SYNOPSIS
    Provides the ...... alias to go five directory up

### SYNTAX
````PowerShell
Set-LocationParent5 [<CommonParameters>]
````

### DESCRIPTION
    Provides the ...... alias to go five directory up

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Set-MonitorPowerOff
````PowerShell
Set-MonitorPowerOff
````

### SYNOPSIS
    Turns the monitor power off

### SYNTAX
````PowerShell
Set-MonitorPowerOff [<CommonParameters>]
````

### DESCRIPTION
    Turns the monitor power off

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Set-MonitorPowerOn
````PowerShell
Set-MonitorPowerOn
````

### SYNOPSIS
    Turns the monitor power on

### SYNTAX
````PowerShell
Set-MonitorPowerOn [<CommonParameters>]
````

### DESCRIPTION
    Turns the monitor power on

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Set-SpotifyActiveDevice
````PowerShell
Set-SpotifyActiveDevice
````

### SYNOPSIS
    Transfers playback to provided Spotify device

### SYNTAX
````PowerShell
Set-SpotifyActiveDevice [-DeviceId] <String> [<CommonParameters>]
````

### DESCRIPTION
    Transfers playback to provided Spotify device

### PARAMETERS
    -DeviceId <String>
        The Spotify deviceId to transfer playback to
        Required?                    true
        Position?                    1
        Default value                
        Accept pipeline input?       true (ByPropertyName)
        Accept wildcard characters?  false
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Set-SpotifyApiToken
````PowerShell
Set-SpotifyApiToken
````

### SYNOPSIS
    Caches an Spotify API-token for later use

### SYNTAX
````PowerShell
Set-SpotifyApiToken [-ApiToken] <String> [<CommonParameters>]
````

### DESCRIPTION
    Caches an Spotify API-token for later use

### PARAMETERS
    -ApiToken <String>
        The API-token to cache
        Required?                    true
        Position?                    1
        Default value                
        Accept pipeline input?       false
        Accept wildcard characters?  false
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Set-SpotifyNext
````PowerShell
Set-SpotifyNext                      --> next, skip
````

### SYNOPSIS
    Skips to next track on Spotify

### SYNTAX
````PowerShell
Set-SpotifyNext [<CommonParameters>]
````

### DESCRIPTION
    Skips to next track on the device that is active on Spotify

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Set-SpotifyPause
````PowerShell
Set-SpotifyPause                     --> pausemusic, Resume-Music
````

### SYNOPSIS
    Pauses Spotify playback

### SYNTAX
````PowerShell
Set-SpotifyPause [<CommonParameters>]
````

### DESCRIPTION
    Pauses playback on the device that is active on Spotify

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Set-SpotifyPlaylistDetails
````PowerShell
Set-SpotifyPlaylistDetails           --> spld
````

### SYNOPSIS
    Sets the main properties of a Spotify playlist

### SYNTAX
````PowerShell
Set-SpotifyPlaylistDetails [-PlaylistId] <String> [-Name] <String> 
[[-Description] <String>] [-Public] [-Collabrative] [-Private] 
[-NoCollabrations] [<CommonParameters>]
````

### DESCRIPTION
    Sets the main properties of a Spotify playlist

### PARAMETERS
    -PlaylistId <String>
        The Spotify playlist to set properties for
        Required?                    true
        Position?                    1
        Default value                
        Accept pipeline input?       false
        Accept wildcard characters?  false
    -Name <String>
        The new name for the playlist
        Required?                    true
        Position?                    2
        Default value                
        Accept pipeline input?       false
        Accept wildcard characters?  false
    -Description <String>
        The new description for the playlist
        Required?                    false
        Position?                    3
        Default value                
        Accept pipeline input?       false
        Accept wildcard characters?  false
    -Public [<SwitchParameter>]
        Make the playlist public
        Required?                    false
        Position?                    named
        Default value                False
        Accept pipeline input?       false
        Accept wildcard characters?  false
    -Collabrative [<SwitchParameter>]
        Allow others to make changes
        Required?                    false
        Position?                    named
        Default value                False
        Accept pipeline input?       false
        Accept wildcard characters?  false
    -Private [<SwitchParameter>]
        Make the playlist private
        Required?                    false
        Position?                    named
        Default value                False
        Accept pipeline input?       false
        Accept wildcard characters?  false
    -NoCollabrations [<SwitchParameter>]
        Disallow others to make changes
        Required?                    false
        Position?                    named
        Default value                False
        Accept pipeline input?       false
        Accept wildcard characters?  false
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Set-SpotifyPlaylistOrder
````PowerShell
Set-SpotifyPlaylistOrder
````

### SYNOPSIS
    Reorders a range of tracks inside a Spotify playlist

### SYNTAX
````PowerShell
Set-SpotifyPlaylistOrder [-PlaylistId] <String> [-RangeStart] <Int32> 
[-InsertBefore] <Int32> [[-RangeLength] <Nullable`1>] [<CommonParameters>]
````

### DESCRIPTION
    Reorders a range of tracks inside a Spotify playlist

### PARAMETERS
    -PlaylistId <String>
        The Spotify playlist to perform the re-ordering on
        Required?                    true
        Position?                    1
        Default value                
        Accept pipeline input?       false
        Accept wildcard characters?  false
    -RangeStart <Int32>
        The position of the first item to be reordered
        Required?                    true
        Position?                    1
        Default value                0
        Accept pipeline input?       false
        Accept wildcard characters?  false
    -InsertBefore <Int32>
        The position where the items should be inserted. To reorder the items 
        to the
        end of the playlist, simply set insert_before to the position after 
        the last
        item. Examples: To reorder the first item to the last position in a 
        playlist
        with 10 items, set range_start to 0, and insert_before to 10. To 
        reorder the
        last item in a playlist with 10 items to the start of the playlist, 
        set range_start
        to 9, and insert_before to 0.
        Required?                    true
        Position?                    2
        Default value                0
        Accept pipeline input?       false
        Accept wildcard characters?  false
    -RangeLength <Nullable`1>
        The amount of items to be reordered. Defaults to 1 if not set. The 
        range of items
        to be reordered begins from the range_start position, and includes the 
        range_length
        subsequent items. Example: To move the items at index 9-10 to the 
        start of the
        playlist, range_start is set to 9, and range_length is set to 2.
        Required?                    false
        Position?                    3
        Default value                
        Accept pipeline input?       false
        Accept wildcard characters?  false
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Set-SpotifyPrevious
````PowerShell
Set-SpotifyPrevious                  --> prev, previous
````

### SYNOPSIS
    Skips to previous track on Spotify

### SYNTAX
````PowerShell
Set-SpotifyPrevious [<CommonParameters>]
````

### DESCRIPTION
    Skips to previous track on the device that is active on Spotify

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Set-SpotifyRepeatContext
````PowerShell
Set-SpotifyRepeatContext             --> repeat
````

### SYNOPSIS
    Enables Spotify playlist-repeat

### SYNTAX
````PowerShell
Set-SpotifyRepeatContext [<CommonParameters>]
````

### DESCRIPTION
    Enables playlist-repeat on the device that is active on Spotify

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Set-SpotifyRepeatOff
````PowerShell
Set-SpotifyRepeatOff                 --> norepeat, repeatoff
````

### SYNOPSIS
    Disables Spotify repeat

### SYNTAX
````PowerShell
Set-SpotifyRepeatOff [<CommonParameters>]
````

### DESCRIPTION
    Disables repeat on the device that is active on Spotify

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Set-SpotifyRepeatSong
````PowerShell
Set-SpotifyRepeatSong                --> repeatsong
````

### SYNOPSIS
    Enables Spotify song-repeat

### SYNTAX
````PowerShell
Set-SpotifyRepeatSong [<CommonParameters>]
````

### DESCRIPTION
    Enables song-repeat on the device that is active on Spotify

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Set-SpotifyShuffleOff
````PowerShell
Set-SpotifyShuffleOff                --> noshuffle, shuffleoff
````

### SYNOPSIS
    Disables Spotify song-shuffle

### SYNTAX
````PowerShell
Set-SpotifyShuffleOff [<CommonParameters>]
````

### DESCRIPTION
    Disables song-shuffle on the device that is active on Spotify

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Set-SpotifyShuffleOn
````PowerShell
Set-SpotifyShuffleOn                 --> shuffle, shuffleon
````

### SYNOPSIS
    Enables Spotify song-shuffle

### SYNTAX
````PowerShell
Set-SpotifyShuffleOn [<CommonParameters>]
````

### DESCRIPTION
    Enables song-shuffle on the device that is active on Spotify

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Set-SpotifyStart
````PowerShell
Set-SpotifyStart                     --> play, Start-Music
````

### SYNOPSIS
    Starts Spotify playback

### SYNTAX
````PowerShell
Set-SpotifyStart [<CommonParameters>]
````

### DESCRIPTION
    Starts playback on the device that is active on Spotify

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Set-SpotifyStop
````PowerShell
Set-SpotifyStop                      --> stop, Stop-Music
````

### SYNOPSIS
    Stops Spotify playback

### SYNTAX
````PowerShell
Set-SpotifyStop [<CommonParameters>]
````

### DESCRIPTION
    Stops playback on the device that is active on Spotify

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Show-GenXDevCmdLets
````PowerShell
Show-GenXDevCmdLets                  --> cmds
````

### SYNOPSIS
    Shows a list of all installed GenXdev modules and their Cmdlets and 
    corresponding aliases

### SYNTAX
````PowerShell
Show-GenXDevCmdLets [[-Filter] <String>] [-ModuleName <String[]>] 
[<CommonParameters>]
````

### DESCRIPTION
    Shows a list of all installed GenXdev modules and their Cmdlets and 
    corresponding aliases

### PARAMETERS
    -Filter <String>
        Allows you to search for cmdLets by providing searchstrings, with or 
        without wildcards
        Required?                    false
        Position?                    1
        Default value                *
        Accept pipeline input?       false
        Accept wildcard characters?  false
    -ModuleName <String[]>
        Required?                    false
        Position?                    named
        Default value                @("GenXdev.*")
        Accept pipeline input?       false
        Accept wildcard characters?  false
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Show-Verb
````PowerShell
Show-Verb
````

### SYNOPSIS
    Shows a short alphabetical list of all PowerShell verbs

### SYNTAX
````PowerShell
Show-Verb [[-Verb] <String[]>] [<CommonParameters>]
````

### DESCRIPTION
    Shows a short alphabetical list of all PowerShell verbs

### PARAMETERS
    -Verb <String[]>
        Required?                    false
        Position?                    1
        Default value                @()
        Accept pipeline input?       true (ByValue, ByPropertyName)
        Accept wildcard characters?  false
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

<br/><hr/><hr/><br/>

##	Start-TextToSpeech
````PowerShell
Start-TextToSpeech                   --> say
````

### SYNOPSIS
    Will use the text-to-speech engine to speak out text

### SYNTAX
````PowerShell
Start-TextToSpeech -lines <String[]> [-wait] [<CommonParameters>]
````

### DESCRIPTION
    Will use the text-to-speech engine to speak out text

### PARAMETERS
    -lines <String[]>
        Required?                    true
        Position?                    named
        Default value                
        Accept pipeline input?       true (ByValue)
        Accept wildcard characters?  false
    -wait [<SwitchParameter>]
        Will wait until all text is spoken
        Required?                    false
        Position?                    named
        Default value                False
        Accept pipeline input?       false
        Accept wildcard characters?  false
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

### NOTES
````PowerShell
    See also: Stop-TextToSpeech -> sst, and Skip-TextToSpeech -> sstSkip
-------------------------- EXAMPLE 1 --------------------------
PS C:\> say "Good morning"
PS C:\> "Good morning" | Start-TextToSpeech
````

<br/><hr/><hr/><br/>

##	Stop-TextToSpeech
````PowerShell
Stop-TextToSpeech                    --> sst
````

### SYNOPSIS
    Will stop the text-to-speech engine from saying anything else

### SYNTAX
````PowerShell
Stop-TextToSpeech [<CommonParameters>]
````

### DESCRIPTION
    Will stop the text-to-speech engine from saying anything else

### PARAMETERS
    <CommonParameters>
        This cmdlet supports the common parameters: Verbose, Debug,
        ErrorAction, ErrorVariable, WarningAction, WarningVariable,
        OutBuffer, PipelineVariable, and OutVariable. For more information, see
        about_CommonParameters 
        (https://go.microsoft.com/fwlink/?LinkID=113216). 

### NOTES
````PowerShell
    See also: Start-TextToSpeech -> say, and Skip-TextToSpeech -> sstSkip
-------------------------- EXAMPLE 1 --------------------------
PS C:\> say "Good morning"; say "Good evening"; Stop-TextToSpeech; # -> 
"G.."
````

<br/><hr/><hr/><br/>

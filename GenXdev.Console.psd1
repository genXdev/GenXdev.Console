#
# Module manifest for module 'GenXdev.Console'
#
# Generated by: genXdev
#
# Generated on: 29/03/2025
#

@{

# Script module or binary module file associated with this manifest.
RootModule = 'GenXdev.Console.psm1'

# Version number of this module.
ModuleVersion = '1.162.2025'

# Supported PSEditions
CompatiblePSEditions = 'Core'

# ID used to uniquely identify this module
GUID = '2f62080f-0483-4421-8497-b3d433b65172'

# Author of this module
Author = 'genXdev'

# Company or vendor of this module
CompanyName = 'GenXdev'

# Copyright statement for this module
Copyright = 'Copyright 2021-2025 GenXdev'

# Description of the functionality provided by this module
Description = 'A Windows PowerShell module for enhancing the commandline experience'

# Minimum version of the PowerShell engine required by this module
PowerShellVersion = '7.5.0'

# Name of the PowerShell host required by this module
# PowerShellHostName = ''

# Minimum version of the PowerShell host required by this module
# PowerShellHostVersion = ''

# Minimum version of Microsoft .NET Framework required by this module. This prerequisite is valid for the PowerShell Desktop edition only.
# DotNetFrameworkVersion = ''

# Minimum version of the common language runtime (CLR) required by this module. This prerequisite is valid for the PowerShell Desktop edition only.
ClrVersion = '9.0.1'

# Processor architecture (None, X86, Amd64) required by this module
ProcessorArchitecture = 'Amd64'

# Modules that must be imported into the global environment prior to importing this module
RequiredModules = @(@{ModuleName = 'PSReadLine'; ModuleVersion = '2.3.6'; }, 
               @{ModuleName = 'GenXdev.Webbrowser'; ModuleVersion = '1.162.2025'; })

# Assemblies that must be loaded prior to importing this module
# RequiredAssemblies = @()

# Script files (.ps1) that are run in the caller's environment prior to importing this module.
# ScriptsToProcess = @()

# Type files (.ps1xml) to be loaded when importing this module
# TypesToProcess = @()

# Format files (.ps1xml) to be loaded when importing this module
# FormatsToProcess = @()

# Modules to import as nested modules of the module specified in RootModule/ModuleToProcess
NestedModules = @('GenXdev.Console.Spotify.psm1', 
               'GenXdev.Console.Vlc.psm1')

# Functions to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no functions to export.
FunctionsToExport = 'Add-SpotifyNewPlaylist', 'Add-SpotifyTracksToLiked', 
               'Add-SpotifyTracksToPlaylist', 'Connect-SpotifyApiToken', 
               'Enable-Screensaver', 'Get-IsSpeaking', 'Get-SpotifyActiveDevice', 
               'Get-SpotifyApiToken', 'Get-SpotifyCurrentlyPlaying', 
               'Get-SpotifyDevice', 'Get-SpotifyLikedTrack', 'Get-SpotifyLyrics', 
               'Get-SpotifyPlaylistIdsByName', 'Get-SpotifyPlaylistTrack', 
               'Get-SpotifyTrackAudioFeatures', 'Get-SpotifyTrackById', 
               'Get-SpotifyUserPlaylists', 'Move-SpotifyLikedTracksToPlaylist', 
               'New-MicrosoftShellTab', 'Now', 'Open-MediaFile', 'Open-VlcMediaPlayer', 
               'Open-VlcMediaPlayerLyrics', 'Remove-SpotifyTracksFromLiked', 
               'Remove-SpotifyTracksFromPlaylist', 'SayDate', 'SayTime', 
               'Search-Spotify', 'Search-SpotifyAndEnqueue', 'Search-SpotifyAndPlay', 
               'Set-LocationParent', 'Set-LocationParent2', 'Set-LocationParent3', 
               'Set-LocationParent4', 'Set-LocationParent5', 'Set-MonitorPowerOff', 
               'Set-MonitorPowerOn', 'Set-SpotifyActiveDevice', 
               'Set-SpotifyApiToken', 'Set-SpotifyNext', 'Set-SpotifyPause', 
               'Set-SpotifyPlaylistDetails', 'Set-SpotifyPlaylistOrder', 
               'Set-SpotifyPrevious', 'Set-SpotifyRepeatContext', 
               'Set-SpotifyRepeatOff', 'Set-SpotifyRepeatSong', 
               'Set-SpotifyShuffleOff', 'Set-SpotifyShuffleOn', 'Set-SpotifyStart', 
               'Set-SpotifyStop', 'Set-VLCPlayerFocused', 'Start-TextToSpeech', 
               'Start-VlcMediaPlayerNextInPlaylist', 
               'Start-VlcMediaPlayerPreviousInPlaylist', 'Stop-TextToSpeech', 
               'Switch-VlcMediaPlayerMute', 'Switch-VlcMediaPlayerPaused', 
               'Switch-VlcMediaPlayerRepeat', 'UtcNow'

# Cmdlets to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no cmdlets to export.
CmdletsToExport = @()

# Variables to export from this module
VariablesToExport = @()

# Aliases to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no aliases to export.
AliasesToExport = '..', '...', '....', '.....', '......', 'addtoplaylist', 'audiofeatures', 
               'dislike', 'fm', 'fmp', 'fmq', 'fvlc', 'gcp', 'Get-SpotifyDevices', 
               'getplaylist', 'gettrack', 'gupl', 'iss', 'like', 'liked', 'lyrics', 'media', 
               'moveliked', 'newplaylist', 'next', 'norepeat', 'noshuffle', 'pausemusic', 
               'play', 'poweroff', 'prev', 'previous', 'removefromplaylist', 'repeat', 
               'repeatoff', 'repeatsong', 'Resume-Music', 'say', 'Set-SpotifyDevice', 
               'showvlc', 'shuffle', 'shuffleoff', 'shuffleon', 'skip', 'sm', 'smp', 'smq', 
               'spld', 'sst', 'Start-Music', 'stop', 'vlc', 'vlcback', 'vlcf', 'vlclyrics', 
               'vlcmedia', 'vlcmute', 'vlcnext', 'vlcpause', 'vlcplay', 'vlcprev', 
               'vlcrepeat', 'vlcunmute', 'wake-monitor', 'x'

# DSC resources to export from this module
# DscResourcesToExport = @()

# List of all modules packaged with this module
ModuleList = @('GenXdev.Console')

# List of all files packaged with this module
FileList = 'GenXdev.Console.psd1', 'GenXdev.Console.psm1', 
               'GenXdev.Console.Spotify.psm1', 'GenXdev.Console.Vlc.psm1', 'LICENSE', 
               'license.txt', 'powershell.jpg', 'README.md', 
               'Tests\GenXdev.Console.Vlc\Open-VlcMediaPlayer.Tests.ps1', 
               'Tests\GenXdev.Console.Vlc\Open-VlcMediaPlayerLyrics.Tests.ps1', 
               'Tests\GenXdev.Console.Vlc\Start-VlcMediaPlayerNextInPlaylist.Tests.ps1', 
               'Tests\GenXdev.Console.Vlc\Start-VlcMediaPlayerPreviousInPlaylist.Tests.ps1', 
               'Tests\GenXdev.Console.Vlc\Switch-VlcMediaPlayerMute.Tests.ps1', 
               'Tests\GenXdev.Console.Vlc\Switch-VLCMediaPlayerPaused.Tests.ps1', 
               'Tests\GenXdev.Console.Vlc\Switch-VlcMediaPlayerRepeat.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Add-SpotifyNewPlaylist.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Add-SpotifyTracksToLiked.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Add-SpotifyTracksToPlaylist.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Connect-SpotifyApiToken.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Get-SpotifyActiveDevice.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Get-SpotifyApiToken.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Get-SpotifyCurrentlyPlaying.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Get-SpotifyDevice.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Get-SpotifyLikedTrack.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Get-SpotifyLyrics.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Get-SpotifyPlaylistIdsByName.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Get-SpotifyPlaylistTrack.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Get-SpotifyTrackAudioFeatures.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Get-SpotifyTrackById.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Get-SpotifyUserPlaylists.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Move-SpotifyLikedTracksToPlaylist.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Remove-SpotifyTracksFromLiked.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Remove-SpotifyTracksFromPlaylist.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Search-Spotify.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Search-SpotifyAndEnqueue.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Search-SpotifyAndPlay.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Set-SpotifyActiveDevice.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Set-SpotifyApiToken.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Set-SpotifyNext.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Set-SpotifyPause.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Set-SpotifyPlaylistDetails.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Set-SpotifyPlaylistOrder.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Set-SpotifyPrevious.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Set-SpotifyRepeatContext.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Set-SpotifyRepeatOff.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Set-SpotifyRepeatSong.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Set-SpotifyShuffleOff.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Set-SpotifyShuffleOn.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Set-SpotifyStart.Tests.ps1', 
               'Tests\GenXdev.Console.Spotify\Set-SpotifyStop.Tests.ps1', 
               'Tests\GenXdev.Console\Enable-Screensaver.Tests.ps1', 
               'Tests\GenXdev.Console\Get-IsSpeaking.Tests.ps1', 
               'Tests\GenXdev.Console\New-MicrosoftShellTab.Tests.ps1', 
               'Tests\GenXdev.Console\Now.Tests.ps1', 
               'Tests\GenXdev.Console\Open-MediaFile.Tests.ps1', 
               'Tests\GenXdev.Console\SayDate.Tests.ps1', 
               'Tests\GenXdev.Console\SayTime.Tests.ps1', 
               'Tests\GenXdev.Console\Set-LocationParent.Tests.ps1', 
               'Tests\GenXdev.Console\Set-LocationParent2.Tests.ps1', 
               'Tests\GenXdev.Console\Set-LocationParent3.Tests.ps1', 
               'Tests\GenXdev.Console\Set-LocationParent4.Tests.ps1', 
               'Tests\GenXdev.Console\Set-LocationParent5.Tests.ps1', 
               'Tests\GenXdev.Console\Set-MonitorPowerOff.Tests.ps1', 
               'Tests\GenXdev.Console\Set-MonitorPowerOn.Tests.ps1', 
               'Tests\GenXdev.Console\Set-VLCPlayerFocused.Tests.ps1', 
               'Tests\GenXdev.Console\Start-TextToSpeech.Tests.ps1', 
               'Tests\GenXdev.Console\Stop-TextToSpeech.Tests.ps1', 
               'Tests\GenXdev.Console\UtcNow.Tests.ps1', 
               'Functions\GenXdev.Console.Vlc\Open-VlcMediaPlayer.ps1', 
               'Functions\GenXdev.Console.Vlc\Open-VlcMediaPlayerLyrics.ps1', 
               'Functions\GenXdev.Console.Vlc\Start-VlcMediaPlayerNextInPlaylist.ps1', 
               'Functions\GenXdev.Console.Vlc\Start-VlcMediaPlayerPreviousInPlaylist.ps1', 
               'Functions\GenXdev.Console.Vlc\Switch-VlcMediaPlayerMute.ps1', 
               'Functions\GenXdev.Console.Vlc\Switch-VLCMediaPlayerPaused.ps1', 
               'Functions\GenXdev.Console.Vlc\Switch-VlcMediaPlayerRepeat.ps1', 
               'Functions\GenXdev.Console.Spotify\Add-SpotifyNewPlaylist.ps1', 
               'Functions\GenXdev.Console.Spotify\Add-SpotifyTracksToLiked.ps1', 
               'Functions\GenXdev.Console.Spotify\Add-SpotifyTracksToPlaylist.ps1', 
               'Functions\GenXdev.Console.Spotify\Connect-SpotifyApiToken.ps1', 
               'Functions\GenXdev.Console.Spotify\Get-SpotifyActiveDevice.ps1', 
               'Functions\GenXdev.Console.Spotify\Get-SpotifyApiToken.ps1', 
               'Functions\GenXdev.Console.Spotify\Get-SpotifyCurrentlyPlaying.ps1', 
               'Functions\GenXdev.Console.Spotify\Get-SpotifyDevice.ps1', 
               'Functions\GenXdev.Console.Spotify\Get-SpotifyLikedTrack.ps1', 
               'Functions\GenXdev.Console.Spotify\Get-SpotifyLyrics.ps1', 
               'Functions\GenXdev.Console.Spotify\Get-SpotifyPlaylistIdsByName.ps1', 
               'Functions\GenXdev.Console.Spotify\Get-SpotifyPlaylistTrack.ps1', 
               'Functions\GenXdev.Console.Spotify\Get-SpotifyTrackAudioFeatures.ps1', 
               'Functions\GenXdev.Console.Spotify\Get-SpotifyTrackById.ps1', 
               'Functions\GenXdev.Console.Spotify\Get-SpotifyUserPlaylists.ps1', 
               'Functions\GenXdev.Console.Spotify\Move-SpotifyLikedTracksToPlaylist.ps1', 
               'Functions\GenXdev.Console.Spotify\Remove-SpotifyTracksFromLiked.ps1', 
               'Functions\GenXdev.Console.Spotify\Remove-SpotifyTracksFromPlaylist.ps1', 
               'Functions\GenXdev.Console.Spotify\Search-Spotify.ps1', 
               'Functions\GenXdev.Console.Spotify\Search-SpotifyAndEnqueue.ps1', 
               'Functions\GenXdev.Console.Spotify\Search-SpotifyAndPlay.ps1', 
               'Functions\GenXdev.Console.Spotify\Set-SpotifyActiveDevice.ps1', 
               'Functions\GenXdev.Console.Spotify\Set-SpotifyApiToken.ps1', 
               'Functions\GenXdev.Console.Spotify\Set-SpotifyNext.ps1', 
               'Functions\GenXdev.Console.Spotify\Set-SpotifyPause.ps1', 
               'Functions\GenXdev.Console.Spotify\Set-SpotifyPlaylistDetails.ps1', 
               'Functions\GenXdev.Console.Spotify\Set-SpotifyPlaylistOrder.ps1', 
               'Functions\GenXdev.Console.Spotify\Set-SpotifyPrevious.ps1', 
               'Functions\GenXdev.Console.Spotify\Set-SpotifyRepeatContext.ps1', 
               'Functions\GenXdev.Console.Spotify\Set-SpotifyRepeatOff.ps1', 
               'Functions\GenXdev.Console.Spotify\Set-SpotifyRepeatSong.ps1', 
               'Functions\GenXdev.Console.Spotify\Set-SpotifyShuffleOff.ps1', 
               'Functions\GenXdev.Console.Spotify\Set-SpotifyShuffleOn.ps1', 
               'Functions\GenXdev.Console.Spotify\Set-SpotifyStart.ps1', 
               'Functions\GenXdev.Console.Spotify\Set-SpotifyStop.ps1', 
               'Functions\GenXdev.Console\Enable-Screensaver.ps1', 
               'Functions\GenXdev.Console\Get-IsSpeaking.ps1', 
               'Functions\GenXdev.Console\New-MicrosoftShellTab.ps1', 
               'Functions\GenXdev.Console\Now.ps1', 
               'Functions\GenXdev.Console\Open-MediaFile.ps1', 
               'Functions\GenXdev.Console\SayDate.ps1', 
               'Functions\GenXdev.Console\SayTime.ps1', 
               'Functions\GenXdev.Console\Set-LocationParent.ps1', 
               'Functions\GenXdev.Console\Set-LocationParent2.ps1', 
               'Functions\GenXdev.Console\Set-LocationParent3.ps1', 
               'Functions\GenXdev.Console\Set-LocationParent4.ps1', 
               'Functions\GenXdev.Console\Set-LocationParent5.ps1', 
               'Functions\GenXdev.Console\Set-MonitorPowerOff.ps1', 
               'Functions\GenXdev.Console\Set-MonitorPowerOn.ps1', 
               'Functions\GenXdev.Console\Set-VLCPlayerFocused.ps1', 
               'Functions\GenXdev.Console\Start-TextToSpeech.ps1', 
               'Functions\GenXdev.Console\Stop-TextToSpeech.ps1', 
               'Functions\GenXdev.Console\UtcNow.ps1'

# Private data to pass to the module specified in RootModule/ModuleToProcess. This may also contain a PSData hashtable with additional module metadata used by PowerShell.
PrivateData = @{

    PSData = @{

        # Tags applied to this module. These help with module discovery in online galleries.
        Tags = 'Console','Shell','GenXdev'

        # A URL to the license for this module.
        LicenseUri = 'https://raw.githubusercontent.com/genXdev/GenXdev.Console/main/LICENSE'

        # A URL to the main website for this project.
        ProjectUri = 'https://powershell.genxdev.net/#GenXdev.Console'

        # A URL to an icon representing this module.
        IconUri = 'https://genxdev.net/favicon.ico'

        # ReleaseNotes of this module
        # ReleaseNotes = ''

        # Prerelease string of this module
        # Prerelease = ''

        # Flag to indicate whether the module requires explicit user acceptance for install/update/save
        # RequireLicenseAcceptance = $false

        # External dependent modules of this module
        # ExternalModuleDependencies = @()

    } # End of PSData hashtable

 } # End of PrivateData hashtable

# HelpInfo URI of this module
# HelpInfoURI = ''

# Default prefix for commands exported from this module. Override the default prefix using Import-Module -Prefix.
# DefaultCommandPrefix = ''

}


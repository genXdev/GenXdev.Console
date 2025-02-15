################################################################################
<#
.SYNOPSIS
Removes tracks from the user's Spotify Library (Liked Songs).

.DESCRIPTION
Removes one or more tracks from the user's Spotify Liked Songs collection.
If no track ID is provided, removes the currently playing track.

.PARAMETER TrackId
One or more Spotify track IDs to remove from the Liked Songs collection.
If omitted, the currently playing track will be removed.

.EXAMPLE
Remove-SpotifyTracksFromLiked -TrackId "1234567890abcdef"

.EXAMPLE
dislike "1234567890abcdef"
#>
function Remove-SpotifyTracksFromLiked {

    [CmdletBinding()]
    [Alias("dislike")]
    param(
        ########################################################################
        [Alias("Id")]
        [parameter(
            Mandatory = $false,
            Position = 0,
            ValueFromPipeline,
            ValueFromPipelineByPropertyName,
            HelpMessage = "The Spotify track IDs to remove from Liked Songs"
        )]
        [string[]] $TrackId = @()
        ########################################################################
    )

    begin {

        # retrieve the spotify api access token for authentication
        $apiToken = Get-SpotifyApiToken
    }

    process {

        # if no track ids were provided, handle currently playing track
        if ($TrackId.Length -eq 0) {

            # get information about the currently playing track
            $CurrentlyPlaying = Get-SpotifyCurrentlyPlaying

            # verify there is a track currently playing
            if ($null -eq $CurrentlyPlaying -or
                $CurrentlyPlaying.CurrentlyPlayingType -ne "track") {

                Write-Warning "There are no tracks playing at this time"
                return
            }

            Write-Verbose "Removing currently playing track from Liked Songs"

            # recursively call this function with the current track's id
            Remove-SpotifyTracksFromLiked -TrackId ($CurrentlyPlaying.Item.Id)

            # return the track that was removed
            $CurrentlyPlaying.Item
        }
        else {

            Write-Verbose "Removing $($TrackId.Count) track(s) from Liked Songs"

            # remove the specified tracks from liked songs using the spotify api
            [GenXdev.Helpers.Spotify]::RemoveFromLiked($apiToken, $TrackId)
        }
    }

    end {
    }
}
################################################################################

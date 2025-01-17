###############################################################################

<#
.SYNOPSIS
Will stop the text-to-speech engine from saying anything else

.DESCRIPTION
Will stop the text-to-speech engine from saying anything else

.PARAMETER None
This function does not take any parameters.

.EXAMPLE
PS C:\> say "Good morning"; say "Good evening"; Stop-TextToSpeech; # -> "G.."

.NOTES
See also: Start-TextToSpeech -> say, and Skip-TextToSpeech -> sstSkip
#>
function Stop-TextToSpeech {

    [CmdletBinding()]
    [Alias("sst")]

    param()

    try {
        [GenXdev.Helpers.Misc]::Speech.SpeakAsyncCancelAll() | Out-Null
        [GenXdev.Helpers.Misc]::SpeechCustomized.SpeakAsyncCancelAll() | Out-Null
    }
    catch {

    }
}

###############################################################################
<#
.SYNOPSIS
Returns true if the text-to-speech engine is speaking

.DESCRIPTION
Returns true if the text-to-speech engine is speaking
#>
function Get-IsSpeaking {

    [CmdletBinding()]
    [Alias("iss")]

    param()

    try {
        return (
            ([GenXdev.Helpers.Misc]::Speech.State -eq "Speaking") -or
            ([GenXdev.Helpers.Misc]::SpeechCustomized.State -eq "Speaking")
        )
    }
    catch {
        return $false
    }
}

<#
.SYNOPSIS
Will use the text-to-speech engine to speak out text

.DESCRIPTION
Will use the text-to-speech engine to speak out text

.PARAMETER lines
The text to speak.

.PARAMETER Language
The language to use.

.PARAMETER PassThru
Will pass the text through the pipeline.

.PARAMETER wait
Will wait until all text is spoken.

.EXAMPLE
PS C:\> say "Good morning"

PS C:\> "Good morning" | Start-TextToSpeech

.NOTES
See also: Stop-TextToSpeech -> sst, and Skip-TextToSpeech -> sstSkip
#>
function Start-TextToSpeech {

    [CmdletBinding()]
    [Alias("say")]

    param(
        [Parameter(Mandatory, Position = 0, ValueFromPipeline, ValueFromRemainingArguments, ParameterSetName = "strings")]
        [string[]] $lines,

        [Parameter(Mandatory = $false, HelpMessage = "The language locale id to use, e.g. 'en-US'")]
        [string] $Locale = $null,

        [Parameter(Mandatory = $False)]
        [string] $VoiceName = $null,

        [Parameter(Mandatory = $False)]
        [switch] $PassThru,

        [Parameter(Mandatory = $False)]
        [switch] $wait
    )

    Begin {

    }

    Process {

        @($lines) | ForEach-Object -Process {

            $txt = $PSItem

            if ($txt -isnot [string]) {

                $txt = "$txt"
            }

            try {

                if ($PassThru -eq $true) {

                    $txt
                }

                $txt = $txt.Replace("`r", ' ').Replace('`n', ' ').Replace('`t', ' ');

                if ([string]::IsNullOrWhiteSpace($Locale)) {

                    if ([string]::IsNullOrWhiteSpace($VoiceName)) {

                        if ($wait -eq $true) {

                            [GenXdev.Helpers.Misc]::Speech.Speak($txt) | Out-Null;
                            return;
                        }
                        [GenXdev.Helpers.Misc]::Speech.SpeakAsync($txt) | Out-Null;

                        return;
                    }

                    try {
                        [GenXdev.Helpers.Misc]::SpeechCustomized.SelectVoice((([GenXdev.Helpers.Misc]::SpeechCustomized.GetInstalledVoices()) | Where-Object { if ([string]::IsNullOrWhiteSpace($VoiceName) -or ($PSItem.VoiceInfo.Name -like "* $VoiceName * ")) { $PSItem } } | Where-Object Name | Select-Object -First 1))
                    }
                    catch {
                        Write-Warning "Could not set voice with provided parameters, maybe no installation found of the voice with your selection parameters"
                    }
                    [GenXdev.Helpers.Misc]::SpeechCustomized.Speak($txt) | Out-Null;
                    return;
                }

                try {
                    [GenXdev.Helpers.Misc]::SpeechCustomized.SelectVoice((([GenXdev.Helpers.Misc]::SpeechCustomized.GetInstalledVoices($locale)) | Where-Object { if ([string]::IsNullOrWhiteSpace($VoiceName) -or ($PSItem.VoiceInfo.Name -like "* $VoiceName * ")) { $PSItem } } | Where-Object Name | Select-Object -First 1))
                }
                catch {
                    Write-Warning "Could not set voice with provided parameters, maybe no installation found of the voice with your selection parameters"
                }

                [GenXdev.Helpers.Misc]::SpeechCustomized.Speak($txt) | Out-Null;
                return;
            }
            catch {
                [System.Console]::WriteLine("Error: $($PSItem.Exception.Message)");
                [GenXdev.Helpers.Misc]::Speech.Speak($txt) | Out-Null;
            }
        }
    }
}

###############################################################################

<#
.SYNOPSIS
Retreives a list of all installed GenXdev modules and their Cmdlets and corresponding aliases

.DESCRIPTION
Retreives a list of all installed GenXdev modules and their Cmdlets and corresponding aliases

.PARAMETER Filter
Allows you to search for Cmdlets by providing searchstrings, with or without wildcards

.PARAMETER $ModuleName
Retreives all Cmdlets of provided modulename(s)

.EXAMPLE
PS d:\documents\PowerShell> cmds

#>
function Get-GenXDevCmdlets {

    [CmdletBinding()]

    param(
        [parameter(
            Mandatory = $false,
            Position = 0,
            ValueFromRemainingArguments
        )]
        [string] $Filter = "*",

        [parameter(
            ParameterSetName = "ModuleFilter",
            Mandatory = $false
        )]
        [string[]] $ModuleName = @("*")
    )

    if (!$Filter.Contains("*")) {

        $Filter = "*$Filter*"
    }

    $ModuleName = $ModuleName.Replace("GenXdev.", "")

    $results = Get-Module "GenXdev.$ModuleName" -All |  ForEach-Object -Process {

        $Module = $PSItem;

        $PSItem.ExportedCommands.Values | ForEach-Object -ErrorAction SilentlyContinue {

            if (($PSItem.Name -like $Filter) -and (($PSItem.Module.Name -eq $Module.Name) -or ($PSItem.Module.Name -like "$($Module.Name).*"))) {

                if ($PSItem.CommandType -eq "Function") {

                    $aliases = ((Get-Alias -Definition $PSItem.Name -ErrorAction SilentlyContinue | ForEach-Object Name) -Join ", ").Trim();

                    $nameAndAliases = ""

                    if ([string]::IsNullOrWhiteSpace($aliases) -eq $false) {

                        $nameAndAliases = "$($PSItem.Name) --> $aliases"
                    }
                    else {
                        $nameAndAliases = $PSItem.Name
                    }

                    $desc = "";
                    try {
                        $desc = (Get-Help $PSItem.Name -Detailed).Description.Text;

                        if ([string]::IsNullOrWhiteSpace($desc)) {

                            try {
                                $desc = (Select-String "#\s*DESCRIPTION\s+$($PSItem.Name):([^`r`n]*)" -input "$((Get-Command "$($PSItem.Name)").ScriptBlock)".Replace("`u{00A0}", " ") -AllMatches | ForEach-Object { $PSItem.matches.Groups[1].Value }).Trim();
                            }
                            catch {
                                # Write-Warning $PSItem.Exception
                                $desc = "";
                            }
                        }
                    }
                    catch {
                        $desc = "";
                    }

                    $result = @{
                        NameAndAliases = $nameAndAliases;
                        Name           = $PSItem.Name;
                        Aliases        = $aliases;
                        Description    = $desc;
                        ModuleName     = $PSItem.Module.Name;
                        Position       = "$($PSItem.Module.Name)_$($Module.Definition.IndexOf("function $($PSItem.Name)").ToString().PadLeft(10,"0"))"
                    }

                    $result
                }
            }
        }
    }

    $results | Sort-Object -Property Position
}

###############################################################################

<#
.SYNOPSIS
Shows a list of all installed GenXdev modules and their Cmdlets and corresponding aliases

.DESCRIPTION
Shows a list of all installed GenXdev modules and their Cmdlets and corresponding aliases

.PARAMETER Filter
Allows you to search for Cmdlets by providing searchstrings, with or without wildcards

.EXAMPLE
PS d:\documents\PowerShell> cmds

#>
function Show-GenXDevCmdlets {

    [CmdletBinding()]
    [Alias("cmds")]

    param(
        [parameter(
            Mandatory = $false,
            Position = 0,
            ValueFromRemainingArguments
        )]
        [string] $Filter = "*",

        [Alias("m")]
        [parameter(
            ParameterSetName = "ModuleFilter",
            Mandatory = $false
        )]
        [string[]] $ModuleName = @("*"),

        [parameter(
            Mandatory = $false
        )]
        [switch] $Online
    )


    if (!$Filter.Contains("*")) {

        $Filter = "*$Filter*"
    }

    $ModuleName = $ModuleName.Replace("GenXdev.", "")

    $modules = Get-Module "GenXdev.$ModuleName" -All;
    $modules | ForEach-Object -Process {

        if ($Online -eq $true -and ($PSItem.Name -notin @("GenXdev.Local", "GenXdev.Git", "GenXdev.*.*"))) {

            Open-Webbrowser -Url "https://github.com/genXdev/$($PSItem.Name)/blob/main/README.md#cmdlet-index" -Monitor -1
            return;
        }
        else {
            if ($Online -eq $true) {

                return;
            }
            $module = $PSItem
            $name = $module.Name.SubString("GenXdev.".Length);
            $first = $true;
            [System.Collections.Generic.List[string]] $commandFilter = [System.Collections.Generic.List[string]]::new();

            if ($name.Contains(".") -eq $false) {

                foreach ($otherModule in $modules) {

                    if ($otherModule.Name -like "GenXdev.$name.*") {

                        foreach ($filteredCommand in $otherModule.ExportedCommands.Values) {

                            if ($commandFilter.Contains($filteredCommand.Name)) { continue; }

                            $commandFilter.Add($filteredCommand.Name);
                        }
                    }
                }
            }

            $result = [string]::Join(", ", @(
                    $PSItem.ExportedCommands.Values | ForEach-Object -ErrorAction SilentlyContinue {

                        $exportedCommand = $PSItem;

                        if ($exportedCommand.Name -in $commandFilter) { return }

                        if ($exportedCommand.Name -like $Filter) {

                            if ($first) {

                                "`r`n" + $name + ":" | Write-Host -ForegroundColor "Yellow"
                            }

                            $first = $false;

                            if ($exportedCommand.CommandType -eq "Function") {

                                $aliases = ((Get-Alias -Definition $exportedCommand.Name -ErrorAction SilentlyContinue | ForEach-Object Name) -Join ", ").Trim();

                                if ([string]::IsNullOrWhiteSpace($aliases) -eq $false) {

                                    "$($exportedCommand.Name) --> $aliases"
                                }
                                else {
                                    $exportedCommand.Name
                                }
                            }
                        }
                    }
                )
            ).Trim("`r`n ".ToCharArray())

            if ([string]::IsNullOrWhiteSpace($result) -eq $false) {

                $result;
            }
        }
    }
}

###############################################################################

<#
.SYNOPSIS
Opens a new Windows Terminal tab

.DESCRIPTION
Opens a new Windows Terminal tab and closes current by default

.PARAMETER DontCloseThisTab
Keeps current tab open
#>
function New-MicrosoftShellTab {

    [CmdletBinding()]
    [Alias("x")]

    param(

        [switch] $DontCloseThisTab
    )
    Begin {

        try {
            (Get-PowershellMainWindow).SetForeground();

            $helper = New-Object -ComObject WScript.Shell;
            $helper.sendKeys("^+t");

            if ($DontCloseThisTab -ne $true) {
                Start-Sleep 3
                exit
            }
        }
        catch {

        }
    }
}

###############################################################################

<#
.SYNOPSIS
Will extract all archive files (zip, 7z, tar, etc) found in current directory and then DELETE them

.DESCRIPTION
Will extract all archive files (zip, 7z, tar, etc) found in current directory and then DELETE them.
Each archive file is extracted into their own directory with the same name as the file

.EXAMPLE
PS D:\downloads> Invoke-Fasti

.NOTES
You need 7z installed
#>
function Invoke-Fasti {

    [CmdletBinding()]
    [Alias("Fasti")]

    param()

    Get-ChildItem @("*.7z", "*.xz", "*.bzip2", "*.gzip", "*.tar", "*.zip", "*.wim", "*.ar", "*.arj", "*.cab", "*.chm", "*.cpio", "*.cramfs", "*.dmg", "*.ext", "*.fat", "*.gpt", "*.hfs", "*.ihex", "*.iso", "*.lzh", "*.lzma", "*.mbr", "*.msi", "*.nsis", "*.ntfs", "*.qcow2", "*.rar", "*.rpm", "*.squashfs", "*.udf", "*.uefi", "*.vdi", "*.vhd", "*.vmdk", "*.wim", "*.xar", "*.z") -File -ErrorAction SilentlyContinue  | ForEach-Object {

        $7z = "7z"
        $zip = $PSItem.fullname;
        $n = [system.IO.Path]::GetFileNameWithoutExtension($zip);
        $p = [System.IO.Path]::GetDirectoryName($zip);
        $r = [system.Io.Path]::Combine($p, $n);

        if ([System.IO.Directory]::exists($r) -eq $false) {

            [System.IO.Directory]::CreateDirectory($r)
        }

        if ((Get-Command $7z -ErrorAction SilentlyContinue).Length -eq 0) {

            $7z = "C:\Program Files\7-Zip\7z.exe";

            if (![IO.File]::Exists($7z)) {

                if ((Get-Command winget -ErrorAction SilentlyContinue).Length -eq 0) {

                    throw "You need to install 7zip or winget first";
                }

                winget install 7zip

                if (![IO.File]::Exists($7z)) {

                    throw "You need to install 7-zip";
                }
            }
        }

        & $7z x -y "-o$r" $zip;

        if ($?) {

            try {
                Remove-Item "$zip" -Force -ErrorAction silentlycontinue
            }
            catch {
            }
        }
    }
}

###############################################################################

<#
.SYNOPSIS
Provides the .. alias to go one directory up

.DESCRIPTION
Provides the .. alias to go one directory up
#>
function Set-LocationParent {

    [CmdletBinding()]
    [Alias("..")]

    param (

    )

    Set-Location ..
    Get-ChildItem
}
###############################################################################

<#
.SYNOPSIS
Provides the ... alias to go two directory up

.DESCRIPTION
Provides the ... alias to go two directory up
#>
function Set-LocationParent2 {

    [CmdletBinding()]
    [Alias("...")]

    param (

    )

    Set-Location ..
    Set-Location ..
    Get-ChildItem
}
###############################################################################

<#
.SYNOPSIS
Provides the .... alias to go three directory up

.DESCRIPTION
Provides the .... alias to go three directory up
#>
function Set-LocationParent3 {

    [CmdletBinding()]
    [Alias("....")]

    param (

    )

    Set-Location ..
    Set-Location ..
    Set-Location ..
    Get-ChildItem
}
###############################################################################

<#
.SYNOPSIS
Provides the ..... alias to go four directory up

.DESCRIPTION
Provides the ..... alias to go four directory up
#>
function Set-LocationParent4 {

    [CmdletBinding()]
    [Alias(".....")]

    param (

    )

    Set-Location ..
    Set-Location ..
    Set-Location ..
    Set-Location ..
    Get-ChildItem
}
###############################################################################

<#
.SYNOPSIS
Provides the ...... alias to go five directory up

.DESCRIPTION
Provides the ...... alias to go five directory up
#>
function Set-LocationParent5 {

    [CmdletBinding()]
    [Alias("......")]

    param (

    )

    Set-Location ..
    Set-Location ..
    Set-Location ..
    Set-Location ..
    Set-Location ..
    Get-ChildItem
}


###############################################################################

<#
.SYNOPSIS
Starts the configured Windows screensaver

.DESCRIPTION
Starts the configured Windows screensaver

#>
function Enable-Screensaver {

    [CmdletBinding()]

    param()

    & "$ENV:SystemRoot\system32\scrnsave.scr" / s
}

###############################################################################

<#
.SYNOPSIS
Turns the monitor power off

.DESCRIPTION
Turns the monitor power off
#>
function Set-MonitorPowerOff {

    [CmdletBinding()]

    param()

    Start-Sleep 2

    [GenXdev.Helpers.WindowObj]::SleepMonitor();
}

###############################################################################

<#
.SYNOPSIS
Turns the monitor power on

.DESCRIPTION
Turns the monitor power on
#>
function Set-MonitorPowerOn {

    [CmdletBinding()]

    param()

    [GenXdev.Helpers.WindowObj]::WakeMonitor();
}

###############################################################################

<#
.SYNOPSIS
Shows a short alphabetical list of all PowerShell verbs

.DESCRIPTION
Shows a short alphabetical list of all PowerShell verbs
#>
function Show-Verb {

    [CmdletBinding()]

    param(

        [parameter(
            Position = 0,
            ValueFromPipeline,
            ValueFromPipelineByPropertyName,
            Mandatory = $False
        )] [string[]] $Verb = @()
    )

    process {

        if ($Verb.Length -eq 0) {

            $verbs = Get-Verb
        }
        else {
            $verbs = Get-Verb | ForEach-Object -ErrorAction SilentlyContinue {

                $existingVerb = $PSItem;

                foreach ($verb in $Verb) {

                    if ($existingVerb.Verb -like $verb) {

                        $existingVerb
                    }
                }
            }
        }

        ($verbs | Sort-Object { $PSItem.Verb } | ForEach-Object Verb -ErrorAction SilentlyContinue) -Join ", "
    }
}

###############################################################################

function Get-GenXDevModuleInfo {

    [CmdletBinding()]

    param(
        [Alias("Name", "Module")]
        [parameter(
            Mandatory = $false,
            Position = 0,
            ValueFromPipeline,
            ValueFromPipelineByPropertyName
        )] [string[]] $ModuleName = @()
    )

    begin {
        [System.IO.FileSystemInfo[]] $AllModules = @(Get-GenXDevModules);

        if ("GenXdev.Local" -in $ModuleName) {

            throw "Can not publish GenXdev.Local"
        }

    }

    process {

        if ($ModuleName.Count -gt 0) {

            foreach ($currentModuleName in $ModuleName) {

                foreach ($currentModule in $AllModules) {

                    if ($currentModule.Parent.Name -ne $currentModuleName) {

                        continue;
                    }

                    $configPath = "$($currentModule.FullName)\$currentModuleName.psd1"
                    $configText = [IO.File]::ReadAllText($configPath)

                    $config = Invoke-Expression -Command ($configText)

                    $currentVersion = [Version]::new($config.ModuleVersion);
                    $newVersion = [Version]::new($currentVersion.Major, $currentVersion.Minor + 1, $currentVersion.Build).ToString();

                    @{
                        ModulePath     = $currentModule.FullName;
                        ModuleName     = $currentModuleName;
                        ConfigPath     = $configPath;
                        ConfigText     = $configText
                        Config         = $config;
                        CurrentVersion = $currentVersion;
                        NewVersion     = $newVersion;
                        HasREADME      = [IO.File]::Exists("$($currentModule.FullName)\README.md");
                        HasLICENSE     = [IO.File]::Exists("$($currentModule.FullName)\LICENSE") -and [IO.File]::Exists("$($currentModule.FullName)\license.txt");
                    }
                }
            }
            return;
        }

        foreach ($currentModule in $AllModules) {

            $currentModuleName = $currentModule.Parent.Name;
            $configPath = "$($currentModule.FullName)\$currentModuleName.psd1"
            $configText = [IO.File]::ReadAllText($configPath)

            $config = Invoke-Expression -Command ($configText)

            $currentVersion = [Version]::new($config.ModuleVersion);
            $newVersion = [Version]::new($currentVersion.Major, $currentVersion.Minor + 1, $currentVersion.Build).ToString();

            @{
                ModulePath     = $currentModule.FullName;
                ModuleName     = $currentModuleName;
                ConfigPath     = $configPath;
                ConfigText     = $configText
                Config         = $config;
                CurrentVersion = $currentVersion;
                NewVersion     = $newVersion;
                HasREADME      = [IO.File]::Exists("$($currentModule.FullName)\README.md");
                HasLICENSE     = [IO.File]::Exists("$($currentModule.FullName)\LICENSE") -and [IO.File]::Exists("$($currentModule.FullName)\license.txt");
            }
        }
    }
}

###############################################################################

function SayTime() {

    $d = Get-Date
    $h = $d.Hour
    $m = $d.Minute
    say ("The time is " + $h.ToString("0") + " hours and " + $m.ToString("0") + " minutes")
}

function SayDate() {

    say ("It is " + [DateTime]::Now.ToString("dddd, MMMM d yyyy"))
}


###############################################################################

function Now() {

    [CmdletBinding()]
    [OutputType("System.DateTime")]

    param()

    return [DateTime]::Now
}

###############################################################################

function UtcNow() {

    [CmdletBinding()]
    [OutputType("System.DateTime")]

    param()

    return [DateTime]::UtcNow
}

################################################################################
<#
.SYNOPSIS
Selects media files and plays them in VLC player

.DESCRIPTION
Selects media files and plays them in VLC player

.PARAMETER DirectoryPath
Specify the directory path containing media files.
Default is the current directory.

.PARAMETER OnlyVideos
Only include video files in the playlist.

.PARAMETER OnlyAudio
Only include audio files in the playlist.

.PARAMETER OnlyPictures
Only include pictures in the playlist.

.PARAMETER IncludeVideos
Also include videos in the playlist.

.PARAMETER IncludeAudio
Also include audio files in the playlist.

.PARAMETER IncludePictures
Also include pictures in the playlist.

.EXAMPLE
PS C:\users\MyName\Videos> media

.EXAMPLE
PS C:\> media ~\Pictures -OnlyPictures

.EXAMPLE
PS C:\> media ~\
#>
function Invoke-VLCPlayer {

    [CmdletBinding()]
    [alias("vlc", "media")]

    param(

        [Parameter(Mandatory = $false, Position = 0, HelpMessage = "Specify the directory path containing media files.")]
        [string]$DirectoryPath = ".\*",

        [Parameter(Mandatory = $false, Position = 1, HelpMessage = "Specify the keywords to search for in the file meta data")]
        [string[]]$keywords = @(),

        [Parameter(Mandatory = $false, Position = 2, HelpMessage = "Specify the search mask for files.")]
        [string]$SearchMask = "*",

        [Parameter(Mandatory = $false, HelpMessage = "Only include video files in the playlist.")]
        [switch]$OnlyVideos,

        [Parameter(Mandatory = $false, HelpMessage = "Only include audio files in the playlist.")]
        [switch]$OnlyAudio,

        [Parameter(Mandatory = $false, HelpMessage = "Only include pictures in the playlist.")]
        [switch]$OnlyPictures,

        [Parameter(Mandatory = $false, HelpMessage = "Also include videos in the playlist.")]
        [switch]$IncludeVideos,

        [Parameter(Mandatory = $false, HelpMessage = "Also include audio files in the playlist.")]
        [switch]$IncludeAudio,

        [Parameter(Mandatory = $false, HelpMessage = "Also include pictures in the playlist.")]
        [switch]$IncludePictures
    )

    # Get the list of files in the directory and subdirectories
    $SearchMask = "$((Expand-Path $DirectoryPath))\$SearchMask"
    $files = Find-Item -SearchMask $SearchMask -PassThru | Sort-Object -Property FullName

    # Filter files to only include those with extensions that VLC player can play
    $videoFiles = @(".mp4", ".avi", ".mkv", ".mov", ".wmv");
    $audioFiles = @(".mp3", ".flac", ".wav", ".midi", ".mid", ".au", ".aiff", ".aac", ".m4a", ".ogg", ".wma", ".ra", ".ram", ".rm", ".rmm");
    $pictureFiles = @(".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".tif");

    $validExtensions = $OnlyVideos ? $videoFiles : ($OnlyAudio ? $audioFiles : ($OnlyPictures ? $pictureFiles : ($videoFiles + $audioFiles + $pictureFiles)) );

    if ($IncludeVideos) {

        $validExtensions += $videoFiles
    }
    if ($IncludeAudio) {

        $validExtensions += $audioFiles
    }
    if ($IncludePictures) {

        $validExtensions += $pictureFiles
    }

    $files = $files | Where-Object {

        try {
            if (-not ($validExtensions -contains $PSItem.Extension.ToLower())) {

                return $false;
            }

            if ($keywords.Length -gt 0) {

                $srtSearchMask = [io.Path]::Combine([IO.Path]::GetDirectoryName($PSItem.FullName), [IO.Path]::GetFileNameWithoutExtension($PSItem.FullName) + "*.srt");

                Get-ChildItem $srtSearchMask -File -ErrorAction SilentlyContinue | ForEach-Object {

                    $srt = [IO.File]::ReadAllText($PSItem.FullName);

                    foreach ($keyword in $keywords) {

                        if ($srt -like "*$keyword*") {

                            return $true;
                        }
                    }
                }

                if ([IO.File]::Exists("$($PSItem.FullName):description.json")) {

                    $description = [IO.File]::ReadAllText("$($PSItem.FullName):description.json");

                    foreach ($keyword in $keywords) {

                        if ($description -like "*$keyword*") {

                            return $true;
                        }
                    }
                }

                return $false;
            }

            return $true;
        }
        catch {

            return $false;
        }
    }

    if ($files.Length -eq 0) {

        Write-Host "No media files found in the specified directory."
        return
    }

    [string]$PlaylistPath = [System.IO.Path]::ChangeExtension([System.IO.Path]::GetTempFileName(), ".m3u")

    # Generate the .xspf formatted XML content
    $m3uContent = "#EXTM3U`r`n";

    foreach ($file in $files) {

        $m3uContent += "#EXTINF:-1, $($file.Name.Replace("_", " ").Replace(".", " ").Replace("  ", " "))`r`n$(($file.FullName))`r`n";
    }

    # Save the playlist to the specified file
    $m3uContent | Out-File -FilePath $PlaylistPath -Encoding utf8 -Force

    "$PlaylistPath" | Set-Clipboard

    # check if VLC player is installed
    if (-not (Test-Path "C:\Program Files\VideoLAN\VLC\vlc.exe")) {

        # check if powershell module 'Microsoft.WinGet.Client' is installed
        if (-not (Get-Module -ListAvailable -Name 'Microsoft.WinGet.Client')) {

            # install module 'Microsoft.WinGet.Client'
            Install-Module -Name 'Microsoft.WinGet.Client' -Force -Scope CurrentUser -AllowClobber -SkipPublisherCheck -AcceptLicense

            # import module
            Import-Module -Name 'Microsoft.WinGet.Client'
        }

        # install VLC player using 'Microsoft.WinGet.Client'
        Install-WinGetPackage -Name 'VLC media player' -Scope System -Force -AcceptLicense
    }

    # Start VLC player with the playlist file
    Get-Process vlc -ErrorAction SilentlyContinue | Stop-Process -Force
    $p = Start-Process -FilePath "C:\Program Files\VideoLAN\VLC\vlc.exe" -ArgumentList @($PlaylistPath) -PassThru

    try {
        Write-Verbose "Launching VLC player with the playlist file: $PlaylistPath"

        [System.Threading.Thread]::Sleep(4000) | Out-Null

        Set-VLCPlayerFocused

        # Set fullscreen
        if ($p -and $p.MainWindowHandle -ge 0) {

            [System.Threading.Thread]::Sleep(1000) | Out-Null

            if (($Global:DefaultSecondaryMonitor -gt 0) -and ((Get-MonitorCount) -gt 1)) {

                Set-WindowPosition -Process $p -Fullscreen -Monitor -2
            }
            else {

                Set-WindowPosition -Process $p -Right
                Set-WindowPosition -Process (Get-PowershellMainWindowProcess) -Left
            }
        }
    }
    finally {
        Set-VLCPlayerFocused

        # send F11
        $helper = New-Object -ComObject WScript.Shell;
        $helper.sendKeys("F");

        Write-Verbose "Sending F"

        [System.Threading.Thread]::Sleep(1000) | Out-Null

    (Get-PowershellMainWindow).SetForeground();
    }
}

################################################################################

function Set-VLCPlayerFocused {

    [CmdletBinding()]
    [alias("showvlc", "vlcf", "fvlc")]
    param()

    try {
        Write-Verbose "Focussing VLC player"
        $w = Get-Window -ProcessName vlc
        $w.SetForeground();
    }
    catch {

        Write-Warning "VLC player is not running"
    }
}

################################################################################

Param(
    [Parameter(Mandatory, HelpMessage = "Provide a pack format number`n`nA list is available at https://minecraft.wiki/w/Pack_format")]
    $PackFormat,
    [Parameter(Mandatory, HelpMessage = "Subpath of .\Assets, which contains the screenshots.`ne.g. Clouds\Programmer Art")]
    $ScreenshotPath,
    [switch]$Show
)
# todo: allow decsription to be changed
# have variable default desciption
# change pack.mcmeta template to have $PackDescription


if ($PackFormat -match "^\d+$") {
    Write-Host $ScreenshotPath
    $Output = ".\Releases\pf$PackFormat\$ScreenshotPath"
    Write-Host $Output
    
    Copy-Item -Path ".\Assets\pack" -Destination $Output -Recurse -Force
    Copy-Item -Path ".\Assets\$ScreenshotPath\*.png" -Destination "$Output\assets\minecraft\textures\gui\title\background" -Force
    Copy-Item -Path ".\Assets\$ScreenshotPath\panorama_0.png" -Destination "$Output" -Force -PassThru |Rename-Item -NewName "pack.png" -Force

    # Change pack format
    $CWD = Get-Location
    $PackMcmeta = "$CWD\$Output\pack.mcmeta"
    $mcmeta = [System.IO.File]::ReadAllText($PackMcmeta).Replace('$PackFormat', "$PackFormat")
    #.Replace('$PackDescription', "$PackDescription")
    [System.IO.File]::WriteAllText($PackMcmeta, $mcmeta)

    # Archive
    Compress-Archive -Path "$Output\*" -Destination "$Output.zip" -Force
    Remove-Item -Path "$Output" -Force -Recurse
    
    # Show with explorer
    If ($Show) {
        Start-Process explorer.exe -ArgumentList "/Select, `"$CWD\$Output.zip`""
    }
    
} else {
    Write-Host "The pack format must be an integer-like string. i.e., matches \d+."
}
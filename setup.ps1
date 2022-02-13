# Setup.ps1 for MC-HEXColor-Gadient-Generator by Lingrottin
$datename = Get-Date -Format "yyyy.MMdd"
$filename = 'MC-HEXColor-Gadient-Generator-' + $datename + ".zip"
$release_name = "Release " + $datename
Add-Content -Path $env:GITHUB_ENV -Value "TagName=$datename"
Add-Content -Path $env:GITHUB_ENV -Value "ReleaseName=\"$release_name\""
Add-Content -Path $env:GITHUB_ENV -Value "ReleaseFileName=$filename"

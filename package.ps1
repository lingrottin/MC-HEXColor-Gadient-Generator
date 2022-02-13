# Package.ps1 for MC-HEXColor-Gadient-Generator by Lingrottin
$datename = Get-Date -Format "yyyy.MMdd"
$compress_arguments = @{
    Path = ".\bin\Release"
    CompressionLevel = "Fastest"
    DestinationPath = ".\MC-HEXColor-Gadient-Generator-" + $datename + ".zip"
}
Compress-Archive @compress_arguments

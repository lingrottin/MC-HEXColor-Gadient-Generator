# Hash.ps1 for MC-HEXColor-Gadient-Generator by Lingrottin
$releasename = Get-Date -Format "yyyy.MMdd"
$filename = "MC-HEXColor-Gadient-Generator-Release${releasename}.zip"

# generate hashes
$hash_sha256 = Get-FileHash -Path $filename -Algorithm SHA256
$hash_md5 = Get-FileHash -Path $filename -Algorithm MD5

# write into text
Write-Output "Format: Filename (Hash-Type) Hash-Value" >> sums.txt
$output = "$filename (SHA256) " + $hash_sha256.Hash
Write-Output -InputObject $output >> sums.txt
$output = "$filename (MD5) " + $hash_md5.Hash
Write-OutPut -InputObject $output >> sums.txt
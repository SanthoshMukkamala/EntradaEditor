# Rename the correct setup file to setup.exe,
# and delete the other one
if ($OctopusEnvironmentName -eq "QA") {
	Rename-Item setup-qa.exe setup.exe | Write-Host
	Rename-Item index-qa.html index.html | Write-Host
	Rename-Item Entrada.Editor.application-qa Entrada.Editor.application | Write-Host
	Remove-Item setup-prod.exe | Write-Host
	Remove-Item index-prod.html | Write-Host
	Remove-Item Entrada.Editor.application-prod | Write-Host
} elseif ($OctopusEnvironmentName -eq "Production") {
	Rename-Item setup-prod.exe setup.exe | Write-Host
	Rename-Item index-prod.html index.html | Write-Host
	Rename-Item Entrada.Editor.application-prod Entrada.Editor.application | Write-Host
	Remove-Item setup-qa.exe | Write-Host
	Remove-Item index-qa.html | Write-Host
	Remove-Item Entrada.Editor.application-qa | Write-Host
}

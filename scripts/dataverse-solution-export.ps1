pac auth create --name PORTALDEV --url https://pg-dataverse-dev.crm4.dynamics.com/
pac auth list
pac auth select --name PORTALDEV

$solutionName = "LetsMeet"
$SolutionFileName = "LetsMeet.zip"
$exportLocation = "..\Solutions"
$managedSolutionFolder = "LetsMeet_managed"
$unmanagedSolutionFolder = "LetsMeet"

Write-Output "Creating solution folder if not exists..."
If(!(test-path $exportLocation))
{
		New-Item -ItemType Directory -Force -Path $exportLocation
}

cls;

Write-Output "exporting managed customization file..."

pac solution export --path $exportLocation\$SolutionFileName --name $solutionName --managed true --include general

Write-Output "Extracting customization file and removing downloaded zip archive..."
pac solution unpack `
--zipfile $exportLocation\$SolutionFileName `
--folder $exportLocation\$managedSolutionFolder `
--packagetype Managed `
--allowDelete true `
--map ..\configs\solution-mappings.xml

Write-output "extraction process completed."

Write-Output "Deleting managed solution's file..."
Remove-Item $exportLocation\$solutionFileName
Write-Output "Operation completed."

Write-Output "exporting unmanaged customization file..."
pac solution export --path $exportLocation\$SolutionFileName --name $solutionName --managed false --include general

Write-Output "Extracting customization file and removing downloaded zip archive..."
pac solution unpack `
--zipfile $exportLocation\$SolutionFileName `
--folder $exportLocation\$unmanagedSolutionFolder `
--packagetype Unmanaged `
--allowDelete true `
--map ..\configs\solution-mappings.xml

Write-output "extraction process completed."

Write-Output "Deleting managed solution's file..."
Remove-Item $exportLocation\$solutionFileName
Write-Output "Operation completed."




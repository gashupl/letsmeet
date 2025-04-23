pac auth create --name PORTALDEV --url https://pg-dataverse-dev.crm4.dynamics.com
pac auth list
pac auth select --name PORTALDEV

$portalName = "Lets meet - letsmeetdev"

[array] $cmdOutput = pac pages list
$portal = $cmdOutput | Where-Object { $_ -match $portalName }

if($portal){
    $dataOutput = ($portal -replace '\s+', ' ').split()
    $websiteId = $dataOutput[2]
    Write-Host "website id: " $websiteId
    Write-Host "website id: " $websiteId
    Write-Host "Exporting portal configuration..."
    pac pages download --path '..\portal' --webSiteId $websiteId --overwrite true --modelVersion 2
    Write-Host "Portal configuration exported"
}
else{
    Write-Host "Portal not found"
}

param(
    [Parameter(Mandatory=$true)]
    [string]$eventsApiManagedIdentityId, 

    [Parameter(Mandatory=$true)]
    [string]$identityApiManagedIdentityId 
)

az deployment group create `
  --resource-group rg-letsmeet-dev `
  --template-file events-api.deploy.bicep `
  --parameters '@events-api.deploy.parameters.dev.json' `
  --parameters eventsApiManagedIdentityId=$eventsApiManagedIdentityId `
  --parameters identityApiManagedIdentityId=$identityApiManagedIdentityId
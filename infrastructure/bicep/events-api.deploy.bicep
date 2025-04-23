@description('Environment code')
@allowed([
  'dev'
  'test'
  'prd'
  'poc'
])
param env string = 'dev'

@description('Dataverse environment url')
param crm_url string = 'https://pg-dataverse-dev.crm4.dynamics.com'

@description('Client Id of Managed Identity used by Events Azure Function')
param eventsApiManagedIdentityId string = ''

@description('Client Id of Managed Identity used by Identity Azure Function')
param identityApiManagedIdentityId string = ''

@description('Resource group location')
param location string = resourceGroup().location

var appName = 'events-api'
var eventsApiName = 'func-${appName}-${env}'
var identityApiName = 'func-identity-api-${env}'
var eventsApiManagedIdentityName = 'id-${appName}-${env}'
var identityApiManagedIdentityName = 'id-identity-api-${env}'
var eventsApiHostingPlanName = 'asp-${appName}-${env}'
var identityApiHostingPlanName = 'asp-identity-api-${env}'
var applicationInsightsName = 'appi-${appName}-${env}'
var eventsApiStorageAccountName = 'eventsapi${env}'
var identityApiStorageAccountName = 'identityeventsapi${env}'
var spaStaticAppName  = 'stapp-events-${env}'

var subscriptionId = subscription().subscriptionId
var resourceGroupName = resourceGroup().name

resource eventsApiStorageAccount 'Microsoft.Storage/storageAccounts@2021-04-01' = {
  name: eventsApiStorageAccountName
  location: location
  sku: {
    name: 'Standard_LRS'
    // tier: 'Standard'
  }
  kind: 'Storage'
  properties: {
    supportsHttpsTrafficOnly: true
  }
}

resource identityApiStorageAccount 'Microsoft.Storage/storageAccounts@2021-04-01' = {
  name: identityApiStorageAccountName
  location: location
  sku: {
    name: 'Standard_LRS'
    // tier: 'Standard'
  }
  kind: 'Storage'
  properties: {
    supportsHttpsTrafficOnly: true
  }
}

resource applicationInsights 'microsoft.insights/components@2020-02-02' = {
  name: applicationInsightsName
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
  }
}

resource eventsApiHostingPlan 'Microsoft.Web/serverfarms@2018-02-01' = {
  name: eventsApiHostingPlanName
  location: location
  sku: {
    name: 'Y1'
    tier: 'Dynamic'
    size: 'Y1'
    family: 'Y'
    capacity: 0
  }
  kind: 'app'
  properties: {
    perSiteScaling: false
    maximumElasticWorkerCount: 1
    isSpot: false
    reserved: false
    isXenon: false
    hyperV: false
    targetWorkerCount: 0
    targetWorkerSizeId: 0
  }
}

resource identityApiHostingPlan 'Microsoft.Web/serverfarms@2018-02-01' = {
  name: identityApiHostingPlanName
  location: location
  sku: {
    name: 'Y1'
    tier: 'Dynamic'
    size: 'Y1'
    family: 'Y'
    capacity: 0
  }
  kind: 'app'
  properties: {
    perSiteScaling: false
    maximumElasticWorkerCount: 1
    isSpot: false
    reserved: false
    isXenon: false
    hyperV: false
    targetWorkerCount: 0
    targetWorkerSizeId: 0
  }
}

resource eventsFunctionApp 'Microsoft.Web/sites@2018-11-01' = {
  name: eventsApiName
  location: location
  kind: 'functionapp'
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
      '/subscriptions/${subscriptionId}/resourcegroups/${resourceGroupName}/providers/Microsoft.ManagedIdentity/userAssignedIdentities/${eventsApiManagedIdentityName}': {}
    }
  }
  properties: {
    enabled: true
    serverFarmId: eventsApiHostingPlan.id
    httpsOnly: true
    siteConfig: {
      appSettings: [
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: applicationInsights.properties.InstrumentationKey
        }
        {
          name: 'AzureWebJobsStorage'
          value: 'DefaultEndpointsProtocol=https;AccountName=${eventsApiStorageAccountName};AccountKey=${listKeys(eventsApiStorageAccount.id,'2017-06-01').keys[0].value}'
        }
        {
          name: 'WEBSITE_CONTENTAZUREFILECONNECTIONSTRING'
          value: 'DefaultEndpointsProtocol=https;AccountName=${eventsApiStorageAccountName};AccountKey=${listKeys(eventsApiStorageAccount.id,'2017-06-01').keys[0].value}'
        }
        {
          name: 'WEBSITE_CONTENTSHARE'
          value: toLower(eventsApiName)
        }
        {
          name: 'FUNCTIONS_EXTENSION_VERSION'
          value: '~4'
        }
        {
          name: 'FUNCTIONS_WORKER_RUNTIME'
          value: 'dotnet'
        }
        {
          name: 'WEBSITE_RUN_FROM_PACKAGE'
          value: '1'
        }
        {
          name: 'WEBSITE_TIME_ZONE'
          value: 'UTC'
        }
        {
          name: 'crm-base-url'
          value: crm_url
        }
        {
          name: 'dataverse-identity-id'
          value: eventsApiManagedIdentityId
        }
      ]
    }
  }
}

resource identityFunctionApp 'Microsoft.Web/sites@2018-11-01' = {
  name: identityApiName
  location: location
  kind: 'functionapp'
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
      '/subscriptions/${subscriptionId}/resourcegroups/${resourceGroupName}/providers/Microsoft.ManagedIdentity/userAssignedIdentities/${identityApiManagedIdentityName}': {}
    }
  }
  properties: {
    enabled: true
    serverFarmId: identityApiHostingPlan.id
    httpsOnly: true
    siteConfig: {
      appSettings: [
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: applicationInsights.properties.InstrumentationKey
        }
        {
          name: 'AzureWebJobsStorage'
          value: 'DefaultEndpointsProtocol=https;AccountName=${identityApiStorageAccountName};AccountKey=${listKeys(identityApiStorageAccount.id,'2017-06-01').keys[0].value}'
        }
        {
          name: 'WEBSITE_CONTENTAZUREFILECONNECTIONSTRING'
          value: 'DefaultEndpointsProtocol=https;AccountName=${identityApiStorageAccountName};AccountKey=${listKeys(identityApiStorageAccount.id,'2017-06-01').keys[0].value}'
        }
        {
          name: 'WEBSITE_CONTENTSHARE'
          value: toLower(identityApiName)
        }
        {
          name: 'FUNCTIONS_EXTENSION_VERSION'
          value: '~4'
        }
        {
          name: 'FUNCTIONS_WORKER_RUNTIME'
          value: 'dotnet'
        }
        {
          name: 'WEBSITE_RUN_FROM_PACKAGE'
          value: '1'
        }
        {
          name: 'WEBSITE_TIME_ZONE'
          value: 'UTC'
        }
        {
          name: 'crm-base-url'
          value: crm_url
        }
        {
          name: 'dataverse-identity-id'
          value: identityApiManagedIdentityId
        }
      ]
    }
  }
}

resource spaStaticWebApp 'Microsoft.Web/staticSites@2023-01-01' = {
  name: spaStaticAppName 
  location: 'West Europe'
  sku: {
    name: 'Free'
    tier: 'Free'
  }
  properties: {
    repositoryUrl: 'https://monkeyshockdev.visualstudio.com/Lets meet/_git/Lets meet'
    branch: 'spa-deployment'
    stagingEnvironmentPolicy: 'Enabled'
    allowConfigFileUpdates: true
    provider: 'Custom'
    enterpriseGradeCdnStatus: 'Disabled'
  }
}

resource sspaStaticWebAppBasicAuth 'Microsoft.Web/staticSites/basicAuth@2023-01-01' = {
  parent: spaStaticWebApp
  name: 'default'
  location: 'West Europe'
  properties: {
    applicableEnvironmentsMode: 'SpecifiedEnvironments'
  }
}


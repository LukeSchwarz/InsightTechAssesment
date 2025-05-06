param location string = resourceGroup().location
param appServicePlanName string = 'ins-api-plan'
param webAppName string = 'ins-web-plan'

resource appServicePlan 'Microsoft.Web/serverfarms@2019-08-01' = {
    name: appServicePlanName
    location: location
    sku: {
        name: 'B1'
        tier: 'Free'
    }
    kind: 'app'
}

resource webApp 'Microsoft.Web/sites@2019-08-01' = {
    name: webAppName
    location: location
    properties: {
        serverFarmId: appServicePlan.id
        httpsOnly: true
    }
    kind: 'app'
}

output webAppUrl string = 'https://${webApp.name}.azurewebsites.net'

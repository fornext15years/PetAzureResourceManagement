using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetConsoleAzureResources
{
    class ServicePrincipal
    {
        public IAzure CreateServicePrincipal()
        {
            try
            {
                ServicePrincipalLoginInformation loginInfo = new ServicePrincipalLoginInformation()
                {
                    ClientId = Environment.GetEnvironmentVariable("ClientId", EnvironmentVariableTarget.Machine),
                    ClientSecret = Environment.GetEnvironmentVariable("ClientSecret", EnvironmentVariableTarget.Machine)
                };

                var credentials = new AzureCredentials(loginInfo, Environment.GetEnvironmentVariable("TenantId", EnvironmentVariableTarget.Machine), AzureEnvironment.AzureGlobalCloud);
                var azureAuth = Azure.Configure().WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic).Authenticate(credentials);
                var azure = azureAuth.WithSubscription(Environment.GetEnvironmentVariable("SubscriptionId", EnvironmentVariableTarget.Machine));

                return azure;
            }
            catch (Exception)
            {

            }

            return null;
        }

        private static ServicePrincipal servicePrincipal;
        public static ServicePrincipal CreateInstance()
        {
            if (servicePrincipal == null)
            {
                servicePrincipal = new ServicePrincipal();
            }

            return servicePrincipal;
        }
    }
}


using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using System;
using System.Threading.Tasks;

namespace PetConsoleAzureResources
{
    public class ResourceCreator
    {
        public bool CreateResourceGroup(string rg, string adgroup)
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
                var resourceGroup = azure.ResourceGroups.Define(rg).WithRegion(Region.USEast).Create();

                return true;
            }
            catch (Exception ex)
            {
            }

            return false;
        }
    }
}
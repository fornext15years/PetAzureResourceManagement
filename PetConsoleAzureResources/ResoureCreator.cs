
using Microsoft.Azure.Management.AppService.Fluent;
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
        public AzureActionResult CreateResourceGroup(IAzure azure, string rg, string adgroup)
        {
            AzureActionResult result = new AzureActionResult();
            try
            {                
                IResourceGroup resGrp = null;

                if (!azure.ResourceGroups.Contain(rg))
                {
                    resGrp = azure.ResourceGroups.Define(rg).WithRegion(Region.USEast).Create();
                    result.Message = $"{resGrp.Name} in {resGrp.RegionName} has been Created successfully!";
                }
                else
                {
                    resGrp = azure.ResourceGroups.GetByName(rg);
                    result.Message = $"{resGrp.Name} in {resGrp.RegionName} already exists";
                }

                result.Succeed = true;
                result.Value = resGrp;
                return result;
            }
            catch (Exception ex)
            {
                result.Succeed = false;
                result.Value = null;
                result.Message = $"Failed to create {rg} because of {ex.Message}";
            }

            return result;
        }

        public AzureActionResult CreateWebApp(IAzure azure, IResourceGroup resGrp, string webappName)
        {
            AzureActionResult result = new AzureActionResult();

            try
            {
                var webapp = azure.WebApps.Define(webappName).WithRegion(Region.USEast).WithExistingResourceGroup(resGrp).WithNewWindowsPlan(PricingTier.StandardS1).Create();
                result.Message = $"{webapp.Name} in {webapp.RegionName} has been Created successfully!";
                result.Succeed = true;
                result.Value = webapp;

                return result;
            }
            catch (Exception ex)
            {
                result.Succeed = false;
                result.Value = null;
                result.Message = $"Failed to create {webappName} because of {ex.Message}";
            }

            return result;
        }
    }
}
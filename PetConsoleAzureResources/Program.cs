using Microsoft.Azure.Management.ResourceManager.Fluent;
using System;

namespace PetConsoleAzureResources
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = new ConsoleLogger();
            ResourceCreator creator = new ResourceCreator();

            var resgrpName = PromptAcknowledge("Please enter resource group name");

            var azure = ServicePrincipal.CreateInstance().CreateServicePrincipal();
            if (azure != null)
            {
                logger.WriteLog(AzureActionMessages.AzureActionCreateServicePrincipalOk);

                var actionResult = creator.CreateResourceGroup(azure, resgrpName, "testadgroup");
                logger.WriteLog(actionResult.Message);

                if (actionResult.Succeed)
                {
                    var resgrp = actionResult.Value as IResourceGroup;

                    var webappName = PromptAcknowledge("Please enter web app name");
                    actionResult = creator.CreateWebApp(azure, resgrp, "demoapp16082019");
                    logger.WriteLog(actionResult.Message);
                }
            }
            else
            {
                logger.WriteLog(AzureActionMessages.AzureActionCreateServicePrincipalFail);
            }

            Console.WriteLine("Done. please enter key to finish.");
            Console.ReadKey();
        }

        private static string PromptAcknowledge(string msg)
        {
            Console.WriteLine(msg);

            var userInput = Console.ReadLine();
            Console.WriteLine($"Processing..");
            return userInput;
        }

        
    }
}

using System;

namespace PetConsoleAzureResources
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating resource group!");

            ResourceCreator creator = new ResourceCreator();
            var succeed = creator.CreateResourceGroup("testrg16082019","testadgroup");

            if (succeed)
            {
                Console.WriteLine("Created successfully!");
            }
            else
            {
                Console.WriteLine("Failed");
            }

            Console.ReadKey();
        }
    }
}

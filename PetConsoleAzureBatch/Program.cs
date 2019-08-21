using Microsoft.Azure.Batch;
using System;

namespace PetConsoleAzureBatch
{
    class Program
    {
        private const string PoolId = "DotNetQuickstartPool";
        private const string JobId = "DotNetQuickstartJob";
        private const int PoolNodeCount = 2;
        private const string PoolVMSize = "STANDARD_A1_v2";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        private static VirtualMachineConfiguration CreateVMConfiguration(ImageReference imageReference)
        {
            return new VirtualMachineConfiguration(imageReference: imageReference,nodeAgentSkuId: "batch.node.windows amd64");
        }

        private static ImageReference CreateImageReference()
        {
            return new ImageReference(
                publisher: "MicrosoftWindowsServer",
                offer: "WindowsServer",
                sku: "2016-datacenter-smalldisk",
                version: "latest");
        }

        private static void CreatePool(BatchClient batchClient, VirtualMachineConfiguration vmConfiguration)
        {
            CloudPool pool = batchClient.PoolOperations.CreatePool(poolId:PoolId, 
                virtualMachineSize:PoolVMSize, 
                virtualMachineConfiguration:vmConfiguration, 
                targetDedicatedComputeNodes:PoolNodeCount);
            pool.Commit();
        }
        private static void CreateJob() { }
        private static void CreateTask() { }

    }
}

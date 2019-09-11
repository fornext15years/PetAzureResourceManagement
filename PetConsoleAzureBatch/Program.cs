using Microsoft.Azure.Batch;
using Microsoft.Azure.Batch.Auth;
using Microsoft.Azure.Batch.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;

namespace PetConsoleAzureBatch
{
    class Program
    {
        private const string PoolId = "demopool2208";
        private const string JobId = "demojob2208";
        private const int PoolNodeCount = 2;
        private const string PoolVMSize = "STANDARD_A1_v2";
        private const int TotalTaskCount = 3;
        private static List<CloudTask> tasks;

        static void Main(string[] args)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Console.WriteLine("Creating batch client");
            BatchSharedKeyCredentials cred = new BatchSharedKeyCredentials("https://demobatch2208.eastus.batch.azure.com","demobatch2208", "1nYqVtDR+8cn9vjVMsZ30jLXT9GguYa8TFSrn9cJUYotRpEUg7evhAfLi5D8KCaF6hJxG/dUkB7TaOGdEf6+fg==");
            using (var batchClient = BatchClient.Open(cred))
            {
                CreatePool(batchClient, CreateVMConfiguration(CreateImageReference()));
                CreateJob(batchClient);
                CreateTask(batchClient);


                TimeSpan timeout = TimeSpan.FromMinutes(30);
                Console.WriteLine("Monitoring all tasks for 'Completed' state, timeout in {0}...", timeout);

                IEnumerable<CloudTask> addedTasks = batchClient.JobOperations.ListTasks(JobId);

                batchClient.Utilities.CreateTaskStateMonitor().WaitAll(addedTasks, TaskState.Completed, timeout);

                Console.WriteLine("All tasks reached state Completed.");

                // Print task output
                Console.WriteLine();
                Console.WriteLine("Printing task output...");

                IEnumerable<CloudTask> completedtasks = batchClient.JobOperations.ListTasks(JobId);

                foreach (CloudTask task in completedtasks)
                {
                    string nodeId = String.Format(task.ComputeNodeInformation.ComputeNodeId);
                    Console.WriteLine("Task: {0}", task.Id);
                    Console.WriteLine("Node: {0}", nodeId);
                    Console.WriteLine("Standard out:");
                    Console.WriteLine(task.GetNodeFile(Constants.StandardOutFileName).ReadAsString());
                }

                // Print out some timing info
                timer.Stop();
                Console.WriteLine();
                Console.WriteLine("Sample end: {0}", DateTime.Now);
                Console.WriteLine("Elapsed time: {0}", timer.Elapsed);
            }


            Console.ReadKey();
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
        private static void CreateJob(BatchClient batchClient)
        {
            CloudJob job = batchClient.JobOperations.CreateJob();
            job.Id = JobId;
            job.PoolInformation = new PoolInformation { PoolId = PoolId };
            job.Commit();
        }

        private static void CreateTask(BatchClient batchClient)
        {
            if(tasks == null)
            {
                tasks = new List<CloudTask>();
            }

            for (int i = 0; i < TotalTaskCount; i++)
            {
                string taskId = String.Format("Task{0}", i);
                string taskCommandLine = String.Format(@"cmd /c ""set AZ_BATCH & timeout / t 90 > NUL""");

                CloudTask task = new CloudTask(taskId, taskCommandLine);
                tasks.Add(task);
            }

            batchClient.JobOperations.AddTask(JobId, tasks);
        }

    }
}

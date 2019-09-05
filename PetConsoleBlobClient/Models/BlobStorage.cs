using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PetConsoleBlobClient.Models
{
    class BlobStorage : IStorage
    {
        private CloudBlobClient cloudBlobClient = null;
        public string SourceContainerName { get; set; }
        public string DestinationContainerName { get; set; }

        public BlobStorage()
        {
            
        }
        public async Task<IEnumerable<string>> GetNames()
        {
            List<string> names = new List<string>();
            
            CloudBlobContainer container = cloudBlobClient.GetContainerReference(SourceContainerName);
            await container.CreateIfNotExistsAsync();

            BlobContinuationToken token = null;
            BlobResultSegment segments = null;
            do
            {
                segments = await container.ListBlobsSegmentedAsync(token);
                foreach(var item in segments.Results)
                {
                    var blockblob = item as CloudBlockBlob;
                    names.Add(blockblob.Name);
                }

                token = segments.ContinuationToken;
            } while (token != null);

            return names;
        }

        public void Initialize()
        {
            Console.WriteLine("Initialize");
            var connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING", EnvironmentVariableTarget.Machine);
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
            cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
        }

        public async Task Download(string blobName, string filename)
        {
            Console.WriteLine("Start downloading.");
            CloudBlobContainer container = cloudBlobClient.GetContainerReference(SourceContainerName);
            await container.CreateIfNotExistsAsync();

            ICloudBlob blob = await container.GetBlobReferenceFromServerAsync(blobName);
            await blob.DownloadToFileAsync(filename, FileMode.Create);
            Console.WriteLine("End downloading.");
            return;
        }

        public async Task Upload(string blobname, string filename)
        {
            Console.WriteLine("Start uploding.");
            CloudBlobContainer container = cloudBlobClient.GetContainerReference(SourceContainerName);
            await container.CreateIfNotExistsAsync();

            CloudBlockBlob blob = container.GetBlockBlobReference(blobname);
            await blob.UploadFromFileAsync(filename);
            Console.WriteLine("End uploding.");
            return;
        }

        public async Task Delete(string blobname)
        {
            Console.WriteLine("Start deleting.");
            CloudBlobContainer container = cloudBlobClient.GetContainerReference(SourceContainerName);
            await container.CreateIfNotExistsAsync();

            ICloudBlob blob = await container.GetBlobReferenceFromServerAsync(blobname);
            await blob.DeleteIfExistsAsync();
            Console.WriteLine("End deleting.");
        }
    }
}

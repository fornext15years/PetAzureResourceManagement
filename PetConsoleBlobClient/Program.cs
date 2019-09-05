using PetConsoleBlobClient.Models;
using System;

namespace PetConsoleBlobClient
{
    class Program
    {
        static void Main(string[] args)
        {
            BlobStorage blobStorage = new BlobStorage();
            blobStorage.SourceContainerName = "testcontainer";
            blobStorage.Initialize();

            //blobStorage.Download("1.jpg", "download1.jpg").GetAwaiter().GetResult();
            //blobStorage.Upload("download1-1.jpg", "download1.jpg").GetAwaiter().GetResult();

            var names = blobStorage.GetNames().GetAwaiter().GetResult();
            if(names != null)
            {
                foreach(var name in names)
                {
                    Console.WriteLine(name);
                    blobStorage.Delete(name).GetAwaiter().GetResult();
                }
            }

            names = blobStorage.GetNames().GetAwaiter().GetResult();
            if (names != null)
            {
                foreach (var name in names)
                {
                    Console.WriteLine(name);
                    blobStorage.Delete(name).GetAwaiter().GetResult();
                }
            }
            else
            {
                Console.WriteLine("no blob");
            }

            Console.ReadKey();
        }
    }
}

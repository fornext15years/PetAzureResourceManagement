using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PetConsoleBlobClient.Models
{
    interface IStorage
    {
        Task Upload(string blobname, string filename);
        Task<IEnumerable<string>> GetNames();
        Task Download(string blobname, string filename);

        Task Delete(string blobname);
        void Initialize();
    }
}

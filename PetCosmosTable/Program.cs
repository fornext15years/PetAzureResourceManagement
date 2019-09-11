using Microsoft.Azure.Cosmos.Table;
using System;
using PetCosmosTable.Models;

namespace PetCosmosTable
{
    class Program
    {
        private const string storageconnectionstring = "DefaultEndpointsProtocol=https;AccountName=democosmostable;AccountKey=TVrILRyU028JG88mGAEYpa0GhLT9wZtix3v7JMGD0PWOtWgdW01Ohp7LFb01wORTmPYMyJAYphNLymiLplcxcQ==;TableEndpoint=https://democosmostable.table.cosmos.azure.com:443/;";

        static void Main(string[] args)
        {
            Console.WriteLine("start");

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageconnectionstring);
            Console.WriteLine("Got valid storage account");

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
            Console.WriteLine("Created a table client");

            var tableName = "demotable22082127";

            CloudTable table = tableClient.GetTableReference(tableName);
            if (table.CreateIfNotExists())
            {
                Console.WriteLine($"Created a table{tableName}");
            }
            else
            {
                Console.WriteLine($"Already exists. {tableName}");
            }

            CustomerEntity customer = new CustomerEntity("Harp", "Walter")
            {
                Email = "Walter@gmail.com",
                PhoneNumber = "0000"
            };

            ExtendedCustomerEntity extendedCustomer = new ExtendedCustomerEntity("Kim", "Jinpyi")
            {
                Email = "nunknjp@gmail.com",
                PhoneNumber = "0000",
                PostCode = "3133"
            };
            Console.WriteLine($"Created customer entity");
            TableOperation tbOp = TableOperation.InsertOrReplace(customer);
            TableResult result = table.Execute(tbOp);
            CustomerEntity insertedCustomer = result.Result as CustomerEntity;
            Console.WriteLine($"Inserted customer entity");

            if (result.RequestCharge.HasValue)
            {
                Console.WriteLine("Request Charge of InsertOrMerge Operation: " + result.RequestCharge);
            }

            Console.WriteLine("Update an existing Entity using the InsertOrMerge Upsert Operation.");
            tbOp = TableOperation.InsertOrMerge(extendedCustomer);
            result = table.Execute(tbOp);
            Console.WriteLine($"Inserted customer entity");

            if (result.RequestCharge.HasValue)
            {
                Console.WriteLine("Request Charge of InsertOrMerge Operation: " + result.RequestCharge);
            }

            tbOp = TableOperation.Retrieve<CustomerEntity>("Harp", "Walter");
            result = table.Execute(tbOp);
            CustomerEntity retreivecustomer = result.Result as CustomerEntity;
            if (retreivecustomer != null)
            {
                Console.WriteLine("\t{0}\t{1}\t{2}\t{3}", retreivecustomer.PartitionKey, retreivecustomer.RowKey, retreivecustomer.Email, retreivecustomer.PhoneNumber);
            }

            // Get the request units consumed by the current operation. RequestCharge of a TableResult is only applied to Azure CosmoS DB 
            if (result.RequestCharge.HasValue)
            {
                Console.WriteLine("Request Charge of Retrieve Operation: " + result.RequestCharge);
            }

            tbOp = TableOperation.Retrieve<ExtendedCustomerEntity>("Kim", "Jinpyi");
            result = table.Execute(tbOp);
            var ret = result.Result as ExtendedCustomerEntity;
            if (ret != null)
            {
                Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}", ret.PartitionKey, ret.RowKey, ret.Email, ret.PhoneNumber, ret.PostCode);
            }

            // Get the request units consumed by the current operation. RequestCharge of a TableResult is only applied to Azure CosmoS DB 
            if (result.RequestCharge.HasValue)
            {
                Console.WriteLine("Request Charge of Retrieve Operation: " + result.RequestCharge);
            }

            Console.ReadKey();

        }
    }
}

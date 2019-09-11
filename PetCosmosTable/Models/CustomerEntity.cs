﻿using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetCosmosTable.Models
{
    class CustomerEntity : TableEntity
    {
        public CustomerEntity() { }

        public CustomerEntity(string lastName, string firstName)
        {
            PartitionKey = lastName;
            RowKey = firstName;
        }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    class ExtendedCustomerEntity : TableEntity
    {
        public ExtendedCustomerEntity() { }

        public ExtendedCustomerEntity(string lastName, string firstName)
        {
            PartitionKey = lastName;
            RowKey = firstName;
        }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PostCode { get; set; }
    }
}

using System;
using System.Text;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Newtonsoft.Json;

namespace PetSearch.Models
{
    public partial class Address
    {
        [IsSearchable]
        public string StreetAddress { get; set; }

        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string City { get; set; }

        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string StateProvince { get; set; }

        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string PostalCode { get; set; }

        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string Country { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            if (!IsEmpty)
            {
                builder.AppendFormat("{0}\n{1}, {2} {3}\n{4}", StreetAddress, City, StateProvince, PostalCode, Country);
            }

            return builder.ToString();
        }

        [JsonIgnore]
        public bool IsEmpty => String.IsNullOrEmpty(StreetAddress) &&
                               String.IsNullOrEmpty(City) &&
                               String.IsNullOrEmpty(StateProvince) &&
                               String.IsNullOrEmpty(PostalCode) &&
                               String.IsNullOrEmpty(Country);
    }
}
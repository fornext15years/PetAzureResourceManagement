using System;
using System.Text;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Spatial;
using Newtonsoft.Json;

namespace PetSearch.Models
{
    public partial class Hotel
    {
        [System.ComponentModel.DataAnnotations.Key]
        [IsFilterable]
        public string HotelId { get; set; }

        [IsSearchable, IsSortable]
        public string HotelName { get; set; }

        [IsSearchable]
        [Analyzer(AnalyzerName.AsString.EnLucene)]
        public string Description { get; set; }

        [IsSearchable]
        [Analyzer(AnalyzerName.AsString.FrLucene)]
        [JsonProperty("Description_fr")]
        public string DescriptionFr { get; set; }

        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string Category { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        public string[] Tags { get; set; }

        [IsFilterable, IsSortable, IsFacetable]
        public bool? ParkingIncluded { get; set; }

        // SmokingAllowed reflects whether any room in the hotel allows smoking.
        // The JsonIgnore attribute indicates that a field should not be created 
        // in the index for this property and it will only be used by code in the client.
        [JsonIgnore]
        public bool? SmokingAllowed => (Rooms != null) ? Array.Exists(Rooms, element => element.SmokingAllowed == true) : (bool?)null;

        [IsFilterable, IsSortable, IsFacetable]
        public DateTimeOffset? LastRenovationDate { get; set; }

        [IsFilterable, IsSortable, IsFacetable]
        public double? Rating { get; set; }

        public Address Address { get; set; }

        [IsFilterable, IsSortable]
        public GeographyPoint Location { get; set; }

        public Room[] Rooms { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            if (!String.IsNullOrEmpty(HotelId))
            {
                builder.AppendFormat("HotelId: {0}\n", HotelId);
            }

            if (!String.IsNullOrEmpty(HotelName))
            {
                builder.AppendFormat("Name: {0}\n", HotelName);
            }

            if (!String.IsNullOrEmpty(Description))
            {
                builder.AppendFormat("Description: {0}\n", Description);
            }

            if (!String.IsNullOrEmpty(DescriptionFr))
            {
                builder.AppendFormat("Description (French): {0}\n", DescriptionFr);
            }

            if (!String.IsNullOrEmpty(Category))
            {
                builder.AppendFormat("Category: {0}\n", Category);
            }

            if (Tags != null && Tags.Length > 0)
            {
                builder.AppendFormat("Tags: [ {0} ]\n", String.Join(", ", Tags));
            }

            if (ParkingIncluded.HasValue)
            {
                builder.AppendFormat("Parking included: {0}\n", ParkingIncluded.Value ? "yes" : "no");
            }

            if (SmokingAllowed.HasValue)
            {
                builder.AppendFormat("Smoking allowed: {0}\n", SmokingAllowed.Value ? "yes" : "no");
            }

            if (LastRenovationDate.HasValue)
            {
                builder.AppendFormat("Last renovated on: {0}\n", LastRenovationDate);
            }

            if (Rating.HasValue)
            {
                builder.AppendFormat("Rating: {0}\n", Rating);
            }

            if (Address != null && !Address.IsEmpty)
            {
                builder.AppendFormat("Address: \n{0}\n", Address.ToString());
            }

            if (Location != null)
            {
                builder.AppendFormat("Location: Latitude {0}, Longitude {1}\n", Location.Latitude, Location.Longitude);
            }

            if (Rooms != null)
            {
                builder.AppendFormat("\nRooms: \n");
                foreach (var room in Rooms)
                {
                    if (room != null)
                    {
                        builder.AppendFormat("{0}\n\n", room.ToString());
                    }
                }
            }

            return builder.ToString();
        }
    }
}

using System;
using System.Text;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Newtonsoft.Json;

namespace PetSearch.Models
{
    public partial class Room
    {
        [IsSearchable]
        [Analyzer(AnalyzerName.AsString.EnMicrosoft)]
        public string Description { get; set; }

        [IsSearchable]
        [Analyzer(AnalyzerName.AsString.FrMicrosoft)]
        [JsonProperty("Description_fr")]
        public string DescriptionFr { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        public string Type { get; set; }

        [IsFilterable, IsFacetable]
        public double? BaseRate { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        public string BedOptions { get; set; }

        [IsFilterable, IsFacetable]
        public int SleepsCount { get; set; }

        [IsFilterable, IsFacetable]
        public bool? SmokingAllowed { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        public string[] Tags { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            if (!String.IsNullOrEmpty(Description))
            {
                builder.AppendFormat("Description: {0}\n", Description);
            }

            if (!String.IsNullOrEmpty(DescriptionFr))
            {
                builder.AppendFormat("DescriptionFr: {0}\n", DescriptionFr);
            }

            if (!String.IsNullOrEmpty(Type))
            {
                builder.AppendFormat("Type: {0}\n", Type);
            }

            if (BaseRate.HasValue)
            {
                builder.AppendFormat("BaseRate: {0}\n", BaseRate);
            }

            if (!String.IsNullOrEmpty(BedOptions))
            {
                builder.AppendFormat("BedOptions: {0}\n", BedOptions);
            }

            builder.AppendFormat("SleepsCount: {0}\n", BedOptions);

            if (SmokingAllowed.HasValue)
            {
                builder.AppendFormat("Smoking allowed: {0}\n", SmokingAllowed.Value ? "yes" : "no");
            }

            if (Tags != null && Tags.Length > 0)
            {
                builder.AppendFormat("Tags: [ {0} ]\n", String.Join(", ", Tags));
            }

            return builder.ToString();
        }
    }
}

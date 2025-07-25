using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Utils_for_PBI.Models.SerializationModel
{
    public class TableMetadataDTO
    {
        [JsonPropertyName("ID")]
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Data Category")]
        public string DataCategory { get; set; }

        [JsonPropertyName("Is Hidden")]
        public bool IsHidden { get; set; }

        [JsonPropertyName("Modified Time")]
        public string ModifiedTime { get; set; }

        [JsonPropertyName("Structure Modified Time")]
        public string StructureModifiedTime { get; set; }

        [JsonPropertyName("System Flags")]
        public string SystemFlags { get; set; }

        [JsonPropertyName("Calculation Group ID")]
        public int CalculationGroupId { get; set; }

        [JsonPropertyName("Exclude From Model Refresh")]
        public bool ExcludeFromModelRefresh { get; set; }
    }
}

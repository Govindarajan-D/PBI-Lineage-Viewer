using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Utils_for_PBI.Models.SerializationModel
{
    public class ColumnMetadataDTO
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Table Id")]
        public int TableId { get; set; }

        [JsonPropertyName("Explicit Name")]
        public string ExplicitName { get; set; }

        [JsonPropertyName("Inferred Name")]
        public string InferredName { get; set; }

        [JsonPropertyName("Explicit Data Type")]
        public string ExplicitDataType { get; set; }

        [JsonPropertyName("Inferred Data Type")]
        public string InferredDataType { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Is Hidden")]
        public bool IsHidden { get; set; }

        [JsonPropertyName("Is Unique")]
        public bool IsUnique { get; set; }

        [JsonPropertyName("Is Key")]
        public bool IsKey { get; set; }

        [JsonPropertyName("Is Nullable")]
        public bool IsNullable { get; set; }

        [JsonPropertyName("Summarize By")]
        public string SummarizeBy { get; set; }

        [JsonPropertyName("Type")]
        public int Type { get; set; }

        [JsonPropertyName("Expression")]
        public string Expression { get; set; }

        [JsonPropertyName("Is Available In Mdx")]
        public bool IsAvailableInMDX { get; set; }

        [JsonPropertyName("Sort By Column Id")]
        public string SortByColumnId { get; set; }

        [JsonPropertyName("Modified Time")]
        public string ModifiedTime { get; set; }

        [JsonPropertyName("Structure Modified Time")]
        public string StructureModifiedTime { get; set; }

        [JsonPropertyName("Refreshed Time")]
        public string RefreshedTime { get; set; }

        [JsonPropertyName("System Flags")]
        public int SystemFlags { get; set; }
    }
}

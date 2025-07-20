using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils_for_PBI.Models
{
    public class ColumnsMetadataRow
    {
        public string ID { get; set; }
        public string TABLE_ID { get; set; }
        public string EXPLICIT_NAME { get; set; }
        public string INFERRED_NAME { get; set; }
        public string EXPLICIT_DATA_TYPE { get; set; }
        public string INFERRED_DATA_TYPE { get; set; }
        public string DESCRIPTION { get; set; }
        public string IS_HIDDEN { get; set; }
        public string IS_UNIQUE { get; set; }
        public string IS_KEY { get; set; }
        public string IS_NULLABLE { get; set; }
        public string SUMMARIZE_BY { get; set; }
        public string TYPE { get; set; }
        public string EXPRESSION { get; set; }
        public string IS_AVAILABLE_IN_MDX { get; set; }
        public string SORT_BY_COLUMN_ID { get; set; }
        public string MODIFIED_TIME { get; set; }
        public string STRUCTURE_MODIFIED_TIME { get; set; }
        public string REFRESHED_TIME { get; set; }
        public string SYSTEM_FLAGS { get; set; }

        public ColumnsMetadataRow()
        {
            ID = string.Empty;
            TABLE_ID = string.Empty;
            EXPLICIT_NAME = string.Empty;
            INFERRED_NAME = string.Empty;
            EXPLICIT_DATA_TYPE = string.Empty;
            INFERRED_DATA_TYPE = string.Empty;
            DESCRIPTION = string.Empty;
            IS_HIDDEN = string.Empty;
            IS_UNIQUE = string.Empty;
            IS_KEY = string.Empty;
            IS_NULLABLE = string.Empty;
            SUMMARIZE_BY = string.Empty;
            TYPE = string.Empty;
            EXPRESSION = string.Empty;
            IS_AVAILABLE_IN_MDX = string.Empty;
            SORT_BY_COLUMN_ID = string.Empty;
            MODIFIED_TIME = string.Empty;
            STRUCTURE_MODIFIED_TIME = string.Empty;
            REFRESHED_TIME = string.Empty;
            SYSTEM_FLAGS = string.Empty;
        }

        public static ColumnsMetadataRow MapRowToObject(IDataRecord dataRecord) => new ColumnsMetadataRow
        {
            ID = Convert.ToString(dataRecord["ID"]),
            TABLE_ID = Convert.ToString(dataRecord["TableID"]),
            EXPLICIT_NAME = Convert.ToString(dataRecord["ExplicitName"]),
            INFERRED_NAME = Convert.ToString(dataRecord["InferredName"]),
            EXPLICIT_DATA_TYPE = Convert.ToString(dataRecord["ExplicitDataType"]),
            INFERRED_DATA_TYPE = Convert.ToString(dataRecord["InferredDataType"]),
            DESCRIPTION = Convert.ToString(dataRecord["Description"]),
            IS_HIDDEN = Convert.ToString(dataRecord["IsHidden"]),
            IS_UNIQUE = Convert.ToString(dataRecord["IsUnique"]),
            IS_KEY = Convert.ToString(dataRecord["IsKey"]),
            IS_NULLABLE = Convert.ToString(dataRecord["IsNullable"]),
            SUMMARIZE_BY = Convert.ToString(dataRecord["SummarizeBy"]),
            TYPE = Convert.ToString(dataRecord["Type"]),
            EXPRESSION = Convert.ToString(dataRecord["Expression"]),
            IS_AVAILABLE_IN_MDX = Convert.ToString(dataRecord["IsAvailableInMDX"]),
            SORT_BY_COLUMN_ID = Convert.ToString(dataRecord["SortByColumnID"]),
            MODIFIED_TIME = Convert.ToString(dataRecord["ModifiedTime"]),
            STRUCTURE_MODIFIED_TIME = Convert.ToString(dataRecord["StructureModifiedTime"]),
            REFRESHED_TIME = Convert.ToString(dataRecord["RefreshedTime"]),
            SYSTEM_FLAGS = Convert.ToString(dataRecord["SystemFlags"])
        };


        public enum SummarizeBy
        {
            Default = 1,
            None = 2,
            Sum = 3,
            Min = 4,
            Max = 5,
            Count = 6,
            Average = 7,
            DistinctCount = 8
        }
    }
}

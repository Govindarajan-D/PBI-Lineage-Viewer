using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils_for_PBI.Models.MetadataRows
{
    public class ColumnsMetadataRow
    {
        public int ID { get; set; }
        public int TABLE_ID { get; set; }
        public string EXPLICIT_NAME { get; set; }
        public string INFERRED_NAME { get; set; }
        public int EXPLICIT_DATA_TYPE { get; set; }
        public int INFERRED_DATA_TYPE { get; set; }
        public string DESCRIPTION { get; set; }
        public bool IS_HIDDEN { get; set; }
        public bool IS_UNIQUE { get; set; }
        public bool IS_KEY { get; set; }
        public bool IS_NULLABLE { get; set; }
        public int SUMMARIZE_BY { get; set; }
        public int TYPE { get; set; }
        public string EXPRESSION { get; set; }
        public bool IS_AVAILABLE_IN_MDX { get; set; }
        public string SORT_BY_COLUMN_ID { get; set; }
        public string MODIFIED_TIME { get; set; }
        public string STRUCTURE_MODIFIED_TIME { get; set; }
        public string REFRESHED_TIME { get; set; }
        public int SYSTEM_FLAGS { get; set; }

        public ColumnsMetadataRow()
        {
            ID = 0;
            TABLE_ID = 0;
            EXPLICIT_NAME = string.Empty;
            INFERRED_NAME = string.Empty;
            EXPLICIT_DATA_TYPE = 0;
            INFERRED_DATA_TYPE = 0;
            DESCRIPTION = string.Empty;
            IS_HIDDEN = false;
            IS_UNIQUE = false;
            IS_KEY = false;
            IS_NULLABLE = false;
            SUMMARIZE_BY = 0;
            TYPE = 0;
            EXPRESSION = string.Empty;
            IS_AVAILABLE_IN_MDX = false;
            SORT_BY_COLUMN_ID = string.Empty;
            MODIFIED_TIME = string.Empty;
            STRUCTURE_MODIFIED_TIME = string.Empty;
            REFRESHED_TIME = string.Empty;
            SYSTEM_FLAGS = 0;
        }

        public static ColumnsMetadataRow MapRowToObject(IDataRecord dataRecord) => new ColumnsMetadataRow
        {
            ID = Convert.ToInt32(dataRecord["ID"]),
            TABLE_ID = Convert.ToInt32(dataRecord["TableID"]),
            EXPLICIT_NAME = Convert.ToString(dataRecord["ExplicitName"]),
            INFERRED_NAME = Convert.ToString(dataRecord["InferredName"]),
            EXPLICIT_DATA_TYPE = Convert.ToInt32(dataRecord["ExplicitDataType"]),
            INFERRED_DATA_TYPE = Convert.ToInt32(dataRecord["InferredDataType"]),
            DESCRIPTION = Convert.ToString(dataRecord["Description"]),
            IS_HIDDEN = bool.Parse(Convert.ToString(dataRecord["IsHidden"])),
            IS_UNIQUE = bool.Parse(Convert.ToString(dataRecord["IsUnique"])),
            IS_KEY = bool.Parse(Convert.ToString(dataRecord["IsKey"])),
            IS_NULLABLE = bool.Parse(Convert.ToString(dataRecord["IsNullable"])),
            SUMMARIZE_BY = Convert.ToInt32(dataRecord["SummarizeBy"]),
            TYPE = Convert.ToInt32(dataRecord["Type"]),
            EXPRESSION = Convert.ToString(dataRecord["Expression"]),
            IS_AVAILABLE_IN_MDX = bool.Parse(Convert.ToString(dataRecord["IsAvailableInMDX"])),
            SORT_BY_COLUMN_ID = Convert.ToString(dataRecord["SortByColumnID"]),
            MODIFIED_TIME = Convert.ToString(dataRecord["ModifiedTime"]),
            STRUCTURE_MODIFIED_TIME = Convert.ToString(dataRecord["StructureModifiedTime"]),
            REFRESHED_TIME = Convert.ToString(dataRecord["RefreshedTime"]),
            SYSTEM_FLAGS = Convert.ToInt32(dataRecord["SystemFlags"])
        };


        public enum SummarizeByEnum
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

        public enum DataTypeEnum
        {
            Automatic = 1,
            String = 2,
            Int64 = 6,
            Double = 8,
            DateTime = 9,
            Decimal = 10,
            Boolean = 11,
            Binary = 17,
            Unknown = 19
        }
    }
}

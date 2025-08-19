using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

//Enum values specified based on the official docs: https://learn.microsoft.com/en-us/openspecs/sql_server_protocols/ms-ssas-t/f85cd3b9-690c-4bc7-a1f0-a854d7daecd8

namespace Utils_for_PBI.Models.MetadataRows
{
    public class TablesMetadataRow
    {
        public int ID { get; set; }
        public int MODEL_ID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string DATA_CATEGORY { get; set; }
        public bool IS_HIDDEN { get; set; }
        public string MODIFIED_TIME { get; set; }
        public string STRUCTURE_MODIFIED_TIME { get; set; }
        public int SYSTEM_FLAGS { get; set; }
        public int CALCULATION_GROUP_ID { get; set; }
        public bool EXCLUDE_FROM_MODEL_REFRESH { get; set; }

        public TablesMetadataRow()
        {
            ID = 0;
            MODEL_ID = 0;
            NAME = string.Empty;
            DESCRIPTION = string.Empty;
            DATA_CATEGORY = string.Empty;
            IS_HIDDEN = false;
            MODIFIED_TIME = string.Empty;
            STRUCTURE_MODIFIED_TIME = string.Empty;
            SYSTEM_FLAGS = 0;
            CALCULATION_GROUP_ID = 0;
            EXCLUDE_FROM_MODEL_REFRESH = false;
        }

        public static TablesMetadataRow MapRowToObject(IDataRecord dataRecord) => new TablesMetadataRow
        {
            ID = Convert.ToInt32(dataRecord["ID"]),
            MODEL_ID = Convert.ToInt32(dataRecord["ModelID"]),
            NAME = Convert.ToString(dataRecord["Name"]),
            DESCRIPTION = Convert.ToString(dataRecord["Description"]),
            DATA_CATEGORY = Convert.ToString(dataRecord["DataCategory"]),
            IS_HIDDEN = bool.Parse(Convert.ToString(dataRecord["IsHidden"])),
            MODIFIED_TIME = Convert.ToString(dataRecord["ModifiedTime"]),
            STRUCTURE_MODIFIED_TIME = Convert.ToString(dataRecord["StructureModifiedTime"]),
            SYSTEM_FLAGS = Convert.ToInt32(dataRecord["SystemFlags"]),
            CALCULATION_GROUP_ID = Convert.ToInt32(dataRecord["CalculationGroupID"]),
            EXCLUDE_FROM_MODEL_REFRESH = bool.Parse(Convert.ToString(dataRecord["ExcludeFromModelRefresh"]))
        };

        public enum SystemFlagsEnum
        {
            [Description("Table")]
            Table = 0,

            [Description("Calculated Table")]
            CalculatedTable = 2
        }

    }
}

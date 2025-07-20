using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils_for_PBI.Models
{
    public class TablesMetadataRow
    {
        public string ModelID { get; set; }
        public string Name { get; set; }
        public string DataCategory { get; set; }
        public string IsHidden { get; set; }
        public string ModifiedTime { get; set; }
        public string StructureModifiedTime { get; set; }
        public string SystemFlags { get; set; }
        public string CalculationGroupID { get; set; }
        public string ExcludeFromModelRefresh { get; set; }

        public TablesMetadataRow()
        {
            ModelID = string.Empty;
            Name = string.Empty;
            DataCategory = string.Empty;
            IsHidden = string.Empty;
            ModifiedTime = string.Empty;
            StructureModifiedTime = string.Empty;
            SystemFlags = string.Empty;
            CalculationGroupID = string.Empty;
            ExcludeFromModelRefresh = string.Empty;
        }

        public static TablesMetadataRow MapRowToObject(IDataRecord dataRecord) => new TablesMetadataRow
        {
            ModelID = Convert.ToString(dataRecord["ModelID"]),
            Name = Convert.ToString(dataRecord["Name"]),
            DataCategory = Convert.ToString(dataRecord["DataCategory"]),
            IsHidden = Convert.ToString(dataRecord["IsHidden"]),
            ModifiedTime = Convert.ToString(dataRecord["ModifiedTime"]),
            StructureModifiedTime = Convert.ToString(dataRecord["StructureModifiedTime"]),
            SystemFlags = Convert.ToString(dataRecord["SystemFlags"]),
            CalculationGroupID = Convert.ToString(dataRecord["CalculationGroupID"]),
            ExcludeFromModelRefresh = Convert.ToString(dataRecord["ExcludeFromModelRefresh"])
        };
    }
}

using Microsoft.AnalysisServices.Tabular;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Json = System.Text.Json;

namespace Utils_for_PBI.Models.MetadataRows
{
    /// <summary>
    /// CalcDependencyMetadataRow represents the object fields for storing the data from the Semantic Model's DMV
    ///
    /// </summary>
    public class CalcDependencyMetadataRow
    {
        public string OBJECT_TYPE { get; set; }
        public string SOURCE_TABLE { get; set; }
        public string OBJECT { get; set; }
        public string EXPRESSION { get; set; }
        public string REFERENCED_OBJECT_TYPE { get; set; }
        public string REFERENCED_TABLE { get; set; }
        public string REFERENCED_OBJECT { get; set; }

        public CalcDependencyMetadataRow()
        {
            OBJECT_TYPE = string.Empty;
            SOURCE_TABLE = string.Empty;
            OBJECT = string.Empty;
            EXPRESSION = string.Empty;
            REFERENCED_OBJECT_TYPE = string.Empty;
            REFERENCED_TABLE = string.Empty;
            REFERENCED_OBJECT = string.Empty;
        }

        public static CalcDependencyMetadataRow MapRowToObject(IDataRecord dataRecord) => new CalcDependencyMetadataRow
        {
            OBJECT_TYPE = Convert.ToString(dataRecord["OBJECT_TYPE"]),
            SOURCE_TABLE = Convert.ToString(dataRecord["SOURCE_TABLE"]),
            OBJECT = Convert.ToString(dataRecord["OBJECT"]),
            EXPRESSION = Convert.ToString(dataRecord["EXPRESSION"]),
            REFERENCED_OBJECT_TYPE = Convert.ToString(dataRecord["REFERENCED_OBJECT_TYPE"]),
            REFERENCED_TABLE = Convert.ToString(dataRecord["REFERENCED_TABLE"]),
            REFERENCED_OBJECT = Convert.ToString(dataRecord["REFERENCED_OBJECT"])
        };
    }

}

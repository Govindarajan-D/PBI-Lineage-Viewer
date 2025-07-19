using Microsoft.AnalysisServices.Tabular;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Utils_for_PBI.Models
{
    /// <summary>
    /// Represents metadata of Power BI Measures (Retrieved from DMV)
    /// </summary>
    public class MeasuresMetadataRow
    {
        public string NAME { get; set; }
        public string EXPRESSION { get; set; }
        public string FORMAT_STRING { get; set; }
        public string IS_HIDDEN { get; set; }
        public string IS_SIMPLE_MEASURE { get; set; }
        public string DISPLAY_FOLDER { get; set; }
        public string MODIFIED_TIME { get; set; }

        public MeasuresMetadataRow()
        {
            NAME = string.Empty;
            EXPRESSION = string.Empty;
            FORMAT_STRING = string.Empty;
            IS_HIDDEN = string.Empty;
            IS_SIMPLE_MEASURE = string.Empty;
            DISPLAY_FOLDER = string.Empty;
            MODIFIED_TIME = string.Empty;
        }

        public static MeasuresMetadataRow MapRowToObject(IDataRecord dataRecord) => new MeasuresMetadataRow
        {
            NAME = Convert.ToString(dataRecord["Name"]),
            EXPRESSION = Convert.ToString(dataRecord["Expression"]),
            FORMAT_STRING = Convert.ToString(dataRecord["FormatString"]),
            IS_HIDDEN = Convert.ToString(dataRecord["IsHidden"]),
            IS_SIMPLE_MEASURE = Convert.ToString(dataRecord["IsSimpleMeasure"]),
            DISPLAY_FOLDER = Convert.ToString(dataRecord["DisplayFolder"]),
            MODIFIED_TIME = Convert.ToString(dataRecord["ModifiedTime"])
        };
    }


}

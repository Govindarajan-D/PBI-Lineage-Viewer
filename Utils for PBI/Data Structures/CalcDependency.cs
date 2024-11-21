using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils_for_PBI.Data_Structures
{
    public class CalcDependencyDataRow
    {
        public string OBJECT_TYPE { get; set; }
        public string SOURCE_TABLE { get; set; }
        public string OBJECT { get; set; }
        public string EXPRESSION { get; set; }
        public string REFERENCED_OBJECT_TYPE { get; set; }
        public string REFERENCED_TABLE { get; set; }
        public string REFERENCED_OBJECT { get; set; }

        public CalcDependencyDataRow()
        {
            OBJECT_TYPE = string.Empty;
            SOURCE_TABLE = string.Empty;
            OBJECT = string.Empty;
            EXPRESSION = string.Empty;
            REFERENCED_OBJECT_TYPE = string.Empty;
            REFERENCED_TABLE = string.Empty;
            REFERENCED_OBJECT = string.Empty;
        }
    }

    public class CalcDepedencyData
    {
        public List<CalcDependencyDataRow> calcDepedencyData = new List<CalcDependencyDataRow>();
    }
}

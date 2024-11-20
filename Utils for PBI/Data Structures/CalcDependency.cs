using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils_for_PBI.Data_Structures
{
    public class CalcDependencyRow
    {
        public string OBJECT_TYPE { get; set; }
        public string SOURCE_TABLE { get; set; }
        public string OBJECT { get; set; }
        public string EXPRESSION { get; set; }
        public string REFERENCED_OBJECT_TYPE { get; set; }
        public string REFERENCED_TABLE { get; set; }
        public string REFERENCED_OBJECT { get; set; }

        public CalcDependencyRow()
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

    public class CalcDepedencyDataset
    {
        public List<CalcDependencyRow> CalcDepedencyData = new List<CalcDependencyRow>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utils_for_PBI.Models;

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

    //TO-DO: Add colors and change weightage if required
    public class CalcDepedencyData
    {
        public List<CalcDependencyDataRow> calcDepedencyData = new List<CalcDependencyDataRow>();

        public void ParseIntoJSON()
        {
            var result = calcDepedencyData.Where(c => !c.SOURCE_TABLE.Contains("DateTableTemplate"))
                                          .Select(r => new
                                          {
                                              data = new
                                              {
                                                  name = r.OBJECT,
                                                  faveColor = r.OBJECT_TYPE.ToUpper() switch
                                                  {
                                                      "CALC_COLUMN" => "#",
                                                      "MEASURE" => "#"
                                                      _ => "#"
                                                  },
                                                  faveShape = "rectangle"
                                              }
                                          });

            string dependencyJSON = JsonSerializer.Serialize(result);
        }
    }
}

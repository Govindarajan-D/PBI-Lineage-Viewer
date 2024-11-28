using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utils_for_PBI.Models;

namespace Utils_for_PBI.Data_Structures
{
    /// <summary>
    /// CalcDependencyDataRow represents the object fields for storing the data from the Semantic Model's DMV
    ///
    /// </summary>
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

    /// <summary>
    /// CalcDependencyData is a list of CalcDependencyRow. Effectively it is a table of rows which is used to process DMV data
    /// </summary>
    //TO-DO: Add colors and change weightage if required
    public class CalcDepedencyData
    {
        public List<CalcDependencyDataRow> calcDepedencyData = new List<CalcDependencyDataRow>();
        public string dependencyNodesJSON, dependencyEdgesJSON;

        /// <summary>
        /// Converts the List of Rows (CalcDependencyData) into a particular JSON string which is acceptable by the JS script for lineage
        /// LINQ is used to process the data into a json object for nodes and links which will be used in the frontend for lineage
        /// </summary>
        public void ParseIntoJSON()
        {
            var cleansedDependencyData = calcDepedencyData.Where(c => !c.SOURCE_TABLE.Contains("DateTableTemplate"));
            var objectNodes = cleansedDependencyData.Select(c => new
                                                {
                                                    c.OBJECT,
                                                    c.OBJECT_TYPE
                                                }).Distinct();

            var refObjectNodes = cleansedDependencyData.Select(c => new
                                                    {
                                                        OBJECT = c.REFERENCED_OBJECT,
                                                        OBJECT_TYPE = c.REFERENCED_OBJECT_TYPE
                                                    }).Distinct();
            var allNodes = objectNodes.Union(refObjectNodes).Distinct();

            var nodesJSON = allNodes.Select(r => new
                                    {
                                        data = new
                                        {
                                            id = r.OBJECT,
                                            name = r.OBJECT,
                                            faveColor = r.OBJECT_TYPE.ToUpper() switch
                                            {
                                                "CALC_COLUMN" => "#26b1ff",
                                                "MEASURE" => "#26b1cc",
                                                _ => "#2613ff"
                                            },
                                            faveShape = "rectangle"
                                        }
                                    });

            var edgesJSON = cleansedDependencyData.Select(c => new
                                                    {
                                                        data = new
                                                        {
                                                            source = c.REFERENCED_OBJECT,
                                                            target = c.OBJECT,
                                                            faveColor = "#5c658d",
                                                            strength = 60
                                                        }

                                                    });

            dependencyNodesJSON = JsonSerializer.Serialize(nodesJSON, new JsonSerializerOptions { WriteIndented = true});
            dependencyEdgesJSON = JsonSerializer.Serialize(edgesJSON, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}

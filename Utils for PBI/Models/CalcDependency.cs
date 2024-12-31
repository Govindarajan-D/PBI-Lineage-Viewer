using Microsoft.AnalysisServices.Tabular;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Json = System.Text.Json;
using System.Threading.Tasks;
using Utils_for_PBI.Services;

namespace Utils_for_PBI.Models
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
        public string dependencyNodesJSON, dependencyEdgesJSON, nodesInfoJSON, objectTypeInfoJSON;

        /// <summary>
        /// Converts the List of Rows (CalcDependencyData) into a particular JSON string which is acceptable by the JS script for lineage
        /// LINQ is used to process the data into a json object for nodes and links which will be used in the frontend for lineage
        /// </summary>
        
        //TO-DO: Add a relationship for Column to Table (For e.g. Amount and Year should have base as predecessor
        //TO-DO: A measure when it uses a column from a table, has the table as a dependency as well. This should be handled in the code. 
        public void ParseIntoJSON()
        {
            List<String> objectTypeNotInFilter = new List<String> { "HIERARCHY", "ATTRIBUTE_HIERARCHY", "ACTIVE_RELATIONSHIP", "RELATIONSHIP" };
            var cleansedDependencyData = calcDepedencyData.Where(c => !c.SOURCE_TABLE.Contains("DateTable"))
                                                          .Where(e => !e.OBJECT_TYPE.Contains("ACTIVE_RELATIONSHIP") && !e.OBJECT_TYPE.Contains("RELATIONSHIP"));
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
                                                _ => "#1376ff"
                                            },
                                            objectType = r.OBJECT_TYPE.ToUpper(),
                                            faveShape = "rectangle"
                                        }
                                    });

            var edgesJSON = cleansedDependencyData.Where(c => c.REFERENCED_OBJECT_TYPE.ToUpper() != "TABLE" || (c.OBJECT_TYPE.ToUpper() == "CALC_TABLE" && c.REFERENCED_OBJECT_TYPE.ToUpper() == "TABLE"))
                                                  .Select(c => new
                                                    {
                                                        data = new
                                                        {
                                                            source = c.REFERENCED_OBJECT,
                                                            target = c.OBJECT,
                                                            faveColor = "#5c658d",
                                                            strength = 60
                                                        }

                                                    });

            var refTableEdges = cleansedDependencyData.Where(c => c.REFERENCED_OBJECT_TYPE.ToUpper() != "TABLE" || (c.OBJECT_TYPE.ToUpper() == "CALC_TABLE" && c.REFERENCED_OBJECT_TYPE.ToUpper() == "TABLE")) 
                                                      .Select(c => new
                                                        {
                                                            data = new
                                                            {
                                                                source = c.REFERENCED_TABLE,
                                                                target = c.REFERENCED_OBJECT,
                                                                faveColor = "#5c658d",
                                                                strength = 60
                                                            }
                                                        });
            edgesJSON = edgesJSON.Union(refTableEdges).Distinct();

            var nodesInfo = allNodes.Select(c => new
                                            {
                                               id = c.OBJECT,
                                               name = c.OBJECT,
                                                objectTypeID = c.OBJECT_TYPE.ToUpper()
                                            });

            var objectTypeInfo = allNodes.Select(c => new
                                            {
                                               objectTypeID = c.OBJECT_TYPE.ToUpper()
                                            }).Distinct().Select(r => new
                                            {
                                                r.objectTypeID,
                                                objectTypeName = r.objectTypeID switch
                                                {
                                                    "CALC_COLUMN" => "Calculated Column",
                                                    "MEASURE" => "Measure",
                                                    "TABLE" => "Table",
                                                    "COLUMN" => "Column",
                                                    _ => "Others"
                                                }
                                            });

            objectTypeInfoJSON = Json.JsonSerializer.Serialize(objectTypeInfo, new Json.JsonSerializerOptions { WriteIndented = true });
            nodesInfoJSON = Json.JsonSerializer.Serialize(nodesInfo, new Json.JsonSerializerOptions { WriteIndented = true });
            dependencyNodesJSON = Json.JsonSerializer.Serialize(nodesJSON, new Json.JsonSerializerOptions { WriteIndented = true});
            dependencyEdgesJSON = Json.JsonSerializer.Serialize(edgesJSON, new Json.JsonSerializerOptions { WriteIndented = true });
        }
    }
}

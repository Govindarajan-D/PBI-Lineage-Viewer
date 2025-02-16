using System;
using System.Collections.Generic;
using System.Linq;
using Json = System.Text.Json;

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
            List<String> objectTypeNotInFilter = new List<String> { "HIERARCHY", "ATTRIBUTE_HIERARCHY", "ACTIVE_RELATIONSHIP", "RELATIONSHIP", "PARTITION", "M_EXPRESSION"};
            var cleansedDependencyData = calcDepedencyData.Where(c => !c.SOURCE_TABLE.Contains("DateTable"))
                                                          .Where(c => !objectTypeNotInFilter.Contains(c.OBJECT_TYPE) && !objectTypeNotInFilter.Contains(c.REFERENCED_OBJECT_TYPE))
                                                          .Where(e => !e.OBJECT_TYPE.Contains("ACTIVE_RELATIONSHIP") && !e.OBJECT_TYPE.Contains("RELATIONSHIP") && !e.REFERENCED_OBJECT_TYPE.Contains("ACTIVE_RELATIONSHIP") && !e.REFERENCED_OBJECT_TYPE.Contains("RELATIONSHIP"));
            
            // Object Nodes and Ref Object Nodes are Objects (MEASURE, CALC COLUMN, COLUMN)

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

            // Tables are the source and referenced tables. This information will not be available in the object & refobj nodes
            var tables =  cleansedDependencyData.Select(c => new
            {
                OBJECT = c.SOURCE_TABLE,
                OBJECT_TYPE = "TABLE"
            }).Distinct();

            var refTables = cleansedDependencyData.Select(c => new
            {
                OBJECT = c.REFERENCED_TABLE,
                OBJECT_TYPE = "TABLE"
            }).Distinct();

            // All objects and tables are combined to get the nodes
            var allTables = tables.Union(refTables).Distinct();

            var allNodes = objectNodes.Union(refObjectNodes).Union(allTables).Distinct();

            // Nodes are then expanded with more information
            var nodesJSON = allNodes.Select(r => new
                                    {
                                        data = new
                                        {
                                            id = r.OBJECT,
                                            name = r.OBJECT,
                                            faveColor = r.OBJECT_TYPE.ToUpper() switch
                                            {
                                                "CALC_COLUMN" => "#a2d9ce",
                                                "MEASURE" => "#f9e79f",
                                                "TABLE" => "#aed6f1",
                                                "COLUMN" => "#d7bde2",
                                                _ => "#1376ff"
                                            },
                                            objectType = r.OBJECT_TYPE.ToUpper(),
                                            faveShape = "rectangle"
                                        }
                                    });

            // Edges are created between the nodes. 
            
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
            var tableEdges = cleansedDependencyData.Where(c => c.REFERENCED_OBJECT_TYPE.ToUpper() != "TABLE" || (c.OBJECT_TYPE.ToUpper() == "CALC_TABLE" && c.REFERENCED_OBJECT_TYPE.ToUpper() == "TABLE"))
                                                      .Select(c => new
                                                      {
                                                          data = new
                                                          {
                                                              source = c.SOURCE_TABLE,
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

            edgesJSON = edgesJSON.Union(tableEdges).Union(refTableEdges).Distinct();

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

using Microsoft.AnalysisServices.Tabular;
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
        public string nodesInfoJSON, objectTypeInfoJSON, svelte_flow_nodes_json, svelte_flow_edges_json ;

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
            
            // Object Nodes are Objects (MEASURE, CALC COLUMN, COLUMN)

            var objectNodes = cleansedDependencyData.SelectMany(c => new[]
                                                {
                                                    new {c.OBJECT,c.OBJECT_TYPE },
                                                    new {OBJECT = c.REFERENCED_OBJECT, OBJECT_TYPE = c.REFERENCED_OBJECT_TYPE}

                                                }).Distinct();

            // Tables are the source and referenced tables. This information will not be available in the object & refobj nodes
            var allTables = cleansedDependencyData.SelectMany(c => new[]
                                                        {
                                                            c.SOURCE_TABLE,
                                                            c.REFERENCED_TABLE
                                                        })
                                                        .Distinct()
                                                        .Select(tableName => new
                                                        {
                                                            OBJECT = tableName,
                                                            OBJECT_TYPE = "TABLE"
                                                        });

            var allNodes = objectNodes.Union(allTables).Distinct();

            var svelteflow_nodes = allNodes.Select(r => new
                                                {
                                                    id = r.OBJECT,
                                                    type = "selectorNode",
                                                    data = new
                                                    {
                                                        CalcName = r.OBJECT,
                                                        CalcType = r.OBJECT_TYPE.ToUpper() switch
                                                        {
                                                            "CALC_COLUMN" => "Calc Column",
                                                            "MEASURE" => "Measure",
                                                            "TABLE" => "Table",
                                                            "CALC_TABLE" => "Calc Table",
                                                            "COLUMN" => "Column",
                                                            _ => "Others"
                                                        }
                                                    },
                                                    position = new {
                                                        x = 0,
                                                        y = 0
                                                    }
                                                });

            var edgesReferenceData = cleansedDependencyData.Where(c => c.REFERENCED_OBJECT_TYPE.ToUpper() != "TABLE" || (c.OBJECT_TYPE.ToUpper() == "CALC_TABLE" && c.REFERENCED_OBJECT_TYPE.ToUpper() == "TABLE"));

            var svelteflow_edges = edgesReferenceData.SelectMany(c => new[]
                                                                {
                                                                     new { source = c.REFERENCED_OBJECT, target = c.OBJECT },
                                                                     new { source = c.SOURCE_TABLE, target = c.OBJECT },
                                                                     new { source = c.REFERENCED_TABLE, target = c.REFERENCED_OBJECT }
                                                                })
                                                                .Select(c => new
                                                                {
                                                                    id = c.source + c.target + "",
                                                                    source = c.source,
                                                                    target = c.target,
                                                                    type = "bezier",
                                                                    animated = true
                                                                }).Distinct();
                                                                
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
            svelte_flow_nodes_json = Json.JsonSerializer.Serialize(svelteflow_nodes, new Json.JsonSerializerOptions { WriteIndented = true });
            svelte_flow_edges_json = Json.JsonSerializer.Serialize(svelteflow_edges, new Json.JsonSerializerOptions { WriteIndented = true });
        }
    }
}

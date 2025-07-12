using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AnalysisServices.Tabular;
using Json = System.Text.Json;
using Utils_for_PBI.Models;
using log4net.Repository.Hierarchy;
using System.Windows.Forms;
using log4net;
using Utils_for_PBI.Server;
using System.Runtime.Versioning;

namespace Utils_for_PBI.Services
{

    /// <summary>
    /// ModelMetadata is a list of CalcDependencyRow. Effectively it is a table of rows which is used to process DMV data
    /// </summary>
    //TO-DO: Add colors and change weightage if required
    [SupportedOSPlatform("windows")]
    public class ModelMetadata
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UtilsPBIHTTPServer));

        private List<CalcDependencyMetadataRow> _calcDependencyMetadata = new List<CalcDependencyMetadataRow>();
        private List<MeasuresMetadataRow> _measuresMetadataRows = new List<MeasuresMetadataRow>();
        private bool _preprocessStepsDone = false;

        private IEnumerable<CalcDependencyMetadataRow> _cleansedData;
        private IEnumerable<(string OBJECT, string OBJECT_TYPE)> _allNodes;


        //TO-DO: Add a relationship for Column to Table (For e.g. Amount and Year should have base as predecessor
        //TO-DO: A measure when it uses a column from a table, has the table as a dependency as well. This should be handled in the code.


        public void CalcDependencyMetadataAddRow(CalcDependencyMetadataRow row)
        {
            if (row != null)
            {
                _calcDependencyMetadata.Add(row);
            }

            _preprocessStepsDone = false;
        }

        public void CalcDependencyMetadataAddRows(IEnumerable<CalcDependencyMetadataRow> rows)
        {
            if (rows != null)
            {
                _calcDependencyMetadata.AddRange(rows);
            }
            _preprocessStepsDone = false;
        }

        public void MeasuresMetadataAddRow(MeasuresMetadataRow row)
        {
            if (row != null)
            {
                _measuresMetadataRows.Add(row);
            }

            _preprocessStepsDone = false;
        }

        public void MeasuresMetadataAddRows(IEnumerable<MeasuresMetadataRow> rows)
        {
            if (rows != null)
            {
                _measuresMetadataRows.AddRange(rows);
            }
            _preprocessStepsDone = false;
        }

        public void PopulateModelMetadata(AdomdConnection adomdConnection)
        {
            try
            {
                string dependencySQLQuery = @"SELECT OBJECT_TYPE, [TABLE] AS SOURCE_TABLE, OBJECT, EXPRESSION, REFERENCED_OBJECT_TYPE, REFERENCED_TABLE, REFERENCED_OBJECT FROM $SYSTEM.DISCOVER_CALC_DEPENDENCY";
                var dependencies = adomdConnection.ExecuteQuery<CalcDependencyMetadataRow>(adomdConnection.connection, dependencySQLQuery, CalcDependencyMetadataRow.MapRowToObject);
                this.CalcDependencyMetadataAddRows(dependencies);


                string measureMetadataSQLQuery = @"SELECT [Name], [Expression], FormatString, IsHidden, IsSimpleMeasure, DisplayFolder, ModifiedTime FROM $SYSTEM.TMSCHEMA_MEASURES";
                var measures = adomdConnection.ExecuteQuery<MeasuresMetadataRow>(adomdConnection.connection, measureMetadataSQLQuery, MeasuresMetadataRow.MapRowToObject);
                this.MeasuresMetadataAddRows(measures);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                MessageBox.Show($"Error: {ex.Message}", "Error executing ADOMD command", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                adomdConnection.Close();

            }


        }

        private IEnumerable<CalcDependencyMetadataRow> CleanData(IEnumerable<CalcDependencyMetadataRow> rows)
        {
            List<String> objectTypeNotInFilter = new List<String> { "HIERARCHY", "ATTRIBUTE_HIERARCHY", "ACTIVE_RELATIONSHIP", "RELATIONSHIP", "PARTITION", "M_EXPRESSION" };

            var cleansedData = rows.Where(c => !c.SOURCE_TABLE.Contains("DateTable"))
                                                          .Where(c => !objectTypeNotInFilter.Contains(c.OBJECT_TYPE) && !objectTypeNotInFilter.Contains(c.REFERENCED_OBJECT_TYPE))
                                                          .Where(e => !e.OBJECT_TYPE.Contains("ACTIVE_RELATIONSHIP") && !e.OBJECT_TYPE.Contains("RELATIONSHIP") && !e.REFERENCED_OBJECT_TYPE.Contains("ACTIVE_RELATIONSHIP") && !e.REFERENCED_OBJECT_TYPE.Contains("RELATIONSHIP"));

            return cleansedData;

        }

        private IEnumerable<(string OBJECT, string OBJECT_TYPE)> GetAllNodes(IEnumerable<CalcDependencyMetadataRow> rows)
        {

            // Object Nodes are Objects (MEASURE, CALC COLUMN, COLUMN)

            var objectNodes = rows.SelectMany(c => new[]
                                                {
                                                     (c.OBJECT,c.OBJECT_TYPE ),
                                                     (c.REFERENCED_OBJECT, c.REFERENCED_OBJECT_TYPE)

                                                }).Distinct();

            // Tables are the source and referenced tables. This information will not be available in the object & refobj nodes
            var allTables = rows.SelectMany(c => new[]
                                                        {
                                                            c.SOURCE_TABLE,
                                                            c.REFERENCED_TABLE
                                                        })
                                                        .Distinct()
                                                        .Select(tableName =>
                                                        (
                                                            OBJECT: tableName,
                                                            OBJECT_TYPE: "TABLE"
                                                        ));

            // Marks that the predefined steps are done to avoid reprocessing
            _preprocessStepsDone = true;

            return objectNodes.Union(allTables).Distinct();
        }

        /// <summary>
        /// Converts the List of Rows (ModelMetadata) into SvelteFlow Nodes compatible format
        /// </summary>
        public string GetSvelteFlowNodesJson()
        {
            if (_preprocessStepsDone == false)
            {
                _cleansedData = CleanData(_calcDependencyMetadata);
                _allNodes = GetAllNodes(_cleansedData);
            }

            var svelteflowNodes = _allNodes.Select(r => new
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
                position = new
                {
                    x = 0,
                    y = 0
                }
            });
            return Json.JsonSerializer.Serialize(svelteflowNodes, new Json.JsonSerializerOptions { WriteIndented = true });
        }

        /// <summary>
        /// Converts the List of Rows (ModelMetadata) into SvelteFlow Edges compatible format
        /// </summary>
        public string GetSvelteFlowEdgesJson()
        {
            if (_preprocessStepsDone == false)
            {
                _cleansedData = CleanData(_calcDependencyMetadata);
                _allNodes = GetAllNodes(_cleansedData);
            }

            var edgesReferenceData = _cleansedData.Where(c => c.REFERENCED_OBJECT_TYPE.ToUpper() != "TABLE" || (c.OBJECT_TYPE.ToUpper() == "CALC_TABLE" && c.REFERENCED_OBJECT_TYPE.ToUpper() == "TABLE"));

            var svelteflowEdges = edgesReferenceData.SelectMany(c => new[]
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
            return Json.JsonSerializer.Serialize(svelteflowEdges, new Json.JsonSerializerOptions { WriteIndented = true });
        }

        public string GetNodesInfo()
        {
            if (_preprocessStepsDone == false)
            {
                _cleansedData = CleanData(_calcDependencyMetadata);
                _allNodes = GetAllNodes(_cleansedData);
            }

            var nodesInfo = _allNodes.Select(c => new
            {
                id = c.OBJECT,
                name = c.OBJECT,
                objectTypeID = c.OBJECT_TYPE.ToUpper()
            });

            return Json.JsonSerializer.Serialize(nodesInfo, new Json.JsonSerializerOptions { WriteIndented = true });
        }

        public string GetObjectTypeInfo()
        {
            if (_preprocessStepsDone == false)
            {
                _cleansedData = CleanData(_calcDependencyMetadata);
                _allNodes = GetAllNodes(_cleansedData);
            }

            var objectTypeInfo = _allNodes.Select(c => new
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

            return Json.JsonSerializer.Serialize(objectTypeInfo, new Json.JsonSerializerOptions { WriteIndented = true });
        }
    }
}

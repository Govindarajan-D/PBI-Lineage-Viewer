using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils_for_PBI.Models.MetadataRows;
using Utils_for_PBI.Models.SerializationModel;
using static System.Windows.Forms.DataFormats;
using static Utils_for_PBI.Models.MetadataRows.ColumnsMetadataRow;
using static Utils_for_PBI.Models.MetadataRows.TablesMetadataRow;
using SysJson = System.Text.Json;

namespace Utils_for_PBI.Services
{
    public partial class ModelMetadata
    {
        /// <summary>
        /// Cleans the data by removing unnecessary objects not required for the lineage graph
        /// </summary>
        private IEnumerable<CalcDependencyMetadataRow> CleanData(IEnumerable<CalcDependencyMetadataRow> rows)
        {
            List<String> objectTypeNotInFilter = new List<String> { "HIERARCHY", "ATTRIBUTE_HIERARCHY", "ACTIVE_RELATIONSHIP", "RELATIONSHIP", "PARTITION", "M_EXPRESSION" };

            var cleansedData = rows.Where(c => !c.SOURCE_TABLE.Contains("DateTable"))
                                                          .Where(c => !objectTypeNotInFilter.Contains(c.OBJECT_TYPE) && !objectTypeNotInFilter.Contains(c.REFERENCED_OBJECT_TYPE))
                                                          .Where(e => !e.OBJECT_TYPE.Contains("ACTIVE_RELATIONSHIP") && !e.OBJECT_TYPE.Contains("RELATIONSHIP") && !e.REFERENCED_OBJECT_TYPE.Contains("ACTIVE_RELATIONSHIP") && !e.REFERENCED_OBJECT_TYPE.Contains("RELATIONSHIP"));

            return cleansedData;

        }

        /// <summary>
        /// Gets all the nodes (OBJECTS, OBJECT_TYPE) from the CalcDependencyMetadataRow data. 
        /// The nodes are nothing but objects like Measure, Calculated column, Columns
        /// </summary>

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
                _cleansedData = CleanData(_calcDependencyMetadataRows);
                _allNodes = GetAllNodes(_cleansedData);
            }

            /* We generate the data according to the Svelte Node configuration required
             * https://svelteflow.dev/api-reference/types/node
             * The necessary data for lineage graph is generated within 'data' field
             * and any other additional data is generated in the AdditionalData field within the data
             */

            var svelteflowNodesAddlData = _allNodes.GroupJoin(
                                                _measuresMetadataRows,
                                                node => node.OBJECT,
                                                measure => measure.NAME,
                                                (node, measure) => new { node, measure }
                                             )
                                            .SelectMany(
                                               x => x.measure.DefaultIfEmpty(),
                                               (x, measure) => new
                                               {
                                                   id = x.node.OBJECT,
                                                   type = "selectorNode",
                                                   data = new
                                                   {
                                                       CalcName = x.node.OBJECT,
                                                       CalcType = x.node.OBJECT_TYPE.ToUpper() switch
                                                       {
                                                           "CALC_COLUMN" => "Calc Column",
                                                           "MEASURE" => "Measure",
                                                           "TABLE" => "Table",
                                                           "CALC_TABLE" => "Calc Table",
                                                           "COLUMN" => "Column",
                                                           _ => "Others"
                                                       },
                                                       AdditionalData = new
                                                       {
                                                           Name = measure?.NAME ?? "N/A",
                                                           Expression = measure?.EXPRESSION ?? "N/A",
                                                           DisplayFolder = measure?.DISPLAY_FOLDER ?? "N/A",
                                                           FormatString = measure?.FORMAT_STRING ?? "N/A",
                                                           IsHidden = measure?.IS_HIDDEN,
                                                           IsSimpleMeasure = measure?.IS_SIMPLE_MEASURE,
                                                           ModifiedTime = measure?.MODIFIED_TIME ?? "N/A"

                                                       }
                                                   },
                                                   position = new
                                                   {
                                                       x = 0,
                                                       y = 0
                                                   }
                                               }
                                            );
            return SysJson.JsonSerializer.Serialize(svelteflowNodesAddlData, new SysJson.JsonSerializerOptions { WriteIndented = true });
        }

        /// <summary>
        /// Converts the List of Rows (ModelMetadata) into SvelteFlow Edges compatible format
        /// </summary>
        public string GetSvelteFlowEdgesJson()
        {
            if (_preprocessStepsDone == false)
            {
                _cleansedData = CleanData(_calcDependencyMetadataRows);
                _allNodes = GetAllNodes(_cleansedData);
            }

           /* We generate the data according to the Svelte Edge configuration required
             * https://svelteflow.dev/api-reference/types/edge
             */

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
            return SysJson.JsonSerializer.Serialize(svelteflowEdges, new SysJson.JsonSerializerOptions { WriteIndented = true });
        }

        public string GetNodesInfo()
        {
            if (_preprocessStepsDone == false)
            {
                _cleansedData = CleanData(_calcDependencyMetadataRows);
                _allNodes = GetAllNodes(_cleansedData);
            }

            var nodesInfo = _allNodes.Select(c => new
                                    {
                                        id = c.OBJECT,
                                        name = c.OBJECT,
                                        objectTypeID = c.OBJECT_TYPE.ToUpper()
                                    });

            return SysJson.JsonSerializer.Serialize(nodesInfo, new SysJson.JsonSerializerOptions { WriteIndented = true });
        }

        public string GetObjectTypeInfo()
        {
            if (_preprocessStepsDone == false)
            {
                _cleansedData = CleanData(_calcDependencyMetadataRows);
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

            return SysJson.JsonSerializer.Serialize(objectTypeInfo, new SysJson.JsonSerializerOptions { WriteIndented = true });
        }

        /// <summary>
        /// Converts the List of Rows (TableMetadata) into Table JSON String
        /// </summary>

        public string GetTablesInfo()
        {
            var tablesInfo = _tablesMetadataRows.Select(c => new TableMetadataDTO
            {
                                                        Id = c.ID,
                                                        Name = c.NAME,
                                                        DataCategory = c.DATA_CATEGORY,
                                                        IsHidden = c.IS_HIDDEN,
                                                        ModifiedTime = c.MODIFIED_TIME,
                                                        StructureModifiedTime = c.STRUCTURE_MODIFIED_TIME,
                                                        SystemFlags = ((SystemFlagsEnum)c.SYSTEM_FLAGS).GetEnumDescription(),
                                                        CalculationGroupId = c.CALCULATION_GROUP_ID,
                                                        ExcludeFromModelRefresh = c.EXCLUDE_FROM_MODEL_REFRESH
                                                    });

            return SysJson.JsonSerializer.Serialize(tablesInfo, new SysJson.JsonSerializerOptions { WriteIndented = true });
        }

        /// <summary>
        /// Converts the List of Rows (ColumnMetadata) into Column JSON String
        /// </summary>

        public string GetColumnsInfo()
        {
            var columnsInfo = _columnsMetadataRows.Select(c => new ColumnMetadataDTO
            {
                                                    Id = c.ID,
                                                    TableId = c.TABLE_ID,
                                                    ExplicitName = c.EXPLICIT_NAME,
                                                    InferredName = c.INFERRED_NAME,
                                                    ExplicitDataType = ((DataTypeEnum)c.EXPLICIT_DATA_TYPE).GetEnumDescription(),
                                                    InferredDataType = ((DataTypeEnum)c.INFERRED_DATA_TYPE).GetEnumDescription(),
                                                    Description = c.DESCRIPTION,
                                                    IsHidden = c.IS_HIDDEN,
                                                    IsUnique = c.IS_UNIQUE,
                                                    IsKey = c.IS_KEY,
                                                    IsNullable = c.IS_NULLABLE,
                                                    SummarizeBy = ((SummarizeByEnum)c.SUMMARIZE_BY).GetEnumDescription(),
                                                    Type = c.TYPE,
                                                    Expression = c.EXPRESSION,
                                                    IsAvailableInMDX = c.IS_AVAILABLE_IN_MDX,
                                                    SortByColumnId = c.SORT_BY_COLUMN_ID,
                                                    ModifiedTime = c.MODIFIED_TIME,
                                                    StructureModifiedTime = c.STRUCTURE_MODIFIED_TIME,
                                                    RefreshedTime = c.REFRESHED_TIME,
                                                    SystemFlags = c.SYSTEM_FLAGS
                                                });
            return SysJson.JsonSerializer.Serialize(columnsInfo, new SysJson.JsonSerializerOptions { WriteIndented = true });
        }
    }
}

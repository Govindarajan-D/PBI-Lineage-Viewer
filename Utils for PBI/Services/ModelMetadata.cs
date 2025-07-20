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
using PowerBIConnections.Connections;

namespace Utils_for_PBI.Services
{

    /// <summary>
    /// ModelMetadata is a list of CalcDependencyRow and other Metadata objects. 
    /// Effectively it is a list of tables which is used to process DMV data
    /// </summary>
    //TO-DO: Add colors and change weightage if required
    [SupportedOSPlatform("windows")]
    public partial class ModelMetadata
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ModelMetadata));

        private List<CalcDependencyMetadataRow> _calcDependencyMetadataRows = new List<CalcDependencyMetadataRow>();
        private List<MeasuresMetadataRow> _measuresMetadataRows = new List<MeasuresMetadataRow>();
        private List<TablesMetadataRow> _tablesMetadataRows = new List<TablesMetadataRow>();
        private List<ColumnsMetadataRow> _columnsMetadataRows = new List<ColumnsMetadataRow>();
        private bool _preprocessStepsDone = false;

        private IEnumerable<CalcDependencyMetadataRow> _cleansedData;
        private IEnumerable<(string OBJECT, string OBJECT_TYPE)> _allNodes;


        //TO-DO: Add a relationship for Column to Table (For e.g. Amount and Year should have base as predecessor
        //TO-DO: A measure when it uses a column from a table, has the table as a dependency as well. This should be handled in the code.

        /// <summary>
        /// Adds row to the List of CalcDependencyMetadataRow.
        /// </summary>
        public void CalcDependencyMetadataAddRow(CalcDependencyMetadataRow row)
        {
            if (row != null)
            {
                _calcDependencyMetadataRows.Add(row);
            }

            _preprocessStepsDone = false;
        }
        /// <summary>
        /// Add rows in batch to the List of CalcDependencyMetadataRow.
        /// </summary>
        public void CalcDependencyMetadataAddRows(IEnumerable<CalcDependencyMetadataRow> rows)
        {
            if (rows != null)
            {
                _calcDependencyMetadataRows.AddRange(rows);
            }
            _preprocessStepsDone = false;
        }
        /// <summary>
        /// Adds row to the List of MeasuresMetadata.
        /// </summary>
        public void MeasuresMetadataAddRow(MeasuresMetadataRow row)
        {
            if (row != null)
            {
                _measuresMetadataRows.Add(row);
            }

            _preprocessStepsDone = false;
        }
        /// <summary>
        /// Add rows in batch to the List of MeasuresMetadataRow.
        /// </summary>
        public void MeasuresMetadataAddRows(IEnumerable<MeasuresMetadataRow> rows)
        {
            if (rows != null)
            {
                _measuresMetadataRows.AddRange(rows);
            }
            _preprocessStepsDone = false;
        }

        public void TablesMetadataAddRow(TablesMetadataRow row)
        {
            if (row != null)
            {
                _tablesMetadataRows.Add(row);
            }

            _preprocessStepsDone = false;
        }
        /// <summary>
        /// Add rows in batch to the List of TablesMetadataRow.
        /// </summary>
        public void TablesMetadataAddRows(IEnumerable<TablesMetadataRow> rows)
        {
            if (rows != null)
            {
                _tablesMetadataRows.AddRange(rows);
            }
            _preprocessStepsDone = false;
        }

        public void ColumnsMetadataAddRow(ColumnsMetadataRow row)
        {
            if (row != null)
            {
                _columnsMetadataRows.Add(row);
            }

            _preprocessStepsDone = false;
        }
        /// <summary>
        /// Add rows in batch to the List of TablesMetadataRow.
        /// </summary>
        public void ColumnsMetadataAddRows(IEnumerable<ColumnsMetadataRow> rows)
        {
            if (rows != null)
            {
                _columnsMetadataRows.AddRange(rows);
            }
            _preprocessStepsDone = false;
        }

        /// <summary>
        /// Populates the ModelMetadata with data from the connectionInfo. Contains queries to fetch the metadata for calculation dependencies and measures. 
        /// </summary>
        public void PopulateModelMetadata(DatasetConnection connectionInfo)
        {
            try
            {
                //TO-DO: Simple blocking parallelism using Task. Can be converted to Async
                var dependenciesTask = Task.Run(() => 
                        AdomdConnection.ExecuteQuery<CalcDependencyMetadataRow>(
                            connectionInfo, 
                            ModelMetadataQueries.DependencyQuery, 
                            CalcDependencyMetadataRow.MapRowToObject)
                        );
                var measuresTask = Task.Run(() =>
                    AdomdConnection.ExecuteQuery<MeasuresMetadataRow>(
                        connectionInfo, 
                        ModelMetadataQueries.MeasureMetadataQuery, 
                        MeasuresMetadataRow.MapRowToObject)
                    );

                var tablesTask = Task.Run(() =>
                    AdomdConnection.ExecuteQuery<TablesMetadataRow>(
                        connectionInfo, 
                        ModelMetadataQueries.TableMetadataQuery,
                        TablesMetadataRow.MapRowToObject)
                    );

                var columnsTask = Task.Run(() =>
                    AdomdConnection.ExecuteQuery<ColumnsMetadataRow>(
                        connectionInfo,
                        ModelMetadataQueries.ColumnMetadataQuery,
                        ColumnsMetadataRow.MapRowToObject)
                    );


                Task.WaitAll(dependenciesTask, measuresTask, tablesTask, columnsTask);

                if (dependenciesTask.Result == null ||
                    measuresTask.Result == null ||
                    tablesTask.Result == null || 
                    columnsTask.Result == null)
                {
                    throw new Exception("Failed to retrieve data from ADOMD connection.");
                }

                this.CalcDependencyMetadataAddRows(dependenciesTask.Result);
                this.MeasuresMetadataAddRows(measuresTask.Result);
                this.TablesMetadataAddRows(tablesTask.Result);
                this.ColumnsMetadataAddRows(columnsTask.Result);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                MessageBox.Show($"Error: {ex.Message}", "Error executing ADOMD command", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

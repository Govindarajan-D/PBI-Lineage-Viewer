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
    /// ModelMetadata is a list of CalcDependencyRow and other Metadata objects. 
    /// Effectively it is a list of tables which is used to process DMV data
    /// </summary>
    //TO-DO: Add colors and change weightage if required
    [SupportedOSPlatform("windows")]
    public partial class ModelMetadata
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UtilsPBIHTTPServer));

        private List<CalcDependencyMetadataRow> _calcDependencyMetadataRows = new List<CalcDependencyMetadataRow>();
        private List<MeasuresMetadataRow> _measuresMetadataRows = new List<MeasuresMetadataRow>();
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

        /// <summary>
        /// Populates the ModelMetadata with data from the ADOMD connection. Contains queries to fetch the metadata for calculation dependencies and measures. 
        /// </summary>
        public void PopulateModelMetadata(AdomdConnection adomdConnection)
        {
            try
            {
                // Query for retrieving the CalcDependency Results
                string dependencySQLQuery = @"SELECT OBJECT_TYPE, [TABLE] AS SOURCE_TABLE, OBJECT, EXPRESSION, REFERENCED_OBJECT_TYPE, REFERENCED_TABLE, REFERENCED_OBJECT FROM $SYSTEM.DISCOVER_CALC_DEPENDENCY";
                var dependencies = adomdConnection.ExecuteQuery<CalcDependencyMetadataRow>(adomdConnection.connection, dependencySQLQuery, CalcDependencyMetadataRow.MapRowToObject);
                this.CalcDependencyMetadataAddRows(dependencies);

                // Query for retrieving the Measure Metadata Results
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
    }
}

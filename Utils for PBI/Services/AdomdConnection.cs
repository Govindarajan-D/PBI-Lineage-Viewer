using log4net;
using PowerBIConnections.Connections;
using System;
using System.Data;
using System.Runtime.Versioning;
using System.Windows.Forms;
using Utils_for_PBI.Models;
using AdomdClient = Microsoft.AnalysisServices.AdomdClient;


namespace Utils_for_PBI.Services
{
    /// <summary>
    /// The Adomd Connection class establishes a adomd connection which is used to retrive the DMV data
    /// The DMV Data is stored in object of CalcDependency type. The reader enumerates records 
    /// which is mapped to CalcDependency object in the MapRowToObject() function
    ///
    /// </summary>
    [SupportedOSPlatform("windows")]
    //TO-DO: Inherit from IDisposable and add Dispose/Close method()
    public class AdomdConnection : IDisposable
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(AdomdConnection));

        public AdomdClient.AdomdConnection adomdConnection;
        public bool isConnected = false;
        public bool endAdomdSession = true;

        public void Connect(DatasetConnection datasetConnection)
        {
            try
            {
                if(datasetConnection.ConnectionType == ConnectionType.PowerBIService)
                {
                    adomdConnection = new AdomdClient.AdomdConnection($"Provider=MSOLAP;Data Source={datasetConnection.ConnectString};Initial Catalog={datasetConnection.DatabaseName}");
                }
                else
                {
                    adomdConnection = new AdomdClient.AdomdConnection("Datasource=" + datasetConnection.ConnectString);
                }
                
                
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                MessageBox.Show($"Error: {ex.Message}", "Error establishing ADOMD connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            isConnected = true;

            Logger.Info("ADOMD Connection Established");
    }

        public CalcDependencyData RetrieveCalcDependency()
        {
            String dependencySQLQuery = @"SELECT OBJECT_TYPE, [TABLE] AS SOURCE_TABLE, OBJECT, EXPRESSION, REFERENCED_OBJECT_TYPE, REFERENCED_TABLE, REFERENCED_OBJECT FROM $SYSTEM.DISCOVER_CALC_DEPENDENCY";
            CalcDependencyData calcDepedencyData = new CalcDependencyData();
            try
            {
                adomdConnection.Open();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Database is empty"))
                {
                    Logger.Warn("Database is empty, no data to retrieve.");
                    MessageBox.Show("The database is empty, no data to retrieve.", "Empty Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;
                }
                else
                {
                    Logger.Error(ex.Message);
                    MessageBox.Show($"Error: {ex.Message}", "Error establishing ADOMD connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                
            }

            AdomdClient.AdomdCommand adomdCommand = new AdomdClient.AdomdCommand(dependencySQLQuery, adomdConnection);
            AdomdClient.AdomdDataReader records;
            try
            {
                records = adomdCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                MessageBox.Show($"Error: {ex.Message}", "Error executing ADOMD command, Is the Model Empty?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }


            while (records.Read())
            {
                CalcDependencyDataRow row = MapRowToObject(records);
                calcDepedencyData.AddRow(row);

            }

            adomdCommand.Dispose();
            adomdConnection.Close();

            return calcDepedencyData;
        }

        public CalcDependencyDataRow MapRowToObject(IDataRecord dataRecord) => new CalcDependencyDataRow
        {
            OBJECT_TYPE = Convert.ToString(dataRecord["OBJECT_TYPE"]),
            SOURCE_TABLE = Convert.ToString(dataRecord["SOURCE_TABLE"]),
            OBJECT = Convert.ToString(dataRecord["OBJECT"]),
            EXPRESSION = Convert.ToString(dataRecord["EXPRESSION"]),
            REFERENCED_OBJECT_TYPE = Convert.ToString(dataRecord["REFERENCED_OBJECT_TYPE"]),
            REFERENCED_TABLE = Convert.ToString(dataRecord["REFERENCED_TABLE"]),
            REFERENCED_OBJECT = Convert.ToString(dataRecord["REFERENCED_OBJECT"])
        };

        public void Disconnect(bool endSession = true)
        {
            endAdomdSession = endSession;
            Dispose();
        }

        public void Dispose()
        {
            isConnected = false;
            adomdConnection.Close(endAdomdSession);
            GC.SuppressFinalize(this);
        }
    }
}

using log4net;
using Microsoft.AnalysisServices.AdomdClient;
using PowerBIConnections.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Versioning;
using System.Windows.Forms;
using Utils_for_PBI.Models;
using AdomdClient = Microsoft.AnalysisServices.AdomdClient;


namespace Utils_for_PBI.Services.Connections
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

        public AdomdClient.AdomdConnection connection;
        public bool isConnected = false;
        public bool endAdomdSession = true;

        public AdomdConnection()
        {
            // Default constructor for AdomdConnection
        }

        public AdomdConnection(DatasetConnection connection)
        {
            Connect(connection);
        }

        public void Connect(DatasetConnection datasetConnection)
        {
            try
            {
                if(datasetConnection.ConnectionType == ConnectionType.PowerBIService)
                {
                    connection = new AdomdClient.AdomdConnection($"Provider=MSOLAP;Data Source={datasetConnection.ConnectString};Initial Catalog={datasetConnection.DatabaseName}");
                }
                else
                {
                    connection = new AdomdClient.AdomdConnection("Datasource=" + datasetConnection.ConnectString);
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

        // Executes the query and maps the result to a list of objects using the provided mapping function
        // AdomdDataReader is a class that implements IDataRecord which allows to use MapRowToObject function to map each row to an object of type T
        public static List<T> ExecuteQuery<T>(DatasetConnection connection, string query, Func<AdomdDataReader, T> MapRowToObject)
        {
            List<T> results = new List<T>();
            var adomdConnection = new AdomdConnection(connection).connection;

            try
            {
                adomdConnection.Open();
                using (var command = new AdomdCommand(query, adomdConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(MapRowToObject(reader));
                        }
                    }
                }
            }
            finally
            {
                adomdConnection.Close();
            }

            return results;
        }

        public void Disconnect(bool endSession = true)
        {
            endAdomdSession = endSession;
            Dispose();
        }

        public void Close()
        {
            connection.Close(endAdomdSession);
        }

        public void Dispose()
        {
            isConnected = false;
            connection.Close(endAdomdSession);
            GC.SuppressFinalize(this);
        }
    }
}

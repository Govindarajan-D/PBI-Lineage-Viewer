using PowerBIConnections.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils_for_PBI.Data_Structures;
using AdomdClient = Microsoft.AnalysisServices.AdomdClient;




//TO-DO: Move it to a separate project if necessary for creating DLLs
namespace Utils_for_PBI.Models
{
    /// <summary>
    /// The Adomd Connection class establishes a adomd connection which is used to retrive the DMV data
    /// The DMV Data is stored in object of CalcDependency type. The reader enumerates records 
    /// which is mapped to CalcDependency object in the MapRowToObject() function
    ///
    /// </summary>

    //TO-DO: Inherit from IDisposable and add Dispose/Close method()
    public class AdomdConnection : IDisposable
    {
        public AdomdClient.AdomdConnection adomdConnection;
        public bool isConnected = false;

        public void Connect(DatasetConnection datasetConnection)
        {
            adomdConnection = new AdomdClient.AdomdConnection("Datasource=" + datasetConnection.ConnectString);
            isConnected = true;
    }

        public CalcDepedencyData RetrieveCalcDependency()
        {
            String dependencySQLQuery = @"SELECT OBJECT_TYPE, [TABLE] AS SOURCE_TABLE, OBJECT, EXPRESSION, REFERENCED_OBJECT_TYPE, REFERENCED_TABLE, REFERENCED_OBJECT FROM $SYSTEM.DISCOVER_CALC_DEPENDENCY";
            CalcDepedencyData calcDepedencyData = new CalcDepedencyData();
            adomdConnection.Open();
            AdomdClient.AdomdCommand adomdCommand = new AdomdClient.AdomdCommand(dependencySQLQuery, adomdConnection);
            AdomdClient.AdomdDataReader records = adomdCommand.ExecuteReader();

            while (records.Read())
            {
                CalcDependencyDataRow row = MapRowToObject(records);
                calcDepedencyData.calcDepedencyData.Add(row);

            }

            adomdCommand.Dispose();
            adomdConnection.Close();

            return calcDepedencyData;
        }

        public CalcDependencyDataRow MapRowToObject(IDataRecord dataRecord)
        {
            return new CalcDependencyDataRow
            {
                OBJECT_TYPE = Convert.ToString(dataRecord["OBJECT_TYPE"]),
                SOURCE_TABLE = Convert.ToString(dataRecord["SOURCE_TABLE"]),
                OBJECT = Convert.ToString(dataRecord["OBJECT"]),
                EXPRESSION = Convert.ToString(dataRecord["EXPRESSION"]),
                REFERENCED_OBJECT_TYPE = Convert.ToString(dataRecord["REFERENCED_OBJECT_TYPE"]),
                REFERENCED_TABLE = Convert.ToString(dataRecord["REFERENCED_TABLE"]),
                REFERENCED_OBJECT = Convert.ToString(dataRecord["REFERENCED_OBJECT"])
            };
        }

        public void Disconnect(bool endSession = true)
        {
            isConnected = false;
            Dispose(endSession);
        }

        public void Dispose()
        {
            Dispose(true);
        }
        public void Dispose(bool endSession)
        {
            
            adomdConnection.Close(endSession);
        }
    }
}

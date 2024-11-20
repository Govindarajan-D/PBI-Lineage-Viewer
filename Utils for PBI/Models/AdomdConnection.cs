using PowerBIConnections.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils_for_PBI.Data_Structures;
using AdomdClient = Microsoft.AnalysisServices.AdomdClient;

namespace Utils_for_PBI.Models
{
    public static class AdomdConnection
    {
        public static AdomdClient.AdomdConnection adomdConnection;

        public static void Connect(DatasetConnection datasetConnection)
        {
            adomdConnection = new AdomdClient.AdomdConnection("Datasource=" + datasetConnection.ConnectString);
        }

        public static CalcDepedencyDataset RetrieveCalcDependency()
        {
            CalcDepedencyDataset calcDepedencyDataset = new CalcDepedencyDataset();
            adomdConnection.Open();
            AdomdClient.AdomdCommand adomdCommand = new AdomdClient.AdomdCommand(@"SELECT OBJECT_TYPE, [TABLE] AS SOURCE_TABLE, OBJECT, EXPRESSION, REFERENCED_OBJECT_TYPE, REFERENCED_TABLE, REFERENCED_OBJECT FROM $SYSTEM.DISCOVER_CALC_DEPENDENCY", adomdConnection);
            AdomdClient.AdomdDataReader records = adomdCommand.ExecuteReader();

            while (records.Read())
            {
                CalcDependencyRow row = MapObjectToRow(records);
                calcDepedencyDataset.CalcDepedencyData.Add(row);

            }

            adomdCommand.Dispose();
            adomdConnection.Close();

            return calcDepedencyDataset;
        }

        public static CalcDependencyRow MapObjectToRow(IDataRecord dataRecord)
        {
            return new CalcDependencyRow
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
    }
}

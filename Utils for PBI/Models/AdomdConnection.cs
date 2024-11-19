using PowerBIConnections.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdomdClient = Microsoft.AnalysisServices.AdomdClient;

namespace Utils_for_PBI.Models
{
    public static class AdomdConnection
    {
        public static AdomdClient.AdomdConnection adomdConnection;

        public static void Connect(DatasetConnection datasetConnection)
        {
            adomdConnection = new AdomdClient.AdomdConnection("Datasource=" + datasetConnection.ConnectString + ";Catalog=85cc2b41-2452-4e2d-adc4-dfeae6e68e70");
            adomdConnection.Open();
            AdomdClient.AdomdCommand adomdCommand = new AdomdClient.AdomdCommand(@"SELECT * FROM $System.DBSCHEMA_COLUMNS", adomdConnection);
            AdomdClient.AdomdDataReader records = adomdCommand.ExecuteReader();

            adomdCommand.Dispose();
            adomdConnection.Close();
        }
    }
}

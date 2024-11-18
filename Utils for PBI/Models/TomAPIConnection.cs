using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tabular = Microsoft.AnalysisServices.Tabular;
using AdomdClient = Microsoft.AnalysisServices.AdomdClient;
using PowerBIConnections.Connections;

//TO-DO: Move it to a separate project if necessary for creating DLLs


namespace Utils_for_PBI.Models
{
    public class TomAPIConnection
    {
        public static Tabular.Server server = new Tabular.Server();
        public static Tabular.Database database;
        public static Tabular.Model model;
        public static bool IsConnected = false;

        static TomAPIConnection()
        {

        }

        public static void Connect(DatasetConnection connection)
        {
            if(IsConnected)
            {
                Disconnect(IsConnected);
            }
            server.Connect(connection.ConnectString);
            database = server.Databases[0];
            model = database.Model;
            IsConnected = true;
        }

        public static void Disconnect(bool endSession = true)
        {
            IsConnected = false;
            server.Disconnect(endSession);
        }
        
    }
}

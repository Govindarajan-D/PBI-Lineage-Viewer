using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tabular = Microsoft.AnalysisServices.Tabular;
using PowerBIConnections.Connections;

//TO-DO: Move it to a separate project if necessary for creating DLLs
//TO-DO: If nothing populated in dropdown, disable connect button


namespace Utils_for_PBI.Models
{
    public class TomAPIConnection
    {
        public static Tabular.Server server = new Tabular.Server();
        public static Tabular.Database database;
        public static Tabular.Model model;
        public static bool isConnected = false;

        static TomAPIConnection()
        {

        }

        public static void Connect(DatasetConnection datasetConnection)
        {
            if(isConnected)
            {
                Disconnect(isConnected);
            }
            server.Connect(datasetConnection.ConnectString);
            database = server.Databases[0];
            model = database.Model;
            isConnected = true;
        }

        public static void Disconnect(bool endSession = true)
        {
            isConnected = false;
            server.Disconnect(endSession);
        }
        
    }

}

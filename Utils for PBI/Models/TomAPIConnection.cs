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

namespace Utils_for_PBI.Models
{
    public class TomAPIConnection
    {
        public Tabular.Server server = new Tabular.Server();
        public Tabular.Database database;
        public Tabular.Model model;
        public bool isConnected = false;

        static TomAPIConnection()
        {

        }

        public void Connect(DatasetConnection datasetConnection)
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

        public void Disconnect(bool endSession = true)
        {
            isConnected = false;
            server.Disconnect(endSession);
        }
        
    }

}

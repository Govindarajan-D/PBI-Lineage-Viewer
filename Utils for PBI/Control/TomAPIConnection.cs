using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AnalysisServices.Tabular;
using PowerBIConnections.Connections;


namespace Utils_for_PBI.Interaction
{
    public class TomAPIConnection
    {
        public static Server server = new Server();
        public static Database database;
        public static Model model;
        public static bool IsConnected = false;

        static TomAPIConnection()
        {

        }

        public static void Connect(DatasetConnection connection)
        {
            server.Connect(connection.ConnectString);
            database = server.Databases[0];
            model = database.Model;
            IsConnected = true;

            MessageBox.Show(model.Name);
        }
    }
}

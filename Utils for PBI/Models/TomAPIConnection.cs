using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tabular = Microsoft.AnalysisServices.Tabular;
using PowerBIConnections.Connections;
using log4net.Repository.Hierarchy;
using log4net;

//TO-DO: Move it to a separate project if necessary for creating DLLs

namespace Utils_for_PBI.Models
{
    public class TomAPIConnection : IDisposable
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TomAPIConnection));
        public Tabular.Server server = new Tabular.Server();
        public Tabular.Database database;
        public Tabular.Model model;
        public bool isConnected = false;
        public bool endTOMSession = true;

        static TomAPIConnection()
        {

        }

        public void Connect(DatasetConnection datasetConnection)
        {
            if(isConnected)
            {
                Disconnect(isConnected);
            }
            try
            {
                server.Connect(datasetConnection.ConnectString);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                MessageBox.Show($"Error: {ex.Message}", "Error establishing TOMAPI connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            database = server.Databases[0];
            model = database.Model;
            isConnected = true;
        }

        public void Disconnect(bool endSession)
        {
            endTOMSession = endSession;
            Dispose();
            
        }

        public void Dispose()
        {
            isConnected = false;
            server.Disconnect(endTOMSession);
            GC.SuppressFinalize(this);
        }
    }

}

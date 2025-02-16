using log4net;
using PowerBIConnections.Connections;
using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Windows.Forms;
using Tabular = Microsoft.AnalysisServices.Tabular;

//TO-DO: Move it to a separate project if necessary for creating DLLs

namespace Utils_for_PBI.Services
{
    [SupportedOSPlatform("windows")]
    public class TomAPIConnection : IDisposable
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TomAPIConnection));
        public Tabular.Server server = new Tabular.Server();
        public List<String> databases = new List<String>();
        public Tabular.Database currentDatabase;
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

            for(int i=0; i < server.Databases.Count; i++)
            {
                databases.Add(server.Databases[i].Name);
            }

            currentDatabase = server.Databases[0];
            datasetConnection.DatabaseName = currentDatabase.Name;
            model = currentDatabase.Model;
            isConnected = true;
        }

        public void Disconnect(bool endSession = true)
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

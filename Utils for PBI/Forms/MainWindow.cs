using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PowerBIConnections.Connections;
using Utils_for_PBI.Forms;
using Utils_for_PBI.Models;
using Utils_for_PBI.Server;


namespace Utils_for_PBI.Forms
{
    public partial class MainWindow : Form
    {
        public List<GenerateLineagePage> lineagePages;
        private UtilsPBIHTTPServer _dataServer;
        private AdomdConnection _adomdConnection;
        private TomAPIConnection _tomAPIConnection;
        public MainWindow()
        {
            InitializeComponent();
            //await DisplayLineageWebView.EnsureCoreWebView2Async(null);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
        }

        private void connectDesktopModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectDesktopDataset connectDatasetWindow = new ConnectDesktopDataset();
            connectDatasetWindow.NotifyAction += EnableControls;

            if (connectDatasetWindow.ShowDialog() == DialogResult.OK)
            {
                var connection = connectDatasetWindow.returnConnection;
                if (connection != null)
                {
                    _tomAPIConnection = new TomAPIConnection();
                    _adomdConnection = new AdomdConnection();

                    _tomAPIConnection.Connect(connection);
                    _adomdConnection.Connect(connection);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void viewdependencies_Click(object sender, EventArgs e)
        {
            GenerateLineagePage showDependencyGraph = new GenerateLineagePage();
            string filePath = showDependencyGraph.GenerateHTMLPage();

            string fileUri = new Uri(filePath).AbsoluteUri;
            DisplayLineageWebView.CoreWebView2.Navigate(fileUri);

            if (_adomdConnection.isConnected)
            {
                var dependencies = _adomdConnection.RetrieveCalcDependency();
                dependencies.ParseIntoJSON();

                //TO-DO: (High) Check if a server is already started and then start
                _dataServer = new UtilsPBIHTTPServer("http://localhost:8080/utilspbi/", dependencies);
                _dataServer.Start();
            }

        }

//TO-DO: Add enable and disable code while connecting and disconnecting respectively
        private void OnConnection()
        {
            
        }

        private void OnDisconnection()
        {
            _dataServer.Stop();
        }

        private void EnableControls(string message)
        {
            OnConnection();
        }
    }
}

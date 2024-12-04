using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PowerBIConnections.Connections;
using Utils_for_PBI.Forms;
using Utils_for_PBI.Models;
using Utils_for_PBI.Server;


namespace Utils_for_PBI.Forms
{
    [SupportedOSPlatform("windows")]
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
            ConnectDataset connectDatasetWindow = new ConnectDataset();
            connectDatasetWindow.NotifyAction += OnConnection;

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

            GenerateLineagePage showDependencyGraph = new GenerateLineagePage();
            string filePath = showDependencyGraph.GenerateHTMLPage();

            string fileUri = new Uri(filePath).AbsoluteUri;
            DisplayLineageWebView.CoreWebView2.Navigate(fileUri);

            if (_adomdConnection.isConnected)
            {
                var dependencies = _adomdConnection.RetrieveCalcDependency();
                dependencies.ParseIntoJSON();

                //TO-DO: (High) Check if a server is already started and then start
                if (_dataServer == null)
                {
                    _dataServer = new UtilsPBIHTTPServer("http://localhost:8080/utilspbi/", dependencies);
                    _dataServer.Start();
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //TO-DO: Add enable and disable code while connecting and disconnecting respectively
        private void OnConnection(string message)
        {
            EnableControls();
        }

        private void OnDisconnection()
        {
            _dataServer.Stop();
        }

        private void EnableControls()
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

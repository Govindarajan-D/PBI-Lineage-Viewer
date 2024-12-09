using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Windows.Forms;
using PowerBIConnections.Connections;
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

        /* Once the user presses, connect, a new TOM API Connection and Adomd Connection are created.
         * Since the connection string required for PBI Service and PBI Desktop differs, a check is performed and connection string 
         * is formatted accordingly. The status bar is also set to show the connection information.
         * Once the connection is established, the internal HTTP Server starts and serves the data for the lineage view
         */
        private void connectDesktopModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectDataset connectDatasetWindow = new ConnectDataset();
            connectDatasetWindow.NotifyAction += OnConnection;

            if (connectDatasetWindow.ShowDialog() == DialogResult.OK)
            {
                var connection = connectDatasetWindow.selectedConnection;
                if (connection != null)
                {
                    _tomAPIConnection = new TomAPIConnection();
                    _adomdConnection = new AdomdConnection();

                    _tomAPIConnection.Connect(connection);

                    /* If the connection is of Power BI Service type, show a dialog box to the user
                     * to get the Semantic model (database) to connect.
                     */

                    if(connection.ConnectionType == ConnectionType.PowerBIService)
                    {
                        SelectModelForm selectModelForm = new SelectModelForm(_tomAPIConnection.databases);
                        if(selectModelForm.ShowDialog() == DialogResult.OK && selectModelForm.selectedDatabaseName != null)
                        {
                            connection.DatabaseName = selectModelForm.selectedDatabaseName;
                            _adomdConnection.Connect(connection);
                        }
                        else
                        {
                            MessageBox.Show($"Info: No Model Selected", "Select Model", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _tomAPIConnection.Disconnect();
                            return;
                        }
                    }
                    else
                    {
                        _adomdConnection.Connect(connection);
                    }

                    if(connection.ConnectionType == ConnectionType.PowerBIDesktop)
                    {
                        modelURLStatusLabel.Text = "Local connection: " + connection.ConnectString;
                    }
                    else
                    {
                        modelURLStatusLabel.Text = "XMLA Endpoint:" + connection.ConnectString + " Model:" + connection.DatabaseName;
                    }
                    
                }
            }

            /* HTML page is generated from the resources and then it is displayed in the WebView2 component.
             * The calc dependencies are retrieved and then served using the UtilsPBIHTTPServer server.
             */

            GenerateLineagePage showDependencyGraph = new GenerateLineagePage();
            string filePath = showDependencyGraph.GenerateHTMLPage();

            string fileUri = new Uri(filePath).AbsoluteUri;
            DisplayLineageWebView.CoreWebView2.Navigate(fileUri);

            if (_adomdConnection.isConnected)
            {
                var dependencies = _adomdConnection.RetrieveCalcDependency();
                dependencies.ParseIntoJSON();

                if (_dataServer == null)
                {
                    _dataServer = new UtilsPBIHTTPServer("http://localhost:8080/utilspbi/", dependencies);
                    _dataServer.Start();
                }
            }

            ConnectDatasetPlaceholderLabel.Visible = false;
            DisplayLineageWebView.Visible = true;

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
            _adomdConnection.Disconnect();
            _tomAPIConnection.Disconnect();
        }

        private void EnableControls()
        {

        }

    }
}

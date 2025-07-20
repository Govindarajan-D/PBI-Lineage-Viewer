using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Windows.Forms;
using log4net;
using log4net.Repository.Hierarchy;
using PowerBIConnections.Connections;
using Utils_for_PBI.Services;
using Utils_for_PBI.Server;


namespace Utils_for_PBI.Forms
{
    [SupportedOSPlatform("windows")]
    public partial class MainWindow : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MainWindow));
        public List<GenerateLineagePage> lineagePages;
        private UtilsPBIHTTPServer _dataServer;
        private TomAPIConnection _tomAPIConnection;
        private DatasetConnection _connection;
        public MainWindow()
        {
            InitializeComponent();
            //await DisplayLineageWebView.EnsureCoreWebView2Async(null);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
        }

        /* Once the user presses connect, the connection string is being created from the user selection.
         * Since the connection string required for PBI Service and PBI Desktop differs, a check is performed and connection string 
         * is formatted accordingly. The status bar is also set to show the connection information.
         * Once the connection is established, the internal HTTP Server starts and serves the data for the lineage view
         */
        private void connectDesktopModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectDataset connectDatasetWindow = new ConnectDataset();
            connectDatasetWindow.NotifyAction += OnConnection;

            var connectionWindow = connectDatasetWindow.ShowDialog();

            if (connectionWindow == DialogResult.OK)
            {
                // If a new connection, we stop the server. 
                // TO-DO: Instead of stopping and starting again, need to change the data that is served 
                if (_dataServer != null)
                {
                    _dataServer.Stop();
                }
                _connection = connectDatasetWindow.selectedConnection;
                if (_connection != null)
                {
                    _tomAPIConnection = new TomAPIConnection();

                    _tomAPIConnection.Connect(_connection);

                    /* If the connection is of Power BI Service type, show a dialog box to the user
                     * to get the Semantic model (database) to connect.
                     */

                    if(_connection.ConnectionType == ConnectionType.PowerBIService)
                    {
                        SelectModelForm selectModelForm = new SelectModelForm(_tomAPIConnection.databases);
                        if (_tomAPIConnection.databases.Count > 0)
                        {
                            if (selectModelForm.ShowDialog() == DialogResult.OK && selectModelForm.selectedDatabaseName != null)
                            {
                                _connection.DatabaseName = selectModelForm.selectedDatabaseName;
                            }
                            else
                            {
                                MessageBox.Show($"Info: No Model Selected", "Select Model", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _tomAPIConnection.Disconnect();
                                return;
                            }
                        }
                    }

                    //Check the type of connection and status bar is set accordingly
                    string connectionString;
                    if (_connection.ConnectionType == ConnectionType.PowerBIDesktop)
                    {
                        connectionString = "Name: " + _connection.DisplayName;
                    }
                    else
                    {
                        connectionString =  "XMLA Endpoint:" + _connection.ConnectString + " | Model:" + _connection.DatabaseName;
                    }

                    modelURLStatusLabel.Text = connectionString;
                    Logger.Info(connectionString);
                }
            }
            else if (connectionWindow == DialogResult.Cancel)
            {
                return;
            }

           /* HTML page is generated from the resources and then it is displayed in the WebView2 component.
            * The modelMetadata is retrieved and then served using the UtilsPBIHTTPServer server.
            */

            GenerateLineagePage showDependencyGraph = new GenerateLineagePage();
            string filePath = showDependencyGraph.GenerateHTMLPage();

            ModelMetadata modelMetadata = new ModelMetadata();
            modelMetadata.PopulateModelMetadata(_connection);

            if (modelMetadata == null)
            {
                return;
            }

            if (_dataServer != null)
            {
                _dataServer.Close();
            }
            //TO-DO: Add configuration functionality to change the port number and other settings

            _dataServer = new UtilsPBIHTTPServer(Constants.urlAddress, modelMetadata);
            _dataServer.Start();

            string fileUri = new Uri(filePath).AbsoluteUri;
            DisplayLineageWebView.CoreWebView2.Navigate(fileUri);

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
            _tomAPIConnection.Disconnect();
        }

        private void EnableControls()
        {

        }

    }
}

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


namespace Utils_for_PBI.Forms
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            //await DisplayLineageWebView.EnsureCoreWebView2Async(null);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectDataset connectDatasetWindow = new ConnectDataset();
            connectDatasetWindow.NotifyAction += EnableControls;
            connectDatasetWindow.ShowDialog();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void viewdependencies_Click(object sender, EventArgs e)
        {
            string filePath = GenerateLineagePage.GenerateHTMLPage();

            string fileUri = new Uri(filePath).AbsoluteUri;
            DisplayLineageWebView.CoreWebView2.Navigate(fileUri);

            if (AdomdConnection.isConnected && TomAPIConnection.isConnected)
            {
                AdomdConnection.RetrieveCalcDependency();
            }

        }

//TO-DO: Add enable and disable code while connecting and disconnecting respectively
        private void OnConnection()
        {
            
        }

        private void OnDisconnection()
        {

        }

        private void EnableControls(string message)
        {
            OnConnection();
        }
    }
}

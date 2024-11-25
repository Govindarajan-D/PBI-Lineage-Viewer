﻿using System;
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
        private JSONDataServer dataServer;
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
            connectDatasetWindow.ShowDialog();

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

            if (AdomdConnection.isConnected)
            {
                var dependencies = AdomdConnection.RetrieveCalcDependency();
                dependencies.ParseIntoJSON();

                //TO-DO: (High) Check if a server is already started and then start
                dataServer = new JSONDataServer("http://localhost:8080/utilspbi/", dependencies);
                dataServer.Start();
            }

        }

//TO-DO: Add enable and disable code while connecting and disconnecting respectively
        private void OnConnection()
        {
            
        }

        private void OnDisconnection()
        {
            dataServer.Stop();
        }

        private void EnableControls(string message)
        {
            OnConnection();
        }
    }
}

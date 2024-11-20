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
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectDataset ConnectDatasetWindow = new ConnectDataset();
            ConnectDatasetWindow.ShowDialog();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void viewdependencies_Click(object sender, EventArgs e)
        {
            AdomdConnection.RetrieveCalcDependency();
        }
    }
}

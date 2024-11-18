using PowerBIConnections.Connections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utils_for_PBI.Models;

namespace Utils_for_PBI.Forms
{
    public partial class ConnectDataset : Form
    {
        public ConnectDataset()
        {
            InitializeComponent();
            LoadModelConnections();
        }

        public void LoadModelConnections()
        {
            //MessageBox.Show("MyFunction was called", "Checkpoint");
            var ActiveSessions = ActiveConnections.GetActiveConnections();
            DesktopModelComboBox.DataSource = ActiveSessions;
            DesktopModelComboBox.DisplayMember = "DisplayName";
        }
        private void ConnectDatasetCancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ConnectDatasetOkButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            var SelectedItem = this.DesktopModelComboBox.SelectedItem as DatasetConnection;
            TomAPIConnection.Connect(SelectedItem);
            this.Close();
        }
    }
}

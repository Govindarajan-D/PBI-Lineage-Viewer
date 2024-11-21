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
        public delegate void NotifyHandler(string message);
        public event NotifyHandler NotifyAction;

        public ConnectDataset()
        {
            InitializeComponent();
            LoadModelConnections();
        }

        public void LoadModelConnections()
        {
            var activeSessions = ActiveConnections.GetActiveConnections();
            if (activeSessions.Count != 0)
            {
                ConnectDatasetOkButton.Enabled = true;
                ConnectDatasetOkButton.BackColor = Color.LightSkyBlue;
                DesktopModelComboBox.DataSource = activeSessions;
                DesktopModelComboBox.DisplayMember = "DisplayName";
            }
            else
            {
                ConnectDatasetOkButton.Enabled = false;
                ConnectDatasetOkButton.BackColor = Color.LightGray;
            }
        }
        private void ConnectDatasetCancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ConnectDatasetOkButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            var selectedItem = this.DesktopModelComboBox.SelectedItem as DatasetConnection;
            if (selectedItem != null)
            {
                TomAPIConnection.Connect(selectedItem);
                AdomdConnection.Connect(selectedItem);
            }
            this.Close();

            NotifyAction?.Invoke("Connection Established");
        }

        private void ConnectDatasetRefreshButton_Click(object sender, EventArgs e)
        {
            LoadModelConnections();
        }
    }
}

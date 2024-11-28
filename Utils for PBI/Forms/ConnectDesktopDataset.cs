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
    public partial class ConnectDesktopDataset : Form
    {
        public DatasetConnection returnConnection;
        public delegate void NotifyHandler(string message);
        public event NotifyHandler NotifyAction;

        public ConnectDesktopDataset()
        {
            InitializeComponent();
            LoadModelConnections();
        }

        public void LoadModelConnections()
        {
            var activeSessions = ActiveConnections.GetActiveConnections();
            if (activeSessions.Count != 0)
            {
                ConnectDesktopDatasetOkButton.Enabled = true;
                ConnectDesktopDatasetOkButton.BackColor = Color.LightSkyBlue;
                DesktopModelComboBox.DataSource = activeSessions;
                DesktopModelComboBox.DisplayMember = "DisplayName";
            }
            else
            {
                ConnectDesktopDatasetOkButton.Enabled = false;
                ConnectDesktopDatasetOkButton.BackColor = Color.LightGray;
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
            returnConnection = this.DesktopModelComboBox.SelectedItem as DatasetConnection;
            this.Close();

            NotifyAction?.Invoke("Connection Established");
        }

        private void ConnectDatasetRefreshButton_Click(object sender, EventArgs e)
        {
            LoadModelConnections();
        }
    }
}

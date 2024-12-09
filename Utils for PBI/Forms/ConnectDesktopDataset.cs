using PowerBIConnections.Connections;
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
using Utils_for_PBI.Models;

namespace Utils_for_PBI.Forms
{
    [SupportedOSPlatform("windows")]
    public partial class ConnectDataset : Form
    {
        public DatasetConnection selectedConnection;
        public delegate void NotifyHandler(string message);
        public event NotifyHandler NotifyAction;

        public ConnectDataset()
        {
            InitializeComponent();
            this.DesktopModelComboBox.DropDownClosed += DesktopModelComboBox_Changed;
            this.OnlineModelComboBox.DropDownClosed += OnlineModelComboBox_Changed;
            this.DesktopModelComboBox.SelectedIndexChanged += DesktopModelComboBox_Changed;
            this.OnlineModelComboBox.SelectedIndexChanged += OnlineModelComboBox_Changed;
            this.OnlineModelComboBox.TextChanged += OnlineModelComboBox_Changed;
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

            if (this.DesktopModelComboBox.SelectedItem as DatasetConnection != null)
            {
                selectedConnection = this.DesktopModelComboBox.SelectedItem as DatasetConnection;
            }
            else
            {

                selectedConnection = new DatasetConnection
                {
                    ConnectString = this.OnlineModelComboBox.Text,
                    ConnectionType = ConnectionType.PowerBIService
                };
            }

            this.Close();

            NotifyAction?.Invoke("Connection Selected");
        }

        private void ConnectDatasetRefreshButton_Click(object sender, EventArgs e)
        {
            LoadModelConnections();
        }

        private void DesktopModelComboBox_Changed(object sender, EventArgs e)
        {
            if (this.DesktopModelComboBox.SelectedItem != null || this.DesktopModelComboBox.SelectedIndex != -1)
            {
                this.OnlineModelComboBox.SelectedIndex = -1;
            }
        }
        private void OnlineModelComboBox_Changed(object sender, EventArgs e)
        {
            if (this.OnlineModelComboBox.SelectedItem != null || this.OnlineModelComboBox.SelectedIndex != -1 || this.OnlineModelComboBox.Text != "")
            {
                this.DesktopModelComboBox.SelectedIndex = -1;
            }
        }

        private void ConnectOnlineModelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

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

namespace Utils_for_PBI.Forms
{
    [SupportedOSPlatform("windows")]
    public partial class SelectModelForm : Form
    {
        public string selectedDatabaseName;
        public SelectModelForm(List<String> databaseList)
        {
            InitializeComponent();
            DatabaseListBox.DataSource = databaseList;
        }

        private void SelectModelOkButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            selectedDatabaseName = DatabaseListBox.SelectedValue as String;
            this.Close();
        }

        private void SelectModelCancelButton_Click(object sender, EventArgs e)
        {
            selectedDatabaseName = null;
            this.Close();
        }
    }
}

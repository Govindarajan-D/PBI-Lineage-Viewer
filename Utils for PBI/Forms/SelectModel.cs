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
        public SelectModelForm(List<String> databaseList)
        {
            InitializeComponent();
            DatabaseListBox.DataSource = databaseList;
        }
    }
}

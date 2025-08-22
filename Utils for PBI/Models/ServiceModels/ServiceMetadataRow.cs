using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils_for_PBI.Models.ServiceModels
{
    /// <summary>
    /// Represents metadata fetched from Power BI REST API for each reoirt
    /// </summary>
    public class ServiceMetadataRow
    {
        public string WorkspaceID { get; set; }
        public string WorkspaceName { get; set; }
        public string ReportID { get; set; }
        public string ReportName { get; set; }
        public string DatasetID { get; set; }
    }
}

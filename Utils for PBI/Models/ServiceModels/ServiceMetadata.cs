using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils_for_PBI.Models.ReportMetadata
{
    public class ServiceMetadata
    {
        public string workspaceId { get; set; }
        public string workspaceName { get; set; }
        public string reportId { get; set; }
        public string reportName { get; set; }
        public string datasetId { get; set; }
    }
}

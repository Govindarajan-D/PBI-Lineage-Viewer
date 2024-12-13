using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils_for_PBI.Models
{
    public class ReportObjectUsageData
    {
        public string pageName { get; set; }
        public string visualType { get; set; }
        public List<VisualObject> visualObjects { get; set; }
    }
    public class VisualObject
    {
        public string name { get; set; }
        public string source { get; set; }
        public string type { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils_for_PBI.Models
{
    // sections[Page]-> visualContainers[visuals]-> visual[measures/columns]

    public class Section
    {
        public List<PageObject> pageObjects;
    }

    public class PageObject
    {
        public string pageName { get; set; }
        public List<VisualContainerObject> visualContainers { get; set; }
    }
    public class VisualContainerObject
    {
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

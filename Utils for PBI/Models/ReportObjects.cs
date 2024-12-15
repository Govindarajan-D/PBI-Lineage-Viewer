using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils_for_PBI.Models
{
    // sections[Page]-> visualContainers[visuals]-> visual[measures/columns]
    /// <summary>
    /// Section is the top-most level in a report layout and it contains an array of pages 
    /// </summary>
    public class Section
    {
        public List<PageObject> pageObjects;
    }
    /// <summary>
    /// Each report page contains a visual Container which is an array of visual objects 
    /// </summary>
    public class PageObject
    {
        public string pageName { get; set; }
        public List<VisualContainerObject> visualContainers { get; set; }
    }
    /// <summary>
    /// Visual Container contains the metadata of a visual like type of visual (chart, table), table names and objects used
    /// </summary>
    public class VisualContainerObject
    {
        public string visualType { get; set; }
        public Dictionary<string,string> sources { get; set; }
        public List<VisualObject> visualObjects { get; set; }
    }
    /// <summary>
    /// Visual Object stores the name and type (measure/column) used in the visual
    /// </summary>
    public class VisualObject
    {
        public string name { get; set; }
        public string source { get; set; }
        public string type { get; set; }
    }
}

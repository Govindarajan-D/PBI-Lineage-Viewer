using System.Collections.Generic;

namespace Utils_for_PBI.Models
{
    // sections[Page]-> visualContainers[visuals]-> visual[measures/columns]
    /// <summary>
    /// Section is the top-most level in a report layout and it contains an array of pages 
    /// </summary>
    public class Section
    {
        // List of PageObjects which store page-level information
        public List<PageObject> pageObjects;
    }
    /// <summary>
    /// Each report page contains a visual Container which is an array of visual objects 
    /// </summary>
    public class PageObject
    {
        //Name of the Page 
        public string name;
        // Display name from the report
        public string pageDisplayName { get; set; }

        //Ordinal - Page number (starts with 0)
        public string ordinal;
        
        // Visual containers are part of the page and they hold the information for each visual
        public List<VisualContainerObject> visualContainers { get; set; }
    }
    /// <summary>
    /// Visual Container contains the metadata of a visual like type of visual (chart, table), table names and objects used
    /// </summary>
    public class VisualContainerObject
    {
        // Type of the visual - Table, Chart
        public string visualType { get; set; }

        // Dictionary of sources (table names) and its id 
        public Dictionary<string,string> sources { get; set; }

        // List of each Visual Object
        public List<VisualObject> visualObjects { get; set; }
    }
    /// <summary>
    /// Visual Object stores the name and type (measure/column) used in the visual
    /// </summary>
    public class VisualObject
    {
        // Name of the visual - although it would be a unique id rather than a human readable name
        public string name { get; set; }

        // Source id of the visual - This needs to be joined with the dictionary in the VisualContainerObject to get the actual table name
        public string source { get; set; }

        // Type of the object - Measure/Column
        public string type { get; set; }
    }
}

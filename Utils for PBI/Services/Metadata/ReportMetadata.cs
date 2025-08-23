using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using

using Utils_for_PBI.Models;

namespace Utils_for_PBI.Services.Metadata
{
    public class ReportMetadata
    {
        private List<ReportSection> reportSections = new List<ReportSection>();
        
        public ReportMetadata()
        {
            //TO-DO: Build a loop to go over all the reports and download only the necessary reports for finding the lineage and adding it to the List
        }

        public void GetReportLineage()
        {
            var flattenedLineage = from reportSection in reportSections
                          from page in reportSection.pageObjects
                          from visualContainer in page.visualContainers
                          from visualObject in visualContainer.visualObjects
                          where visualContainer.sources.ContainsKey(visualObject.source)
                          select new
                          {
                              PageName = page.pageDisplayName,
                              VisualType = visualContainer.visualType,
                              VisualObjectName = visualObject.name,
                              SourceTable = visualContainer.sources[visualObject.source]

                          };

        }



    }
}

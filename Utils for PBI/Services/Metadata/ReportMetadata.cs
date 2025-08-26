using Microsoft.AnalysisServices.AdomdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

using Utils_for_PBI.Models;

namespace Utils_for_PBI.Services.Metadata
{
    public class ReportMetadata
    {
        private List<ReportSection> reportSections = new List<ReportSection>();
        public IEnumerable<dynamic> FlattenedLineage;

        public ReportMetadata() { }
        
        public ReportMetadata(ReportSection reportSection)
        {
            //TO-DO: Build a loop to go over all the reports and download only the necessary reports for finding the lineage and adding it to the List
            reportSections.Add(reportSection);
        }

        public IEnumerable<dynamic> GetReportLineage()
        {
            FlattenedLineage = from reportSection in reportSections
                          from page in reportSection.pageObjects
                          from visualContainer in page.visualContainers
                          from visualObject in visualContainer.visualObjects
                          where visualContainer.sources.ContainsKey(visualObject.source)
                          select new
                          {
                              PageName = page.pageDisplayName,
                              VisualType = visualContainer.visualType,
                              SourceObjectName = visualObject.name,
                              SourceTable = visualContainer.sources[visualObject.source]

                          };

            return FlattenedLineage;
        }

        public IEnumerable<dynamic> GetReportSvelteFlowNodes()
        {
            return FlattenedLineage.Select(c => new
                                        {
                                            id = "",
                                            type = "selectorNode",
                                            data = new
                                            {
                                                calcName = c.SourceObjectName,
                                                calcType = c.VisualType
                                            },
                                            position = new
                                                {
                                                    x = 0,
                                                    y = 0
                                                }

                                        });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using Utils_for_PBI.Services.Metadata;

namespace Utils_for_PBI.Services
{
    [SupportedOSPlatform("windows")]

    /// <summary>
    /// The lineage objects from various sources are aggregated and returned as output
    /// </summary>

    public class LineageAggregator
    {
        ModelMetadata modelMetadata = new ModelMetadata();
        ReportMetadata reportMetadata = new ReportMetadata();

        public LineageAggregator()
        {

        }

        public void AggregateLineage()
        {
            var modelNodes = modelMetadata.GetModelSvelteFlowNodes();
            var modelEdges = modelMetadata.GetModelSvelteFlowEdges();

        }

    }
}

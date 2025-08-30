using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        public ModelMetadata _modelMetadata = new ModelMetadata();
        public ReportMetadata _reportMetadata = new ReportMetadata();

        public LineageAggregator(ModelMetadata modelMetadata, ReportMetadata reportMetadata)
        {
            _modelMetadata = modelMetadata;
            _reportMetadata = reportMetadata;
        }

        public void AggregateLineage()
        {
            var modelNodes = _modelMetadata.GetModelSvelteFlowNodes();
            var modelEdges = _modelMetadata.GetModelSvelteFlowEdges();

            var reportNodes = _reportMetadata.GetReportSvelteFlowNodes();

            var allNodes = modelNodes.Concat(reportNodes);

            var reportEdges = reportNodes.Join(
                modelNodes,
                reportNode => reportNode.data.AdditionalData.SourceObjectName,
                modelNode => modelNode.id,
                (reportNode, modelNode) => new
                {
                    id = modelNode.id + "" + reportNode.data.AdditionalData.SourceObjectName,
                    source = modelNode.id,
                    target = reportNode.id,
                    type = "bezier",
                    animated = true
                });
        }
    }
}

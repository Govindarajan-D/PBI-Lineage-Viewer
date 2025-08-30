using log4net;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using Utils_for_PBI.Services.Metadata;
using SysJson = System.Text.Json;

namespace Utils_for_PBI.Services
{
    [SupportedOSPlatform("windows")]

    /// <summary>
    /// The lineage objects from various sources are aggregated and returned as output
    /// </summary>

    public class LineageAggregator
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(LineageAggregator));

        public ModelMetadata _modelMetadata = new ModelMetadata();
        public ReportMetadata _reportMetadata = new ReportMetadata();

        public IEnumerable<dynamic> aggregatedSvelteFlowNodes, aggregatedSvelteFlowEdges;



        public LineageAggregator(ModelMetadata modelMetadata, ReportMetadata reportMetadata)
        {
            _modelMetadata = modelMetadata;
            _reportMetadata = reportMetadata;
        }

        public void AggregateLineage()
        {
            Logger.Info("Aggregating Lineage by combining nodes from multiple sources");

            var modelNodes = _modelMetadata.GetModelSvelteFlowNodes();
            var modelEdges = _modelMetadata.GetModelSvelteFlowEdges();

            var reportNodes = _reportMetadata.GetReportSvelteFlowNodes();

            aggregatedSvelteFlowNodes = modelNodes.Concat(reportNodes);

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

            aggregatedSvelteFlowEdges = modelEdges.Concat(reportEdges);
        }

        public string GetAggregatedSvelteFlowNodesJson()
        {
            return SysJson.JsonSerializer.Serialize(aggregatedSvelteFlowNodes, new SysJson.JsonSerializerOptions { WriteIndented = true }); ;
        }

        public string GetAggregatedSvelteFlowEdgesJson()
        {
            return SysJson.JsonSerializer.Serialize(aggregatedSvelteFlowEdges, new SysJson.JsonSerializerOptions { WriteIndented = true }); ;
        }
    }
}

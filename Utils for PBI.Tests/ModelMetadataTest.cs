using Utils_for_PBI.Models.MetadataRows;
using Newtonsoft.Json.Linq;
using FluentAssertions;
using Utils_for_PBI.Services;

namespace Utils_for_PBI.Tests
{
    public class ModelMetadataTest
    {
        private readonly JArray _nodes;
        private readonly JArray _edges;

        public ModelMetadataTest()
        {
            ModelMetadata modelMetadata = new ModelMetadata();
            modelMetadata.CalcDependencyMetadataAddRow(new CalcDependencyMetadataRow
            {
                OBJECT_TYPE = "MEASURE",
                SOURCE_TABLE = "Sales",
                OBJECT = "Total Sales",
                EXPRESSION = "SUM(Sales[Amount])",
                REFERENCED_OBJECT_TYPE = "COLUMN",
                REFERENCED_TABLE = "Sales",
                REFERENCED_OBJECT = "Amount"
            });

            _nodes = JArray.Parse(modelMetadata.GetSvelteFlowNodesJson());
            _edges = JArray.Parse(modelMetadata.GetSvelteFlowEdgesJson());

        }

        [Fact]
        public void ValidateNodesCount()
        {
            _nodes.Count.Should().Be(3);
        }


        [Fact]
        public void ValidateEdgesCount()
        {
            _edges.Count.Should().Be(3);
        }

        [Fact]
        public void ValidateEdgeData()
        {
            var edge = _edges.Children<JObject>()
                                .FirstOrDefault(e => e.Value<String>("source") == "Sales" &&
                                                     e.Value<String>("target") == "Amount");

            edge.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void ValidateNodeData()
        {
            var node = _nodes.Children<JObject>()
                    .FirstOrDefault(e => e.Value<String>("id") == "Amount");

            node.Should().NotBeNullOrEmpty();
            node.SelectToken("data.CalcName").Value<String>().Should().Be("Amount");
            node.SelectToken("data.CalcType").Value<String>().Should().Be("Column");

        }
    }
}
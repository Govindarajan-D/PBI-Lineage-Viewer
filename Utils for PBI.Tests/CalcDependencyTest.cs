using Utils_for_PBI.Models;
using Newtonsoft.Json.Linq;
using FluentAssertions;

namespace Utils_for_PBI.Tests
{
    public class CalcDependencyTest
    {
        private readonly JArray _nodes;
        private readonly JArray _edges;

        public CalcDependencyTest()
        {
            CalcDepedencyData calcDepedencyData = new CalcDepedencyData();
            calcDepedencyData.calcDepedencyData.Add(new CalcDependencyDataRow
            {
                OBJECT_TYPE = "MEASURE",
                SOURCE_TABLE = "Sales",
                OBJECT = "Total Sales",
                EXPRESSION = "SUM(Sales[Amount])",
                REFERENCED_OBJECT_TYPE = "COLUMN",
                REFERENCED_TABLE = "Sales",
                REFERENCED_OBJECT = "Amount"
            });

            calcDepedencyData.ParseIntoJSON();

            _nodes = JArray.Parse(calcDepedencyData.svelte_flow_nodes_json);
            _edges = JArray.Parse(calcDepedencyData.svelte_flow_edges_json);

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
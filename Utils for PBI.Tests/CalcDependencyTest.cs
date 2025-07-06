using Utils_for_PBI.Models;
using Newtonsoft.Json.Linq;
using FluentAssertions;

namespace Utils_for_PBI.Tests
{
    public class CalcDependencyTest
    {
        [Fact]
        public void ValidateCalcDependencyDataJSONOutput()
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

            JArray nodesArray = JArray.Parse(calcDepedencyData.svelte_flow_nodes_json);
            JArray edgesArray = JArray.Parse(calcDepedencyData.svelte_flow_edges_json);

            nodesArray.Count.Should().Be(3);
            edgesArray.Count.Should().Be(3);

        }
    }
}
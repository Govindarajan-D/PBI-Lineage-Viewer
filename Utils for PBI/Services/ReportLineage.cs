using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utils_for_PBI.Models;

namespace Utils_for_PBI.Services
{
    public class ReportLineage
    {
        public ReportLineage()
        {
            var extractionFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Constants.ProgramName, "ReportLineage");
            var zipFileName = "SimpleDAX.pbix";
            var zipFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Constants.ProgramName, zipFileName);

            //System.IO.Compression.ZipFile.ExtractToDirectory(zipFilePath, extractionFilePath);

            string jsonFileData = File.ReadAllText(Path.Combine(extractionFilePath, "Report/Layout.txt"), Encoding.Unicode);

            using (JsonDocument jsonDocument = JsonDocument.Parse(jsonFileData))
            {
                // Get the root node of the JSON file and get till the nested property of 'config' and then retrieve singleVisual
                // The singleVisual contains the projections, PrototypeQuery which contains the measures being used
                JsonElement jsonSections = jsonDocument.RootElement.GetProperty("sections");
                Dictionary<string, object> data = new Dictionary<string, object>();

                List<ReportObjectUsageData> reportObjects = new List<ReportObjectUsageData>();

                foreach (JsonElement jsonSection in jsonSections.EnumerateArray())
                {
                    JsonElement jsonVisualContainers = jsonSection.GetProperty("visualContainers");
                    string sectionPageName = jsonSection.GetProperty("displayName").GetString();

                    string visualType = "";
                    string[] queryRefs = [];

                    foreach (JsonElement jsonContainer in jsonVisualContainers.EnumerateArray())
                    {
                        var jsonConfig = JsonDocument.Parse(jsonContainer.GetProperty("config").GetString());
                        var deserializedObject = JsonSerializer.Deserialize<SingleVisual>(jsonConfig.RootElement.GetProperty("singleVisual"));

                        var value = FindPropertyRecursive(jsonConfig.RootElement, "Property");

                        visualType = deserializedObject.visualType;
                        var prototypeQuery = deserializedObject.prototypeQuery.From.Select(x => new { x.Name, x.Entity });
                        var objectsUsed = deserializedObject.prototypeQuery.@Select
                            .Select(x => new {
                                NativeReferenceName = x.NativeReferenceName, 
                                Entity = x.Measure?.Expression?.SourceRef?.Source
                                        ?? x.Column?.Expression?.SourceRef?.Source
                            });
                        queryRefs = deserializedObject.projections.Values.Select(c => c.queryRef).ToArray();
                        data.Add(visualType, queryRefs);
                    }

                    reportObjects.Add(new ReportObjectUsageData{
                        pageName = sectionPageName,
                        visualType = visualType, 
                        queryRefs = queryRefs
                    });

                    Console.WriteLine(sectionPageName);
                }

            }

        }

        public string FindPropertyRecursive(JsonElement jsonElement, string propertyName)
        {
            if(jsonElement.ValueKind == JsonValueKind.Object)
            {
                foreach(var property in jsonElement.EnumerateObject())
                {
                    if(property.Name == propertyName)
                    {
                        return property.Value.ToString();
                    }
                    else
                    {
                        var value = FindPropertyRecursive(property.Value, propertyName);
                        if (value != null)
                        {
                            return value.ToString();
                        }
                    }
                }
            }
            else if (jsonElement.ValueKind == JsonValueKind.Array)
            {
                foreach(var item in jsonElement.EnumerateArray())
                {
                    var value = FindPropertyRecursive(item, propertyName);
                    if (value != null)
                    {
                        return value.ToString();
                    }
                }
            }

            return null;
        }
    }
}

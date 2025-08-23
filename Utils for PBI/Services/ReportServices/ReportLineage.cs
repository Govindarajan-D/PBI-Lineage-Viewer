using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Utils_for_PBI.Models;

namespace Utils_for_PBI.Services.ReportServices
{
    /// <summary>
    /// ReportLineage class reads the pbix file and unzips it into the AppData folder where it reads the layout file
    /// The Layout file stores the metadata information about the visuals, the objects (measures, columns) used. It uses a 
    /// recursive function to identify the objects as it can be stored in any level of hierarchy
    /// </summary>
    public class ReportLineage
    {
        public Section reportSection = new Section();

        public ReportLineage(string filePath)
        { 
            var extractionFilePath = Path.GetFileNameWithoutExtension(filePath);
            System.IO.Compression.ZipFile.ExtractToDirectory(sourceArchiveFileName: filePath, destinationDirectoryName: Path.Combine(Path.GetDirectoryName(filePath), extractionFilePath), overwriteFiles: true);

            string jsonFileData = File.ReadAllText(Path.Combine(extractionFilePath, "Report/Layout"), Encoding.Unicode);
            List<PageObject> pageObjects = new List<PageObject>();
            reportSection.pageObjects = pageObjects;

            using (JsonDocument jsonDocument = JsonDocument.Parse(jsonFileData))
            {
                // Get the root node of the JSON file and get till the nested property of 'config' and then retrieve singleVisual
                // The singleVisual contains the projections, PrototypeQuery which contains the measures being used
                JsonElement jsonSections = jsonDocument.RootElement.GetProperty("sections");
                Dictionary<string, object> data = new Dictionary<string, object>();

                List<VisualContainerObject> reportObjects = new List<VisualContainerObject>();

                foreach (JsonElement jsonSection in jsonSections.EnumerateArray())
                {
                    JsonElement jsonVisualContainers = jsonSection.GetProperty("visualContainers");
                    PageObject pageObject = new PageObject();
                    List<VisualContainerObject> visualContainers = new List<VisualContainerObject>();
                    pageObject.name = jsonSection.GetProperty("name").GetString();
                    pageObject.pageDisplayName = jsonSection.GetProperty("displayName").GetString();

                    pageObject.ordinal = jsonSection.TryGetProperty("ordinal", out var ordinalElement) ? ordinalElement.ToString() : "0";

                    foreach (JsonElement jsonContainer in jsonVisualContainers.EnumerateArray())
                    {
                        var jsonConfig = JsonDocument.Parse(jsonContainer.GetProperty("config").GetString());
                        JsonElement singleVisual = jsonConfig.RootElement.GetProperty("singleVisual");

                        var sources = new Dictionary<string, string>();

                        var entities = singleVisual.GetProperty("prototypeQuery").GetProperty("From");
                        foreach(var entity in entities.EnumerateArray())
                        {
                            var source = entity.GetProperty("Name").GetString();
                            var entityName = entity.GetProperty("Entity").GetString();

                            sources.Add(source, entityName);
                        }

                        //var deserializedObject = JsonSerializer.Deserialize<SingleVisual>(jsonConfig.RootElement.GetProperty("singleVisual"));


                        VisualContainerObject currentReportPage = new VisualContainerObject();
                        List<VisualObject> visualObjects = new List<VisualObject>();

                        var value = FindPropertyRecursive("singleVisual", singleVisual, "Property", visualObjects);

                        currentReportPage.visualType = singleVisual.GetProperty("visualType").GetString();
                        currentReportPage.visualObjects = visualObjects;
                        currentReportPage.sources = sources;

                        visualContainers.Add(currentReportPage);

                    }

                    pageObject.visualContainers = visualContainers;
                    reportSection.pageObjects.Add(pageObject);
                }
            }
        }

        /// <summary>
        /// FindPropertyRecursive function is used to find the necessary property by recursively searching for it. Recursive function 
        /// is necessary as the property can appear at any level of nested json.
        /// </summary>
        public string FindPropertyRecursive(string rootPropertyName, JsonElement jsonElement, string searchPropertyName, List<VisualObject> visualObjects)
        {
            bool propertyFound = false;
            if(jsonElement.ValueKind == JsonValueKind.Object)
            {
                foreach(var property in jsonElement.EnumerateObject())
                {
                    if(property.Name == searchPropertyName)
                    {
                        propertyFound = true;
                        break;
                    }
                    else
                    {
                        var value = FindPropertyRecursive(property.Name, property.Value, searchPropertyName, visualObjects);
                        if (value != null)
                        {
                            return value.ToString();

                        }
                    }
                }
                // If property is found, return the source of this particular column/measure
                if (propertyFound)
                {
                    visualObjects.Add(new VisualObject
                    {
                        source = jsonElement.GetProperty("Expression").GetProperty("SourceRef").GetProperty("Source").GetString(),
                        name = jsonElement.GetProperty("Property").GetString(),
                        type = rootPropertyName
                    });

                    return "Found";

                }
                
            }
            else if (jsonElement.ValueKind == JsonValueKind.Array)
            {
                foreach(var item in jsonElement.EnumerateArray())
                {
                    var value = FindPropertyRecursive(rootPropertyName, item, searchPropertyName, visualObjects);
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

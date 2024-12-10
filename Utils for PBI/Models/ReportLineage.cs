﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Utils_for_PBI.Models
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
                var jsonRoot = jsonDocument.RootElement.GetProperty("sections")[0].GetProperty("visualContainers")[0].GetProperty("config").GetString();
                var jsonConfig = JsonDocument.Parse(jsonRoot);

                var deserializedObject = JsonSerializer.Deserialize<SingleVisual>(jsonConfig.RootElement.GetProperty("singleVisual"));

                Dictionary<string, object> data = new Dictionary<string, object>();
                var visualType = deserializedObject.visualType;
                string[] queryRefs = deserializedObject.projections.Values.Select(c => c.queryRef).ToArray();
                data.Add(visualType, queryRefs);

            }

        }

    }
    public class SingleVisual
    {
        public string visualType { get; set; }
        public Projections projections { get; set; }
        public PrototypeQuery prototypeQuery { get; set; }
        public bool drillFilterOtherVisuals { get; set; }
        public Dictionary<string, object> objects { get; set; }
        public Dictionary<string, object> vcObjects { get; set; }
    }

    public class Projections
    {
        public List<Value> Values { get; set; }
    }

    public class Value
    {
        public string queryRef { get; set; }
    }

    public class PrototypeQuery
    {
        public int Version { get; set; }
        public List<From> From { get; set; }
        public List<Select> Select { get; set; }
    }

    public class From
    {
        public string Name { get; set; }
        public string Entity { get; set; }
        public int Type { get; set; }
    }

    public class Select
    {
        public Measure Measure { get; set; }
        public Column Column { get; set; }
        public string Name { get; set; }
        public string NativeReferenceName { get; set; }
    }

    public class Measure
    {
        public Expression Expression { get; set; }
        public string Property { get; set; }
    }

    public class Column
    {
        public Expression Expression { get; set; }
        public string Property { get; set; }
    }

    public class Expression
    {
        public SourceRef SourceRef { get; set; }
    }

    public class SourceRef
    {
        public string Source { get; set; }
    }


}

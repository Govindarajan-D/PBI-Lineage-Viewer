using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils_for_PBI.Models
{
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

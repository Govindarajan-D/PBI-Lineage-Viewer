using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils_for_PBI
{
    public class CommandLineOptions
    {

        [Option('o', "output", Required = false, HelpText = "Output file path for lineage files")]
        public string OutputFile { get; set; }
    }
}

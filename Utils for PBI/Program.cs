using System;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using log4net;
using log4net.Config;
using System.Runtime.Versioning;
using Utils_for_PBI.Services;
using CommandLine;
using System.Runtime.InteropServices;

/* TO-DO:
 * Check for errors in lineage - Add Relationship (Active, Inactive)
 * Build script in github for automated building of exe
 * Add Installer for the program
 * Make classes more aligned with best practices (IDisposable)
 * See if async can be done for any other parts
 * Memory Leakage Check (windbg, MS CLR profiler)
 * Enhance Logging with Try-Catch Exceptions
 * Literal values as resources
 * Add Testingr
 * Check TomAPI for getting metadata
 * Test the program with different access levels and visuals. Take into account 3rd party visuals
 * Add full-screen for web view
 * Add a starting screen instead of 'Connect to dataset' - Add radio buttons for asking the type of report they have
 * Export reports and read visual information from them
 * Support PBIR format
 * Attribute Flaticons
 * Config file for more flexibility
 * Add filters (visual, page, all page) into the lineage
 */

/* TO-DO in HTML Page
 * Use mapData for width control
 * Add option to move from LR/RL
 * Try preset for Cytoscape
 * Use a proper color palette - Yellow or Green to align with PBI
 */

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]


namespace Utils_for_PBI.Forms
{
    /// <summary>
    ///  Program class is the entry point of the application. Contains the Main() function
    /// </summary>
    [SupportedOSPlatform("windows")]
    internal static class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FreeConsole();

        /*
         * STAThread ensures the Main program runs as a single thread, but Main() cannot be run
         * as Async Task as there seems to be a bug that does not allow STAThread and Async together
        */
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Logger.Info("No arguments received. Starting GUI Window");
                //XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));

                //ReportLineage reportLineage = new ReportLineage();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Logger.Info("Launching Application");
                Application.Run(new MainWindow());

            }
            else if (args.Length >= 1)
            {
                Logger.Info("Arguments received. Starting command line");

                AllocConsole();

                Console.WriteLine("Arguments received: " + string.Join(",", args));

                Parser.Default.ParseArguments<CommandLineOptions>(args)
                              .WithParsed<CommandLineOptions>(options => {
                                
                              })
                              .WithNotParsed<CommandLineOptions>(errs =>
                              {
                                  Console.WriteLine("Error parsing command line arguments. Please check the syntax by running --help");
                              });

                FreeConsole();

            }

        }
    }
}

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

/* TO-DO:
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
 * Use selectors and filters
 * Try preset for Cytoscape
 * Use Alpine.js
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
        /*
         * STAThread ensures the Main program runs as a single thread, but Main() cannot be run
         * as Async Task as there seems to be a bug that does not allow STAThread and Async together
        */
        [STAThread]
        static void Main(string[] args)
        {

            //XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));
            /* GetAwaiter().GetResult() waits for the function to complete (Synchronous) while
             * Task.Run() runs asynchronously and in case if it needs to be run synchronously, 
             * the GetAwaiter() line can be uncommented and the Task.Run() can be commented
             */
            
            //DownloadJSLibs().GetAwaiter().GetResult();
            
            Task.Run(() => DownloadJSLibs());

            //ReportLineage reportLineage = new ReportLineage();

            Logger.Info("Launching Application");
            Application.Run(new MainWindow());
        }

        /// <summary>
        ///  DownloadJSLibs is used to check if the AppData folder is created for this application 
        ///  and the necessary JS files for lineage graph are cached.The files are downloaded only if it does not 
        ///  already exist (after first run, the files are not downloaded again unless the files have been deleted)
        ///
        /// </summary>
        static async Task DownloadJSLibs()
        {
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Constants.ProgramName, "js");

            Dictionary<String, String> filesDict = new Dictionary<string, string>
            {
                { Constants.cytoscapeMinJS, Constants.cytoscapeMinJSURL },
                { Constants.dagreJS, Constants.dagreJSURL },
                { Constants.cytoscapeDagreJS, Constants.cytoscapeDagreJSURL }
            };

            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }
            foreach(var (fileName, fileURL) in filesDict)
            {
                var fileFullPath = Path.Combine(appDataPath, fileName);
                if (!File.Exists(fileFullPath))
                {
                    Logger.Info($"Downloading {fileName}");
                    using var client = new HttpClient();
                    using var response = await client.GetAsync(fileURL, HttpCompletionOption.ResponseHeadersRead);
                    response.EnsureSuccessStatusCode();

                    try
                    {
                        await using var fs = new FileStream(fileFullPath, FileMode.OpenOrCreate, FileAccess.Write);
                        await response.Content.CopyToAsync(fs);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex.Message);
                        MessageBox.Show($"Error: {ex.Message}", "Error downloading JS to local system", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Logger.Info($"Downloaded {fileName}");
                }
            }
        }
    }
}

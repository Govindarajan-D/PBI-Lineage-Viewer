using System;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Utils_for_PBI.Forms
{
    internal static class Program
    {
        /*
         * STAThread ensures the Main programs runs a single thread, but Main() cannot be run
         * as Async Task as there seems to be a bug that does not allow STAThread and Async together
        */
        [STAThread]
        static void Main(string[] args)
        {

            CheckPrerequisitesAsync().GetAwaiter().GetResult();
            Application.Run(new MainWindow());
        }
        /*
         * CheckPrerequisitesAsync is used to check if the AppData folder is created for this application
         * and the necessary JS files for lineage graph are cached. The files are downloaded only if it does
         * not already exist (after first run, the files are not downloaded again unless the files have been deleted)
        */
        static async Task CheckPrerequisitesAsync()
        {
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UtilsPBI");

            Dictionary<String, String> filesDict = new Dictionary<string, string>();
            filesDict.Add("cytoscape.min.js", "https://unpkg.com/cytoscape@3.30.0/dist/cytoscape.min.js");
            filesDict.Add("dagre.js", "https://unpkg.com/dagre@0.8.5/dist/dagre.js");
            filesDict.Add("cytoscape-dagre.js", "https://unpkg.com/cytoscape-dagre@2.5.0/cytoscape-dagre.js");

            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }
            foreach(var (fileName, fileURL) in filesDict)
            {
                var fileFullPath = Path.Combine(appDataPath, fileName);
                if (!File.Exists(fileFullPath))
                {
                    using var client = new HttpClient();
                    using var response = await client.GetAsync(fileURL, HttpCompletionOption.ResponseHeadersRead);
                    response.EnsureSuccessStatusCode();
                    await using var fs = new FileStream(fileFullPath, FileMode.OpenOrCreate, FileAccess.Write);
                    await response.Content.CopyToAsync(fs);
                }
            }

        }
    }
}

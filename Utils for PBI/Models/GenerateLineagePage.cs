using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Utils_for_PBI.Models
{
    public class GenerateLineagePage
    {
        public string HTMLFileLocation;
        public string GenerateHTMLPage()
        {
            string HTMLContent = Encoding.UTF8.GetString(File.ReadAllBytes("LineageGraph.bin"));
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UtilsPBI");
            Directory.CreateDirectory(appDataPath);
            HTMLFileLocation = Path.Combine(appDataPath, "Lineage.html");

            // Write the HTML content to the file
            File.WriteAllText(HTMLFileLocation, HTMLContent);

            return HTMLFileLocation;
        }
    }
}

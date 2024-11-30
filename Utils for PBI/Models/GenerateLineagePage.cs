using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Utils_for_PBI.Models
{
    public class GenerateLineagePage
    {
        public string HTMLFileLocation;
        public string GenerateHTMLPage()
        {
            string HTMLContent;
            var assembly = Assembly.GetExecutingAssembly();
            var HTMLFileResource = Constants.lineageGraphHTML;


            using (Stream stream = assembly.GetManifestResourceStream(HTMLFileResource))
            using (StreamReader reader = new StreamReader(stream))
            {
                HTMLContent = reader.ReadToEnd();
            }

            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UtilsPBI");
            Directory.CreateDirectory(appDataPath);
            HTMLFileLocation = Path.Combine(appDataPath, "Lineage.html");

            // Write the HTML content to the file
            File.WriteAllText(HTMLFileLocation, HTMLContent);

            return HTMLFileLocation;
        }
    }
}

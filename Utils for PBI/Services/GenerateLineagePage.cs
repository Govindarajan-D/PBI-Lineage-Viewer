using System;
using System.IO;
using System.Reflection;

namespace Utils_for_PBI.Services
{
    /// <summary>
    /// GenerateLineagePage copies the Lineage HTML file from the program resource to the AppData folder 
    /// </summary>
    public class GenerateLineagePage
    {
        public string HTMLFileLocation;
        public string GenerateHTMLPage()
        {
            string HTMLContent;
            var assembly = Assembly.GetExecutingAssembly();
            var HTMLFileResource = Constants.LineageGraphHTML;


            using (Stream stream = assembly.GetManifestResourceStream(HTMLFileResource))
            using (StreamReader reader = new StreamReader(stream))
            {
                HTMLContent = reader.ReadToEnd();
            }

            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UtilsPBI");
            Directory.CreateDirectory(appDataPath);
            HTMLFileLocation = Path.Combine(appDataPath, "index.html");
            
            // Write the HTML content to the file
            File.WriteAllText(HTMLFileLocation, HTMLContent);

            return HTMLFileLocation;
        }
    }
}

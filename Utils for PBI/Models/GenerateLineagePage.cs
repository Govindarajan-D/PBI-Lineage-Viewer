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
        public string htmlFileLocation;
        public string GenerateHTMLPage()
        {
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UtilsPBI");
            Directory.CreateDirectory(appDataPath);
            htmlFileLocation = Path.Combine(appDataPath, "Lineage.html");

            string htmlContent = @"
                <!DOCTYPE html>
                <html>
                <head>
                    <title>LineageViewer</title>
                </head>
                <body>
                    <h1>LineageViewer HTML Page</h1>
                </body>
                </html>";

            // Write the HTML content to the file
            File.WriteAllText(htmlFileLocation, htmlContent);

            return htmlFileLocation;
        }
    }
}

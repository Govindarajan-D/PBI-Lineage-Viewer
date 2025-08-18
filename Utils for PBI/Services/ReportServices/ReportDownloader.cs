using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System.IO;

namespace Utils_for_PBI.Services.ReportServices
{
    public class ReportDownloader
    {
        private static readonly HttpClient httpClient = new HttpClient();

        private ReportDownloader()
        {

        }

        public static async Task<string> DownloadReportAsync(string AccessToken, string group_id = "", string report_id = "")
        {
            var fileBytes = await ReportDownloader.FetchReportFromAPIAsync(AccessToken, $"{Constants.PowerBIAPIURL}/groups/{group_id}/reports/{report_id}/Export?downloadType=LiveConnect");
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Constants.ProgramName, $"{report_id}.pbix");
            await File.WriteAllBytesAsync(filePath, fileBytes);
            return filePath;

        }
        public static async Task<byte[]> FetchReportFromAPIAsync(string AccessToken, string reportUrl)
        { 
            using HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, reportUrl);

            requestMessage.Headers.Add("Authorization", AccessToken);

            using HttpResponseMessage response = await httpClient.SendAsync(requestMessage);

            var responseContent = await response.Content.ReadAsByteArrayAsync();

            return responseContent;
        }
    }
}

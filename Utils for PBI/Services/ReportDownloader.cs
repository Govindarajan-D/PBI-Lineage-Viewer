using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;

namespace Utils_for_PBI.Services
{
    public class ReportDownloader
    {
        private static readonly HttpClient httpClient = new HttpClient();

        private ReportDownloader()
        {

        }

        public static async Task<ReportDownloader> InitializeDownload(string AccessToken, string group_id = "", string report_id = "")
        {
            var instance = new ReportDownloader();
            await instance.DownloadReportAsync(AccessToken, $"https://api.powerbi.com/v1.0/myorg/groups/{group_id}/reports/{report_id}/Export?downloadType=LiveConnect");
            return instance;
        }

        public async Task<byte[]> DownloadReportAsync(string AccessToken, string reportUrl)
        {
            using HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, reportUrl);

            requestMessage.Headers.Add("Authorization", AccessToken);

            using HttpResponseMessage response = await httpClient.SendAsync(requestMessage);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return Encoding.ASCII.GetBytes(jsonResponse);
        }
    }
}

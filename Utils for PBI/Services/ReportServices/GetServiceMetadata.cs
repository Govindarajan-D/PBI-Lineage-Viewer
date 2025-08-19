using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation.Language;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Utils_for_PBI.Services.ReportServices
{
    public class GetServiceMetadata
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public GetServiceMetadata()
        {


        }

        public static async Task<String> FetchAdminMetadata(string AccessToken)
        {
            var serviceMetadataAPIURL = $"{Constants.PowerBIAdminAPIURL}/groups?$expand=reports&$top=1000";

            using HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, serviceMetadataAPIURL);
            requestMessage.Headers.Add("Authorization", AccessToken);
            
            using HttpResponseMessage response = await httpClient.SendAsync(requestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();

            JsonDocument jsonDocument = JsonDocument.Parse(responseContent);
            JsonElement workspaces = jsonDocument.RootElement.GetProperty("value");

            foreach (JsonElement workspace in workspaces.EnumerateArray())
            {
                JsonElement reportItems = workspace.GetProperty("reports");

                foreach (JsonElement reportItem in reportItems.EnumerateArray())
                {

                }

            }

            return responseContent;
        }
    }
}

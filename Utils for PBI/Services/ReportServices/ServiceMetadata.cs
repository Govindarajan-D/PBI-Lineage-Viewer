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
using Utils_for_PBI.Models.ServiceModels;

namespace Utils_for_PBI.Services.ReportServices
{
    public class ServiceMetadata
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private List<ServiceMetadataRow> _serviceMetadataRows = new List<ServiceMetadataRow>();

        public ServiceMetadata()
        {


        }

        public async Task<String> FetchAdminMetadata(string AccessToken)
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
                var workspaceId = workspace.GetProperty("id").ToString();
                var workspaceName = workspace.GetProperty("name").ToString();

                JsonElement reportItems = workspace.GetProperty("reports");

                foreach (JsonElement reportItem in reportItems.EnumerateArray())
                {
                    var serviceMetadataRow = new ServiceMetadataRow
                    {
                        workspaceId = workspaceId,
                        workspaceName = workspaceName,
                        reportId = reportItem.GetProperty("id").ToString(),
                        reportName = reportItem.GetProperty("name").ToString(),
                        datasetId = reportItem.GetProperty("datasetId").ToString()
                    };

                    _serviceMetadataRows.Add(serviceMetadataRow);

                }
            }

            return responseContent;
        }
    }
}

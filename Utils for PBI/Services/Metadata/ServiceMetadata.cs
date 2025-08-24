using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;


using Newtonsoft.Json.Linq;
using Utils_for_PBI.Models.ServiceModels;
using log4net;

namespace Utils_for_PBI.Services.Metadata
{
    /// <summary>
    /// ServiceMetadata class reads data from Admin API for getting metadata for every workspace 
    /// The API requires a POST request to be made after which an API is provided for checking the result status
    /// Once the result is available, it can be fetched which contains the metadata. 
    /// Reference: https://learn.microsoft.com/en-us/rest/api/power-bi/admin/workspace-info-post-workspace-info
    /// </summary>

    // TO-DO: Right now the function doesn't support paging of results. Need to expand it for querying metadata from larger number of workspaces
    public class ServiceMetadata
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ServiceMetadata));
        private static readonly HttpClient httpClient = new HttpClient();
        private List<ServiceMetadataRow> _serviceMetadataRows = new List<ServiceMetadataRow>();
        private string[] _workspaces;

        private ServiceMetadata()
        {

        }

        // Function to fetch the list of all the workspace IDs in the tenant
        public async Task<string[]> FetchWorkspaces(string AccessToken)
        {
            Logger.Info("Fetching all workspaces from the tenant");
            var workspaceMetadataAPIURL = $"{Constants.PowerBIAdminAPIURL}/groups?$top=100";
            using HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, workspaceMetadataAPIURL);
            requestMessage.Headers.Add("Authorization", AccessToken);
            using HttpResponseMessage response = await httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                JArray jArray = JArray.Parse(responseContent);
                return jArray.Select(item => item["id"].ToString()).ToArray();
            }
            return null;

        }

        /// <summary>
        /// Fetches Power BI Service metadata from getInfo API
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <returns></returns>
        public async Task FetchAdminMetadata(string AccessToken)
        {
            _workspaces = await FetchWorkspaces(AccessToken);

            // Once the workspace IDs are fetched, it is passed as body content to the getInfo API
            // getInfo API returns a scan ID which needs to be passed to scanStatus API to know if the result is ready to be fetched
            // Once the result is ready, it is fetched using scanResult API and parsed into object

            Logger.Info("Calling Workspaces Scan API for workspace metadata");


            var serviceMetadataAPIURL = $"{Constants.PowerBIAdminAPIURL}/workspaces/getInfo?lineage=true&datasourceDetails=true&datasetSchema=false&datasetExpressions=false&getArtifactUsers=true";

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

            var jsonWorkspacesArray = JsonContent.Create(_workspaces);

            var scanRequestResponse = await httpClient.PostAsync(serviceMetadataAPIURL, jsonWorkspacesArray);

            string metadataScanID;

            if (scanRequestResponse.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                //TO-DO: Throw exception
            }
            else
            {

                JObject jsonResponse = JObject.Parse(await scanRequestResponse.Content.ReadAsStringAsync());
                metadataScanID = jsonResponse["id"].ToString();

                string scanResultStatus = null;
                var scanResultStatusAPIURL = $"{Constants.PowerBIAdminAPIURL}/scanStatus/{metadataScanID}";
                var scanResultAPIURL = $"{Constants.PowerBIAdminAPIURL}/workspaces/scanResult/{metadataScanID}";

                // Check the status of Scan Result with wait time of 1 sec in between
                // Loop until the scan result status is not null

                while (scanResultStatus != null)
                {
                    using HttpRequestMessage scanResultStatusRequest = new HttpRequestMessage(HttpMethod.Get, scanResultStatusAPIURL);
                    scanResultStatusRequest.Headers.Add("Authorization", AccessToken);
                    using HttpResponseMessage scanResultStatusResponse = await httpClient.SendAsync(scanResultStatusRequest);

                    if (scanResultStatusResponse.IsSuccessStatusCode)
                    {
                        // Check is the Scan Result Status is succeeded, which means the result is available for fetching
                        var scanResultStatusResponseContent = await scanResultStatusResponse.Content.ReadAsStringAsync();
                        if (JObject.Parse(scanResultStatusResponseContent)["status"].ToString().ToUpper() == "SUCCEEDED")
                        {
                            using HttpRequestMessage scanResultRequest = new HttpRequestMessage(HttpMethod.Get, scanResultAPIURL);
                            scanResultRequest.Headers.Add("Authorization", AccessToken);

                            using HttpResponseMessage scanResultResponse = await httpClient.SendAsync(scanResultRequest);
                            ParseAdminMetadata(await scanResultResponse.Content.ReadAsStringAsync());

                            scanResultStatus = scanResultStatusResponse.IsSuccessStatusCode.ToString();
                            Logger.Info("Fetch complete for workspaces Scan Result");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Parses the response content returned by getInfo Power BI API URL
        /// </summary>
        /// <param name="responseContent"></param>
        public void ParseAdminMetadata(string responseContent)
        {
            /* The function parses json output as shown below
             * Sample JSON:
             * { 
             *  "workspaces": [
             *      {
             *          "id": <guid>,
             *          "name": <workspace-name>,
             *          "reports": [
             *              {
             *                  "id": <guid>,
             *                  "name": <report-name">,
             *                  "datasetId": <guid>
             *              }
             *       }
             *  }
             */

            JsonDocument jsonDocument = JsonDocument.Parse(responseContent);
            JsonElement workspaces = jsonDocument.RootElement.GetProperty("workspaces");

            foreach (JsonElement workspace in workspaces.EnumerateArray())
            {
                var workspaceId = workspace.GetProperty("id").ToString();
                var workspaceName = workspace.GetProperty("name").ToString();

                JsonElement reportItems = workspace.GetProperty("reports");

                foreach (JsonElement reportItem in reportItems.EnumerateArray())
                {
                    var serviceMetadataRow = new ServiceMetadataRow
                    {
                        WorkspaceID = workspaceId,
                        WorkspaceName = workspaceName,
                        ReportID = reportItem.GetProperty("id").ToString(),
                        ReportName = reportItem.GetProperty("name").ToString(),
                        DatasetID = reportItem.GetProperty("datasetId").ToString()
                    };

                    _serviceMetadataRows.Add(serviceMetadataRow);
                }
            }
        }

        public void GetServiceMetadata()
        {
            _serviceMetadataRows.Select(c => new
            {
                _serviceMetadataRow = c,
            });
        }
    }
}

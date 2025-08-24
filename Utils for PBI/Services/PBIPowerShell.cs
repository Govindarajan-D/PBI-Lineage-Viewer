using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

using log4net;
using Utils_for_PBI.Forms;

namespace Utils_for_PBI.Services
{
    public class PBIPowerShell
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(PBIPowerShell));
        public static async Task<PSDataCollection<PSObject>> RunPowerShell(string script)
        {
            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript(script);
                var results = await Task.Factory.FromAsync(ps.BeginInvoke(), ps.EndInvoke);
                return results;
            }
                
        }

        public static async Task<string> GetPowerBIAccessTokenAsync()
        {
            Logger.Info("Installing MicrosoftPowerBIMgmt PowerShell module");
            string installScript = "Install-Module -Name MicrosoftPowerBIMgmt -Force -Scope CurrentUser -AllowClobber";
            await RunPowerShell(installScript);

            Logger.Info("Importing MicrosoftPowerBIMgmt PowerShell module");
            string importScript = "Import-Module MicrosoftPowerBIMgmt -Force -ErrorAction Stop";
            await RunPowerShell(importScript);

            Logger.Info("Connecting to Power BI");
            string connectPowerBIToken = "Connect-PowerBIServiceAccount";
            await RunPowerShell(connectPowerBIToken);

            Logger.Info("Fetching Power BI Access Token through PowerShell");
            string getPowerBIToken = "Get-PowerBIAccessToken -AsString";
            var results = await RunPowerShell(getPowerBIToken);

            if (results.Count != 0 && results[0].BaseObject != null)
            {
                Logger.Info("Fetch complete for Power BI Access Token through PowerShell");
                return results[0].BaseObject.ToString();
            }

            return "Invalid Code";
        }
    }
}

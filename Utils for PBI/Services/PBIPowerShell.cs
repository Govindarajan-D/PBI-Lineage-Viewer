using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace Utils_for_PBI.Services
{
    public class PBIPowerShell
    {
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
            string installScript = "Install-Module -Name MicrosoftPowerBIMgmt -Force -Scope CurrentUser -AllowClobber";
            await RunPowerShell(installScript);

            string importScript = "Import-Module MicrosoftPowerBIMgmt -Force -ErrorAction Stop";
            await RunPowerShell(importScript);

            string connectPowerBIToken = "Connect-PowerBIServiceAccount";
            await RunPowerShell(connectPowerBIToken);

            string getPowerBIToken = "Get-PowerBIAccessToken -AsString";
            var results = await RunPowerShell(getPowerBIToken);

            if (results.Count != 0 && results[0].BaseObject != null)
            {
                return results[0].BaseObject.ToString();
            }

            return "Invalid Code";
        }
    }
}

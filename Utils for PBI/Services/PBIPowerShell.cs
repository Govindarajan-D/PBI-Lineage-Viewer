using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils_for_PBI.Services
{
    public class PBIPowerShell
    {
        public PBIPowerShell()
        {
            string installScript = "Install-Module -Name MicrosoftPowerBIMgmt -Force -Scope CurrentUser -AllowClobber";

            string importScript = "Import-Module MicrosoftPowerBIMgmt -Force -ErrorAction Stop";
        }
    }
}


namespace Utils_for_PBI
{
    public class Constants
    {
        public static readonly string ProgramName = "UtilsPBI";
        public static readonly string log4netconfigfile = "log4net.config";
        public static readonly string LineageGraphHTML = "Utils_for_PBI.FrontendWeb.dist.index.html";

        /*URLs need to end with '/' for it to be added to Server Prefix, the reason for InternalServerURLAddress having a slash at end*/
        public static readonly string InternalServerURLAddress = "http://localhost:8080/utilspbi/";

        public static readonly string PowerBIAPIURL = "https://api.powerbi.com/v1.0/myorg";
        public static readonly string PowerBIAdminAPIURL = "https://api.powerbi.com/v1.0/myorg/admin";

    }
}

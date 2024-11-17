using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerBIConnections.Connections
{
    public enum ConnectionType
    {
        PowerBIDesktop,
        PowerBIService
    }
    public class DatasetConnection
    {
        public String ConnectString { get; set; }
        public String DatasetName { get; set; }
        public ConnectionType ConnectionType { get; set; }
        public String DisplayName
        {
            get
            {
                return DatasetName + " (" + ConnectString + ")";
            }
        }
    }
}

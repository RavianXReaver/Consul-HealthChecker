using System.Collections.Generic;

namespace ConsulHealthChecker.Models
{
    public class ServiceListModel
    {
        public ServiceListAttributesModel Service { get; set; }
        
    }

    public class ServiceListAttributesModel
    {
        public string ID { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
    }
}
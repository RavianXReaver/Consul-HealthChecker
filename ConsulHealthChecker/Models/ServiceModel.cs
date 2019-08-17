namespace ConsulHealthChecker.Models
{
    public class ServiceModel
    {
        /// <summary>
        /// Address of the Consul Server : Host:port
        /// </summary>
        public string ConsulServerAddress { get; set; }
        /// <summary>
        /// Name of the Service
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// Health Check Interval in ms
        /// </summary>
        public int Interval { get; set; }
    }
}
using System.Collections.Generic;

namespace DostavItClient
{
    public class CarrierInfo
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Url { get; set; }
        public string LargeLogoUrl { get; set; }
        public IEnumerable<DeliveryService> Services { get; set; }
    }
}

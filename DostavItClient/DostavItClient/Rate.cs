using System;

namespace DostavItClient
{
    public class Rate
    {
        public string Carrier { get; set; }
        public string Service { get; set; }
        public DateTime ShipDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public Money Cost { get; set; }
        public Money Insurance { get; set; }
    }
}

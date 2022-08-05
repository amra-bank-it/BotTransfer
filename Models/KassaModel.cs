using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotTransfer.Models
{
    public class Amount
    {
        public string currency { get; set; }
        public string value { get; set; }
    }

    public class Confirmation
    {
        public string type { get; set; }
        public string confirmationUrl { get; set; }
        public string returnUrl { get; set; }
        public string webhookUrl { get; set; }
    }

    public class Metadata
    {
        public int orderId { get; set; }
    }

    public class Root
    {
        public string status { get; set; }
        public object capturedAt { get; set; }
        public object canceledAt { get; set; }
        public string id { get; set; }
        public Amount amount { get; set; }
        public DateTime createdAt { get; set; }
        public string description { get; set; }
        public Confirmation confirmation { get; set; }
        public Metadata metadata { get; set; }
        public string provider { get; set; }
        public string recipient { get; set; }
    }

}

using System;
using System.Collections.Generic;

namespace MetricsAgent.Models
{
    public class NetworkMetrics
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}

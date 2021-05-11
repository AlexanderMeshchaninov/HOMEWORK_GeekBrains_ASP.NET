using System;
using System.Collections.Generic;

namespace MetricsAgent.Models
{
    public class CpuMetrics
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public long Time { get; set; }
    }
}

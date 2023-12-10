using Core.Metric.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Metric.Concrate
{
    public class AknMetricContext : IAknMetricContext
    {
        public List<Prometheus.Counter> CounterList { get; set; } = new List<Prometheus.Counter>();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Metric.Abstract
{
    public interface IAknMetricContext
    {
        public List<Prometheus.Counter> CounterList { get; set; }
    }
}

using Core.Metric.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities
{
    public class AknMetricsUtilities
    {
        private readonly IAknMetricContext _aknMetricContext;
        public AknMetricsUtilities(IAknMetricContext aknMetricContext)
        {
            _aknMetricContext = aknMetricContext;
        }
        public  void CreateCounter(string path)
        {
            path = path.Replace("/", "_");
            path = path.Replace(".", "_");
            if (!string.IsNullOrEmpty(path))
            {
                var counter = Prometheus.Metrics.CreateCounter(path, path);
                counter.Inc();
                AddorUpdateCounter(counter);
            }
           

        }

        public void TotalRequestCounter()
        {
            var counter = Prometheus.Metrics.CreateCounter("total_request", "Akn System Total Request");
            counter.Inc();
            AddorUpdateCounter(counter);
        }

        public void TotalHttpStatusCodeCounter(string statusCode)
        {
            var counter= Prometheus.Metrics.CreateCounter($"statusCode_{statusCode}", $"status Code display total count http {statusCode}");
            counter.Inc();
            AddorUpdateCounter(counter);
        }

        public List<Prometheus.Counter> AddorUpdateCounter(Prometheus.Counter counter)
        {
            if (!_aknMetricContext.CounterList.Contains(counter))
            {
                _aknMetricContext.CounterList.Add(counter);
            }

            return _aknMetricContext.CounterList;
        }
    }
}

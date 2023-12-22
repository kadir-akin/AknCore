using Core.LogAkn.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.RequestContext.Concrate
{
    public class ProjectInfoConfiguration
    {
        public string ProjectName { get; set; }
        public string ApplicationName { get; set; }
        public int MinWorkerThreadsCount { get; set; }
        public int MinCompletionPortThreadsCount { get; set; }
        public int MaxWorkerThreadsCount { get; set; }
        public int MaxCompletionPortThreadsCount { get; set; }

    }
}

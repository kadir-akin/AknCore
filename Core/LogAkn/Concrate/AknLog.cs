using Core.LogAkn.Abstract;
using Core.Security.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.LogAkn.Concrate
{
    public class AknLog :IAknLog
    {

        public IAknRequestContext RequestContext { get; set; }

        public IAknUser User { get; set; }

        public int MyProperty { get; set; }


    }
}

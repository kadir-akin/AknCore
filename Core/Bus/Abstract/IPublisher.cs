using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Abstract
{
    public interface IPublisher
    {
        public Task Send(IBusMessage message);
    }
}

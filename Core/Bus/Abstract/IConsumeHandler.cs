using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Abstract
{
    public interface IConsumeHandler<TBusMessage> : IConsumeHandler where TBusMessage : class,IBusMessage
    {
        public Task HandleAsync(TBusMessage message);

    }

    public interface IConsumeHandler 
    {
        public Task HandleAsync(IBusMessage message);
    }
}

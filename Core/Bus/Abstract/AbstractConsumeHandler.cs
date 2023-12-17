using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Abstract
{
    public abstract class AbstractConsumeHandler<TBusMessage> : IConsumeHandler<TBusMessage>, IConsumeHandler where TBusMessage : class, IBusMessage
    {
        public Task HandleAsync(IBusMessage message)
        {
            return ConsumeHandleAsync((TBusMessage)message); ;
        }

        public Task HandleAsync(TBusMessage message)
        {
           return ConsumeHandleAsync(message);
        }


        public abstract Task ConsumeHandleAsync(TBusMessage message);
    }
}

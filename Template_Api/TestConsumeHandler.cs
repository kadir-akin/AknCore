using Core.Bus.Abstract;
using Core.Bus.RabbitMq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Template_Api
{
    public class TestConsumeHandler : AbstractConsumeHandler<BusMessageTest>
    {
        public override Task ConsumeHandleAsync(BusMessageTest message)
        {
            var result = JsonConvert.SerializeObject(message);
            return Task.FromResult(result);
        }
    }
    [RabbitMq("test", "test")]
    public class BusMessageTest :IBusMessage
    {
        public string Deneme { get; set; }
    }
}

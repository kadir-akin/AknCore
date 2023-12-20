using Core.Bus.Abstract;
using Core.Bus.RabbitMq;
using Core.LogAkn.Abstract;
using Core.LogAkn.Extantions;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Template_Api
{
    public class TestConsumeHandler : AbstractConsumeHandler<BusMessageTest>
    {
        private readonly ILogService _logService;
        public TestConsumeHandler(ILogService logService)
        {
            _logService = logService;
        }
        public override Task ConsumeHandleAsync(BusMessageTest message)
        {
            var result = JsonConvert.SerializeObject(message);
            _logService.LogDebug(result);
            return Task.FromResult(result);
        }
    }
    [RabbitMq("test", "test")]
    public class BusMessageTest :IBusMessage
    {
        public string Deneme { get; set; }
    }
}

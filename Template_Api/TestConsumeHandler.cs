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
            _logService.LogInformationAsync($"Test topiğinden mesaj geldi : {message.Deneme}");
            return Task.CompletedTask;
        }
    }
    [RabbitMq("test", "test",true)]
    public class BusMessageTest :IBusMessage
    {
        public string Deneme { get; set; }
    }


    public class DenemeConsumeHandler : AbstractConsumeHandler<DenemeMessageTest>
    {
        private readonly ILogService _logService;
        public DenemeConsumeHandler(ILogService logService)
        {
            _logService = logService;
        }
        public override Task ConsumeHandleAsync(DenemeMessageTest message)
        {
            _logService.LogInformationAsync($"Deneme topiğinden mesaj geldi : {message.Deneme}");
            return Task.CompletedTask;
        }
    }
    [RabbitMq("deneme", "deneme", true)]
    public class DenemeMessageTest : IBusMessage
    {
        public string Deneme { get; set; }
    }
}

using System.Text.Json;
using System.Threading.Tasks;
using DotNetCore.CAP;
using DotNetCore.CAP.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.WebApi.EventHandlers.Ctos;

namespace Sample.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublishController : ControllerBase
    {
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly ILogger<PublishController> _logger;
        
        public PublishController(IDistributedEventBus distributedEventBus, ILogger<PublishController> logger)
        {
            _distributedEventBus = distributedEventBus;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<string> Get()
        {
            _logger.LogInformation("发布消息成功");
            await _distributedEventBus.PublishAsync(new StockCountChangeCto
            {
                OrderId = 9898,
                StockCount = 68
            });
            
            return "OK";
        }

        [NonAction]
        //[CapSubscribe("StockCountChangeCallback")]
        public Task StockCountChangeCallback(StockCountChangeCallbackCto data)
        {
            _logger.LogInformation("业务回调了");
            _logger.LogInformation(JsonSerializer.Serialize(data));
            return Task.CompletedTask;
        }
    }
}
using System.Text.Json;
using System.Threading.Tasks;
using DotNetCore.CAP;
using DotNetCore.CAP.Extensions;
using Microsoft.Extensions.Logging;
using Sample.WebApi.EventHandlers.Ctos;

namespace Sample.WebApi.EventHandlers
{
    public class StockCountChangeHandler : DistributedEventHandler<StockCountChangeCto, StockCountChangeCallbackCto>
    {
        private ILogger<StockCountChangeHandler> _logger;
        public StockCountChangeHandler(ILogger<StockCountChangeHandler> logger)
        {
            _logger = logger;
        }
        
        public override Task<StockCountChangeCallbackCto> HandleEventAsync(StockCountChangeCto eventData, [FromCap] CapHeader headers)
        {
            _logger.LogInformation($"更新库存入参: {JsonSerializer.Serialize(eventData)}");
            _logger.LogInformation("库存更新成功");
            // todo 其他业务
            return Task.FromResult(new StockCountChangeCallbackCto
            {
                Success = true,
                Message = "库存更新成功"
            });
        }
    }

    public class StockCountChangeCallbackHandler : DistributedEventHandler<StockCountChangeCallbackCto>
    {
        private ILogger<StockCountChangeCallbackHandler> _logger;
        public StockCountChangeCallbackHandler(ILogger<StockCountChangeCallbackHandler> logger)
        {
            _logger = logger;
        }
        
        public override Task HandleEventAsync(StockCountChangeCallbackCto eventData, [FromCap] CapHeader headers)
        {
            _logger.LogInformation("业务回调了");
            _logger.LogInformation(JsonSerializer.Serialize(eventData));
            return Task.CompletedTask;
        }
    }
}
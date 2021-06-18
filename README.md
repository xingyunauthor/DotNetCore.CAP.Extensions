# DotNetCore.CAP.Extensions 
> DotNetCore.CAP.Extensions 对 DotNetCore.CAP 做了扩展，使用传统的事件发布的写法，并且与 CAP 原来的写法相兼容

### 配置

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddTransient<StockCountChangeHandler>();
    services.AddTransient<StockCountChangeCallbackHandler>();

    services.AddCapExt(options =>
    {
        options.UseInMemoryStorage();
        options.UseInMemoryMessageQueue();
    });
}
```
### 定义两个 Cto
```cs
public class StockCountChangeCallbackCto
{
    public bool Success { get; set; }
    
    public string Message { get; set; }
}

public class StockCountChangeCto : IEventCallback<StockCountChangeCallbackCto>
{
    public long OrderId { get; set; }
    
    public int StockCount { get; set; }
}
```

### Controller 定义发布信息 Action
```cs
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
```

### 最后定义 EventHandler
```cs
public class StockCountChangeHandler : DistributedEventHandler<StockCountChangeCto, StockCountChangeCallbackCto>
{
    private ILogger<StockCountChangeHandler> _logger;
    public StockCountChangeHandler(ILogger<StockCountChangeHandler> logger)
    {
        _logger = logger;
    }
    
    public override Task<StockCountChangeCallbackCto> HandleEventAsync(StockCountChangeCto eventData, [FromCap] CapHeader headers)
    {
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
        _logger.LogInformation(JsonSerializer.Serialize(eventData));
        return Task.CompletedTask;
    }
}
```
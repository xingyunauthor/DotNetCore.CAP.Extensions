using DotNetCore.CAP;

namespace Sample.WebApi.EventHandlers.Ctos
{
    /// <summary>
    /// CapSubscribeAttribute 可以不定义
    /// </summary>
    [CapSubscribe("StockCountChangeCallback")]
    public class StockCountChangeCallbackCto
    {
        public bool Success { get; set; }
        
        public string Message { get; set; }
    }
}
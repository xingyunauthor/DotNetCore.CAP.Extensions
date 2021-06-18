using DotNetCore.CAP;
using DotNetCore.CAP.Extensions;

namespace Sample.WebApi.EventHandlers.Ctos
{
    /// <summary>
    /// CapSubscribeAttribute 可以不定义
    /// </summary>
    [CapSubscribe("StockCountChange")]
    public class StockCountChangeCto : IEventCallback<StockCountChangeCallbackCto>
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public long OrderId { get; set; }
        
        /// <summary>
        /// 库存
        /// </summary>
        public int StockCount { get; set; }
    }
}
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DotNetCore.CAP.Extensions.Impl
{
    public class CapDistributedEventBus : IDistributedEventBus
    {
        private readonly ICapPublisher _publisher;

        public CapDistributedEventBus(ICapPublisher publisher)
        {
            _publisher = publisher;
        }
        
        public async Task PublishAsync<T>(T data)
        {
            var attribute = typeof(T).GetCustomAttribute(typeof(CapSubscribeAttribute));
            var name = attribute != null
                ? ((CapSubscribeAttribute) attribute).Name
                : typeof(T).ToString();

            string callbackName = null;
            if (typeof(T).HasImplementedRawGeneric(typeof(ICapCallback<>)))
            {
                var capCallbackType = typeof(T).GetInterfaces().Single(a => a.HasImplementedRawGeneric(typeof(ICapCallback<>)));
                var argumentType = capCallbackType.GetGenericArguments()[0];
                var callbackAttribute = argumentType.GetCustomAttribute<CapSubscribeAttribute>();
                callbackName = callbackAttribute != null
                    ? callbackAttribute.Name
                    : argumentType.ToString();
            }
            
            await _publisher.PublishAsync(name, data, callbackName);
        }
    }
}
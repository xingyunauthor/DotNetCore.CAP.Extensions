using System.Threading.Tasks;

namespace DotNetCore.CAP.Extensions
{
    public class DistributedEventHandler<T>
    {
        public virtual Task HandleEventAsync(T eventData)
        {
            return Task.CompletedTask;
        }
        
        public virtual Task HandleEventAsync(T eventData, [FromCap] CapHeader headers)
        {
            return Task.CompletedTask;
        }
    }

    public class DistributedEventHandler<T, T1> where T : IEventCallback<T1> 
        where T1 : class
    {
        public virtual Task<T1> HandleEventAsync(T eventData)
        {
            return Task.FromResult(default(T1));
        }
        
        public virtual Task<T1> HandleEventAsync(T eventData, [FromCap] CapHeader headers)
        {
            return Task.FromResult(default(T1));
        }
    }
}
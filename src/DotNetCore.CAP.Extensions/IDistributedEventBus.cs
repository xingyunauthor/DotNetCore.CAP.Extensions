using System.Threading.Tasks;

namespace DotNetCore.CAP.Extensions
{
    public interface IDistributedEventBus
    {
        Task PublishAsync<T>(T data);
    }
}
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCore.CAP.Extensions
{
    public interface IDistributedEventBus
    {
        Task PublishAsync<T>(T data, CancellationToken cancellationToken = default);

        Task PublishAsync<T>(T data, IDictionary<string, string> headers, CancellationToken cancellationToken = default);
    }
}
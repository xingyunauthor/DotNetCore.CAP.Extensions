using System;
using DotNetCore.CAP;
using DotNetCore.CAP.Extensions;
using DotNetCore.CAP.Extensions.Impl;
using DotNetCore.CAP.Internal;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        internal static IServiceCollection ServiceCollection;
        
        public static CapBuilder AddCapExt(this IServiceCollection services, Action<CapOptions> setupAction)
        {
            ServiceCollection = services;

            services.AddTransient<IDistributedEventBus, CapDistributedEventBus>();
            services.AddSingleton<IConsumerServiceSelector, MyConsumerServiceSelector>();
            
            return services.AddCap(setupAction);
        }
    }
}
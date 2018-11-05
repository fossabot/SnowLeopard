﻿using System;
using AspectCore.Extensions.Autofac;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SnowLeopard.Caching;
using SnowLeopard.Caching.Abstractions;

namespace SnowLeopard
{
    /// <summary>
    /// RedisCachingExtensions
    /// </summary>
    public static class RedisCachingExtensions
    {
        /// <summary>
        /// AddSnowLeopardRedisCache
        /// </summary>
        /// <param name="services"></param>
        public static IServiceProvider AddSnowLeopardRedisCache(this IServiceCollection services)
        {
            services.AddTransient<ICachingProvider, RedisCachingProvider>();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterDynamicProxy();

            var serviceProvider = new AutofacServiceProvider(builder.Build());
            GlobalServices.SetServiceProvider(serviceProvider);

            return serviceProvider;
        }

    }
}

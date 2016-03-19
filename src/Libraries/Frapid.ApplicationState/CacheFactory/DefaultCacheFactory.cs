﻿using System;
using Frapid.Configuration;

namespace Frapid.ApplicationState.CacheFactory
{
    public class DefaultCacheFactory : ICacheFactory
    {
        public DefaultCacheFactory()
        {
            this.Factory = this.GetDefault();
        }

        public ICacheFactory Factory { get; }

        public bool Add<T>(string key, T value, DateTimeOffset expiresAt)
        {
            return this.Factory.Add(key, value, expiresAt);
        }

        public T Get<T>(string key) where T : class
        {
            return this.Factory.Get<T>(key);
        }

        private ICacheFactory GetDefault()
        {
            string type = CacheConfig.GetDefaultCacheType();

            switch (type)
            {
                case "Redis":
                    return new RedisCacheFactory();
                default:
                    return new MemoryCacheFactory();
            }
        }
    }
}
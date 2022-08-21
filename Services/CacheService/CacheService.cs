using Microsoft.Extensions.Caching.Memory;
using System;

namespace Slackiffy.Services.CacheService
{
    public class CacheService : ICacheService
    {
        public CacheService(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        private readonly IMemoryCache memoryCache;

        public T GetData<T>(string key)
        {
            try
            {
                T item = (T)this.memoryCache.Get(key);
                return item;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            bool res = true;
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    this.memoryCache.Set(key, value, expirationTime);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return res;
        }
        public void RemoveData(string key)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    this.memoryCache.Remove(key);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}

using Domain.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.CacheService
{
    public class CacheService (ICacheRepository _cacheRepository): ICacheService
    {
        public async Task<string> GetAsync(string cacheKey)
        
          =>await _cacheRepository.GetAsync(cacheKey);
        public async Task SetAsync(string cacheKey, string cacheValue, TimeSpan timeToLive)
        => await _cacheRepository.SetAsync(cacheKey, cacheValue, timeToLive);
        
    }
}

using System.Text.Json;
using RedisAPI.Models;
using StackExchange.Redis;

namespace RedisAPI.Data
{
    public class RedisPlatformRepo : IPlatformRepo
    {
        private readonly IConnectionMultiplexer _redis;
        public RedisPlatformRepo(IConnectionMultiplexer redis)
        {
            _redis=redis;
        }
        public void CreatePlatform(Platform platform)
        {
            if(platform==null)
            {
                throw new ArgumentOutOfRangeException(nameof(platform) );
            }
            var db = _redis.GetDatabase();
            var serialPlat=JsonSerializer.Serialize(platform);
            db.StringSet(platform.Id, serialPlat);
            db.SetAdd("PlatformSet",serialPlat);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            throw new NotImplementedException();
        }

        public Platform? GetPlatformById(string id)
        {
            var db = _redis.GetDatabase();
            var plat=db.StringGet(id);
            if(!string.IsNullOrEmpty(plat))
            {
                return JsonSerializer.Deserialize<Platform>(plat);
            }
            return null;
        }
    }
}
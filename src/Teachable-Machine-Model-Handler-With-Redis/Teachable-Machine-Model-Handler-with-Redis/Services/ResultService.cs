using StackExchange.Redis;

namespace Teachable_Machine_Model_Handler_with_Redis.Services
{
    public class ResultService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public ResultService()
        {
            _redis = ConnectionMultiplexer.Connect("127.0.0.1");
            _database = _redis.GetDatabase();
        }

        public void SendResultToRedisChannel(string result)
        {
            // Gửi dữ liệu video tới Redis channel
            //_database.Publish("video", videoData);
            _database.StringSet("result", result);
        }
    }
}

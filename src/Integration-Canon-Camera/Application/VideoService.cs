using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfExample
{
    public class VideoService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public VideoService()
        {
            _redis = ConnectionMultiplexer.Connect("127.0.0.1");
            _database = _redis.GetDatabase();
        }

        public void SendVideoDataToRedisChannel(byte[] videoData)
        {
            // Gửi dữ liệu video tới Redis channel
            //_database.Publish("video", videoData);
            _database.StringSet("video", videoData);
        }
    }
}

using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfExample
{
    public class ResultService
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public ResultService()
        {
            _redis = ConnectionMultiplexer.Connect("127.0.0.1");
            _database = _redis.GetDatabase();
        }

        public string ListenForData()
        {
            string imageInRedis = _database.StringGet("result");
            return imageInRedis;
        }
    }
}

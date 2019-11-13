using DotNetServerless.Models;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetServerless.Service
{
    public class InstanceService : IInstanceService
    {
        private static string CounterKey = "LambdaInstanceCounter";
        private static int DefaultDatabase = 0;

        protected IRedisCacheClient Cli { get; }
        public InstanceService([NotNull]IRedisCacheClient cli)
        {
            Cli = cli;
        }

        private IRedisDatabase DB
        {
            get
            {
                return Cli.Db0;
            }
        }


        public async Task<string> GetInstanceId()
        {
            string client = Guid.NewGuid().ToString();
            await DB.SortedSetAddAsync(CounterKey, client, 0.0);
            return client;   
        }



        public async Task<int> IncrementInstance(string instanceId)
        {
            var ct = await DB.SortedSetAddIncrementAsync(CounterKey, instanceId, 1.0);
            return (int)ct;
        }

        public async Task ResetInstanceKey()
        {
            await DB.RemoveAsync(CounterKey);
        }

        public async Task<IEnumerable<InstanceInformation>> GetInstanceStats()
        {
            var data = await DB.Database.SortedSetRangeByRankWithScoresAsync(CounterKey);
            var retVal = data.Select(a => new InstanceInformation()
            {
                ExecutionCount = (int)a.Score,
                InstanceId = a.Element.ToString()?.Replace("\"", string.Empty)
            }) ;
            
            return retVal;
        }
    }
}

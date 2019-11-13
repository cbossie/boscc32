using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetServerless.Models;

namespace DotNetServerless.Service
{
    public class SensorService : ISensorService
    {
        readonly Random Rng = new Random(DateTime.Now.Millisecond);
        const int MaxPauseMilliseconds = 5000;

        public SensorService()
        {
                
        }
        public async Task<SensorData> GetData(string sensorId)
        {
            var data = new SensorData(sensorId, new Decimal(Rng.NextDouble()));
            //Pause so that methods don't all return instantaneously
            await Task.Delay(Rng.Next(0, MaxPauseMilliseconds));
            return data;
        }
    }
}

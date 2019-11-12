using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetServerless.Models;

namespace DotNetServerless.Service
{
    public class SensorService : ISensorService
    {
        public SensorService()
        {

        }
        public SensorData GetData(string sensorId)
        {
            var data = new SensorData(sensorId, decimal.One);
            return data;
        }
    }
}

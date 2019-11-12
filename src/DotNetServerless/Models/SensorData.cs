using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetServerless.Models
{
    public class SensorData : ModelBase
    {
        public SensorData(string sensorId, decimal data)
        {
            SensorId = sensorId;
            Data = data;
        }
        public string SensorId { get; set; }
        public decimal Data { get; set; }
    }
}

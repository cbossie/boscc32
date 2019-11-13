using DotNetServerless.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetServerless.Service
{
    public interface ISensorService
    {
        Task<SensorData> GetData(string sensorId);
    }
}

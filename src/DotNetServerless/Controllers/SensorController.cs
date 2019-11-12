using System;
using DotNetServerless.Filters;
using DotNetServerless.Models;
using DotNetServerless.Service;
using Microsoft.AspNetCore.Mvc;

namespace DotNetServerless.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : BaseController
    {
        private ISensorService Svc { get; }

        public SensorController(ISensorService svc, InstanceInformation info) :
            base(info)
        {
            Svc = svc;
        }

        [HttpGet("{sensorId}")]

        public SensorData GetSensorData(string? sensorId)
        {
            var data = Svc.GetData(sensorId?? throw new ArgumentNullException(nameof(sensorId)));
            return data;
        }


    }
}
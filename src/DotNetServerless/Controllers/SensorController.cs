using System;
using System.Threading.Tasks;
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
        private IInstanceService InstSvc {get;}

        public SensorController(ISensorService svc, InstanceInformation info, IInstanceService inst) :
            base(info)
        {
            Svc = svc;
            InstSvc = inst;
        }

        [HttpGet("{sensorId}")]

        public SensorData GetSensorData(string? sensorId)
        {
            var data = Svc.GetData(sensorId?? throw new ArgumentNullException(nameof(sensorId)));
            return data;
        }

        [HttpGet("Reset")]
        public async Task<ModelBase> ResetInstanceCount()
        {
            await InstSvc.ResetInstanceKey();
            return new ModelBase();
        }

    }
}
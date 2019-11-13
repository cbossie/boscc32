using System;
using System.Collections.Generic;
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

        [HttpGet("data/{sensorId}")]

        public async Task<ActionResult<SensorData>> GetSensorData(string? sensorId)
        {
            var data = await Svc.GetData(sensorId?? throw new ArgumentNullException(nameof(sensorId)));
            return this.Ok(data);
        }

        [HttpGet("Reset")]
        public async Task<ActionResult<ModelBase>> ResetInstanceCount()
        {
            await InstSvc.ResetInstanceKey();
            return Ok(new ModelBase());
        }

        [HttpGet("stats")]
        public async Task<ActionResult<IEnumerable<InstanceInformation>>> GetInstanceExecutionStats() 
        {
            var data = await InstSvc.GetInstanceStats();
            return Ok(data);
        }



    }
}
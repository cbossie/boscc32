using DotNetServerless.Filters;
using DotNetServerless.Models;
using DotNetServerless.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetServerless.Controllers
{
    [ServiceFilter(typeof(IncrementInstanceCountFilter))]
    public class BaseController : ControllerBase
    {
        protected InstanceInformation Info { get; }

        public BaseController(InstanceInformation info)
        {
            Info = info;
        }

        protected void UpdateInstanceInformation(ModelBase model)
        {
            model.InstanceID = Info.InstanceId;
            model.ExecutionCount = Info.ExecutionCount;
        }


    }
}

using DotNetServerless.Models;
using DotNetServerless.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetServerless.Filters
{
    public class IncrementInstanceCountFilter : IAsyncActionFilter
    {
        private InstanceInformation Info { get; }
        private IInstanceService Svc { get; }

        public IncrementInstanceCountFilter(InstanceInformation info, IInstanceService svc)
        {
            Info = info;
            Svc = svc;
        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (Info.InstanceId == null)
            {
                Info.InstanceId = await Svc.GetInstanceId();
            }
            Info.ExecutionCount = await Svc.IncrementInstance(Info.InstanceId);
            
            var resultContext = await next();

            var res = resultContext.Result as ObjectResult;
            var mb = res?.Value as ModelBase;
            if (mb != null)
            {
                mb.ExecutionCount = Info.ExecutionCount;
                mb.InstanceID = Info.InstanceId;
            }

        }
    }
}

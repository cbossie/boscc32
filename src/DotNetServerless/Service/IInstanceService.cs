using DotNetServerless.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetServerless.Service
{
    public interface IInstanceService
    {
        Task<int> IncrementInstance(string instanceId);
        Task<string> GetInstanceId();
        Task ResetInstanceKey();

        Task<IEnumerable<InstanceInformation>> GetInstanceStats();

    }
}

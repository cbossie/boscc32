using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetServerless.Models
{
    public class InstanceInformation
    {
       public string? InstanceId { get; set; }
        public int ExecutionCount { get; set; }
    }
}

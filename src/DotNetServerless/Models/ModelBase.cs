using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetServerless.Models
{
    public abstract class ModelBase
    {
        public string? InstanceID { get; set; }
        public int ExecutionCount { get; set; }
    }
}

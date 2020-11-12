using System;
using System.Collections.Generic;
using System.Text;

namespace Sysplan.Crosscutting.Infrastructure.Contexts.Redis
{
    public class RedisContextConfig
    {
        public string ConnectionString { get; set; }
        public string InstanceName { get; set; }
    }
}

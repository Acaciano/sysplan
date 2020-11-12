using System;
using System.Collections.Generic;
using System.Text;

namespace Sysplan.Crosscutting.Common.Data
{
    public class ExportObject<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> Results { get; set; }
    }
}

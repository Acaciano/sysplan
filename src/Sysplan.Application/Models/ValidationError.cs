using System;
using System.Collections.Generic;
using System.Text;

namespace Sysplan.Application.Models
{
    public class ValidationError
    {
        public int RowNumber { get; set; }
        public string ColumnName { get; set; }
        public string Data { get; set; }
        public string ValidationMessage { get; set; }
    }
}

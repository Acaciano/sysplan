using System.Collections.Generic;

namespace Sysplan.Crosscutting.Domain.Bus.Extensions
{
    public class TResult<T>
    {
        public TResult() { }
        public bool Success { get; set; }
        public List<string> ErrorMessages { get; set; }
        public T Result { get; set; }
    }
}
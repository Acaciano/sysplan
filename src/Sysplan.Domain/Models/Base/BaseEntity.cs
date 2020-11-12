using Sysplan.Crosscutting.Domain.Model;
using System;

namespace Sysplan.Domain.Models.Base
{
    public class BaseEntity : Entity
    {
        public BaseEntity()
        {
            this.MarcarStatusAtivo();
        }
    }
}

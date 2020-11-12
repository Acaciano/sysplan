using System;

namespace Sysplan.Crosscutting.Domain.Model
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
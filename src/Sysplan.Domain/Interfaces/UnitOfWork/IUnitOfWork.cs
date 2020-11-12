using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sysplan.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync();
    }
}

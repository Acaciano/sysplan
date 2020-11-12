using Sysplan.Application.ViewModels;
using Sysplan.Crosscutting.Common.Data;
using Sysplan.Domain.Commands;
using System;
using System.Threading.Tasks;

namespace Sysplan.Application.Interfaces
{
    public interface IClienteServiceApp
    {
        Task<PagedList<ClienteViewModel>> GetPaged(int page, int size, string orderProperty,
            bool orderCrescent, string filterProperty, string filterValue);
        Task<ClienteViewModel> Get(Guid id);
        Task<bool> Add(AddClienteCommand model);
        Task<bool> Update(UpdateClienteCommand model);
        Task Remove(Guid id);
    }
}

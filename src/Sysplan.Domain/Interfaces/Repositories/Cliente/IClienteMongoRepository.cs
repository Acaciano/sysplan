using Sysplan.Crosscutting.Domain.Interfaces.Repositories;
using Sysplan.Domain.Models;
using System.Threading.Tasks;

namespace Sysplan.Domain.Interfaces.Repositories
{
    public interface IClienteMongoRepository : IMongoDbRepository<Cliente>
    {
        Task<Cliente> BuscarPorEmailESenha(string email, string senha);
    }
}
using Sysplan.Crosscutting.Infrastructure.Contexts.MongoDb;
using Sysplan.Crosscutting.Infrastructure.Repositories;
using Sysplan.Domain.Interfaces.Repositories;
using Sysplan.Domain.Models;
using System.Threading.Tasks;

namespace Sysplan.Infrastructure.Repositories
{
    public class ClienteMongoRepository : MongoRepository<Cliente>, IClienteMongoRepository
    {
        public ClienteMongoRepository(IMongoDbContext context, string collectionName = "Cliente")
            : base(collectionName, context) { }

        public async Task<Cliente> BuscarPorEmailESenha(string email, string senha)
        {
            return await this.GetOneAsync(x => x.Email == email && x.Senha == senha);
        }
    }
}
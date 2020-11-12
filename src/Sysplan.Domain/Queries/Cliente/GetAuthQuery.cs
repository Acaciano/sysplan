using Sysplan.Domain.Models;

namespace Sysplan.Domain.Queries
{
    public class GetAuthQuery : ClienteQuery<Cliente>
    {
        public GetAuthQuery(string email, string senha)
        {
            this.Email = email;
            this.Senha = senha;
        }

        public string Email { get; set; }
        public string Senha { get; set; }

        public override bool IsValid()
        {
            return true;
        }
    }
}
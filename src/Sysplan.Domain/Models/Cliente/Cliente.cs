using Sysplan.Crosscutting.Domain.Model;

namespace Sysplan.Domain.Models
{
    public class Cliente : Entity
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public int Idade { get; private set; }
        public string Senha { get; private set; }

        public void SetNome(string nome) => this.Nome = nome;
        public void SetEmail(string email) => this.Email = email;
        public void SetIdade(int idade) => this.Idade = idade;
        public void SetSenha(string senha) => this.Senha = senha;

        public Cliente()
        {
            this.MarcarStatusAtivo();
        }
    }
}

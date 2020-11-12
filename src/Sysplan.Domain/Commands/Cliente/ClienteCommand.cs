using Sysplan.Crosscutting.Domain.Commands;
using System;

namespace Sysplan.Domain.Commands
{
    public abstract class ClienteCommand : Command
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Idade { get; set; }
        public string Senha { get; set; }
    }
}
using System;

namespace Sysplan.Application.ViewModels
{
    public class ClienteViewModel
    {
        public Guid? Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Idade { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
    }
}

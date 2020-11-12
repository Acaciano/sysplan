using Sysplan.Crosscutting.Common.Extensions;
using System;

namespace Sysplan.Crosscutting.Domain.Model
{
    public class Entity : IEntity
    {
        public Guid Id { get; set; }
        public bool Ativo { get; private set; }
        public DateTime DataCriacao { get; private set; } = DateTime.Now.ToBrazilianTimezone();
        public DateTime? DataAlteracao { get; private set; }

        public void SetId(Guid id) => this.Id = id;

        public void AtualizarDataAlteracao()
        {
            this.DataAlteracao = DateTime.Now.ToBrazilianTimezone();
        }

        protected void MarcarStatusAtivo() => this.Ativo = true;

        protected void MarcarStatusInativo() => this.Ativo = false;
    }
}
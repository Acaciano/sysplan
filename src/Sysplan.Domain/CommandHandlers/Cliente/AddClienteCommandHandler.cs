using Sysplan.Crosscutting.Common.Security;
using Sysplan.Crosscutting.Domain.Bus;
using Sysplan.Crosscutting.Domain.Commands;
using Sysplan.Domain.Commands;
using Sysplan.Domain.Events;
using Sysplan.Domain.Interfaces.Repositories;
using Sysplan.Domain.Interfaces.UnitOfWork;
using Sysplan.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Sysplan.Domain.CommandHandlers
{
    public class AddClienteCommandHandler : MediatorCommandHandlerBase<AddClienteCommand>
    {
        private readonly IClienteSqlServerRepository _clienteSqlServerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddClienteCommandHandler(
            IClienteSqlServerRepository clienteSqlServerRepository,
            IUnitOfWork unitOfWork,
            IMediatorHandler mediator) : base(mediator)
        {
            _clienteSqlServerRepository = clienteSqlServerRepository;
            _unitOfWork = unitOfWork;
        }

        public override async Task<bool> AfterValidation(AddClienteCommand request)
        {
            var registered = await _clienteSqlServerRepository
                .ExistsByExpressionAsync(x => x.Email == request.Email);

            if (registered)
            {
                NotifyError("O registro já existe");
                return false;
            }

            Cliente cliente = new Cliente();

            cliente.SetId(Guid.NewGuid());
            cliente.SetNome(request.Nome);
            cliente.SetEmail(request.Email);
            cliente.SetIdade(request.Idade);
            cliente.SetSenha(request.Senha.GetSha1Hash());

            await _clienteSqlServerRepository.InsertOrUpdateAsync(cliente);

            if (!HasNotification() && _unitOfWork.CommitAsync().Result)
            {
                await _mediator.RaiseEvent(new AddedClienteEvent(cliente));
                return true;
            }
            else
            {
                NotifyError("Commit", "Tivemos um problema ao tentar salvar seus dados.");
                return false;
            }
        }
    }
}
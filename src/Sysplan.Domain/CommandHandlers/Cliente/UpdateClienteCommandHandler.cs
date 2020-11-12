using Sysplan.Crosscutting.Domain.Bus;
using Sysplan.Crosscutting.Domain.Commands;
using Sysplan.Domain.Commands;
using Sysplan.Domain.Events;
using Sysplan.Domain.Interfaces.Repositories;
using Sysplan.Domain.Interfaces.UnitOfWork;
using System.Threading.Tasks;

namespace Sysplan.Domain.CommandHandlers
{
    public class UpdateClienteCommandHandler : MediatorCommandHandlerBase<UpdateClienteCommand>
    {
        private readonly IClienteSqlServerRepository _clienteSqlServerRepository;
        private readonly IClienteMongoRepository _clienteMongoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateClienteCommandHandler(
            IClienteSqlServerRepository clienteSqlServerRepository,
            IClienteMongoRepository clienteMongoRepository,
            IUnitOfWork unitOfWork,
            IMediatorHandler mediator) : base(mediator)
        {
            _clienteSqlServerRepository = clienteSqlServerRepository;
            _clienteMongoRepository = clienteMongoRepository;
            _unitOfWork = unitOfWork;
        }

        public override async Task<bool> AfterValidation(UpdateClienteCommand request)
        {
            var cliente = await _clienteMongoRepository.GetOneAsync(x => x.Id == request.Id);

            if (cliente == null)
            {
                NotifyError($"O registro com o código {request.Id} não existe");
                return false;
            }

            cliente.SetNome(request.Nome);
            cliente.SetIdade(request.Idade);
            cliente.AtualizarDataAlteracao();

            await _clienteSqlServerRepository.InsertOrUpdateAsync(cliente);

            if (!HasNotification() && _unitOfWork.CommitAsync().Result)
            {
                await _mediator.RaiseEvent(new UpdatedClienteEvent(cliente));
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
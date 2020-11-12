using Sysplan.Crosscutting.Domain.Bus;
using Sysplan.Crosscutting.Domain.Commands;
using Sysplan.Domain.Commands;
using Sysplan.Domain.Events;
using Sysplan.Domain.Interfaces.Repositories;
using Sysplan.Domain.Interfaces.UnitOfWork;
using System.Threading.Tasks;

namespace Sysplan.Domain.CommandHandlers
{
    public class RemoveClienteCommandHandler : MediatorCommandHandlerBase<RemoveClienteCommand>
    {
        private readonly IClienteSqlServerRepository _clienteSqlServerRepository;
        private readonly IClienteMongoRepository _clienteMongoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveClienteCommandHandler(
            IClienteSqlServerRepository clienteSqlServerRepository,
            IClienteMongoRepository clienteMongoRepository,
            IUnitOfWork unitOfWork,
            IMediatorHandler mediator) : base(mediator)
        {
            _clienteSqlServerRepository = clienteSqlServerRepository;
            _clienteMongoRepository = clienteMongoRepository;
            _unitOfWork = unitOfWork;
        }

        public override async Task<bool> AfterValidation(RemoveClienteCommand request)
        {
            var cliente = await _clienteMongoRepository.GetOneAsync(x => x.Id == request.Id);

            if (cliente == null)
            {
                NotifyError($"O registro com o código {request.Id} não existe");
                return false;
            }

            await _clienteSqlServerRepository.DeleteByIdAsync(cliente.Id);

            if (!HasNotification() && _unitOfWork.CommitAsync().Result)
            {
                await _mediator.RaiseEvent(new RemovedClienteEvent(cliente));

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
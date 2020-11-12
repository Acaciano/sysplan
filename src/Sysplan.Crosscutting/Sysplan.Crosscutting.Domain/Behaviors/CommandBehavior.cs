using MediatR.Pipeline;
using System.Threading;
using System.Threading.Tasks;

namespace Sysplan.Crosscutting.Domain.Behaviors
{
    public class CommandBehavior<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            //TODO: CRIAR ROTINA PARA GRAVAÇÃO DE LOG APÓS A EXECUÇÃO DE UM COMMAND/QUERY
            await Task.CompletedTask;
        }
    }
}
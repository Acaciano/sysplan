using Sysplan.Domain.Commands;

namespace Sysplan.Domain.Validations
{
    public class RemoveClienteValidation : ClienteValidation<RemoveClienteCommand>
    {
        public RemoveClienteValidation()
        {
            ValidateId();
        }
    }
}
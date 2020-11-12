using Sysplan.Domain.Commands;

namespace Sysplan.Domain.Validations
{
    public class UpdateClienteValidation : ClienteValidation<UpdateClienteCommand>
    {
        public UpdateClienteValidation()
        {
            ValidateId();
            Validate();
        }
    }
}
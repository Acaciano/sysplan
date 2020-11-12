using Sysplan.Domain.Commands;

namespace Sysplan.Domain.Validations
{
    public class AddClienteValidation : ClienteValidation<AddClienteCommand>
    {
        public AddClienteValidation()
        {
            Validate();
        }
    }
}
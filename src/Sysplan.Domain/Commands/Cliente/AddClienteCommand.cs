using Sysplan.Domain.Validations;

namespace Sysplan.Domain.Commands
{
    public class AddClienteCommand : ClienteCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new AddClienteValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
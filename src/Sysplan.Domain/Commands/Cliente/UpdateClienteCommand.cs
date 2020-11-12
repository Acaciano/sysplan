using Sysplan.Domain.Validations;

namespace Sysplan.Domain.Commands
{
    public class UpdateClienteCommand : ClienteCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new UpdateClienteValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
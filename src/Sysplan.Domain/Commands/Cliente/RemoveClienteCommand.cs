using Sysplan.Domain.Validations;
using System;

namespace Sysplan.Domain.Commands
{
    public class RemoveClienteCommand : ClienteCommand
    {
        public RemoveClienteCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveClienteValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
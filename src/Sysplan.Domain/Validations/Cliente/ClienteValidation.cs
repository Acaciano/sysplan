using FluentValidation;
using Sysplan.Domain.Commands;

namespace Sysplan.Domain.Validations
{
    public class ClienteValidation<T> : AbstractValidator<T> where T : ClienteCommand
    {
        protected void ValidateId()
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty().WithMessage("O ID é obrigatório");
        }

        protected void Validate()
        {
            RuleFor(x => x.Nome)
                .NotNull().NotEmpty().WithMessage("O nome é obrigatório");

            RuleFor(x => x.Email)
                .NotNull().NotEmpty().WithMessage("O e-mail é obrigatório");
        }

        protected void ValidateSenha()
        {
            RuleFor(x => x.Senha)
                .NotNull().NotEmpty().WithMessage("A senha é obrigatória");
        }
    }
}
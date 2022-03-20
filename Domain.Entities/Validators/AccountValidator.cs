using Domain.Entities.Entities;
using Domain.Enums;
using FluentValidation;

namespace Domain.Entities.Validators;

public class AccountValidator : AbstractValidator<Account>
{
    public AccountValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("A entidade não pode ser vazia.")

            .NotNull()
            .WithMessage("A entidade não pode ser nula.");

        RuleFor(x => x.Email.Address)
            .NotEmpty()
            .WithMessage("O e-mail não pode ser vazio.")

            .EmailAddress()
            .WithMessage("O e-mail digitado não é válido.");

        RuleFor(x => x.Name.FirstName)
            .NotEmpty()
            .WithMessage("O nome não pode ser vazio.");

        RuleFor(x => x.Name.LastName)
            .NotEmpty()
            .WithMessage("O sobrenome não pode ser vazio.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .When(x => x.Email.LoginType == ELoginType.SelfAccount)
            .WithMessage("A senha não pode ser vazia")

            .MinimumLength(6)
            .When(x => x.Email.LoginType == ELoginType.SelfAccount)
            .WithMessage("A senha precisa ter no mínimo 6 caracteres.");
    }
}
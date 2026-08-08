using FluentValidation;

namespace Application.Contas.Commands.RealizarResgate;

public class RealizarResgateValidator : AbstractValidator<RealizarResgateCommand>
{
    public RealizarResgateValidator()
    {
        RuleFor(x => x.ContaId)
            .NotEmpty().WithMessage("O Id da conta é obrigatório.");

        RuleFor(x => x.Valor)
            .GreaterThan(0).WithMessage("O valor do resgate deve ser maior que zero.");
    }
}

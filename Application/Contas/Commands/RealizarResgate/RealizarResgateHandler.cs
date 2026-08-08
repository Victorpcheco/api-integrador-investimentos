using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using MediatR;

namespace Application.Contas.Commands.RealizarResgate;

public class RealizarResgateHandler(
    IContaRepository contaRepository,
    ITransacaoContaRepository transacaoContaRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<RealizarResgateCommand, RealizarResgateResult>
{
    public async Task<RealizarResgateResult> Handle(
        RealizarResgateCommand request,
        CancellationToken cancellationToken)
    {
        var conta = await contaRepository.ObterPorIdAsync(request.ContaId, cancellationToken)
            ?? throw new KeyNotFoundException(
                $"Conta corrente com Id '{request.ContaId}' não encontrada.");

        conta.Debitar(request.Valor);

        var transacao = TransacaoConta.Criar(
            conta.Id,
            request.Valor,
            TipoTransacao.Resgate);

        await transacaoContaRepository.AdicionarAsync(transacao, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new RealizarResgateResult(
            transacao.Id,
            conta.Saldo.Valor,
            transacao.DataOperacao);
    }
}

using Application.Contas.Commands.RealizarAporte;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Moq;
using Xunit;

namespace Tests.Application.Contas.Commands.RealizarAporte;

public class RealizarAporteHandlerTests
{
    private readonly Mock<IContaRepository> _contaRepositoryMock;
    private readonly Mock<ITransacaoContaRepository> _transacaoContaRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly RealizarAporteHandler _handler;

    public RealizarAporteHandlerTests()
    {
        _contaRepositoryMock = new Mock<IContaRepository>();
        _transacaoContaRepositoryMock = new Mock<ITransacaoContaRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new RealizarAporteHandler(
            _contaRepositoryMock.Object,
            _transacaoContaRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Credit_Account_And_Return_Result_When_Valid()
    {
        // Arrange
        var contaId = Guid.NewGuid();
        var valor = 100m;
        var clienteId = Guid.NewGuid();
        var conta = Conta.Criar("12345", clienteId);
        
        // Use reflection or whatever is needed to set the Id if needed, but Conta.Criar gives an Id.
        // Let's assume the Conta entity methods work as expected.
        
        var command = new RealizarAporteCommand(conta.Id, valor);

        _contaRepositoryMock
            .Setup(x => x.ObterPorIdAsync(command.ContaId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(conta);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(valor, result.SaldoAtualizado); // Saldo was 0, credited 100
        Assert.NotEqual(Guid.Empty, result.TransacaoId);

        _contaRepositoryMock.Verify(
            x => x.ObterPorIdAsync(command.ContaId, It.IsAny<CancellationToken>()), Times.Once);

        _transacaoContaRepositoryMock.Verify(
            x => x.AdicionarAsync(It.Is<TransacaoConta>(t => t.Valor.Valor == valor && t.TipoTransacao == TipoTransacao.Aporte), It.IsAny<CancellationToken>()), Times.Once);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_KeyNotFoundException_When_Conta_Not_Found()
    {
        // Arrange
        var command = new RealizarAporteCommand(Guid.NewGuid(), 100m);

        _contaRepositoryMock
            .Setup(x => x.ObterPorIdAsync(command.ContaId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Conta?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => 
            _handler.Handle(command, CancellationToken.None));

        Assert.Contains(command.ContaId.ToString(), exception.Message);

        _transacaoContaRepositoryMock.Verify(
            x => x.AdicionarAsync(It.IsAny<TransacaoConta>(), It.IsAny<CancellationToken>()), Times.Never);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}

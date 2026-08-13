using Application.Contas.Commands.RealizarAporte;
using Xunit;

namespace Tests.Application.Contas.Commands.RealizarAporte;

public class RealizarAporteValidatorTests
{
    private readonly RealizarAporteValidator _validator;

    public RealizarAporteValidatorTests()
    {
        _validator = new RealizarAporteValidator();
    }

    [Fact]
    public void Should_Have_Error_When_ContaId_Is_Empty()
    {
        // Arrange
        var command = new RealizarAporteCommand(Guid.Empty, 100m);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "ContaId");
    }

    [Fact]
    public void Should_Have_Error_When_Valor_Is_Zero_Or_Less()
    {
        // Arrange
        var command = new RealizarAporteCommand(Guid.NewGuid(), 0m);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Valor");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        // Arrange
        var command = new RealizarAporteCommand(Guid.NewGuid(), 100m);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.True(result.IsValid);
    }
}

using DGII.Domain.Entities;
using DGII.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace DGII.Domain.Tests.Entities;

public class ComprobanteFiscalTests
{
    [Fact]
    public void Crear_ConDatosValidos_DebeRetornarComprobante()
    {
        // GIVEN
        var rncCedula = "98754321012";
        var ncf = "e310000000001";
        var monto = 200.00m;
        var itbis18 = 36.00m;

        // WHEN
        var comprobante = ComprobanteFiscal.Create(rncCedula, ncf, monto, itbis18);

        // THEN
        comprobante.Should().NotBeNull();
        comprobante.RncCedula.Should().Be(rncCedula);

        // AND
        comprobante.NCF.Should().Be("E310000000001");
        comprobante.Monto.Should().Be(200.00m);
        comprobante.Itbis18.Should().Be(36.00m);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void Crear_ConMontoInvalido_DebeLanzarDomainException(decimal monto)
    {
        // GIVEN
        var rncCedula = "98754321012";
        var ncf = "E310000000001";

        // WHEN
        var act = () => ComprobanteFiscal.Create(rncCedula, ncf, monto, 0);

        // THEN
        act.Should().Throw<DomainException>()
           .WithMessage("*Monto*");
    }

    [Fact]
    public void Crear_ConItbisNegativo_DebeLanzarDomainException()
    {
        // GIVEN
        var rncCedula = "98754321012";
        var ncf = "E310000000001";

        // WHEN
        var act = () => ComprobanteFiscal.Create(rncCedula, ncf, 200m, -1m);

        // THEN
        act.Should().Throw<DomainException>()
           .WithMessage("*ITBIS*");
    }
}

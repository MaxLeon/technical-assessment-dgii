using DGII.Domain.Entities;
using DGII.Domain.Enums;
using DGII.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace DGII.Domain.Tests.Entities;

public class ContribuyenteTests
{
    [Fact]
    public void Crear_ConDatosValidos_DebeRetornarContribuyente()
    {
        // GIVEN
        var rncCedula = "98754321012";
        var nombre = "juan perez";
        var tipo = TipoContribuyente.PersonaFisica;
        var estatus = EstatusContribuyente.Activo;

        // WHEN
        var contribuyente = Contribuyente.Create(rncCedula, nombre, tipo, estatus);

        // THEN
        contribuyente.Should().NotBeNull();
        contribuyente.RncCedula.Should().Be(rncCedula);

        // AND
        contribuyente.Nombre.Should().Be("JUAN PEREZ");
        contribuyente.Tipo.Should().Be(TipoContribuyente.PersonaFisica);
        contribuyente.Estatus.Should().Be(EstatusContribuyente.Activo);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Crear_ConRncCedulaVacio_DebeLanzarDomainException(string? rncCedula)
    {
        // GIVEN
        var nombre = "JUAN PEREZ";

        // WHEN
        var act = () => Contribuyente.Create(rncCedula!, nombre, TipoContribuyente.PersonaFisica, EstatusContribuyente.Activo);

        // THEN
        act.Should().Throw<DomainException>()
           .WithMessage("*RNC/Cédula*");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Crear_ConNombreVacio_DebeLanzarDomainException(string? nombre)
    {
        // GIVEN
        var rncCedula = "98754321012";

        // WHEN
        var act = () => Contribuyente.Create(rncCedula, nombre!, TipoContribuyente.PersonaFisica, EstatusContribuyente.Activo);

        // THEN
        act.Should().Throw<DomainException>()
           .WithMessage("*Nombre*");
    }
}

using DGII.Application.DTOs.Contribuyentes;
using DGII.Application.Services;
using DGII.Application.Validators;
using DGII.Domain.Entities;
using Xunit;
using DGII.Domain.Enums;
using DGII.Domain.Exceptions;
using DGII.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace DGII.Application.Tests.Services;

public class ContribuyenteServiceTests
{
    private readonly Mock<IContribuyenteRepository> _repositoryMock;
    private readonly Mock<ILogger<ContribuyenteService>> _loggerMock;
    private readonly ContribuyenteService _service;

    public ContribuyenteServiceTests()
    {
        _repositoryMock = new Mock<IContribuyenteRepository>();
        _loggerMock = new Mock<ILogger<ContribuyenteService>>();
        _service = new ContribuyenteService(_repositoryMock.Object, _loggerMock.Object, new ContribuyenteValidator());
    }

    [Fact]
    public async Task CreateAsync_ConRncValido_DebeGuardarYRetornarDto()
    {
        // GIVEN
        var request = new ContribuyenteRequestDto(
            RncCedula: "98754321012",
            Nombre: "JUAN PEREZ",
            Tipo: TipoContribuyente.PersonaFisica,
            Estatus: EstatusContribuyente.Activo
        );
        _repositoryMock.Setup(r => r.HasContribuyenteAsync(request.RncCedula, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(false);

        // WHEN
        var result = await _service.CreateAsync(request);

        // THEN
        result.Should().NotBeNull();
        result.RncCedula.Should().Be("98754321012");

        // AND
        result.Tipo.Should().Be("PERSONA FISICA");
        result.Estatus.Should().Be("activo");
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Contribuyente>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ConRncDuplicado_DebeLanzarDomainException()
    {
        // GIVEN
        var request = new ContribuyenteRequestDto(
            RncCedula: "98754321012",
            Nombre: "JUAN PEREZ",
            Tipo: TipoContribuyente.PersonaFisica,
            Estatus: EstatusContribuyente.Activo
        );
        _repositoryMock.Setup(r => r.HasContribuyenteAsync(request.RncCedula, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true);

        // WHEN
        var act = async () => await _service.CreateAsync(request);

        // THEN
        await act.Should().ThrowAsync<DomainException>()
                 .WithMessage("*Ya existe*");

        // AND
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Contribuyente>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task GetByRncCedula_ConRncExistente_DebeRetornarDto()
    {
        // GIVEN
        var rncCedula = "98754321012";
        var contribuyente = Contribuyente.Create(rncCedula, "JUAN PEREZ", TipoContribuyente.PersonaFisica, EstatusContribuyente.Activo);
        _repositoryMock.Setup(r => r.GetByRNCCedula(rncCedula, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(contribuyente);

        // WHEN
        var result = await _service.GetByRncCedula(rncCedula);

        // THEN
        result.Should().NotBeNull();
        result.RncCedula.Should().Be(rncCedula);

        // AND
        result.Nombre.Should().Be("JUAN PEREZ");
        result.Tipo.Should().Be("PERSONA FISICA");
    }

    [Fact]
    public async Task GetByRncCedula_ConRncInexistente_DebeLanzarContribuyenteNotFoundException()
    {
        // GIVEN
        var rncCedula = "00000000000";
        _repositoryMock.Setup(r => r.GetByRNCCedula(rncCedula, It.IsAny<CancellationToken>()))
                       .ReturnsAsync((Contribuyente?)null);

        // WHEN
        var act = async () => await _service.GetByRncCedula(rncCedula);

        // THEN
        await act.Should().ThrowAsync<ContribuyenteNotFoundException>();
    }

    [Fact]
    public async Task GetAllAsync_ConContribuyentesExistentes_DebeRetornarLista()
    {
        // GIVEN
        var contribuyentes = new List<Contribuyente>
        {
            Contribuyente.Create("98754321012", "JUAN PEREZ", TipoContribuyente.PersonaFisica, EstatusContribuyente.Activo),
            Contribuyente.Create("123456789", "EMPRESA SA", TipoContribuyente.PersonaJuridica, EstatusContribuyente.Activo)
        };
        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(contribuyentes);

        // WHEN
        var result = await _service.GetAllAsync();

        // THEN
        result.Should().NotBeNull();
        result.Should().HaveCount(2);

        // AND
        result.First().Tipo.Should().Be("PERSONA FISICA");
        result.Last().Tipo.Should().Be("PERSONA JURIDICA");
    }
}

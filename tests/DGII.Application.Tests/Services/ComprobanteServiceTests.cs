using DGII.Application.DTOs.Comprobantes;
using DGII.Application.Services;
using DGII.Application.Validators;
using Xunit;
using DGII.Domain.Entities;
using DGII.Domain.Enums;
using DGII.Domain.Exceptions;
using DGII.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace DGII.Application.Tests.Services;

public class ComprobanteServiceTests
{
    private readonly Mock<IComprobanteRepository> _comprobanteRepoMock;
    private readonly Mock<IContribuyenteRepository> _contribuyenteRepoMock;
    private readonly Mock<ILogger<ComprobanteService>> _loggerMock;
    private readonly ComprobanteService _service;

    public ComprobanteServiceTests()
    {
        _comprobanteRepoMock = new Mock<IComprobanteRepository>();
        _contribuyenteRepoMock = new Mock<IContribuyenteRepository>();
        _loggerMock = new Mock<ILogger<ComprobanteService>>();
        _service = new ComprobanteService(
            _comprobanteRepoMock.Object,
            _contribuyenteRepoMock.Object,
            _loggerMock.Object,
            new ComprobanteValidator());
    }

    [Fact]
    public async Task CrearAsync_ConDatosValidos_DebeGuardarYRetornarDto()
    {
        // GIVEN
        var request = new ComprobanteRequestDto(
            RncCedula: "98754321012",
            NCF: "E310000000001",
            Monto: 200.00m,
            Itbis18: 36.00m
        );
        _contribuyenteRepoMock.Setup(r => r.HasContribuyenteAsync(request.RncCedula, It.IsAny<CancellationToken>()))
                              .ReturnsAsync(true);

        // WHEN
        var result = await _service.CreateAsync(request);

        // THEN
        result.Should().NotBeNull();
        result.NCF.Should().Be("E310000000001");

        // AND
        result.Monto.Should().Be(200.00m);
        result.Itbis18.Should().Be(36.00m);
        _comprobanteRepoMock.Verify(r => r.CreateAsync(It.IsAny<ComprobanteFiscal>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CrearAsync_ConContribuyenteInexistente_DebeLanzarContribuyenteNotFoundException()
    {
        // GIVEN
        var request = new ComprobanteRequestDto("00000000000", "E310000000001", 200m, 36m);
        _contribuyenteRepoMock.Setup(r => r.HasContribuyenteAsync(request.RncCedula, It.IsAny<CancellationToken>()))
                              .ReturnsAsync(false);

        // WHEN
        var act = async () => await _service.CreateAsync(request);

        // THEN
        await act.Should().ThrowAsync<ContribuyenteNotFoundException>();

        // AND
        _comprobanteRepoMock.Verify(r => r.CreateAsync(It.IsAny<ComprobanteFiscal>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ObtenerTotalItbisPorRncAsync_ConComprobantes_DebeRetornarSuma()
    {
        // GIVEN
        var rncCedula = "98754321012";
        var comprobantes = new List<ComprobanteFiscal>
        {
            ComprobanteFiscal.Create(rncCedula, "E310000000001", 200m, 36m),
            ComprobanteFiscal.Create(rncCedula, "E310000000002", 500m, 90m)
        };
        _contribuyenteRepoMock.Setup(r => r.HasContribuyenteAsync(rncCedula, It.IsAny<CancellationToken>()))
                              .ReturnsAsync(true);
        _comprobanteRepoMock.Setup(r => r.GetByRncCedulaAsync(rncCedula, It.IsAny<CancellationToken>()))
                            .ReturnsAsync(comprobantes);

        // WHEN
        var total = await _service.GetTotalItbisByRncCedulaAsync(rncCedula);

        // THEN
        total.Should().Be(126m);
    }
}

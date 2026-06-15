using DGII.Application.DTOs.Comprobantes;
using DGII.Application.Interfaces;
using DGII.Application.Mappers;
using DGII.Domain.Entities;
using DGII.Domain.Exceptions;
using DGII.Domain.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DGII.Application.Services;

public class ComprobanteService : IComprobanteService
{
    private readonly IComprobanteRepository _repository;
    private readonly IContribuyenteRepository _contribuyenteRepository;
    private readonly ILogger<ComprobanteService> _logger;
    private readonly IValidator<ComprobanteRequestDto> _validator;

    public ComprobanteService(
        IComprobanteRepository repository,
        IContribuyenteRepository contribuyenteRepository,
        ILogger<ComprobanteService> logger,
        IValidator<ComprobanteRequestDto> validator)
    {
        _repository = repository;
        _contribuyenteRepository = contribuyenteRepository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<IEnumerable<ComprobanteResponseDto>> GetByRncCedulaAsync(string rncCedula, CancellationToken ct = default)
    {
        _logger.LogInformation("Obteniendo comprobantes del contribuyente: {RncCedula}", rncCedula);

        if (!await _contribuyenteRepository.HasContribuyenteAsync(rncCedula, ct))
            throw new ContribuyenteNotFoundException(rncCedula);

        var comprobantes = await _repository.GetByRncCedulaAsync(rncCedula, ct);
        return comprobantes.Select(c => c.ToResponseDto());
    }

    public async Task<ComprobanteResponseDto> CreateAsync(ComprobanteRequestDto request, CancellationToken ct = default)
    {
        _logger.LogInformation("Creando comprobante NCF: {NCF} para RNC: {RncCedula}", request.NCF, request.RncCedula);

        await _validator.ValidateAndThrowAsync(request, ct);

        if (!await _contribuyenteRepository.HasContribuyenteAsync(request.RncCedula, ct))
            throw new ContribuyenteNotFoundException(request.RncCedula);

        var comprobante = ComprobanteFiscal.Create(request.RncCedula, request.NCF, request.Monto, request.Itbis18);
        await _repository.CreateAsync(comprobante, ct);

        _logger.LogInformation("Comprobante {NCF} creado exitosamente.", request.NCF);
        return comprobante.ToResponseDto();
    }

    public async Task<decimal> GetTotalItbisByRncCedulaAsync(string rncCedula, CancellationToken ct = default)
    {
        _logger.LogInformation("Calculando total ITBIS para RNC: {RncCedula}", rncCedula);

        if (!await _contribuyenteRepository.HasContribuyenteAsync(rncCedula, ct))
            throw new ContribuyenteNotFoundException(rncCedula);

        var comprobantes = await _repository.GetByRncCedulaAsync(rncCedula, ct);
        return comprobantes.Sum(c => c.Itbis18);
    }
}

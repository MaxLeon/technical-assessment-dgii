using DGII.Application.DTOs.Contribuyentes;
using DGII.Application.Interfaces;
using DGII.Application.Mappers;
using DGII.Domain.Entities;
using DGII.Domain.Exceptions;
using DGII.Domain.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DGII.Application.Services;

public class ContribuyenteService : IContribuyenteService
{
    private readonly IContribuyenteRepository _repository;
    private readonly ILogger<ContribuyenteService> _logger;
    private readonly IValidator<ContribuyenteRequestDto> _validator;

    public ContribuyenteService(
        IContribuyenteRepository repository,
        ILogger<ContribuyenteService> logger,
        IValidator<ContribuyenteRequestDto> validator)
    {
        _repository = repository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<IEnumerable<ContribuyenteResponseDto>> GetAllAsync(CancellationToken cancelationToken = default)
    {
        _logger.LogInformation("Obteniendo todos los contribuyentes.");
        var contribuyentes = await _repository.GetAllAsync(cancelationToken);
        return contribuyentes.Select(c => c.ToResponseDto());
    }

    public async Task<ContribuyenteResponseDto> GetByRncCedula(string rncCedula, CancellationToken ct = default)
    {
        _logger.LogInformation("Obteniendo contribuyente con RNC/Cédula: {RncCedula}", rncCedula);

        var contribuyente = await _repository.GetByRNCCedula(rncCedula, ct)
            ?? throw new ContribuyenteNotFoundException(rncCedula);

        return contribuyente.ToResponseDto();
    }

    public async Task<ContribuyenteResponseDto> CreateAsync(ContribuyenteRequestDto request, CancellationToken ct = default)
    {
        _logger.LogInformation("Creando contribuyente con RNC/Cédula: {RncCedula}", request.RncCedula);

        await _validator.ValidateAndThrowAsync(request, ct);

        if (await _repository.HasContribuyenteAsync(request.RncCedula, ct))
            throw new DomainException($"Ya existe un contribuyente con RNC/Cédula '{request.RncCedula}'.");

        var contribuyente = Contribuyente.Create(request.RncCedula, request.Nombre, request.Tipo, request.Estatus);
        await _repository.CreateAsync(contribuyente, ct);

        _logger.LogInformation("Contribuyente {RncCedula} creado exitosamente.", request.RncCedula);
        return contribuyente.ToResponseDto();
    }

}

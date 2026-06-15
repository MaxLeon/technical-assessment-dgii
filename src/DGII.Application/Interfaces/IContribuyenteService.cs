using DGII.Application.DTOs.Contribuyentes;

namespace DGII.Application.Interfaces;

public interface IContribuyenteService
{
    Task<IEnumerable<ContribuyenteResponseDto>> GetAllAsync(CancellationToken ct = default);
    Task<ContribuyenteResponseDto> GetByRncCedula(string rncCedula, CancellationToken ct = default);
    Task<ContribuyenteResponseDto> CreateAsync(ContribuyenteRequestDto request, CancellationToken ct = default);
}

using DGII.Application.DTOs.Comprobantes;

namespace DGII.Application.Interfaces;

public interface IComprobanteService
{
    Task<IEnumerable<ComprobanteResponseDto>> GetByRncCedulaAsync(string rncCedula, CancellationToken ct = default);
    Task<ComprobanteResponseDto> CreateAsync(ComprobanteRequestDto request, CancellationToken ct = default);
    Task<decimal> GetTotalItbisByRncCedulaAsync(string rncCedula, CancellationToken ct = default);
}

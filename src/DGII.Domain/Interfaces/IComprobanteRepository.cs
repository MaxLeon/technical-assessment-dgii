using DGII.Domain.Entities;

namespace DGII.Domain.Interfaces;

public interface IComprobanteRepository
{
    Task<IEnumerable<ComprobanteFiscal>> GetByRncCedulaAsync(string rncCedula, CancellationToken ct = default);    
    Task CreateAsync(ComprobanteFiscal comprobante, CancellationToken ct = default);
}

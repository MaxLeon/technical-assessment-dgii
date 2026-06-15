using DGII.Domain.Entities;

namespace DGII.Domain.Interfaces;

public interface IContribuyenteRepository
{
    Task<Contribuyente?> GetByRNCCedula(string rncCedula, CancellationToken ct = default);
    Task<IEnumerable<Contribuyente>> GetAllAsync(CancellationToken ct = default);
    Task<bool> HasContribuyenteAsync(string rncCedula, CancellationToken ct = default);
    Task CreateAsync(Contribuyente contribuyente, CancellationToken ct = default);
}

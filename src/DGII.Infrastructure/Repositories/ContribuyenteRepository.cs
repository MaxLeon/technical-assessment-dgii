using DGII.Domain.Entities;
using DGII.Domain.Interfaces;
using DGII.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DGII.Infrastructure.Repositories;

// AsNoTracking en las consultas de lectura — no necesitamos change tracking
// porque nunca actualizamos entidades desde estos métodos.
public class ContribuyenteRepository : IContribuyenteRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<ContribuyenteRepository> _logger;

    public ContribuyenteRepository(AppDbContext context, ILogger<ContribuyenteRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Contribuyente?> GetByRNCCedula(string rncCedula, CancellationToken ct = default)
    {
        _logger.LogDebug("Consultando contribuyente {RncCedula} en base de datos.", rncCedula);
        return await _context.Contribuyentes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.RncCedula == rncCedula, ct);
    }

    public async Task<IEnumerable<Contribuyente>> GetAllAsync(CancellationToken ct = default)
    {
        _logger.LogDebug("Consultando todos los contribuyentes en base de datos.");
        return await _context.Contribuyentes.AsNoTracking().ToListAsync(ct);
    }

    public async Task<bool> HasContribuyenteAsync(string rncCedula, CancellationToken ct = default)
    {
        return await _context.Contribuyentes.AnyAsync(c => c.RncCedula == rncCedula, ct);
    }

    public async Task CreateAsync(Contribuyente contribuyente, CancellationToken ct = default)
    {
        _logger.LogDebug("Guardando contribuyente {RncCedula}.", contribuyente.RncCedula);
        await _context.Contribuyentes.AddAsync(contribuyente, ct);
        await _context.SaveChangesAsync(ct);
    }
}

using DGII.Domain.Entities;
using DGII.Domain.Interfaces;
using DGII.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DGII.Infrastructure.Repositories;

public class ComprobanteRepository : IComprobanteRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<ComprobanteRepository> _logger;

    public ComprobanteRepository(AppDbContext context, ILogger<ComprobanteRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ComprobanteFiscal>> GetByRncCedulaAsync(string rncCedula, CancellationToken ct = default)
    {
        _logger.LogDebug("Consultando comprobantes del contribuyente {RncCedula}.", rncCedula);
        return await _context.ComprobantesFiscales
            .AsNoTracking()
            .Where(c => c.RncCedula == rncCedula)
            .ToListAsync(ct);
    }

    public async Task CreateAsync(ComprobanteFiscal comprobante, CancellationToken ct = default)
    {
        _logger.LogDebug("Guardando comprobante {NCF}.", comprobante.NCF);
        await _context.ComprobantesFiscales.AddAsync(comprobante, ct);
        await _context.SaveChangesAsync(ct);
    }
}

using DGII.Domain.Exceptions;
using DGII.Domain.ValueObjects;

namespace DGII.Domain.Entities;

public class ComprobanteFiscal
{
    public int Id { get; private set; }
    public string RncCedula { get; private set; } = default!;
    public string NCF { get; private set; } = default!;
    public decimal Monto { get; private set; }
    public decimal Itbis18 { get; private set; }

    private ComprobanteFiscal() { }

    public static ComprobanteFiscal Create(string rncCedula, string ncf, decimal monto, decimal itbis18)
    {
        if (string.IsNullOrWhiteSpace(ncf))
            throw new DomainException("NCF es requerido.");

        if (monto <= 0)
            throw new DomainException("Monto debe ser mayor a cero.");

        if (itbis18 < 0)
            throw new DomainException("ITBIS no puede ser negativo.");

        return new ComprobanteFiscal
        {
            RncCedula = new RncCedula(rncCedula).Value,
            NCF = ncf.Trim().ToUpper(),
            Monto = monto,
            Itbis18 = itbis18
        };
    }
}

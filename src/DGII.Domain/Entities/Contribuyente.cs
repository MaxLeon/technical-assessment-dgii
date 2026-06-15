using DGII.Domain.Enums;
using DGII.Domain.Exceptions;
using DGII.Domain.ValueObjects;

namespace DGII.Domain.Entities;

public class Contribuyente
{
    public string RncCedula { get; private set; } = default!;
    public string Nombre { get; private set; } = default!;
    public TipoContribuyente Tipo { get; private set; }
    public EstatusContribuyente Estatus { get; private set; }

    private Contribuyente() { }

    public static Contribuyente Create(string rncCedula, string nombre, TipoContribuyente tipo, EstatusContribuyente estatus)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("Nombre es requerido.");

        return new Contribuyente
        {
            RncCedula = new RncCedula(rncCedula).Value,
            Nombre = nombre.Trim().ToUpper(),
            Tipo = tipo,
            Estatus = estatus
        };
    }
}

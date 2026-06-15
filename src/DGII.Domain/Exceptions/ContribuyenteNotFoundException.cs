namespace DGII.Domain.Exceptions;

public class ContribuyenteNotFoundException : DomainException
{
    public ContribuyenteNotFoundException(string rncCedula)
        : base($"Contribuyente con RNC/Cédula '{rncCedula}' no encontrado.") { }
}

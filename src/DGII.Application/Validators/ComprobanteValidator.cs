using DGII.Application.DTOs.Comprobantes;
using FluentValidation;

namespace DGII.Application.Validators;

public class ComprobanteValidator : AbstractValidator<ComprobanteRequestDto>
{
    public ComprobanteValidator()
    {
        RuleFor(x => x.RncCedula)
            .NotEmpty().WithMessage("RNC/Cédula es requerido.")
            .Must(v => v.Length == 9 || v.Length == 11).WithMessage("RNC/Cédula debe tener 9 o 11 dígitos.")
            .Matches(@"^\d+$").WithMessage("RNC/Cédula debe contener solo dígitos.");

        RuleFor(x => x.NCF)
            .NotEmpty().WithMessage("NCF es requerido.")
            .Matches(@"^[A-Z]\d{12}$").WithMessage("NCF debe tener formato válido (ej: E310000000001).");

        RuleFor(x => x.Monto)
            .GreaterThan(0).WithMessage("Monto debe ser mayor a cero.");

        RuleFor(x => x.Itbis18)
            .GreaterThanOrEqualTo(0).WithMessage("ITBIS no puede ser negativo.");
    }
}

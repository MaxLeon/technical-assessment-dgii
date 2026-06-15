using DGII.Application.DTOs.Contribuyentes;
using DGII.Domain.Enums;
using FluentValidation;

namespace DGII.Application.Validators;

public class ContribuyenteValidator : AbstractValidator<ContribuyenteRequestDto>
{
    public ContribuyenteValidator()
    {
        RuleFor(x => x.RncCedula)
            .NotEmpty().WithMessage("RNC/Cédula es requerido.")
            .Must(v => v.Length == 9 || v.Length == 11).WithMessage("RNC/Cédula debe tener 9 o 11 dígitos.")
            .Matches(@"^\d+$").WithMessage("RNC/Cédula debe contener solo dígitos.");

        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("Nombre es requerido.")
            .MaximumLength(255).WithMessage("Nombre no puede exceder 255 caracteres.");

        RuleFor(x => x.Tipo)
            .IsInEnum().WithMessage("Tipo de contribuyente inválido.");

        RuleFor(x => x.Estatus)
            .IsInEnum().WithMessage("Estatus de contribuyente inválido.");
    }
}

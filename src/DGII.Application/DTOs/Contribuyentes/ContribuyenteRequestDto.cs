using DGII.Domain.Enums;

namespace DGII.Application.DTOs.Contribuyentes;

public record ContribuyenteRequestDto(
    string RncCedula,
    string Nombre,
    TipoContribuyente Tipo,
    EstatusContribuyente Estatus
);

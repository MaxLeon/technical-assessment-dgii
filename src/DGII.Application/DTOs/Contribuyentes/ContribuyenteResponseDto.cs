namespace DGII.Application.DTOs.Contribuyentes;

public record ContribuyenteResponseDto(
    string RncCedula,
    string Nombre,
    string Tipo,
    string Estatus
);

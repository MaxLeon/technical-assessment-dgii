namespace DGII.Application.DTOs.Comprobantes;

public record ComprobanteResponseDto(
    int Id,
    string RncCedula,
    string NCF,
    decimal Monto,
    decimal Itbis18
);

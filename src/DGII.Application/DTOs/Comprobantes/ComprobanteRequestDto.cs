namespace DGII.Application.DTOs.Comprobantes;

public record ComprobanteRequestDto(
    string RncCedula,
    string NCF,
    decimal Monto,
    decimal Itbis18
);

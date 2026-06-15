using DGII.Application.DTOs.Comprobantes;
using DGII.Domain.Entities;

namespace DGII.Application.Mappers;

public static class ComprobanteMapper
{
    public static ComprobanteResponseDto ToResponseDto(this ComprobanteFiscal comprobante) =>
        new(
            comprobante.Id,
            comprobante.RncCedula,
            comprobante.NCF,
            comprobante.Monto,
            comprobante.Itbis18
        );
}

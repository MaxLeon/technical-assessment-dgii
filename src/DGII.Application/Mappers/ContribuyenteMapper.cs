using DGII.Application.DTOs.Contribuyentes;
using DGII.Domain.Entities;
using DGII.Domain.Enums;
using DGII.Domain.Exceptions;

namespace DGII.Application.Mappers;

public static class ContribuyenteMapper
{
    public static ContribuyenteResponseDto ToResponseDto(this Contribuyente contribuyente) =>
        new(
            contribuyente.RncCedula,
            contribuyente.Nombre,
            contribuyente.Tipo.ToDisplayString(),
            contribuyente.Estatus.ToDisplayString()
        );

    public static string ToDisplayString(this TipoContribuyente tipo) => tipo switch
    {
        TipoContribuyente.PersonaFisica   => "PERSONA FISICA",
        TipoContribuyente.PersonaJuridica => "PERSONA JURIDICA",
        _ => throw new DomainException($"TipoContribuyente desconocido: {tipo}")
    };

    public static string ToDisplayString(this EstatusContribuyente estatus) => estatus switch
    {
        EstatusContribuyente.Activo   => "activo",
        EstatusContribuyente.Inactivo => "inactivo",
        _ => throw new DomainException($"EstatusContribuyente desconocido: {estatus}")
    };
}

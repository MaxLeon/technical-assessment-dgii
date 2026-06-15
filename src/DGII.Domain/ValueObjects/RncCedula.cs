using DGII.Domain.Exceptions;

namespace DGII.Domain.ValueObjects;

/// <summary>
/// RncCedula es un Value Object que representa el RNC o Cédula de un contribuyente.
/// </summary>
public sealed class RncCedula
{
    public string Value { get; }

    public RncCedula(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("RNC/Cédula no puede estar vacío.");

        var digits = value.Trim();

        if (digits.Length != 9 && digits.Length != 11)
            throw new DomainException("RNC/Cédula debe tener 9 o 11 dígitos.");

        if (!digits.All(char.IsDigit))
            throw new DomainException("RNC/Cédula debe contener solo dígitos.");

        Value = digits;
    }

    public override string ToString() => Value;
    public override bool Equals(object? obj) => obj is RncCedula other && Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();
}

using System.Numerics;

namespace Junlang;

internal readonly struct FractionValue : IEquatable<FractionValue>, IComparable<FractionValue>
{
    public FractionValue(BigInteger numerator, BigInteger denominator)
    {
        if (denominator.IsZero)
        {
            throw new DivideByZeroException();
        }

        if (denominator.Sign < 0)
        {
            numerator = -numerator;
            denominator = -denominator;
        }

        var gcd = BigInteger.GreatestCommonDivisor(BigInteger.Abs(numerator), denominator);
        Numerator = numerator / gcd;
        Denominator = denominator / gcd;
    }

    public BigInteger Numerator { get; }
    public BigInteger Denominator { get; }

    public static FractionValue Zero => new(0, 1);
    public static FractionValue One => new(1, 1);

    public static FractionValue FromInt(BigInteger value) => new(value, 1);

    public FractionValue Abs() => Numerator.Sign < 0 ? -this : this;

    public FractionValue Pow(int exponent)
    {
        if (exponent == 0)
        {
            return One;
        }

        if (exponent < 0)
        {
            if (Numerator.IsZero)
            {
                throw new DivideByZeroException();
            }

            var positiveExponent = checked(-exponent);
            return new FractionValue(
                BigInteger.Pow(Denominator, positiveExponent),
                BigInteger.Pow(Numerator, positiveExponent));
        }

        return new FractionValue(
            BigInteger.Pow(Numerator, exponent),
            BigInteger.Pow(Denominator, exponent));
    }

    public int CompareTo(FractionValue other)
    {
        return (Numerator * other.Denominator).CompareTo(other.Numerator * Denominator);
    }

    public bool Equals(FractionValue other)
    {
        return Numerator == other.Numerator && Denominator == other.Denominator;
    }

    public override bool Equals(object? obj)
    {
        return obj is FractionValue other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Numerator, Denominator);
    }

    public override string ToString()
    {
        return Denominator.IsOne ? Numerator.ToString() : $"{Numerator}/{Denominator}";
    }

    public static FractionValue operator +(FractionValue left, FractionValue right)
    {
        return new FractionValue(
            left.Numerator * right.Denominator + right.Numerator * left.Denominator,
            left.Denominator * right.Denominator);
    }

    public static FractionValue operator -(FractionValue value)
    {
        return new FractionValue(-value.Numerator, value.Denominator);
    }

    public static FractionValue operator -(FractionValue left, FractionValue right)
    {
        return left + -right;
    }

    public static FractionValue operator *(FractionValue left, FractionValue right)
    {
        return new FractionValue(left.Numerator * right.Numerator, left.Denominator * right.Denominator);
    }

    public static FractionValue operator /(FractionValue left, FractionValue right)
    {
        if (right.Numerator.IsZero)
        {
            throw new DivideByZeroException();
        }

        return new FractionValue(left.Numerator * right.Denominator, left.Denominator * right.Numerator);
    }

    public static bool operator ==(FractionValue left, FractionValue right) => left.Equals(right);
    public static bool operator !=(FractionValue left, FractionValue right) => !left.Equals(right);
    public static bool operator <(FractionValue left, FractionValue right) => left.CompareTo(right) < 0;
    public static bool operator >(FractionValue left, FractionValue right) => left.CompareTo(right) > 0;
    public static bool operator <=(FractionValue left, FractionValue right) => left.CompareTo(right) <= 0;
    public static bool operator >=(FractionValue left, FractionValue right) => left.CompareTo(right) >= 0;
}

using System.Numerics;

namespace Junlang;

internal static class RuntimeSupport
{
    public static FractionValue ParseInputNumber(string source)
    {
        try
        {
            source = source.TrimStart('\uFEFF');
            var tokens = new Lexer(source).ScanTokens();
            var literal = tokens.Count > 0 ? tokens[0].Literal : null;
            if (tokens.Count != 2 || tokens[0].Type != TokenType.Number || literal is null)
            {
                throw new JunlangRuntimeException(Errors.BadInput);
            }

            return literal.Value;
        }
        catch (JunlangRuntimeException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new JunlangRuntimeException(Errors.BadInput, ex);
        }
    }

    public static string FormatNumber(FractionValue value)
    {
        var sign = value < FractionValue.Zero;
        value = value.Abs();

        var scale = ExactDecimalScale(value);
        if (scale is null || scale > 8)
        {
            value = RoundFraction(value, 8);
            scale = 8;
            if (value == FractionValue.Zero)
            {
                sign = false;
            }
        }

        var integerPart = value.Numerator / value.Denominator;
        var remainder = value - FractionValue.FromInt(integerPart);
        if (remainder == FractionValue.Zero)
        {
            return FormatInteger(sign && integerPart != BigInteger.Zero ? -integerPart : integerPart);
        }

        var scaledFraction = remainder * new FractionValue(BigInteger.Pow(10, scale.Value), 1);
        var fractionNumber = scaledFraction.Numerator / scaledFraction.Denominator;
        var fractionDigits = fractionNumber.ToString().PadLeft(scale.Value, '0').TrimEnd('0');

        if (fractionDigits.Length == 0)
        {
            return FormatInteger(sign && integerPart != BigInteger.Zero ? -integerPart : integerPart);
        }

        var integerText = FormatInteger(integerPart);
        if (sign)
        {
            integerText = "?" + integerText;
        }

        return integerText + "ㅋ" + string.Join(" ", fractionDigits.Select(digit => FormatDigit(digit - '0')));
    }

    private static string FormatInteger(BigInteger value)
    {
        if (value.IsZero)
        {
            return "오?";
        }

        var sign = value.Sign < 0 ? "?" : string.Empty;
        var digits = BigInteger.Abs(value).ToString();
        var chunks = digits.Select(digit => FormatDigit(digit - '0')).ToList();
        chunks[0] = sign + chunks[0];
        return string.Join(" ", chunks);
    }

    private static string FormatDigit(int digit)
    {
        return digit == 0 ? "오?" : new string('오', digit);
    }

    private static int? ExactDecimalScale(FractionValue value)
    {
        var denominator = value.Denominator;
        var twos = 0;
        var fives = 0;

        while (denominator % 2 == BigInteger.Zero)
        {
            denominator /= 2;
            twos++;
        }

        while (denominator % 5 == BigInteger.Zero)
        {
            denominator /= 5;
            fives++;
        }

        return denominator == BigInteger.One ? Math.Max(twos, fives) : null;
    }

    private static FractionValue RoundFraction(FractionValue value, int places)
    {
        var scale = BigInteger.Pow(10, places);
        var scaled = value * new FractionValue(scale, 1);
        var quotient = BigInteger.DivRem(scaled.Numerator, scaled.Denominator, out var remainder);
        if (remainder * 2 >= scaled.Denominator)
        {
            quotient++;
        }

        return new FractionValue(quotient, scale);
    }
}

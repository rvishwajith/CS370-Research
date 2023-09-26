using System;

static class RandomExtensions
{
    public static long NextLong(this Random rnd)
    {
        byte[] buffer = new byte[8];
        rnd.NextBytes(buffer);
        return BitConverter.ToInt64(buffer, 0);
    }

    public static long NextLong(this Random rnd, long min, long max)
    {
        EnsureMinLEQMax(ref min, ref max);
        long numsInRange = unchecked(max - min + 1);
        if (numsInRange < 0)
            throw new ArgumentException("Random.NextLong (Extension) ERROR: Range of min/max must be <= Int64.MaxValue");

        long randomOffset = NextLong(rnd);
        return IsModuloBiased(randomOffset, numsInRange) ? NextLong(rnd, min, max) : min + PositiveModuloOrZero(randomOffset, numsInRange);
    }

    static bool IsModuloBiased(long randomOffset, long numbersInRange)
    {
        long greatestCompleteRange = numbersInRange * (long.MaxValue / numbersInRange);
        return randomOffset > greatestCompleteRange;
    }

    static long PositiveModuloOrZero(long dividend, long divisor)
    {
        long mod;
        Math.DivRem(dividend, divisor, out mod);
        if (mod < 0)
            mod += divisor;
        return mod;
    }

    static void EnsureMinLEQMax(ref long min, ref long max)
    {
        if (min <= max)
            return;
        long temp = min;
        min = max;
        max = temp;
    }
}
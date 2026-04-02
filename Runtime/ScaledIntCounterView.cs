using Counters;
using System;
using System.Globalization;
using UnityEngine;

public sealed class ScaledIntCounterView : CounterView<int>
{
    protected override string FormatValue(int value)
    {
        if (value == 0)
            return 0.ToString(NumberFormat, CultureInfo.InvariantCulture);

        int absValue = Math.Abs(value);
        int magnitude = (int)Math.Floor(Math.Log10(absValue) / SuffixDigitsStep);
        int exponent = magnitude * SuffixDigitsStep;
        double scaledValue = value / Math.Pow(10, exponent);
        string formattedValue = scaledValue.ToString(NumberFormat, CultureInfo.InvariantCulture);

        if (magnitude < s_Suffixes.Length)
            return formattedValue + s_Suffixes[magnitude];

        return formattedValue + s_exponentChar + exponent;
    }

}

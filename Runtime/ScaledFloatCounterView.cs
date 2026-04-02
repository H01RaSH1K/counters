using System.Globalization;
using UnityEngine;

namespace Counters
{
    public class ScaledFloatCounterView : CounterView<float>
    {
        protected override string FormatValue(float value)
        {
            float minFormatedValue = 1f;

            if (value < minFormatedValue)
                return value.ToString(NumberFormat, CultureInfo.InvariantCulture);

            int magnitude = (int)Mathf.Floor(Mathf.Log10(value) / SuffixDigitsStep);
            int exponent = magnitude * SuffixDigitsStep;
            float scaledValue = value / Mathf.Pow(10, exponent);
            string formattedValue = scaledValue.ToString(NumberFormat, CultureInfo.InvariantCulture);

            if (magnitude < s_Suffixes.Length)
                return formattedValue + s_Suffixes[magnitude];

            return formattedValue + s_exponentChar + exponent;
        }
    }
}
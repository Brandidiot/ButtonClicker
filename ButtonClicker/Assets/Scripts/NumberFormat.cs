using System;
using UnityEngine;

public class NumberFormat : MonoBehaviour
{
    public string[] shortNames = new string[10]
    {
        "Millions",
        "Billion",
        "Trillion",
        "Quadrillion",
        "Quintillion",
        "Sextillion",
        "Septillion",
        "Octillion",
        "Nonillion",
        "Decillion"
    };

    public string ShortNotation(double value)
    {
        var zeros = (int)Math.Log10(value);
        var prefixIndex = zeros / 3;
        prefixIndex -= 2; // We delete the Thousand from the function to start with millions

        // If under the Million, no need to convert
        if (zeros < 6)
            return value.ToString("0");
        if (prefixIndex > 19) // Overflow..
            prefixIndex = 19;

        var prefix = shortNames[prefixIndex];
        var number = value / Math.Pow(10, (prefixIndex + 2) * 3);
        var returnvalue = number.ToString("0.00");
        returnvalue += " ";
        returnvalue += prefix;
        return returnvalue;
    }
}
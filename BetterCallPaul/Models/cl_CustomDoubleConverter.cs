using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace BetterCallPaul.Models
{
    public class CustomDoubleConverter : DefaultTypeConverter
    {
        // Override ConvertFromString method
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0.0; // Handle empty or null values as 0

            // Replace dots as thousand separators and commas as decimal points
            text = text.Replace(".", "").Replace(",", ".");
            if (double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
            {
                return result;
            }

            // If parsing fails, throw an exception
            throw new FormatException($"The value '{text}' is not a valid number.");
        }
    }
}

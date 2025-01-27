using CsvHelper.Configuration.Attributes;

namespace BetterCallPaul.Models
{
    public class cl_Weather
    {
        [Name("Day")]
        public int Day { get; set; }     // Day number
        [Name("MxT")]
        public double MxT { get; set; } // Maximum temperature (MxT)
        [Name("MnT")]
        public double MnT { get; set; } // Minimum temperature (MnT)
    }
}

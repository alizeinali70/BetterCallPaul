using CsvHelper.Configuration.Attributes;

namespace BetterCallPaul.Models
{
    public class cl_Population
    {
        [Name("Name")]
        public string Name { get; set; } // Name of the Country

        [Name("Population")]
        public double Population { get; set; }

        [Name("Area (km²)")]
        public double Area { get; set; }
    }
}

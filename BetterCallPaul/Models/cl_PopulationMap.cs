using CsvHelper.Configuration;

namespace BetterCallPaul.Models
{
    public class cl_PopulationMap : ClassMap<cl_Population>
    {
        public cl_PopulationMap()
        {
            Map(m => m.Name).Name("Name");
            Map(m => m.Population).Name("Population").TypeConverter<CustomDoubleConverter>();
            Map(m => m.Area).Name("Area (km²)").TypeConverter<CustomDoubleConverter>();
        }
    }
}

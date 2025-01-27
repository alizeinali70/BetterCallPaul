using BetterCallPaul.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace BetterCallPaul.Controllers
{
    public class PopulationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>        
        /// </summary>
        /// <param name="csvFile"></param>
        /// <process>Read the file, then print the name of the country with the highest number of people per square kilometre.</process>
        /// <returns>highestDensity.Name</returns>
        [HttpPost]
        public IActionResult Calc_Population_Csv(IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length == 0)
            {
                return BadRequest("Please select a CSV file.");
            }

            try
            {
                using (var stream = csvFile.OpenReadStream())
                {
                    using (var reader = new CsvReader(new StreamReader(stream), new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Delimiter = ";", // Use semicolon as the delimiter
                    }))
                    {
                        // Register the column mappings
                        reader.Context.RegisterClassMap<cl_PopulationMap>();

                        var populations = reader.GetRecords<cl_Population>().ToList();

                        // Check if there are any records
                        if (populations == null || populations.Count == 0)
                        {
                            TempData["ErrorMessage"] = "The CSV file is empty or incorrectly formatted.";
                            return RedirectToAction("Index");
                        }

                        // Calculate highest density
                        var highestDensity = populations
                            .Where(d => d.Area > 0)
                            .OrderByDescending(d => d.Population / d.Area)
                            .First();

                        ViewData["highestnumberofpopulation"] = highestDensity.Name;
                    }
                }
                return View("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = $"Error uploading CSV file. Somethings wrong with Uploading. maybe the file doesn't have the correct Format. " +
                    $"Please Check the File Format.";
                return RedirectToAction("Index");
            }
        }
    }
}

using BetterCallPaul.Models;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;

namespace BetterCallPaul.Controllers
{
    public class WeatherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>        
        /// output
        /// </summary>
        /// <param name="csvFile"></param>
        /// <process>Calculate difference between maximum & minimum temperature of the day.</process>
        /// <returns>minSpreadDay.Day</returns>
        [HttpPost]
        public IActionResult Calc_min_Spread_Day_Csv(IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length == 0)
            {
                return BadRequest("Please select a CSV file.");
            }

            try
            {
                using (var stream = csvFile.OpenReadStream())
                {
                    using (var reader = new CsvReader(new StreamReader(stream), CultureInfo.InvariantCulture))
                    {
                        var days = reader.GetRecords<cl_Weather>();

                        var minSpreadDay = days.OrderBy(d => Math.Abs(d.MxT - d.MnT)).First();

                        ViewData["DayWithSmallestSpread"] = minSpreadDay.Day;
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
        /// <summary>        
        /// output
        /// </summary>
        /// <param name="jsonFile"></param>
        /// <process>Calculate difference between maximum & minimum temperature of the day.</process>
        /// <returns>minSpreadDay.Day</returns>
        [HttpPost]
        public IActionResult Calc_min_Spread_Day_Json(IFormFile jsonFile)
        {
            if (jsonFile == null || jsonFile.Length == 0)
            {
                return BadRequest("Please select a file.");
            }
            try
            {
                using (var stream = jsonFile.OpenReadStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        // Read the JSON content as a string
                        var jsonContent = reader.ReadToEnd();

                        // Deserialize the JSON into a list of cl_Weather objects
                        var days = JsonSerializer.Deserialize<List<cl_Weather>>(jsonContent);

                        if (days != null && days.Any())
                        {
                            // Find the day with the smallest temperature spread
                            var minSpreadDay = days.OrderBy(d => Math.Abs(d.MxT - d.MnT)).First();

                            // Set the result in ViewData
                            ViewData["DayWithSmallestSpread"] = minSpreadDay.Day;
                        }
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

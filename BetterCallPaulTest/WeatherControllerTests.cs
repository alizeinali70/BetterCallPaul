using BetterCallPaul.Controllers;
using BetterCallPaul.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BetterCallPaulTest
{
    public class WeatherControllerTests
    {
        [Fact]
        public void ReturnsDayWithSmallestSpread_Csv()
        {
            // Arrange
            var mockFile = new Mock<IFormFile>();

            // Create a sample CSV string as input
            var csvContent = "Day,MxT,MnT\n" +
                             "1,15,5\n" +
                             "2,12,10\n" +
                             "3,20,0";

            var byteArray = System.Text.Encoding.UTF8.GetBytes(csvContent);
            var stream = new MemoryStream(byteArray);

            mockFile.Setup(f => f.OpenReadStream()).Returns(stream);
            mockFile.Setup(f => f.Length).Returns(stream.Length);

            var controller = new WeatherController(); 

            // Act
            var result = controller.Calc_min_Spread_Day_Csv(mockFile.Object) as ViewResult;

            // Assert
            Assert.NotNull(result); // Ensure the result is not null
            Assert.Equal("Index", result.ViewName); // Check if the correct view is returned
            Assert.Equal(2, result.ViewData["DayWithSmallestSpread"]); // Assert the day with the smallest spread
        }
    

            [Fact]
        public void ReturnsDayWithSmallestSpread_Json()
        {
            // Arrange
            var mockFile = new Mock<IFormFile>();

            // Create a sample JSON string as input
            var weatherData = new List<cl_Weather>
            {
                new cl_Weather { Day = 1, MxT = 15, MnT = 5 },
                new cl_Weather { Day = 2, MxT = 12, MnT = 10 },
                new cl_Weather { Day = 3, MxT = 20, MnT = 0 },
            };

            var jsonContent = JsonSerializer.Serialize(weatherData);
            var byteArray = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var stream = new MemoryStream(byteArray);

            mockFile.Setup(f => f.OpenReadStream()).Returns(stream);
            mockFile.Setup(f => f.Length).Returns(stream.Length);

            var controller = new WeatherController(); 

            // Act
            var result = controller.Calc_min_Spread_Day_Json(mockFile.Object) as ViewResult;

            // Assert
            Assert.NotNull(result); // Ensure the result is not null
            Assert.Equal("Index", result.ViewName); // Check if the correct view is returned
            Assert.Equal(2, result.ViewData["DayWithSmallestSpread"]); // Assert the day with the smallest spread
        }
    }
}

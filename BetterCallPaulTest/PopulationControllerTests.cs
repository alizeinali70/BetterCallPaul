using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using CsvHelper;
using System.Text;
using System.Globalization;
using BetterCallPaul.Controllers;

namespace BetterCallPaulTest
{
    public class PopulationControllerTests
    {
        [Fact]
        public void ReturnsCountryWithHighestPopulationDensity()
        {
            // Arrange
            var mockFile = new Mock<IFormFile>();

            // Create a sample CSV string as input
            var csvContent = "Name;Population;Area (km²);\n" +
                             "Germany;10000000;500000;\n" +
                             "France;20000000;400000;\n" +
                             "Espain;5000000;200000;";

            var byteArray = Encoding.UTF8.GetBytes(csvContent);
            var stream = new MemoryStream(byteArray);

            mockFile.Setup(f => f.OpenReadStream()).Returns(stream);
            mockFile.Setup(f => f.Length).Returns(stream.Length);

            var controller = new PopulationController(); 

            // Act
            var result = controller.Calc_Population_Csv(mockFile.Object) as ViewResult;

            // Assert
            Assert.NotNull(result); // Ensure the result is not null
            Assert.Equal("Index", result.ViewName); // Check if the correct view is returned
            Assert.Equal("France", result.ViewData["highestnumberofpopulation"]); // Assert the country with the highest population density
        }
    }
}


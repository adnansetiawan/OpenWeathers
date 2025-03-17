using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using OpenWeather.Api.Controllers;
using OpenWeather.Api.Middlewares;
using OpenWeather.Core.Dto;
using OpenWeather.Core.Exceptions;
using OpenWeather.Core.Interfaces;
using OpenWeather.Core.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Xunit;

namespace OpenWeather.Api.Tests
{
    public class ApiTest
    {
        private readonly Mock<IWeatherService> _mockService;
        private readonly WeatherForecastController _controller;
        public ApiTest() {
            _mockService = new Mock<IWeatherService>();
            _controller = new WeatherForecastController(_mockService.Object);

        }
        [Fact]
        public async Task Should_ReturnsOkResult_WhenDataIsFound()
        {
            var mockData = WeatherApiMockData.GetOkData();
            _mockService.Setup(c => c.GetWeatherAsync(It.IsAny<WeatherApiRequest>())).ReturnsAsync(mockData);
             var result = await _controller.GetByCity(city: "london");

            // Assert: Verify that the result is an OkObjectResult
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Dto.ResponseApiDto<WeatherApiResponse>>(actionResult.Value);
            Assert.Equal(mockData.Name, returnValue.Data.Name);
        
        }
        [Fact]
        public async Task Should_ReturnsException_WhenDataIsNotFound()
        {
           
            _mockService.Setup(s => s.GetWeatherAsync(It.IsAny<WeatherApiRequest>()))
           .ThrowsAsync(new Core.Exceptions.DataNotFoundException("404", "city is not found"));
            var result = await Assert.ThrowsAsync<Core.Exceptions.DataNotFoundException>(() => _controller.GetByCity("londonx"));
            Assert.Equal("404", result.Code);

        }
        [Fact]
        public async Task Should_ReturnsNotFoundResult_WhenDataIsNotFoundExceptionOccured()
        {
            var _mockNext = new Mock<RequestDelegate>();
            var middleware = new ErrorMiddleware(
            _mockNext.Object
            );

            var httpContext = new DefaultHttpContext();
            _mockNext.Setup(next => next.Invoke(It.IsAny<HttpContext>())).Throws(new DataNotFoundException("404", "City is not found"));

            await middleware.InvokeAsync(httpContext);

            Assert.Equal(StatusCodes.Status404NotFound, httpContext.Response.StatusCode);
           
        }
    }
}

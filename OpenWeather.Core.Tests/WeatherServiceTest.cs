using Moq;
using Moq.Protected;
using OpenWeather.Core.Interfaces;
using System;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using Xunit;
using OpenWeather.Core.Services;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using OpenWeather.Core.Dto;

namespace OpenWeather.Core.Tests
{
    public class WeatherServiceTest
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly IAppConfig _appConfig;
        private readonly Mock<IHttpClientFactory> _mockHttpClientFactory;
        public WeatherServiceTest()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _appConfig = new AppConfig(_mockConfiguration.Object);
            _mockHttpClientFactory = new Mock<IHttpClientFactory>();
        }
        [Fact]
        public async void ShouldThrowInternalErrorException_WhenAppId_IsNull()
        {
           
            // Mock the behavior for a specific key
            _mockConfiguration.Setup(c => c["AppId"]).Returns(string.Empty);
            var weatherService = new WeatherService(_mockHttpClientFactory.Object, _appConfig);
            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exceptions.InternalServerErrorRequestException>(() => weatherService.GetWeatherAsync(new Dto.WeatherApiRequest
            { 
            }));

            Assert.Equal("AppId is not found", exception.Message);
        }

        [Fact]
        
        public async Task ShouldReturnCorrectTemperatur_When_DataIsFound()
        {
           
            _mockConfiguration.Setup(c => c["AppId"]).Returns("xxxx");
            var mockData = WeatherApiMockData.GetOkData();


            var mockHttpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(mockData)
                })
                .Verifiable();

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);


            var weatherService = new WeatherService(_mockHttpClientFactory.Object, _appConfig);

            // Act
            var result = await weatherService.GetWeatherAsync(new Dto.WeatherApiRequest
            {
            });
            // Assert
            Assert.Equal(136.93, result.Main.TempInCelcius);
           
        }
        [Fact]

        public async Task ShouldThrowException_When_DataIsNotFound()
        {
            
            _mockConfiguration.Setup(c => c["AppId"]).Returns("xxxx");


            var mockData = WeatherApiMockData.GetNotFoundData();
            
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent(mockData)
                })
                .Verifiable();

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);


            var weatherService = new WeatherService(_mockHttpClientFactory.Object, _appConfig);

            //act & assert
            var exception = await Assert.ThrowsAsync<Exceptions.DataNotFoundException>(() => weatherService.GetWeatherAsync(new Dto.WeatherApiRequest
            {
            }));

            Assert.Equal("404", exception.Code);
        }

        
    }
    
}

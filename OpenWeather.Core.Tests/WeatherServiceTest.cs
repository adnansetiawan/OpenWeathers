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

        [Fact]
        public async void ShouldThrowInternalErrorException_WhenAppId_IsNull()
        {
            // Arrange
            var mockConfiguration = new Mock<IConfiguration>();

            // Mock the behavior for a specific key
            mockConfiguration.Setup(c => c["AppId"]).Returns(string.Empty);

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();

            var appConfig = new AppConfig(mockConfiguration.Object);
            var weatherService = new WeatherService(mockHttpClientFactory.Object, appConfig);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exceptions.InternalServerErrorRequestException>(() => weatherService.GetWeatherAsync(new Dto.WeatherApiRequest
            { 
            }));

            Assert.Equal("AppId is not found", exception.Message);
        }

        [Fact]
        
        public async Task TemperaturConvertionIsCorrect()
        {
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["AppId"]).Returns("xxxx");


            var mockData = WeatherApiMockData.GetOkData();
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();


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

            mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);


            var appConfig = new AppConfig(mockConfiguration.Object);
            var weatherService = new WeatherService(mockHttpClientFactory.Object, appConfig);

            // Act
            var result = await weatherService.GetWeatherAsync(new Dto.WeatherApiRequest
            {
            });
            // Assert
            Assert.Equal(136.93, result.Main.TempInCelcius);
           
        }
        [Fact]

        public async Task ShouldThrowDataNotFoundException_WhenApi_Return404()
        {
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["AppId"]).Returns("xxxx");


            var mockData = WeatherApiMockData.GetNotFoundData();
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();


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

            mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);


            var appConfig = new AppConfig(mockConfiguration.Object);
            var weatherService = new WeatherService(mockHttpClientFactory.Object, appConfig);

            //act & assert
            var exception = await Assert.ThrowsAsync<Exceptions.DataNotFoundException>(() => weatherService.GetWeatherAsync(new Dto.WeatherApiRequest
            {
            }));

            Assert.Equal("404", exception.Code);
        }

        
    }
    
}

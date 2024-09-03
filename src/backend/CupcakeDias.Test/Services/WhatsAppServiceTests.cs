using System.Net;
using CupcakeDias.Shared.Dtos;
using CupcakeDias.Shared.Services.Implementations;
using Moq;
using Moq.Protected;

namespace CupcakeDias.Test.Services;

public class WhatsAppServiceTests
{
    private readonly WhatsAppService _whatsAppService;
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;

    public WhatsAppServiceTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("https://whatsapp.joaodiasdev.com")
        };
        _whatsAppService = new WhatsAppService(_httpClient);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task SendMessageAsync_ShouldSendRequestAndReturnSuccess()
    {
        // Arrange
        var whatsAppMessageDto = new WhatsAppMessageDto
        {
            PhoneNumber = "+1234567890",
            Message = "Hello, this is a test message!"
        };

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"success\":true}")
            })
            .Verifiable();

        // Act
        await _whatsAppService.SendMessageAsync(whatsAppMessageDto);

        // Assert
        _httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Method == HttpMethod.Post &&
                req.RequestUri == new Uri("https://whatsapp.joaodiasdev.com/api/messages")),
            ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task SendMessageAsync_ShouldThrowExceptionOnHttpError()
    {
        // Arrange
        var whatsAppMessageDto = new WhatsAppMessageDto
        {
            PhoneNumber = "+1234567890",
            Message = "This will cause an error!"
        };

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
            })
            .Verifiable();

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => _whatsAppService.SendMessageAsync(whatsAppMessageDto));

        _httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Method == HttpMethod.Post &&
                req.RequestUri == new Uri("https://whatsapp.joaodiasdev.com/api/messages")),
            ItExpr.IsAny<CancellationToken>());
    }
}

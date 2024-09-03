using System;
using System.Text;
using System.Text.Json;
using CupcakeDias.Shared.Dtos;
using CupcakeDias.Shared.Services.Interfaces;

namespace CupcakeDias.Shared.Services.Implementations;

public class WhatsAppService(HttpClient httpClient) : IWhatsAppService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task SendMessageAsync(WhatsAppMessageDto whatsAppMessageDto)
    {
        var requestBody = new
        {
            phoneNumber = whatsAppMessageDto.PhoneNumber,
            content = whatsAppMessageDto.Message
        };

        var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("https://whatsapp.joaodiasdev.com/api/messages", jsonContent);

        response.EnsureSuccessStatusCode();
    }
}

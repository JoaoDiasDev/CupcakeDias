using CupcakeDias.Shared.Dtos;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CupcakeDias.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WhatsAppController(IWhatsAppService whatsAppService) : ControllerBase
{
    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] WhatsAppMessageDto request)
    {
        if (string.IsNullOrEmpty(request.PhoneNumber) || string.IsNullOrEmpty(request.Message))
        {
            return BadRequest("Phone number and message content are required.");
        }

        try
        {
            await whatsAppService.SendMessageAsync(request);
            return Ok("Message sent successfully.");
        }
        catch (HttpRequestException)
        {
            // Log the exception details (not shown here)
            return StatusCode(500, "A network error occurred while sending the message.");
        }
        catch (TaskCanceledException)
        {
            // Log the exception details (not shown here)
            return StatusCode(500, "The request timed out while sending the message.");
        }
        catch (Exception)
        {
            // Log the exception details (not shown here)
            return StatusCode(500, "An error occurred while sending the message.");
        }
    }
}

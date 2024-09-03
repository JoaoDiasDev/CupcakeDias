using System;

namespace CupcakeDias.Shared.Dtos;

public class WhatsAppMessageDto
{
    public required string PhoneNumber { get; set; }
    public required string Message { get; set; }
}

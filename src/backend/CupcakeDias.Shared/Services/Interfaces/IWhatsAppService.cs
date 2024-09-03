using System;
using CupcakeDias.Shared.Dtos;

namespace CupcakeDias.Shared.Services.Interfaces;

public interface IWhatsAppService
{
    Task SendMessageAsync(WhatsAppMessageDto message);
}

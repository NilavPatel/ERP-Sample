using ERP.Application.Core.Models;

namespace ERP.Application.Core.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(Email email);
    }
}
using ERP.Domain.Core.Models;

namespace ERP.Domain.Core.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(Email email);
    }
}
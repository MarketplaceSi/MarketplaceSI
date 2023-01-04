using MarketplaceSI.Core.Dto.Emails;

namespace MarketplaceSI.Core.Domain.Services.Interfaces
{
    public interface IMailSenderService
    {
        Task<bool> SendEmailAsync(EmailMessage message);
    }
}

using MarketplaceSI.Core.Domain.Services.Interfaces;
using MarketplaceSI.Core.Domain.Settings;
using MarketplaceSI.Core.Dto.Emails;
using AutoMapper;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MarketplaceSI.Core.Infrastructure.Services
{
    public class MailSenderService : IMailSenderService
    {
        private SendGridSettings _options;
        public MailSenderService(IOptions<SendGridSettings> options, IMapper mapper)
        {
            _options = options.Value;
        }
        public async Task<bool> SendEmailAsync(EmailMessage message)
        {
            var sendGridClient = new SendGridClient(_options.ApiKey);
            var from = new EmailAddress(_options.SenderEmail, _options.SenderName);
            var to = new EmailAddress() {
                Email = message.Recipient.Email,
                Name = message.Recipient.Name
            };

            SendGridMessage msg;
            if (!string.IsNullOrEmpty(message.HtmlContent))
            {
                msg = MailHelper.CreateSingleEmail(from, to, message.Subject, "", message.HtmlContent);
            }
            else
            {
                var templateId = _options.Templates.FirstOrDefault(x => x.Key.Equals(message.Template.ToString("G")));
                if (string.IsNullOrEmpty(templateId.Value))
                {
                    return false;
                }
                msg = MailHelper.CreateSingleTemplateEmail(from, to, templateId.Value, message.TemplateData);
            }
            var response = await sendGridClient.SendEmailAsync(msg);

            return response.StatusCode == System.Net.HttpStatusCode.Accepted;
        }
    }
}

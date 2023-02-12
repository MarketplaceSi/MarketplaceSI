using Kernel.Accounts.Commandsp;
using MarketplaceSI.Core.Domain.Settings;
using MarketplaceSI.Core.Dto.Emails.TemplateData;
using MarketplaceSI.Core.Dto.Emails;
using Microsoft.Extensions.Options;
using MarketplaceSI.Core.Dto.Enums;

namespace Kernel.Accounts.Commands;
public class AccountResetPasswordCommandHandler : IRequestHandler<AccountResetPasswordCommand, ActionPayload>
{
    private readonly IUserService _userService;
    private readonly IMailSenderService _mailService;
    private readonly UrlsSettings _settings;

    public AccountResetPasswordCommandHandler(IUserService userService,
        IMailSenderService mailSender,
        IOptions<UrlsSettings> options)
    {
        _userService = userService;
        _mailService = mailSender;
        _settings = options.Value;
    }


    public async Task<ActionPayload> Handle(AccountResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.RequestPasswordReset(request.Email);
        if (user is null)
        {
            return new ActionPayload(true);
        }

        return new ActionPayload(await _mailService.SendEmailAsync(new EmailMessage()
        {
            Subject = "Reset password",
            Template = EmailTemplates.VerifyEmail,
            Recipient = new RecipientAddress(user.FirstName, user.Email),
            TemplateData = new TemplateDataBase()
            {
                Name = user.FirstName,
                Subject = "Reset password",
                RedirectUrl = $"{_settings.FrontEndBaseUrl}/password/reset/{user.ResetKey}",
            }
        }));
    }
}


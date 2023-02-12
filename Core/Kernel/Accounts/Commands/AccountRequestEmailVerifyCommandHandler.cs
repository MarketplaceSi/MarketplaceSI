
using MarketplaceSI.Core.Domain.Settings;
using MarketplaceSI.Core.Dto.Emails.TemplateData;
using MarketplaceSI.Core.Dto.Emails;
using Microsoft.Extensions.Options;
using MarketplaceSI.Core.Dto.Enums;

namespace Kernel.Accounts.Commands;

public class AccountRequestEmailVerifyCommandHandler : IRequestHandler<AccountRequestEmailVerifyCommand, ActionPayload>
{
    private readonly IUserService _userService;
    private readonly IMailSenderService _mailService;
    private readonly UrlsSettings _settings;

    public AccountRequestEmailVerifyCommandHandler(IUserService userService,
        IMailSenderService mailSender,
        IOptions<UrlsSettings> options)
    {
        _userService = userService;
        _mailService = mailSender;
        _settings = options.Value;
    }


    public async Task<ActionPayload> Handle(AccountRequestEmailVerifyCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.RequestEmailVerify(request.Key);
        if (user is null)
        {
            return new ActionPayload(true);
        }

        return new ActionPayload(await _mailService.SendEmailAsync(new EmailMessage()
        {
            Subject = "Verify your email",
            Template = EmailTemplates.VerifyEmail,
            Recipient = new RecipientAddress(user.FirstName, user.Email),
            TemplateData = new TemplateDataBase()
            {
                Name = user.FirstName,
                Subject = "Verify your email",
                RedirectUrl = $"{_settings.FrontEndBaseUrl}/password/create/{user.ResetKey}"
            }
        }));
    }
}
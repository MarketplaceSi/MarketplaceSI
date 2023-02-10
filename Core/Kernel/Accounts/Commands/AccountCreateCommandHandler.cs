using MarketplaceSI.Core.Domain.Settings;
using MarketplaceSI.Core.Dto.Emails;
using MarketplaceSI.Core.Dto.Emails.TemplateData;
using MarketplaceSI.Core.Dto.Enums;
using Microsoft.Extensions.Options;

namespace MarketplaceSI.Core.Kernel.Account.Commands;

public class AccountCreateCommandHandler : IRequestHandler<AccountCreateCommand, AccountPayloadBase>
{
    private readonly IUserService _userService;
    private readonly IMailSenderService _sender;
    private readonly UrlsSettings _settings;

    public AccountCreateCommandHandler(IUserService userService,
        IMailSenderService sender,
        IOptions<UrlsSettings> options)
    {
        _userService = userService;
        _sender = sender;
        _settings = options.Value;
    }

    public async Task<AccountPayloadBase> Handle(AccountCreateCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.RegisterUser(new User()
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
        }, String.Empty);

        await _sender.SendEmailAsync(CreateMessage(user));

        return new AccountPayloadBase(user);
    }

    private EmailMessage CreateMessage(User user)
    {        
        var message = new EmailMessage()
        {
            Recipient = new RecipientAddress()
            {
                Email = user.Email,
                Name = user.LastName
            },
            Template = EmailTemplates.VerifyEmail,
            TemplateData = new TemplateDataBase()
            {
                Name = user.FirstName,
                Subject = "Verify your email",
                RedirectUrl = $"{_settings.FrontEndBaseUrl}/password/create/{user.ResetKey}"
            }
        };
        return message;
    }
}

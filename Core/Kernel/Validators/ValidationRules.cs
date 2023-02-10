using System.Text.RegularExpressions;
using FluentValidation;
namespace MarketplaceSI.Core.Kernel.Validators;

public static class ValidationRules
{
    #region Constants
    private const string _passwordPattern = "^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,}$";
    private const string _emailPattern = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,5}$";
    public const int MaxImageSize = 1024 * 1024 * 5;
    #endregion

    //TODO: Add messages to other validation errors

    #region Rules
    public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("property_empty")
            .Length(8, 16).WithMessage("password_length")
            .Must(IsValidPassword).WithMessage("password_incorrect");
    }

    public static IRuleBuilderOptions<T, string> Email<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("property_empty")
            .EmailAddress().WithMessage("email_incorrect");
            // .Must(IsValidEmail).WithMessage("Email is ivalid");
    }

    public static IRuleBuilderOptions<T, Guid> Id<T>(this IRuleBuilder<T, Guid> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty();
    }

    public static IRuleBuilderOptions<T, long?> MaxPictureSize<T>(this IRuleBuilder<T, long?> ruleBuilder)
    {
        return ruleBuilder
            .Must(ValidSize).WithMessage(string.Format($"Size of file can`t be over {0}", MaxImageSize));
    }

    #endregion


    private static bool IsValidPassword(string str) => string.IsNullOrEmpty(str) ? false : new Regex(_passwordPattern).IsMatch(str);

    private static bool IsValidEmail(string str) => string.IsNullOrEmpty(str) ? false : new Regex(_emailPattern).IsMatch(str);
    private static bool ValidSize(long? size) => size > MaxImageSize ? false : true;
    
}
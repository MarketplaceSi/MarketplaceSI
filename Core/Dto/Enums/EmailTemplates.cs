using System.ComponentModel;

namespace MarketplaceSI.Core.Dto.Enums
{
    public enum EmailTemplates
    {
        [Description("Verifyed email")]
        VerifyEmail,
        [Description("Forgot Passvord")]
        ForgotPassword
    }
}
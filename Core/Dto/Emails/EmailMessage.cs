using MarketplaceSI.Core.Dto.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MarketplaceSI.Core.Dto.Emails
{
    public class EmailMessage
    {
        public string Subject { get; set; } = string.Empty;
        public RecipientAddress Recipient { get; set; } = default!;
        public string? HtmlContent { get; set; }
        public EmailTemplates Template { get; set; }
        public object? TemplateData { get; set; }
    }
}

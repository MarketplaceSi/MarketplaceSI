using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSI.Core.Dto.Emails.TemplateData
{
    public class TemplateDataBase
    {
        public string? Name { get; set; }
        public string? Subject { get; set; }
        public string? RedirectUrl { get; set; }
        public string? Token { get; set; }
    }
}

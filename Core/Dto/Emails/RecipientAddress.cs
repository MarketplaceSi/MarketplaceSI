using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSI.Core.Dto.Emails
{
    public class RecipientAddress
    {
        public RecipientAddress()
        {
        }

        public RecipientAddress(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}

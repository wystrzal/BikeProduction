using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Core.Models
{
    public class TokenModel
    {
        public string Token { get; set; }
        public string NameIdentifier { get; set; }

        public TokenModel(string token, string nameIdentifier)
        {
            Token = token;
            NameIdentifier = nameIdentifier;
        }
    }
}

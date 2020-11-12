using System;

namespace Sysplan.Application.ViewModels.Token
{
    public class TokenViewModel
    {
        public TokenViewModel()
        {
        }

        public TokenViewModel(string token, DateTime expires)
        {
            this.Token = token;
            this.Expires = expires;
        }

        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}

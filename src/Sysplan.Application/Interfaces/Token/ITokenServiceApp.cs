using Sysplan.Application.ViewModels;
using Sysplan.Application.ViewModels.Token;

namespace Sysplan.Application.Interfaces
{
    public interface ITokenServiceApp
    {
        TokenViewModel GenereteToken(ClienteViewModel usuario);
    }
}

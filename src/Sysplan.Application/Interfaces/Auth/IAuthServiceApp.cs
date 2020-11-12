using Sysplan.Application.ViewModels;
using Sysplan.Application.ViewModels.Token;
using System.Threading.Tasks;

namespace Sysplan.Application.Interfaces.Auth
{
    public interface IAuthServiceApp
    {
        Task<TokenViewModel> Auth(AuthViewModel authViewModel);
    }
}

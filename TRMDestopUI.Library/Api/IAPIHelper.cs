using System.Threading.Tasks;
using TRMDesktopUI.Models;

namespace TRMDestopUI.Library.Api
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
        Task GetLoggedInUserInfo(string token);

    }
}
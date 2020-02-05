using System.Collections.Generic;
using System.Threading.Tasks;
using TRMDestopUI.Library.Models;

namespace TRMDestopUI.Library.Api
{
    public interface IUserEndpoint
    {
        Task<List<UserModel>> GetAll();

        Task<Dictionary<string, string>> GetAllRoles();

        Task AddUserToRole(string UserId, string RoleName);

        Task RemoveUserFromRole(string UserId, string RoleName);

    }
}
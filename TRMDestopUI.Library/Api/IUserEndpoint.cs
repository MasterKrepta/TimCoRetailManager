using System.Collections.Generic;
using System.Threading.Tasks;
using TRMDestopUI.Library.Models;

namespace TRMDestopUI.Library.Api
{
    public interface IUserEndpoint
    {
        Task<List<UserModel>> GetAll();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using TRMDestopUI.Library.Models;

namespace TRMDestopUI.Library.Api
{
    public interface IProductEndpoint
    {
        Task<List<ProductModel>> GetAll();
    }
}
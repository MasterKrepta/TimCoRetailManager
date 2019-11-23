using System.Threading.Tasks;
using TRMDestopUI.Library.Models;

namespace TRMDestopUI.Library.Api
{
    public interface ISaleEndpoint
    {
        Task PostSale(SaleModel sale);
    }
}
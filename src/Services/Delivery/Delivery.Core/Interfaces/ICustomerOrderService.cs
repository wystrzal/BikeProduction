using Delivery.Core.Models;
using System.Threading.Tasks;

namespace Delivery.Core.Interfaces
{
    public interface ICustomerOrderService
    {
        Task<Order> GetOrder(int id, string token);
    }
}

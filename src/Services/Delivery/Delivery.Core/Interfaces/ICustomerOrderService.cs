using Delivery.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Core.Interfaces
{
    public interface ICustomerOrderService
    {
        Task<Order> GetOrder(int id);
    }
}

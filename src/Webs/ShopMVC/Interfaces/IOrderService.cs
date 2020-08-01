using ShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrder(Order order);
    }
}

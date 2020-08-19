using ShopMVC.Models;
using ShopMVC.Models.Dtos;
using ShopMVC.Models.ViewModels;
using System.Threading.Tasks;

namespace ShopMVC.Interfaces
{
    public interface IBasketService
    {
        Task<UserBasketViewModel> GetBasket();
        Task AddProduct(BasketProduct basketProduct);
        Task ClearBasket();
        Task ChangeProductQuantity(ChangeProductQuantityDto changeProductQuantityDto);
        Task RemoveProduct(int productId);
        Task<int> GetBasketQuantity();
    }
}

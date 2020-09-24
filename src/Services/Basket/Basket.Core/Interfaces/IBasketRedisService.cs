using Basket.Core.Dtos;
using System.Threading.Tasks;

namespace Basket.Core.Interfaces
{
    public interface IBasketRedisService
    {
        Task RemoveBasket(string userId);
        Task<UserBasketDto> GetBasket(string userId);
        Task SaveBasket(string userId, UserBasketDto basket);
    }
}

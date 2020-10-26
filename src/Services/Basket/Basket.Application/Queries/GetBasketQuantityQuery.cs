using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Basket.Application.Queries
{
    public class GetBasketQuantityQuery : IRequest<int>
    {
        [Required]
        public string UserId { get; set; }

        public GetBasketQuantityQuery(string userId)
        {
            UserId = userId;
        }
    }
}

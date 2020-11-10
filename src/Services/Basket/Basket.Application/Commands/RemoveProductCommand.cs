using System;

namespace Basket.Application.Commands
{
    public class RemoveProductCommand : UserIdCommand
    {
        public int ProductId { get; set; }

        public RemoveProductCommand(string userId, int productId) : base(userId)
        {
            if (productId <= 0)
            {
                throw new ArgumentException("ProductId must be greater than zero.");
            }

            ProductId = productId;
        }
    }
}

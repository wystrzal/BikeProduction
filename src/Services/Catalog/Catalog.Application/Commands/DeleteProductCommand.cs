using MediatR;

namespace Catalog.Application.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public int ProductId { get; set; }

        public DeleteProductCommand(int productId)
        {
            ProductId = productId;
        }
    }
}

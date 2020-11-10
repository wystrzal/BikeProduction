namespace Basket.Application.Commands
{
    public class ClearBasketCommand : UserIdCommand
    {
        public ClearBasketCommand(string userId) : base(userId)
        {
        }
    }
}

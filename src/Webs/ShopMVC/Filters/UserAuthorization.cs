using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace ShopMVC.Filters
{
    public class UserAuthorization : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Controller controller = context.Controller as Controller;

            if (!controller.User.Claims.Any())
            {
                context.Result = controller.RedirectToAction("Index", "Home");
            }
        }
    }
}

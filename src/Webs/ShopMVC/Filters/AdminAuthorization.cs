using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace ShopMVC.Filters
{
    public class AdminAuthorization : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Controller controller = context.Controller as Controller;

            if (!controller.User.Claims.Any(x => x.Value == "admin"))
            {
                context.Result = controller.RedirectToAction("Index", "Home", new { area = "" });
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagementMVC.Filters
{
    public class AuthorizeActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Controller controller = context.Controller as Controller;

            if (!controller.User.Claims.Any(x => x.Value == "admin"))
                context.Result = controller.RedirectToAction("Index", "Identity");
        }
    }
}

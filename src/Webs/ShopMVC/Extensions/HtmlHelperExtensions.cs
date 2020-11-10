using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace ShopMVC.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string IsActive(this IHtmlHelper htmlHelper, string controller, string cssClass = "active")
        {
            string currentController = htmlHelper.ViewContext.RouteData.Values["controller"] as string;

            return currentController.Equals(controller) ?
                cssClass : String.Empty;
        }
    }
}

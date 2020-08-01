using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Filters
{
    public class ModelErrorsResultFilter : ResultFilterAttribute
    {
        public string ErrorsName { get; set; }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Controller controller = context.Controller as Controller;

            controller.TempData.Add(ErrorsName, new List<string>());

            foreach (var obj in context.ModelState.Values)
            {
                foreach (var error in obj.Errors)
                {
                    if (!string.IsNullOrEmpty(error.ErrorMessage))
                    {
                        ((List<string>)controller.TempData[ErrorsName]).Add(error.ErrorMessage);
                    }
                }
            }
        }
    }
}

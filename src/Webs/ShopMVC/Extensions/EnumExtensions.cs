using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Extensions
{
    public static class EnumExtensions
    {
        public static IEnumerable<SelectListItem> SelectListItem(this Enum @enum)
        {
            var enumType = @enum.GetType();

            return Enum.GetNames(enumType)
                .Select(x => new SelectListItem() { Text = x.Replace("_", " "), Value = x });
        }
    }
}

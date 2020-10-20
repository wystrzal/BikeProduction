using ShopMVC.Areas.Admin.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ShopMVC.Areas.Admin.Models.Enums.LoadingPlaceStatusEnum;

namespace ShopMVC.Areas.Admin.Models
{
    public class LoadingPlaceFilteringData
    {
        public LoadingPlaceStatus LoadingPlaceStatus { get; set; }
    }
}

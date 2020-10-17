using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Areas.Admin.Models.Enums
{
    public class LoadingPlaceStatusEnum
    {
        public enum LoadingPlaceStatus
        {
            All = 0,
            WaitingForLoading = 1,
            Loading = 2,
            Sended = 3
        }
    }
}

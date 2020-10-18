using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Areas.Admin.Models.Enums
{
    public class PackStatusEnum
    {
        public enum PackStatus
        {
            Waiting = 1,
            ReadyToSend = 2,
            Sended = 3,
            Delivered = 4
        }
    }
}

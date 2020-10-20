using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ShopMVC.Areas.Admin.Models.Enums.LoadingPlaceStatusEnum;

namespace ShopMVC.Areas.Admin.Models
{
    public class LoadingPlace
    {
        public int Id { get; set; }
        public string LoadingPlaceName { get; set; }
        public int LoadedQuantity { get; set; }
        public int AmountOfSpace { get; set; }
        public LoadingPlaceStatus LoadingPlaceStatus { get; set; }      
    }
}

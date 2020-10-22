using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Areas.Admin.Models.ViewModels
{
    public class PackDetailViewModel
    {
        public PackToDelivery PackToDelivery { get; set; }
        public List<LoadingPlace> LoadingPlaces { get; set; }
    }
}

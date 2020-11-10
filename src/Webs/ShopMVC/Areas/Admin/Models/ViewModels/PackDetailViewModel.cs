using System.Collections.Generic;

namespace ShopMVC.Areas.Admin.Models.ViewModels
{
    public class PackDetailViewModel
    {
        public PackToDelivery PackToDelivery { get; set; }
        public List<LoadingPlace> LoadingPlaces { get; set; }
    }
}

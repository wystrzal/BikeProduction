using System.Collections.Generic;
using static ShopMVC.Areas.Admin.Models.Enums.LoadingPlaceStatusEnum;

namespace ShopMVC.Areas.Admin.Models.ViewModels
{
    public class LoadingPlacesViewModel
    {
        public List<LoadingPlace> LoadingPlaces { get; set; }
        public LoadingPlaceStatus LoadingPlaceStatus { get; set; }
    }
}

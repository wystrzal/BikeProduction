using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ShopMVC.Areas.Admin.Models.Enums.LoadingPlaceStatusEnum;

namespace ShopMVC.Areas.Admin.Models
{
    public class LoadingPlace
    {
        public int Id { get; set; }
        [Required]
        public string LoadingPlaceName { get; set; }
        public int LoadedQuantity { get; set; }
        [Required]
        public int AmountOfSpace { get; set; }
        public LoadingPlaceStatus LoadingPlaceStatus { get; set; }
        public ICollection<PackToDelivery> PacksToDelivery { get; set; }
    }
}

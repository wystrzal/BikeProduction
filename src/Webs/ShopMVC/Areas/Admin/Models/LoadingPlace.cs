using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static ShopMVC.Areas.Admin.Models.Enums.LoadingPlaceStatusEnum;

namespace ShopMVC.Areas.Admin.Models
{
    public class LoadingPlace
    {
        public int Id { get; set; }

        [Required]
        public string LoadingPlaceName { get; set; }

        public int LoadedQuantity { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The field AmountOfSpace must be greater than zero.")]
        public int AmountOfSpace { get; set; }
        public LoadingPlaceStatus LoadingPlaceStatus { get; set; }
        public ICollection<PackToDelivery> PacksToDelivery { get; set; }
    }
}

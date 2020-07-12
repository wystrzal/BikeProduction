using Delivery.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Delivery.Core.Models.Enums.LoadingPlaceStatusEnum;

namespace Delivery.Infrastructure.Data
{
    public static class DataSeed
    {
        public static async Task AddSeed(DataContext dataContext)
        {
            if (!dataContext.LoadingPlaces.Any())
            {
                var loadingPlaces = new List<LoadingPlace>()
                {
                    new LoadingPlace { LoadingPlaceNumber = 1, LoadingPlaceStatus = LoadingPlaceStatus.WaitingForLoading,
                     LoadedQuantity = 0, AmountOfSpace = 30 },
                    new LoadingPlace { LoadingPlaceNumber = 2, LoadingPlaceStatus = LoadingPlaceStatus.WaitingForLoading,
                     LoadedQuantity = 0, AmountOfSpace = 20 },
                    new LoadingPlace { LoadingPlaceNumber = 3, LoadingPlaceStatus = LoadingPlaceStatus.WaitingForLoading,
                     LoadedQuantity = 0, AmountOfSpace = 30 }
                };

                await dataContext.LoadingPlaces.AddRangeAsync(loadingPlaces);
                await dataContext.SaveChangesAsync();
            }
        }
    }
}

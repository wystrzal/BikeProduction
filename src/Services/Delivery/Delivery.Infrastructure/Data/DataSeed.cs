using Delivery.Core.Models;
using System.Collections.Generic;
using System.Linq;
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
                    new LoadingPlace { LoadingPlaceName = "Dock-1", LoadingPlaceStatus = LoadingPlaceStatus.Waiting_For_Loading,
                        LoadedQuantity = 0, AmountOfSpace = 30 },
                    new LoadingPlace { LoadingPlaceName = "Dock-2", LoadingPlaceStatus = LoadingPlaceStatus.Waiting_For_Loading,
                        LoadedQuantity = 0, AmountOfSpace = 20 },
                    new LoadingPlace { LoadingPlaceName = "Dock-3", LoadingPlaceStatus = LoadingPlaceStatus.Waiting_For_Loading,
                        LoadedQuantity = 0, AmountOfSpace = 30 }
                };

                await dataContext.LoadingPlaces.AddRangeAsync(loadingPlaces);
                await dataContext.SaveChangesAsync();
            }
        }
    }
}

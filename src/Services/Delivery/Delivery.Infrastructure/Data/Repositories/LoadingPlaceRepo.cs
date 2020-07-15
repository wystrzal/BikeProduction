using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Infrastructure.Data.Repositories
{
    public class LoadingPlaceRepo : BaseRepository<LoadingPlace> ,ILoadingPlaceRepo
    {
        public LoadingPlaceRepo(DataContext dataContext) : base(dataContext)
        {
        }


    }
}

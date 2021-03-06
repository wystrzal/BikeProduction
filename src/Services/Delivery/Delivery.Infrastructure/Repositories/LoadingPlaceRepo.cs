﻿using BaseRepository;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using Delivery.Infrastructure.Data;

namespace Delivery.Infrastructure.Repositories
{
    public class LoadingPlaceRepo : Repository<LoadingPlace, DataContext>, ILoadingPlaceRepo
    {
        public LoadingPlaceRepo(DataContext dataContext) : base(dataContext)
        {
        }


    }
}

﻿using BikeBaseRepository;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using Delivery.Infrastructure.Data;

namespace Delivery.Infrastructure.Repositories
{
    public class PackToDeliveryRepo : BaseRepository<PackToDelivery, DataContext>, IPackToDeliveryRepo
    {
        public PackToDeliveryRepo(DataContext dataContext) : base(dataContext)
        {

        }
    }
}

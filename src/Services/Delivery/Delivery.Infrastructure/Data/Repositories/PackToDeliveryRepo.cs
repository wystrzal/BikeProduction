using BikeBaseRepository;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Infrastructure.Data.Repositories
{
    public class PackToDeliveryRepo : BaseRepository<PackToDelivery, DataContext>, IPackToDeliveryRepo
    {
        public PackToDeliveryRepo(DataContext dataContext) : base(dataContext)
        {

        }
    }
}

using BikeBaseRepository;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using Delivery.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Infrastructure.Repositories
{
    public class PackToDeliveryRepo : BaseRepository<PackToDelivery, DataContext>, IPackToDeliveryRepo
    {
        public PackToDeliveryRepo(DataContext dataContext) : base(dataContext)
        {

        }
    }
}

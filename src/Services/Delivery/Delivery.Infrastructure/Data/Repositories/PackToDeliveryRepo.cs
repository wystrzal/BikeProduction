using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Infrastructure.Data.Repositories
{
    public class PackToDeliveryRepo : BaseRepository<PackToDelivery>, IBaseRepository<PackToDelivery>, IPackToDeliveryRepo
    {
        public PackToDeliveryRepo(DataContext dataContext) : base(dataContext)
        {

        }
    }
}

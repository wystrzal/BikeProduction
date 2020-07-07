using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Production.Core.Models.Enums
{
    public class ProductionStatusEnum
    {
        public enum ProductionStatus
        {
            Waiting = 1,
            Confirmed = 2,
            Created = 3,
            Finished = 4
        }
    }
}

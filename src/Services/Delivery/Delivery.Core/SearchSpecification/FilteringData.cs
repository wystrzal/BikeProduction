﻿using System;
using System.Collections.Generic;
using System.Text;
using static Delivery.Core.Models.Enums.PackStatusEnum;

namespace Delivery.Core.SearchSpecification
{
    public class FilteringData
    {
        public PackStatus PackStatus { get; set; }
    }
}
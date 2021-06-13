﻿using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public class OrderPaginationQueryResult : BasePaginationQueryResult
    {
        public List<Order> Data { get; set; }
    }
}
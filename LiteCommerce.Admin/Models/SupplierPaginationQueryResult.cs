﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LiteCommerce.DomainModels;

namespace LiteCommerce.Admin.Models
{
    public class SupplierPaginationQueryResult : BasePaginationQueryResult
    {
        public List<Supplier> Data { get; set; }
    }
}
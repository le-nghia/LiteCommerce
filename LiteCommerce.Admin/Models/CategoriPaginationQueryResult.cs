using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public class CategoriPaginationQueryResult : BasePaginationQueryResult
    {
        public List<Categori> Data { get; set; }
    }
}
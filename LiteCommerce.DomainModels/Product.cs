using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// Lớp biểu diễn dữ liệu mặt hàng
    /// </summary>
    public class Product
    {
        /// <summary>
        /// 
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SupplierID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CategoryID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Photo { get; set; }
    }
    /// <summary>
    /// Mặt hàng với các thông tin bổ sung.
    /// </summary>
    public class ProductEx: Product
    {
        /// <summary>
        /// Danh sách các thuộc tính mặt hàng.
        /// </summary>
        public List<ProductAttribute> Attributes { get; set; }
        /// <summary>
        /// Danh sách các hình ảnh của mặt hàng.
        /// </summary>
        public List<ProductGallery> Galleries { get; set; }

    }
}

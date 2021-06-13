using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// Tạo đơn hàng
    /// </summary>
    public class OrderDetail
    {
        /// <summary>
        /// Mã chi tiết đơn hàng.
        /// </summary>
        public int OrderDetailID { get; set; }
        /// <summary>
        /// Mã đơn hàng
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// Mã hàng
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// Định luong của Đơn Hàng.
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Giá bán.
        /// </summary>
        public decimal SalePrice { get; set; }
    }
}

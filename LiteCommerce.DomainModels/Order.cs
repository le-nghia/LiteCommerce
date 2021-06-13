using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// Đặt hàng.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Mã đặt hàng
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// Mã khách hàng.
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// Thời gian đặt hàng
        /// </summary>
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public int EmployeeID { get; set; }
        /// <summary>
        /// Thời gian nhận đơn hàng.
        /// </summary>
        public DateTime AcceptTime { get; set; }
        /// <summary>
        /// Mã nhân viên vận chuyển.
        /// </summary>
        public int ShipperID { get; set; }
        /// <summary>
        /// Thời gian vận chuyển
        /// </summary>
        public DateTime ShippedTime { get; set; }
        /// <summary>
        /// Thời gian kết thúc vận chuyển.
        /// </summary>
        public DateTime FinishedTime { get; set; }
        /// <summary>
        /// Trạng thái.
        /// </summary>
        public int Status { get; set; }
    }
}

using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép dữ liệu liên quan đến thành phố.
    /// </summary>
    public interface ICityDAL
    {
        /// <summary>
        /// Lấy tất cả danh sách Thành Phố.
        /// </summary>
        /// <returns></returns>
        List<City> List();
        /// <summary>
        /// Lấy DS thành phố của 1 Quốc Gia.
        /// </summary>
        /// <param name="countryName">Tên của Quốc gia cần lấy thành phố</param>
        /// <returns></returns>
        List<City> List(string countryName);
    }
}

using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    ///  Định nghĩa các phép dữ liệu liên quan đến Quốc Gia.
    /// </summary>
    public interface ICountryDAL
    {
        /// <summary>
        /// Lấy danh sách Quốc Gia.
        /// </summary>
        /// <returns></returns>
        List<Country> List();
    }
}

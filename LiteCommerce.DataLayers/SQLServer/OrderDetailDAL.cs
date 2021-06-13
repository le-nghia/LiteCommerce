using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SQLServer
{
    public class OrderDetailDAL : _BaseDAL , IOrderDetailDAL
    {
        public OrderDetailDAL (string connectionString): base( connectionString)
        {

        }

        public int Add(OrderDetail data)
        {
            throw new NotImplementedException();
        }

        public int Count(string searchValue)
        {
            return 30;
        }

        public bool Delete(OrderDetail data)
        {
            throw new NotImplementedException();
        }

        public OrderDetail Get(int orderDetailID)
        {
            throw new NotImplementedException();
        }

        public List<OrderDetail> List(int page, int pageSize, string searchValue)
        {
            List<OrderDetail> data = new List<OrderDetail>();
            /*if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            List<OrderDetail> data = new List<OrderDetail>();
            using(SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"select * from (
	                                select *, ROW_NUMBER() OVER (ORDER BY SupplierName) AS RowNumber
	                                from Suppliers
	                                where (@searchValue = '') OR (SupplierName LIKE @searchValue
							                                  OR ContactName LIKE @searchValue
							                                  OR Address LIKE @searchValue
							                                  OR Phone LIKE @searchValue )
                                    ) AS s where s.RowNumber BETWEEN (@page-1)*@pageSize+1 AND @page*@pageSize";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
            }*/
            return data;
        }

        public bool Update(OrderDetail data)
        {
            throw new NotImplementedException();
        }
    }
}

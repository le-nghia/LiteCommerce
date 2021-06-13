using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SQLServer
{
    public class OrderDAL : _BaseDAL , IOrderDAL
    {
        public OrderDAL ( string connectionString ) : base(connectionString)
        {

        }

        public int Add(Order data)
        {
            int orderID = 0;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO Orders 
                                    (
                                        CustomerID, OrderTime, EmployeeID, AcceptTime, ShipperID, ShippedTime, FinishedTime, Status
                                    ) VALUES 
                                        (
                                            @CustomerID, @OrderTime, @EmployeeID, @AcceptTime, @ShipperID, @ShippedTime, @FinishedTime, @Status
                                        );
                                            SELECT @@IDENTITY;";

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);
                cmd.Parameters.AddWithValue("@OrderTime", data.OrderTime);
                cmd.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                cmd.Parameters.AddWithValue("@AcceptTime", data.AcceptTime);
                cmd.Parameters.AddWithValue("@ShipperID", data.ShipperID);
                cmd.Parameters.AddWithValue("@ShippedTime", data.ShippedTime);
                cmd.Parameters.AddWithValue("@FinishedTime", data.FinishedTime);
                cmd.Parameters.AddWithValue("@Status", data.Status);

                orderID = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }

            return orderID;
        }

        public int Count(string searchValue)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%"; //Tim kiem tuong doi.

            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"select COUNT(*)
                                    from Orders
	                                where (@searchValue = '') OR
                                        (
                                            CustomerID LIKE @searchValue
							                OR EmployeeID LIKE @searchValue
							                OR OrderTime LIKE @searchValue
							               
                                        )";

                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();

            }
            return result;
        }

        public bool Delete(int orderID)
        {
            bool result = false;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"DELETE FROM Orders WHERE OrderID = @OrderID 
                                   and not exists (
                                       select * from OrderDetails where OrderID = Orders.OrderID and OrderID = Orders.OrderID );";

                cmd.Parameters.AddWithValue("@OrderID", orderID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }

            return result;
        }

        public Order Get(int orderID)
        {
            Order data = null;
            using (SqlConnection cn = GetConnection())
            {

                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"select * from Orders where OrderID = @OrderID";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@OrderID", orderID);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)) //Using se giai phong vung nho sau khi thuc hien.
                {
                    if (dbReader.Read()) //Neu co du lieu dua ra ket qua.
                    {
                        data = new Order()
                        {
                            OrderID = Convert.ToInt32(dbReader["OrderID"]),
                            CustomerID = Convert.ToInt32(dbReader["CustomerID"]),
                            OrderTime = Convert.ToDateTime(dbReader["OrderTime"]),
                            EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                            AcceptTime = Convert.ToDateTime(dbReader["AcceptTime"]),
                            ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                            ShippedTime = Convert.ToDateTime(dbReader["ShippedTime"]),
                            FinishedTime = Convert.ToDateTime(dbReader["FinishedTime"]),
                            Status = Convert.ToInt32(dbReader["Status"])
                        };
                    }
                }

                cn.Close();
            }
            return data;
        }

        public List<Order> List(int page, int pageSize, int customerID, string searchValue)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            List<Order> data = new List<Order>();
            using(SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM
                                    (
                                        SELECT  *, ROW_NUMBER() OVER(ORDER BY EmployeeID) AS RowNumber
                                        FROM    Orders 
                                        WHERE   (@customerID = 0 OR CustomerID = @customerID)
                                            
                                            AND (@searchValue = '' OR Status LIKE @searchValue)                                          
                                    ) AS s
                                    WHERE s.RowNumber BETWEEN (@page - 1)*@pageSize + 1 AND @page*@pageSize";

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@customerID", customerID);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Order()
                        {
                            OrderID = Convert.ToInt32(dbReader["OrderID"]),
                            CustomerID = Convert.ToInt32(dbReader["CustomerID"]),
                            OrderTime = Convert.ToDateTime(dbReader["OrderTime"]),
                            EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                            AcceptTime = Convert.ToDateTime(dbReader["AcceptTime"]),
                            ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                            ShippedTime = Convert.ToDateTime(dbReader["ShippedTime"]),
                            FinishedTime = Convert.ToDateTime(dbReader["FinishedTime"]),
                            Status = Convert.ToInt32(dbReader["Status"])                         
                        });
                    }
                }

                cn.Close();

            }
            return data;
        }

        public bool Update(Order data)
        {
            bool result = false;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE Orders 
                                        SET 
	                                        CustomerID = @CustomerID,
	                                        OrderTime = @OrderTime,
	                                        EmployeeID = @EmployeeID,
	                                        AcceptTime = @AcceptTime,
	                                        ShipperID = @ShipperID,
	                                        ShippedTime = @ShippedTime,
                                            FinishedTime = @FinishedTime,
                                            Status = @Status
                                        WHERE OrderID = @OrderID;";

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@OrderID", data.OrderID);
                cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);
                cmd.Parameters.AddWithValue("@OrderTime", data.OrderTime);
                cmd.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                cmd.Parameters.AddWithValue("@AcceptTime", data.AcceptTime);
                cmd.Parameters.AddWithValue("@ShipperID", data.ShipperID);
                cmd.Parameters.AddWithValue("@ShippedTime", data.ShippedTime);
                cmd.Parameters.AddWithValue("@FinishedTime", data.FinishedTime);
                cmd.Parameters.AddWithValue("@Status", data.Status);

                result = cmd.ExecuteNonQuery() > 0;
                cn.Close();
            }
            return result;
        }
    }
}

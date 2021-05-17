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
    /// <summary>
    /// Cài đặt các tính năng xử lý dữ liệu nhân viên trong csdl sql server
    /// </summary>
    public class EmployeeDAL : _BaseDAL, IEmployeeDAL
    {
        public EmployeeDAL(string connectionString) : base(connectionString)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Employee data)
        {
            int employeeID = 0;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO Employees
                                    (
	                                    LastName, FirstName, BirthDate, Photo, Notes, Email, Password
                                    )
                                    VALUES
                                    (
	                                    @lastName, @firstName, @birthDate, @photo, @note, @email, @password
                                    );
                                    SELECT @@IDENTITY";

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@lastName", data.LastName);
                cmd.Parameters.AddWithValue("@firstName", data.FirstName);
                cmd.Parameters.AddWithValue("@birthDate", data.BirthDate);
                cmd.Parameters.AddWithValue("@photo", data.Photo);
                cmd.Parameters.AddWithValue("@note", data.Notes);
                cmd.Parameters.AddWithValue("@email", data.Email);
                cmd.Parameters.AddWithValue("@password", data.Password);

                employeeID = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }

            return employeeID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(string searchValue)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%"; //Tim kiem tuong doi.

            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"select COUNT(*)
                                    from Employees
	                                where (@searchValue = '') OR
                                        (
                                            LastName LIKE @searchValue
							                OR FirstName LIKE @searchValue
							                
                                        )";

                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();

            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public bool Delete(int employeeID)
        {
            bool result = false;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"DELETE FROM Employees
                                    WHERE EmployeeID = @employeeID AND NOT EXISTS(SELECT * FROM Orders WHERE EmployeeID = Employees.EmployeeID)";

                cmd.Parameters.AddWithValue("@employeeID", employeeID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public Employee Get(int employeeID)
        {
            Employee data = null;

            using (SqlConnection cn = GetConnection())
            {

                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"select * from Employees where EmployeeID = @EmployeeID";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)) //Using se giai phong vung nho sau khi thuc hien.
                {
                    if (dbReader.Read()) //Neu co du lieu dua ra ket qua.
                    {
                        data = new Employee()
                        {
                            EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                            LastName = Convert.ToString(dbReader["LastName"]),
                            FirstName = Convert.ToString(dbReader["FirstName"]),
                            BirthDate = Convert.ToDateTime(dbReader["BirthDate"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                            Notes = Convert.ToString(dbReader["Notes"]),
                            Email = Convert.ToString(dbReader["Email"]),
                            Password = Convert.ToString(dbReader["Password"])

                        };
                    }
                }

                cn.Close();
            }

            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<Employee> List(int page, int pageSize, string searchValue)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%"; //Tim kiem tuong doi.

            List<Employee> data = new List<Employee>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * from (
	                                select *, ROW_NUMBER() OVER (ORDER BY LastName) AS RowNumber
	                                from Employees
	                                where (@searchValue = '') OR (LastName LIKE @searchValue
							                                  OR FirstName LIKE @searchValue
							                                  
                                    )) AS s where s.RowNumber BETWEEN (@page-1)*@pageSize+1 AND @page*@pageSize";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        /*Country country = new Country();
                        country.CountryName = Convert.ToString(dbReader["CountryName"]);
                        data.Add(country);*/

                        /*Country country = new Country()
                        {
                            country.CountryName = Convert.ToString(dbReader["CountryName"])
                        };
                        data.Add(country);*/


                        data.Add(new Employee()
                        {
                            EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                            LastName = Convert.ToString(dbReader["LastName"]),
                            FirstName = Convert.ToString(dbReader["FirstName"]),
                            BirthDate = Convert.ToDateTime(dbReader["BirthDate"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                            Notes = Convert.ToString(dbReader["Notes"]),
                            Email = Convert.ToString(dbReader["Email"]),
                            Password = Convert.ToString(dbReader["Password"])

                        });
                    }
                } 
                cn.Close();
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Employee data)
        {
            bool result = false;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE Employees
                                    SET LastName = @lastName,
	                                    FirstName = @firstName,
	                                    BirthDate = @birthDate,
	                                    Photo = @photo,
	                                    Notes = @note,
	                                    Email = @email,
	                                    Password = @password
                                    WHERE EmployeeID = @employeeID;";

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@employeeID", data.EmployeeID);
                cmd.Parameters.AddWithValue("@lastName", data.LastName);
                cmd.Parameters.AddWithValue("@firstName", data.FirstName);
                cmd.Parameters.AddWithValue("@birthDate", data.BirthDate);
                cmd.Parameters.AddWithValue("@photo", data.Photo);
                cmd.Parameters.AddWithValue("@note", data.Notes);
                cmd.Parameters.AddWithValue("@email", data.Email);
                cmd.Parameters.AddWithValue("@password", data.Password);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }

            return result;
        }
    }
}

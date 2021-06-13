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
    /// Cài đặt các tính năng xử lý dữ liệu loại hàng trong csdl sql server
    /// </summary>
    public class CategoriDAL : _BaseDAL, ICategoriDAL
    {
        public CategoriDAL(string connectionString):base(connectionString)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Categori data)
        {
            int categoryID = 0;
            using(SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO Categories
                                     (
                                        CategoryName, Description, ParentCategoryId
                                     ) VALUES
                                     (
                                         @CategoryName, @Description, @ParentCategoryId
                                     )
                                        SELECT @@IDENTITY;";

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@ParentCategoryId", data.ParentCategoryId);

                categoryID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return categoryID;
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
                                    from Categories
	                                where (@searchValue = '') OR
                                        (
                                            CategoryName LIKE @searchValue
							                OR Description LIKE @searchValue
							                OR ParentCategoryId LIKE @searchValue							                
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
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public bool Delete(int categoryID)
        {
            
            bool result = false;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"DELETE FROM Categories WHERE CategoryID = @CategoryID 
                                AND NOT EXISTS
                                (
                                    SELECT * FROM Products WHERE CategoryID = Categories.CategoryID
                                )";
                cmd.Parameters.AddWithValue("@categoryID", categoryID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }

            return result;           
        }
        /// <summary>
            /// 
            /// </summary>
            /// <param name="categoryID"></param>
            /// <returns></returns>
        public Categori Get(int categoryID)
        {
            Categori data = null;
            using(SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"select * from Categories
                                    where CategoryID = @CategoryID";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue(@"CategoryID", categoryID);

                using(SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Categori()
                        {
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            CategoryName = Convert.ToString(dbReader["CategoryName"]),
                            Description = Convert.ToString(dbReader["Description"]),
                            ParentCategoryId = Convert.ToInt32(dbReader["ParentCategoryId"])
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
        public List<Categori> List(int page, int pageSize, string searchValue)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%"; //Tim kiem tuong doi.

            List<Categori> data = new List<Categori>();

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();

                cmd.CommandText = @"select * from (
	                                select *, ROW_NUMBER() OVER (ORDER BY CategoryName) AS RowNumber
	                                from Categories
	                                where (@searchValue = '') 
                                    OR (
                                            CategoryName LIKE @searchValue
							                OR Description LIKE @searchValue
                                            OR ParentCategoryId LIKE @searchValue
							            )) AS s where s.RowNumber BETWEEN (@page-1)*@pageSize+1 AND @page*@pageSize";

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                using(SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Categori()
                        {
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            CategoryName = Convert.ToString(dbReader["CategoryName"]),
                            Description = Convert.ToString(dbReader["Description"]),
                            ParentCategoryId = Convert.ToInt32(dbReader["ParentCategoryId"])
                        });
                    }
                }
                cn.Close();
            }
            return data;
        }

        public List<Categori> listOfNameCategorys()
        {
            List<Categori> data = new List<Categori>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT CategoryID, CategoryName FROM Categories";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Categori()
                        {
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            CategoryName = Convert.ToString(dbReader["CategoryName"])
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
        public bool Update(Categori data)
        {
            bool result = false;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE Categories 
                                        SET 
	                                        CategoryName = @CategoryName,
	                                        Description = @Description,
	                                        ParentCategoryId = @ParentCategoryId
                                        WHERE CategoryID = @CategoryID;";

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@ParentCategoryId", data.ParentCategoryId);
                
                result = cmd.ExecuteNonQuery() > 0;
            }
            return result;
        }
    }   
}

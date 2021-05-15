using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    public static class AccountService
    {
        private static IAccountDAL AccountDB;
        public static void Init(DatabaseTypes dbType, string connectionString, AccountTypes accountType)
        {
            switch (dbType)
            {
                case DatabaseTypes.SQLServer:
                    if (accountType == AccountTypes.Employee)
                        AccountDB = new DataLayers.SQLServer.EmployeeAccountDAL(connectionString);
                    else
                        AccountDB = new DataLayers.SQLServer.CustomerAccountDAL(connectionString);
                    break;
                default:
                    throw new Exception("Database Type is not supported!");
            }
        }
        public static Account Authorize(string loginName, string password)
        {
            return AccountDB.Authorize(loginName, password);
        }

        public static bool ChangePassword(string accountId, string oldPassword, string newPassword)
        {
            return AccountDB.ChangePassword(accountId, oldPassword, newPassword);
        }

        public static Account Get(string accountId)
        {
            return AccountDB.Get(accountId);
        }
    }
}
public enum AccountTypes 
{
    /// <summary>
    /// Nhhân viên
    /// </summary>
    Employee,

    /// <summary>
    /// Khách hàng
    /// </summary>
    Customer
}

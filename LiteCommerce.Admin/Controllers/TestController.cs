using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LiteCommerce.DataLayers;
using LiteCommerce.DataLayers.SQLServer;
using LiteCommerce.DomainModels;

namespace LiteCommerce.Admin.Controllers
{
    public class TestController : Controller
    {
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public ActionResult Index()
        {
            string connectionString = ConfigurationManager
                                     .ConnectionStrings["LiteCommerceDB"]
                                     .ConnectionString;

            /* ICountryDAL dal = new CountryDAL(connectionString);
             var data = dal.List();*/

            /*ICityDAL dal = new CityDAL(connectionString);
            var data = dal.List("USA");*/

            /* ISupplierDAL dal = new SupplierDAL(connectionString);
             var data = dal.Count("Big");*/

            /*ISupplierDAL dal = new SupplierDAL(connectionString);
            var data = dal.Get(1);*/

         /*   ISupplierDAL dal = new SupplierDAL(connectionString);
            Supplier up = new Supplier()
            {
                SupplierID = 30,
                SupplierName = "Lê Nghĩa",
                ContactName = "Nghĩa Cute",
                Address = "Quảng Bình ",
                City = "Califonia",
                PostalCode = "hi",
                Country = "VN",
                Phone = "0984292547"

            };
            //int supplierID = dal.Add(s);
            var data = dal.Update(up);*/

            ISupplierDAL dal = new SupplierDAL(connectionString);
            var data = dal.Delete(31);


            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Pagination(int page, int pageSize, string searchValue)
        {
            string connectionString = ConfigurationManager
                                     .ConnectionStrings["LiteCommerceDB"]
                                     .ConnectionString;
            
            ISupplierDAL dal = new SupplierDAL(connectionString);
            var data = dal.List(page, pageSize, searchValue);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
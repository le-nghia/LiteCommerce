using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            /*var model = HRService.Customer_list();*/
            return View();
        }
        public ActionResult List(int page = 1, string searchValue = "")
        {
            int pageSize = 10;

            var listofCustomers = DataService.ListCustomers(page, pageSize, searchValue, out int rowCount);

            var model = new Models.CustomerPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,

                Data = listofCustomers

            };
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Thay đổi thông tin khách hàng";
            var model = DataService.GetCustomers(id);
            if (model == null)
                RedirectToAction("Index");

            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            ViewBag.Title = "Bổ sung Khánh hàng mới";
            Customer model = new Customer()
            {
                CustomerID = 0
            };

            return View("Edit", model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            if (Request.HttpMethod == "POST")
            {
                // Xóa Supplier có mã ID
                DataService.DeleteCustomer(id);

                // Quay lại trang Index.
                return RedirectToAction("Index");
            }
            else
            {
                // Lấy thông tin của Supplier cần xóa.
                var del = DataService.GetCustomers(id);
                if (del == null)
                    return RedirectToAction("Index");

                // Trả thông tin về cho view hiển thị.
                return View(del);

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Save(Customer data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.CustomerName))
                    ModelState.AddModelError("a", "Vui lòng nhập tên khách hàng");
                if (string.IsNullOrWhiteSpace(data.ContactName))
                    ModelState.AddModelError("b", "Bạn chưa nhập tên liên hệ của khách hàng");
                if (string.IsNullOrEmpty(data.Address))
                    data.Address = "";
                if (string.IsNullOrEmpty(data.Country))
                    data.Country = "";
                if (string.IsNullOrEmpty(data.City))
                    data.City = "";
                if (string.IsNullOrEmpty(data.PostalCode))
                    data.PostalCode = "";

                if (!ModelState.IsValid)
                {
                    if (data.CustomerID == 0)
                        ViewBag.Title = "Bổ sung khách hàng mới";
                    else
                        ViewBag.Title = "Thay đổi thông tin khách hàng";
                    return View("Edit", data);
                }

                if (data.CustomerID == 0)
                    DataService.AddCustomer(data);
                else
                    DataService.UpdateCustomer(data);

                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Hehe hình như có lỗi rồi :D");
            }
        }
    }
}
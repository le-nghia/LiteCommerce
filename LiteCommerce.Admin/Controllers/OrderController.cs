using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int page = 1, int customerID = 0, string searchValue = "")
        {
            int pageSize = 10;

            var listOfOrders = DataService.ListOrders(page, pageSize, customerID, searchValue, out int rowCount);

            var model = new Models.OrderPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,

                Data = listOfOrders

            };
            return View(model);
        }
        public ActionResult AddOrder()
        {
            ViewBag.Title = "Bổ sung đơn hàng mới";

            Order model = new Order()
            {
                OrderID = 0
            };
            return View("EditOrder", model);
        }
        public ActionResult EditOrder(int id)
        {

            ViewBag.Title = "Thay đổi thông tin Đơn Hàng";

            var model = DataService.GetOrder(id);
            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }
        public ActionResult Delete(int id)
        {
            if (Request.HttpMethod == "POST")
            {
                // Xóa đơn hàng có mã ID
                DataService.Delete(id);

                // Quay lại trang Index.
                return RedirectToAction("Index");
            }
            else
            {
                // Lấy thông tin của đơn hàng cần xóa.
                var del = DataService.GetOrder(id);
                if (del == null)
                    return RedirectToAction("Index");

                // Trả thông tin về cho view hiển thị.
                return View(del);

            }
        }
        public ActionResult Save(Order data)
        {
            if (data.OrderID == 0)
            {
                DataService.AddOrder(data);
            }
            else
            {
                DataService.Update(data);
            }

            return RedirectToAction("Index");
        }
    }
}
﻿using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class EmployeeController : Controller
    {
        
        public ActionResult Index()
        {
            
            return View();
        }
         /// <summary>
         /// 
         /// </summary>
         /// <param name="page"></param>
         /// <param name="searchValue"></param>
         /// <returns></returns>
        public ActionResult List(int page = 1, string searchValue = "")
        {
            try
            {         
                int rowCount = 0;
                int pageSize = 5;
                var listEmpolyees = DataService.ListEmpolyees(page, pageSize, searchValue, out rowCount);
                var model = new Models.EmpolyeePaginationQueryResult()
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchValue = searchValue,
                    RowCount = rowCount,

                    Data = listEmpolyees

                };
                return View(model);
            }
            catch(Exception e)
            {
                return Content(e.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Thay đổi thông tin nhân viên";

            var model = DataService.GetEmployee(id);
            if ( model == null )
                RedirectToAction("Index");

            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            ViewBag.Title = "Bổ sung nhân viên mới";

            Employee model = new Employee()
            {
                EmployeeID = 0
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
                DataService.DeleteEmployee(id);

                // Quay lại trang Index.
                return RedirectToAction("Index");
            }
            else
            {
                // Lấy thông tin của Supplier cần xóa.
                var del = DataService.GetEmployee(id);
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
        public ActionResult Save(Employee data)
        {
            try
            {
                if (string.IsNullOrEmpty(data.LastName))
                    ModelState.AddModelError("a", "Vui lòng nhập họ nhân viên");
                if (string.IsNullOrWhiteSpace(data.FirstName))
                    ModelState.AddModelError("b", "Vui lòng nhập tên nhân viên");
                if (string.IsNullOrEmpty(data.Photo))
                    data.Photo = "";
                if (string.IsNullOrWhiteSpace(data.Email))
                    ModelState.AddModelError("c", "Vui lòng nhập email nhân viên");
                if (string.IsNullOrWhiteSpace(data.Password))
                    data.Password = "";
                if (string.IsNullOrEmpty(data.Notes))
                    data.Notes = "";

                if (!ModelState.IsValid)
                {
                    if (data.EmployeeID == 0)
                        ViewBag.Title = "Bổ sung nhân viên mới";
                    else
                        ViewBag.Title = "Thay đổi thông tin nhân viên";
                    return View("Edit", data);
                }

                if (data.EmployeeID == 0)
                    DataService.AddEmployee(data);
                else
                    DataService.UpdateEmployee(data);
                //return Json(data);
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Hehe hình như có lỗi rồi :D");
            }
        }
    }
}
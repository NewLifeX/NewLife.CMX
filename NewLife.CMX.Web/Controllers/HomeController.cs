﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NewLife.CMX.Web.Controllers
{
    /// <summary>主页面</summary>
    [AllowAnonymous]
    public class HomeController : Controller
    {
        /// <summary>主页面</summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Message = "主页面";

            return View();
        }

        /// <summary>应用程序描述</summary>
        public ActionResult About()
        {
            ViewBag.Message = "应用程序描述";

            return View();
        }

        /// <summary>联系我们</summary>
        public ActionResult Contact()
        {
            ViewBag.Message = "联系我们";

            return View();
        }
    }
}
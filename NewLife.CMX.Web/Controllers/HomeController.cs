using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NewLife.CMX.Web.Controllers
{
    /// <summary>主页面</summary>
    [AllowAnonymous]
    public class HomeController : Controller
    {
        /// <summary>主页面</summary>
        /// <returns></returns>
        public ActionResult Index() => View();

        /// <summary>应用程序描述</summary>
        [Route("/About")]
        public ActionResult About() => View();

        /// <summary>联系我们</summary>
        [Route("/Contact")]
        public ActionResult Contact() => View();
    }
}
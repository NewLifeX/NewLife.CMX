using System.ComponentModel;
using System.Web;
using System.Web.Mvc;
using NewLife.Cube;
using XCode.Membership;

namespace NewLife.CMX.Web.Controllers
{
    /// <summary>系统设置控制器</summary>
    [DisplayName("高级设置")]
    public class SysController : ControllerBaseX
    {
        /// <summary>站点配置</summary>
        /// <param name="config"></param>
        /// <returns></returns>
        [DisplayName("站点配置")]
        [EntityAuthorize((PermissionFlags)0x10, ResourceName = "站点配置")]
        public ActionResult Site(SiteConfig config)
        {
            if (HttpContext.Request.HttpMethod == "POST")
            {
                config.Save(SiteConfig.Current.ConfigFile);
            }
            config = SiteConfig.Current;

            return View("SiteConfig", config);
        }

        /// <summary>附件设置</summary>
        /// <param name="config"></param>
        /// <returns></returns>
        [DisplayName("附件设置")]
        [EntityAuthorize((PermissionFlags)0x20, ResourceName = "附件设置")]
        public ActionResult Attach(AttachConfig config)
        {
            if (HttpContext.Request.HttpMethod == "POST")
            {
                config.Save(AttachConfig.Current.ConfigFile);
            }
            config = AttachConfig.Current;

            return View("AttachConfig", config);
        }
    }
}
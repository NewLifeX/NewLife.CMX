using System.ComponentModel;
using System.Web.Mvc;
using NewLife.Cube;

namespace NewLife.CMX.Web
{
    [DisplayName("内容管理")]
    public class CMSAreaRegistration : AreaRegistrationBase
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            base.RegisterArea(context);

            //context.MapRoute(
            //    "CMS_default",
            //    "CMS/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);

            // 为所有频道注册路由
            foreach (var chn in Channel.FindAllWithCache())
            {
                var name = AreaName + "_" + chn.ID;
                context.MapRoute(
                    name,
                    name + "/" + chn.Model.Name + "{controller}/{action}/{id}",
                    new { action = "Index", id = UrlParameter.Optional }
                );
            }
        }
    }
}
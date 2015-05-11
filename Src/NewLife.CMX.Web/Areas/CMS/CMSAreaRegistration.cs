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
            context.MapRoute(
               "CMS_category",
               "CMS/{controller}/{channel}_{category}/{action}/{id}",
               new { action = "Index", id = UrlParameter.Optional }, 
               new { channel = "[\\d]+", category = "[\\d]+" }
               );

            context.MapRoute(
                 "CMS_channel",
                 "CMS/{controller}/{channel}/{action}/{id}",
                 new { action = "Index", id = UrlParameter.Optional }, 
                 new { channel = "[\\d]+" }
                 );

            base.RegisterArea(context);

            //// 为所有频道注册路由
            //foreach (var chn in Channel.FindAllWithCache())
            //{
            //    var name = AreaName + "_" + chn.ID;
            //    context.MapRoute(
            //        name,
            //        name + "/{controller}/{action}/{id}",
            //        new { action = "Index", id = UrlParameter.Optional }
            //    );
            //}
        }
    }
}
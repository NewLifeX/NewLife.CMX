using System.ComponentModel;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NewLife.Cube;

namespace NewLife.CMX.Web
{
    [DisplayName("内容管理")]
    public class CMSAreaRegistration : AreaRegistrationBase
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            // 用于左边菜单的两条路由
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

            var routes = context.Routes;
            routes.MapRoute(
                name: "CMX_Channel",
                url: "{channelName}.html",
                defaults: new { controller = "Content", action = "Index" },
                constraints: new { channelName = new ChannelUrlConstraint() }
            );

            routes.MapRoute(
                name: "CMX_Category",
                url: "{channelName}/{categoryid}.html",
                defaults: new { controller = "Content", action = "List" },
                constraints: new { channelName = new ChannelUrlConstraint(), categoryid = "[\\d]+" }
            );

            routes.MapRoute(
                name: "CMX_Category_Page",
                url: "{channelName}/{categoryid}-{pageIndex}.html",
                defaults: new { controller = "Content", action = "List", pageIndex = UrlParameter.Optional },
                constraints: new { channelName = new ChannelUrlConstraint(), categoryid = "[\\d]+", pageIndex = "[\\d]+" }
            );

            routes.MapRoute(
                name: "CMX_Title",
                url: "{channelName}/show-{id}.html",
                defaults: new { controller = "Content", action = "Detail" },
                constraints: new { channelName = new ChannelUrlConstraint(), id = "[\\d]+" }
            );

            routes.MapRoute(
                name: "CMX_Search",
                url: "{channelName}/Search.html",
                defaults: new { controller = "Content", action = "Search" },
                constraints: new { channelName = new ChannelUrlConstraint() }
            );
        }
    }

    public class ChannelUrlConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values[parameterName] != null)
            {
                var channelName = values[parameterName].ToString();
                return Channel.FindByName(channelName) != null;
            }
            return false;
        }
    }
}
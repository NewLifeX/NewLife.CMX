using System;
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
               "CMS_Manage",
               "CMS/{controller}/{action}/{id}",
               new { action = "Index", id = UrlParameter.Optional },
               null
               );

            base.RegisterArea(context);

            var routes = context.Routes;
            routes.MapRoute(
                name: "CMX_Model",
                url: "{modelName}",
                defaults: new { controller = "Content", action = "Index" },
                constraints: new { modelName = new ModelUrlConstraint() }
            );

            routes.MapRoute(
                name: "CMX_Category",
                url: "cat-{categoryid}",
                defaults: new { controller = "Content", action = "List" },
                constraints: new { categoryid = "[\\d]+" }
            );

            routes.MapRoute(
                name: "CMX_Category_Page",
                url: "cat-{categoryid}-{pageIndex}",
                defaults: new { controller = "Content", action = "List", pageIndex = UrlParameter.Optional },
                constraints: new { categoryid = "[\\d]+", pageIndex = "[\\d]+" }
            );

            routes.MapRoute(
                name: "CMX_Category2",
                url: "{categoryCode}",
                defaults: new { controller = "Content", action = "List2" },
                constraints: new { categoryCode = new CategoryUrlConstraint() }
            );

            routes.MapRoute(
                name: "CMX_Category_Page2",
                url: "{categoryCode}-{pageIndex}",
                defaults: new { controller = "Content", action = "List2", pageIndex = UrlParameter.Optional },
                constraints: new { categoryCode = new CategoryUrlConstraint(), pageIndex = "[\\d]+" }
            );

            routes.MapRoute(
                name: "CMX_Info",
                url: "info-{id}",
                defaults: new { controller = "Content", action = "Detail" },
                constraints: new { id = "[\\d]+" }
            );

            routes.MapRoute(
                name: "CMX_Info2",
                url: "{infoCode}",
                defaults: new { controller = "Content", action = "Detail2" },
                constraints: new { infoCode = new InfoUrlConstraint(), id = "[\\d]+" }
            );

            routes.MapRoute(
                name: "CMX_Search",
                url: "Search/{key}/{pageIndex}",
                defaults: new { controller = "Content", action = "Search" },
                constraints: null
            );

            routes.MapRoute(
                name: "CMX_Search2",
                url: "{modelName}/Search/{key}/{pageIndex}",
                defaults: new { controller = "Content", action = "Search" },
                constraints: new { modelName = new ModelUrlConstraint() }
            );

            routes.MapRoute(
                name: "CMX_Search3",
                url: "{categoryCode}/Search/{key}/{pageIndex}",
                defaults: new { controller = "Content", action = "Search" },
                constraints: new { categoryCode = new CategoryUrlConstraint() }
            );

            // 用于UE的处理器
            //   routes.Add(new Route("UEditor", new UEditor.RouteHandler()));
            //   context.Routes.IgnoreRoute("ueditor/{*relpath}");
            // UE这条路由太霸道，让它最后注册
            //TimerX.Delay(s => routes.Add(new Route("UEditor", new UEditor.RouteHandler())), 5000);
        }
    }

    class ModelUrlConstraint : IRouteConstraint
    {
        public Boolean Match(HttpContextBase httpContext, Route route, String parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var name = values[parameterName] + "";
            if (name.IsNullOrEmpty()) return false;

            if (Model.FindByName(name) != null) return true;

            return false;
        }
    }

    class CategoryUrlConstraint : IRouteConstraint
    {
        public Boolean Match(HttpContextBase httpContext, Route route, String parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var name = values[parameterName] + "";
            if (name.IsNullOrEmpty()) return false;

            if (Category.FindByCode(name) != null) return true;

            return false;
        }
    }

    /// <summary>信息路径适配</summary>
    class InfoUrlConstraint : IRouteConstraint
    {
        public Boolean Match(HttpContextBase httpContext, Route route, String parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var name = values[parameterName] + "";
            if (name.IsNullOrEmpty()) return false;

            if (Info.FindByCode(name) != null) return true;

            return false;
        }
    }
}
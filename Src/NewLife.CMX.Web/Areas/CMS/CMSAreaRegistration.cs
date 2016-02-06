using System.ComponentModel;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NewLife.Cube;
using NewLife.Threading;

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
               "CMS/{controller}/{model}_{category}/{action}/{id}",
               new { action = "Index", id = UrlParameter.Optional },
               new { model = "[\\d]+", category = "[\\d]+" }
               );

            context.MapRoute(
                 "CMS_model",
                 "CMS/{controller}/{model}/{action}/{id}",
                 new { action = "Index", id = UrlParameter.Optional },
                 new { model = "[\\d]+" }
                 );

            base.RegisterArea(context);

            var routes = context.Routes;
            routes.MapRoute(
                name: "CMX_Model",
                url: "{modelName}.html",
                defaults: new { controller = "Content", action = "Index" },
                constraints: new { modelName = new ModelUrlConstraint() }
            );
            routes.MapRoute(
                name: "CMX_Model2",
                url: "{modelName}",
                defaults: new { controller = "Content", action = "Index" },
                constraints: new { modelName = new ModelUrlConstraint() }
            );

            routes.MapRoute(
                name: "CMX_Category",
                url: "{modelName}/{categoryid}.html",
                defaults: new { controller = "Content", action = "List" },
                constraints: new { modelName = new ModelUrlConstraint(), categoryid = "[\\d]+" }
            );

            routes.MapRoute(
                name: "CMX_Category_Page",
                url: "{modelName}/{categoryid}-{pageIndex}.html",
                defaults: new { controller = "Content", action = "List", pageIndex = UrlParameter.Optional },
                constraints: new { modelName = new ModelUrlConstraint(), categoryid = "[\\d]+", pageIndex = "[\\d]+" }
            );

            routes.MapRoute(
                name: "CMX_Title",
                url: "{modelName}/show-{id}.html",
                defaults: new { controller = "Content", action = "Detail" },
                constraints: new { modelName = new ModelUrlConstraint(), id = "[\\d]+" }
            );

            routes.MapRoute(
                name: "CMX_Search",
                url: "{modelName}/Search.html",
                defaults: new { controller = "Content", action = "Search" },
                constraints: new { modelName = new ModelUrlConstraint() }
            );

            routes.MapRoute(
                name: "CMX_Category2",
                url: "{categoryCode}",
                defaults: new { controller = "Content", action = "List2" },
                constraints: new { categoryCode = new CategoryUrlConstraint() }
            );

            routes.MapRoute(
                name: "CMX_Category_Page2",
                url: "{categoryCode}-{pageIndex}.html",
                defaults: new { controller = "Content", action = "List2", pageIndex = UrlParameter.Optional },
                constraints: new { categoryCode = new CategoryUrlConstraint(), categoryid = "[\\d]+", pageIndex = "[\\d]+" }
            );

            routes.MapRoute(
                name: "CMX_Title2",
                url: "{categoryCode}/show-{id}.html",
                defaults: new { controller = "Content", action = "Detail" },
                constraints: new { categoryCode = new CategoryUrlConstraint(), id = "[\\d]+" }
            );

            routes.MapRoute(
                name: "CMX_Search2",
                url: "{categoryCode}/Search.html",
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

    public class ModelUrlConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values[parameterName] != null)
            {
                var modelName = values[parameterName].ToString();
                if (modelName.IsNullOrEmpty()) return false;
                return Model.FindByName(modelName) != null;
            }
            return false;
        }
    }

    public class CategoryUrlConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values[parameterName] != null)
            {
                var catCode = values[parameterName].ToString();
                if (catCode.IsNullOrEmpty()) return false;

                // 从所有频道里面找到这个
                var cat = Model.FindCategoryByCode(catCode);
                if (cat != null) return true;
            }
            return false;
        }
    }
}
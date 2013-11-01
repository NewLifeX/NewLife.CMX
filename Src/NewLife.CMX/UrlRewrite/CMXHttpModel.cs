﻿using System;
using System.Web;
using NewLife.Log;
using NewLife.Reflection;
using XUrlRewrite.Configuration;
using System.Reflection;
using NewLife.CMX.Tool;
using NewLife.CMX.Config;
using System.IO;

namespace NewLife.CMX.UrlRewrite
{
    public class CMXHttpModel : IHttpModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(ReUrl_BeginRequest);

            context.Error += new EventHandler(Application_OnError);
        }

        private void Application_OnError(object sender, EventArgs e)
        {
            String CurrentUrl = HelperTool.GetReques();
            HttpApplication httpApplication = (HttpApplication)sender;

            httpApplication.Context.Server.ClearError();

            HttpContext.Current.Response.Redirect(CMXConfigBase.Current.CurrentParentPath.CombinePath("Index.aspx"));
        }

        private void ReUrl_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            Manager manager = Manager.GetConfigManager(app);
            UrlRewriteConfig cfg = manager.GetTemplateConfig();
            if (null != cfg && cfg.Enabled)
            {
                String path = app.Request.AppRelativeCurrentExecutionFilePath.Substring(1);
                String query = app.Request.QueryString.ToString();
                if (IsCustomFilterEnabled(manager, cfg, path, query, app))
                {
                    foreach (Object _url in cfg.Urls)
                    {
                        if (_url is UrlElement)
                        {
                            UrlElement url = (UrlElement)_url;
                            if (url.Enabled && url.RewriteUrl(path, query, app, cfg)) break;
                        }
                    }
                }
            }
        }

        static bool[] IsBindReload = { false };
        static Func<string, string, HttpApplication, bool>[] CustomFilterFunc = { null };
        static bool NeedLoadAssembly = true;

        /// <summary>
        /// 获取自定义过滤器是否通过
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="cfg"></param>
        /// <param name="path"></param>
        /// <param name="query"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        private bool IsCustomFilterEnabled(Manager manager, UrlRewriteConfig cfg, string path, string query, HttpApplication app)
        {
            if (!IsBindReload[0])
            {
                lock (IsBindReload)
                {
                    if (!IsBindReload[0])
                    {
                        manager.LoadConfig += delegate(object sender1, EventArgs e1) { CustomFilterFunc[0] = null; };
                        IsBindReload[0] = true;
                    }
                }
            }

            if (CustomFilterFunc[0] == null)
            {
                lock (CustomFilterFunc)
                {
                    if (CustomFilterFunc[0] == null)
                    {
                        CustomFilterFunc[0] = GetCustomFilterFunc(cfg.CustomFilter);
                    }
                }
            }

            if (CustomFilterFunc[0] == null) return true;
            try
            {
                return CustomFilterFunc[0](path, query, app);
            }
            catch (Exception ex)
            {
                if (Manager.Debug) XTrace.WriteLine("UrlRewrite执行cfg.CustomFilter时发生了异常\r\n{0}", ex);
                return false;
            }
        }

        private static Func<string, string, HttpApplication, bool> GetCustomFilterFunc(string method)
        {
            if (string.IsNullOrEmpty(method)) return EmptyCustomFilterFunc;

            int n = method.LastIndexOf('.');
            string typeName = method.Substring(0, n);
            method = method.Substring(n + 1);

            Type type = null;
            try
            {
                type = TypeX.GetType(typeName, NeedLoadAssembly);
                NeedLoadAssembly = false;
            }
            catch (Exception) { }
            if (type == null)
            {
                if (Manager.Debug) XTrace.WriteLine("Url重写配置的自定义过滤器类型{0}不存在,或不是有效的类", typeName);
                return EmptyCustomFilterFunc;
            }

            MethodInfoX methodInfoX = null;
            try
            {
                methodInfoX = MethodInfoX.Create(type, method, new Type[] { typeof(string), typeof(string), typeof(HttpApplication) });
            }
            catch (Exception) { }
            if (methodInfoX == null)
            {
                if (Manager.Debug) XTrace.WriteLine("Url重写配置的自定义过滤器方法static bool {0}(string, string, HttpApplication)不存在", method);
                return EmptyCustomFilterFunc;
            }

            if (!methodInfoX.Method.IsStatic)
            {
                if (Manager.Debug) XTrace.WriteLine("Url重写配置的自定义过滤器方法不是静态方法", method);
                return EmptyCustomFilterFunc;
            }

            if ((methodInfoX.Method as MethodInfo).ReturnType != typeof(bool))
            {
                if (Manager.Debug) XTrace.WriteLine("Url重写配置的自定义过滤器方法返回值不是bool", method);
                return EmptyCustomFilterFunc;
            }

            return delegate(string path, string query, HttpApplication app)
            {
                return (bool)methodInfoX.Invoke(null, path, query, app);
            };
        }

        private static bool EmptyCustomFilterFunc(string path, string query, HttpApplication app)
        {
            return true;
        }

        public void Dispose()
        {
            
        }
    }
}

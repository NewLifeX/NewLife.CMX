using System;
using System.Web;
using NewLife.Exceptions;
using NewLife.Reflection;
using NewLife.Web;

namespace NewLife.CMX.Templates
{
    /// <summary>Url处理模块</summary>
    public class UrlModule : UrlRewrite
    {
        protected override bool RewritePath(string url)
        {
            //return base.RewritePath(url);

            var name = url;
            var query = "";
            var p = url.IndexOf("?");
            if (p >= 0)
            {
                name = url.Substring(0, p);
                query = url.Substring(p + 1);
            }

            var type = name.GetTypeEx(true);
            if (type == null) throw new XException("无法找到名为{0}的处理器！", name);

            var handler = type.CreateInstance() as IHttpHandler;
            if (handler == null) throw new XException("非法处理器{0}", type.FullName);

            // 处理参数，通过反射设置到处理器里面
            if (!query.IsNullOrWhiteSpace())
            {
                var qs = query.SplitAsDictionary("=", "&");
                foreach (var item in qs)
                {
                    var pi = type.GetPropertyEx(item.Key);
                    if (pi != null && pi.CanWrite) handler.SetValue(pi, item.Value.ChangeType(pi.PropertyType));
                }
            }

            HttpContext.Current.RemapHandler(handler);

            return true;
        }
    }
}
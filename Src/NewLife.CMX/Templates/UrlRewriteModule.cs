using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using NewLife.Web;

namespace NewLife.CMX.Templates
{
    /// <summary>Url重新模块</summary>
    public class UrlRewriteModule : UrlRewrite
    {
        protected override bool RewritePath(string url)
        {
            //return base.RewritePath(url);

            var tmp = Path.GetFileName(url);

            var handler = new Handler();
            handler.TemplateName = tmp;

            HttpContext.Current.RemapHandler(handler);

            return true;
        }
    }
}
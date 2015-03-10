using System;
using System.IO;
using System.Web;
using NewLife.CMX.Config;

namespace NewLife.CMX.Templates
{
    /// <summary>资源处理器</summary>
    public class ResHandler : IHttpHandler
    {
        #region 属性
        private String _File;
        /// <summary>文件</summary>
        public String File { get { return _File; } set { _File = value; } }
        #endregion

        #region IHttpHandler
        /// <summary>是否重用。不可以</summary>
        public bool IsReusable { get { return false; } }

        /// <summary>处理请求</summary>
        /// <param name="context"></param>
        public virtual void ProcessRequest(HttpContext context)
        {
            if (Path.IsPathRooted(File)) throw new XException("致命错误！试图访问{0}！", File);

            var cfg = TemplateConfig.Current;
            var file = cfg.Root.CombinePath(cfg.Style, File);
            file = file.Replace("/", "\\");
            if (!Path.IsPathRooted(file)) file = file.GetFullPath();

            var request = context.Request;
            var response = context.Response;

            //为资源文件增加 浏览器缓存
            var since = request.ServerVariables["HTTP_IF_MODIFIED_SINCE"];
            var lastModified = new FileInfo(file).LastWriteTime;
            if (!String.IsNullOrEmpty(since))
            {
                DateTime dt;
                //if (DateTime.TryParse(since, out dt) && dt >= lastModified)
                //!!! 注意：本地修改时间精确到毫秒，而HTTP_IF_MODIFIED_SINCE只能到秒
                if (DateTime.TryParse(since, out dt) && (dt - lastModified).TotalSeconds > -1)
                {
                    context.Response.StatusCode = 304;
                    return;
                }
            }
            var ts = new TimeSpan(0, 10, 0);// 缓存10分钟
            response.ExpiresAbsolute = DateTime.Now.Add(ts);
            response.Cache.SetMaxAge(ts);
            response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            response.Cache.SetCacheability(HttpCacheability.Public);
            response.Cache.SetLastModified(lastModified);

            // 必须为资源文件指定内容类型，否则都变成text/html
            var ext = Path.GetExtension(file).TrimStart('.');

            switch (ext.ToLower())
            {
                case "css":
                    response.ContentType = "text/css";
                    break;
                case "js":
                    response.ContentType = "application/x-javascript";
                    break;
                case "jpg":
                case "gif":
                case "png":
                    response.ContentType = "image/" + ext;
                    break;
                default:
                    break;
            }

            response.TransmitFile(file);
        }
        #endregion
    }
}
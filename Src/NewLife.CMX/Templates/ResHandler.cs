using System;
using System.IO;
using System.Web;
using NewLife.CMX.Config;
using NewLife.Exceptions;

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

            context.Response.TransmitFile(file);
        }
        #endregion
    }
}
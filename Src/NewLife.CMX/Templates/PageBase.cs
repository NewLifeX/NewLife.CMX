using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Web.SessionState;
using NewLife.CMX.Config;
using NewLife.Exceptions;
using XTemplate.Templating;

namespace NewLife.CMX.Templates
{
    /// <summary>页面模版基类。所有页面模版生成类继承于此类</summary>
    public class PageBase : TemplateBase
    {
        #region 属性
        private String _TemplatePath;
        /// <summary>模版目录</summary>
        public String TemplatePath { get { return _TemplatePath; } set { _TemplatePath = value; } }

        private String _ResourcePath;
        /// <summary>资源路径</summary>
        public String ResourcePath
        {
            get
            {
                return _ResourcePath;
            }
        }

        //private Int32 _CategoryID;
        ///// <summary>属性说明</summary>
        //public Int32 CategoryID { get { return _CategoryID; } set { _CategoryID = value; } }

        //private String _ChannelName;
        ///// <summary>属性说明</summary>
        //public String ChannelName { get { return _ChannelName; } set { _ChannelName = value; } }

        private WebSettingConfig _WebSettingConfig = WebSettingConfig.Current;
        /// <summary>属性说明</summary>
        public WebSettingConfig WebSettingConfig { get { return _WebSettingConfig; } set { _WebSettingConfig = value; } }

        private TemplateConfig _Config = TemplateConfig.Current;
        /// <summary>属性说明</summary>
        public TemplateConfig Config { get { return _Config; } set { _Config = value; } }

        //private Dictionary<String, String> _ArgDic;
        ///// <summary>参数字典，只是为了编译通过</summary>
        //public Dictionary<String, String> ArgDic { get { return _ArgDic; } set { _ArgDic = value; } }

        //private List<IEntityTree> _ListCategory;
        ///// <summary>属性说明</summary>
        //public List<IEntityTree> ListCategory { get { return _ListCategory; } set { _ListCategory = value; } }

        //private String _ModelShortName;
        ///// <summary>属性说明</summary>
        //public String ModelShortName { get { return _ModelShortName; } set { _ModelShortName = value; } }

        //private String _Suffix;
        ///// <summary>属性说明</summary>
        //public String Suffix { get { return _Suffix; } set { _Suffix = value; } }

        private String _PageName;
        /// <summary>页面名称</summary>
        public String PageName { get { return _PageName ?? (_PageName = this.GetType().Name); } set { _PageName = value; } }
        #endregion

        #region 网页属性
        /// <summary>页面请求</summary>
        public HttpRequest Request { get { return HttpContext.Current.Request; } }

        /// <summary>网页响应</summary>
        public HttpResponse Response { get { return HttpContext.Current.Response; } }

        /// <summary>属性说明</summary>
        public HttpServerUtility Server { get { return HttpContext.Current.Server; } }

        /// <summary>属性说明</summary>
        public HttpSessionState Session { get { return HttpContext.Current.Session; } }
        #endregion

        #region 业务处理
        /// <summary>
        ///  临时采用的方案
        /// </summary>
        /// <param name="urlPath"></param>
        /// <returns></returns>
        public String GetTemplateContent(String urlPath)
        {
            var name = urlPath;
            var query = "";
            var p = urlPath.IndexOf("?");
            if (p >= 0)
            {
                name = urlPath.Substring(0, p);
                query = urlPath.Substring(p + 1);
            }

            Dictionary<String, Object> dic = new Dictionary<string, object>();

            if (!query.IsNullOrWhiteSpace())
            {
                var qs = query.SplitAsDictionary("=", "&");
                foreach (var item in qs)
                {
                    dic.Add(item.Key, item.Value);
                }
            }

            var html = Engine.Current.Process(name, dic);

            return html;
        }
        #endregion
    }
}
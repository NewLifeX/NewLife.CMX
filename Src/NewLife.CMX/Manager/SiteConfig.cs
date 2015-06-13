using System;
using System.ComponentModel;
using System.Web;
using NewLife.Xml;

namespace NewLife.CMX
{
    /// <summary>网站设置</summary>
    [DisplayName("站点配置")]
    [XmlConfigFile("Config/Site.config", 15000)]
    public class SiteConfig : XmlConfig<SiteConfig>
    {
        #region 属性
        private String _Name = "默认网站";
        /// <summary>网站名字</summary>
        [DisplayName("网站名字")]
        [Description("网站名字")]
        public String Name { get { return _Name; } set { _Name = value; } }

        private String _Company = "新生命开发团队";
        /// <summary>公司名称</summary>
        [DisplayName("公司名称")]
        [Description("公司名称")]
        public String Company { get { return _Company; } set { _Company = value; } }

        private String _Url = "";
        /// <summary>网站域名</summary>
        [DisplayName("网站域名")]
        [Description("以“http://”开头")]
        public String Url { get { return _Url; } set { _Url = value; } }

        private String _Tel = "";
        /// <summary>联系电话</summary>
        [DisplayName("联系电话")]
        [Description("联系电话")]
        public String Tel { get { return _Tel; } set { _Tel = value; } }

        private String _Fax = "";
        /// <summary>传真号码</summary>
        [DisplayName("传真号码")]
        [Description("传真号码")]
        public String Fax { get { return _Fax; } set { _Fax = value; } }

        private String _Mail = "";
        /// <summary>管理员邮箱</summary>
        [DisplayName("管理员邮箱")]
        [Description("管理员邮箱")]
        public String Mail { get { return _Mail; } set { _Mail = value; } }

        private String _Crod = "";
        /// <summary>网站备案号</summary>
        [DisplayName("网站备案号")]
        [Description("网站备案号")]
        public String Crod { get { return _Crod; } set { _Crod = value; } }

        private String _Title = "CMX内容管理系统";
        /// <summary>首页标题(SEO)</summary>
        [DisplayName("首页标题(SEO)")]
        [Description("自定义的首页标题")]
        public String Title { get { return _Title; } set { _Title = value; } }

        private String _KeyWord = "CMX,新生命开发团队";
        /// <summary>页面关健词(SEO)</summary>
        [DisplayName("页面关健词(SEO)")]
        [Description("页面关键词(keyword)")]
        public String KeyWord { get { return _KeyWord; } set { _KeyWord = value; } }

        private String _Description = "";
        /// <summary>页面描述</summary>
        [DisplayName("页面描述(SEO)")]
        [Description("页面描述(description)")]
        public String Description { get { return _Description; } set { _Description = value; } }

        private String _CopyRight = "版权所有 新生命开发团队";
        /// <summary>网站版权信息</summary>
        [DisplayName("版权信息")]
        [Description("支持HTML格式")]
        public String CopyRight { get { return _CopyRight; } set { _CopyRight = value; } }
        #endregion

        #region 构造
        /// <summary>实例化</summary>
        public SiteConfig()
        {
            var context = HttpContext.Current;
            if (context != null)
            {
                var uri = context.Request.Url;
                var str = uri.ToString();

                // 如果域名后面有路径
                var p = str.IndexOf("/", "https://".Length);
                if (p > 0)
                {
                    // 截断
                    str = str.Substring(0, p);
                    str += HttpRuntime.AppDomainAppVirtualPath;
                }
                Url = str;
            }
        }
        #endregion
    }
}
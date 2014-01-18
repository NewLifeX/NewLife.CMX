using System;
using System.ComponentModel;
using System.Web;
using NewLife.Xml;

namespace NewLife.CMX
{
    /// <summary>系统设置</summary>
    [XmlConfigFile("Config/Sys.config", 15000)]
    public class SysConfig : XmlConfig<SysConfig>
    {
        #region 属性
        private String _Path;
        /// <summary>安装目录</summary>
        [DisplayName("安装目录")]
        [Description("根目录下，输入“/”；如：http://abc.com/web，输入“web/”")]
        public String Path { get { return _Path; } set { _Path = value; } }

        private String _ManagePath = "Admin";
        /// <summary>网站管理目录</summary>
        [DisplayName("网站管理目录")]
        [Description("默认是admin，如已经更改，请输入目录名")]
        public String ManagePath { get { return _ManagePath; } set { _ManagePath = value; } }

        private Int32 _StaticStatus;
        /// <summary>URL重写开关</summary>
        [DisplayName("URL重写开关")]
        [Description("URL重写开关")]
        public Int32 StaticStatus { get { return _StaticStatus; } set { _StaticStatus = value; } }

        private String _StaticExtension = "aspx";
        /// <summary>静态URL后缀</summary>
        [DisplayName("静态URL后缀")]
        [Description("扩展名，不包括“.”，如：aspx、html")]
        public String StaticExtension { get { return _StaticExtension; } set { _StaticExtension = value; } }

        private Int32 _MemberStatus;
        /// <summary>开启会员功能</summary>
        [DisplayName("开启会员功能")]
        [Description("开启会员功能")]
        public Int32 SiteMemberStatus { get { return _MemberStatus; } set { _MemberStatus = value; } }

        private Int32 _CommentStatus;
        /// <summary>开启评论审核</summary>
        [DisplayName("开启评论审核")]
        [Description("开启评论审核")]
        public Int32 CommentStatus { get { return _CommentStatus; } set { _CommentStatus = value; } }

        private Int32 _LogStatus;
        /// <summary>后台管理日志</summary>
        [DisplayName("后台管理日志")]
        [Description("后台管理日志")]
        public Int32 LogStatus { get { return _LogStatus; } set { _LogStatus = value; } }

        private Int32 _WebStatus;
        /// <summary>是否关闭网站</summary>
        [DisplayName("是否关闭网站")]
        [Description("是否关闭网站")]
        public Int32 WebStatus { get { return _WebStatus; } set { _WebStatus = value; } }

        private String _CloseReason = "";
        /// <summary>关闭原因描述</summary>
        [DisplayName("关闭原因描述")]
        [Description("支持HTML格式")]
        public String CloseReason { get { return _CloseReason; } set { _CloseReason = value; } }

        private String _CountCode = "";
        /// <summary>网站统计代码</summary>
        [DisplayName("网站统计代码")]
        [Description("支持HTML格式")]
        public String CountCode { get { return _CountCode; } set { _CountCode = value; } }
        #endregion

        #region 构造
        /// <summary>
        /// 实例化
        /// </summary>
        public SysConfig()
        {
            var context = HttpContext.Current;
            if (context != null)
            {
                Path = HttpRuntime.AppDomainAppVirtualPath;
            }
        }

        //static SysConfig() { }
        #endregion
    }
}
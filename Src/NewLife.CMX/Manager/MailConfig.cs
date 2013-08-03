using System;
using System.ComponentModel;
using NewLife.Xml;

namespace NewLife.CMX
{
    /// <summary>邮件设置</summary>
    [XmlConfigFile("Config/Mail.config", 15000)]
    public class MailConfig : XmlConfig<MailConfig>
    {
        private String _Stmp = "";
        /// <summary>STMP服务器</summary>
        [DisplayName("STMP服务器")]
        [Description("发送邮件的SMTP服务器地址")]
        public String Stmp { get { return _Stmp; } set { _Stmp = value; } }

        private Int32 _Port = 25;
        /// <summary>SMTP端口</summary>
        [DisplayName("SMTP端口")]
        [Description("SMTP服务器的端口")]
        public Int32 Port { get { return _Port; } set { _Port = value; } }

        private String _From = "";
        /// <summary>发件人地址</summary>
        [DisplayName("发件人地址")]
        [Description("发件人地址")]
        public String From { get { return _From; } set { _From = value; } }

        private String _Username = "";
        /// <summary>邮箱账号</summary>
        [DisplayName("邮箱账号")]
        [Description("邮箱账号")]
        public String Username { get { return _Username; } set { _Username = value; } }

        private String _Password = "";
        /// <summary>邮箱密码</summary>
        [DisplayName("邮箱密码")]
        [Description("邮箱密码")]
        public String Password { get { return _Password; } set { _Password = value; } }

        private String _Nickname = "";
        /// <summary>发件人昵称</summary>
        [DisplayName("发件人昵称")]
        [Description("显示发件人的昵称")]
        public String Nickname { get { return _Nickname; } set { _Nickname = value; } }

        #region 构造
        ///// <summary>
        ///// 实例化
        ///// </summary>
        //public MailConfig()
        //{
        //    Stmp = "SiteEmailStmp";
        //    Port = 0;
        //    From = "SiteEmailFrom";
        //    Username = "SiteUsername";
        //    Password = "SiteEmailPassword";
        //    Nickname = "SiteEmailNickname";
        //}

        //static MailConfig() { }
        #endregion
    }
}
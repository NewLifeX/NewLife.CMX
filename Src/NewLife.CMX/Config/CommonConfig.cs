using System;
using System.ComponentModel;
using NewLife.Xml;

namespace NewLife.CMX.Config
{
    /// <summary>通用设置</summary>
    [XmlConfigFile("config/Common.config", 15000)]
    [Description("通用设置")]
    [Serializable]
    public class CommonConfig : XmlConfig<CommonConfig>
    {
        private String _Copyright;
        /// <summary>版权所有</summary>
        [Description("版权所有")]
        public String Copyright { get { return _Copyright; } set { _Copyright = value; } }

        private String _ICP;
        /// <summary>ICP备案</summary>
        [Description("ICP备案")]
        public String ICP { get { return _ICP; } set { _ICP = value; } }

        private String _ContactTel;
        /// <summary>联系电话</summary>
        [Description("联系电话")]
        public String ContactTel { get { return _ContactTel; } set { _ContactTel = value; } }

        private String _Address;
        /// <summary>地址</summary>
        [Description("地址")]
        public String Address { get { return _Address; } set { _Address = value; } }

        private String _Support;
        /// <summary>技术支持</summary>
        [Description("技术支持")]
        public String Support { get { return _Support; } set { _Support = value; } }

        private String _Index;
        /// <summary>首页</summary>
        [Description("首页")]
        public String Index { get { return _Index; } set { _Index = value; } }

        private String _ContactUs;
        /// <summary>联系我们</summary>
        [Description("联系我们")]
        public String ContactUs { get { return _ContactUs; } set { _ContactUs = value; } }

        private String _EnterpriseOnline;
        /// <summary>企业在线</summary>
        [Description("企业在线")]
        public String EnterpriseOnline { get { return _EnterpriseOnline; } set { _EnterpriseOnline = value; } }
    }
}
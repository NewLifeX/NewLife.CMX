using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using NewLife.Xml;

namespace NewLife.CMX.Config
{
    [XmlConfigFile("config/WebSettingConfig.config", 15000)]
    /// <summary>网站配置</summary>
    [Description("网站配置")]
    [Serializable]
    public class WebSettingConfig : CMXmlConfig<WebSettingConfig>
    {
        private String _Index = CMXConfigBase.Current.CurrentRootPath + "/Index.html";
        /// <summary>首页地址</summary>
        public String Index { get { return _Index; } set { _Index = value; } }

        private String _Contact = "#";
        /// <summary>联系我们</summary>
        public String Contact { get { return _Contact; } set { _Contact = value; } }

        private String _Forum = "#";
        /// <summary>论坛地址</summary>
        public String Forum { get { return _Forum; } set { _Forum = value; } }

        private String _Shop = "#";
        /// <summary>商城地址</summary>
        public String Shop { get { return _Shop; } set { _Shop = value; } }

        private String _Copyright = "版权所有 © 2008-2012 东莞市月无声电子设备有限公司";
        /// <summary>版权</summary>
        public String Copyright { get { return _Copyright; } set { _Copyright = value; } }

        private String _Address = "东莞市高埗镇冼沙村广场北路宝源工业区";
        /// <summary>公司地址</summary>
        public String Address { get { return _Address; } set { _Address = value; } }

        private String _Tel = "0769-23107897 0769-23107080 ";
        /// <summary>联系电话</summary>
        public String Tel { get { return _Tel; } set { _Tel = value; } }

        private String _ICP = "粤ICP备09218017号";
        /// <summary>备案信息</summary>
        public String ICP { get { return _ICP; } set { _ICP = value; } }

        private String _Title = "月无声实业";
        /// <summary>标题</summary>
        public String Title { get { return _Title; } set { _Title = value; } }

        private String _Keywords = "灌胶机,混合管,AB胶枪,点胶针筒,点胶针头";
        /// <summary>关键字</summary>
        public String Keywords { get { return _Keywords; } set { _Keywords = value; } }

        private String _Description = "月无声电子设备有限公司坐落在交通方便的东莞市区,毗邻深圳、广州、佛山、惠州等城市。其主要产品有：双液灌胶机，精准双液点胶机，混合管，点胶机,AB胶枪，点胶针头，AB胶筒，点胶针筒，点胶针头，不锈钢压力桶。月无声的点胶设备适用于：环氧树脂，PU，双组份硅胶和其他双组份流体材料。";
        /// <summary>描述</summary>
        public String Description { get { return _Description; } set { _Description = value; } }
    }
}

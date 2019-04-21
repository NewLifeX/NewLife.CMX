﻿using System;
using System.ComponentModel;
using NewLife.Xml;

namespace NewLife.CMX.Config
{
    /// <summary>网站配置</summary>
    [XmlConfigFile("Config/WebSetting.config", 15000)]
    [Description("网站配置")]
    [Serializable]
    public class WebSettingConfig : XmlConfig<WebSettingConfig>
    {
        private String _Index = "Index.html";
        /// <summary>首页地址</summary>
        [Description("首页地址")]
        public String Index { get { return _Index; } set { _Index = value; } }

        private String _Contact = "#";
        /// <summary>联系我们</summary>
        [Description("联系我们")]
        public String Contact { get { return _Contact; } set { _Contact = value; } }

        private String _Forum = "#";
        /// <summary>论坛地址</summary>
        [Description("论坛地址")]
        public String Forum { get { return _Forum; } set { _Forum = value; } }

        private String _Shop = "#";
        /// <summary>商城地址</summary>
        [Description("商城地址")]
        public String Shop { get { return _Shop; } set { _Shop = value; } }

        private String _Copyright = "版权所有 © 2008-2012 东莞市月无声电子设备有限公司";
        /// <summary>版权</summary>
        [Description("版权")]
        public String Copyright { get { return _Copyright; } set { _Copyright = value; } }

        private String _Address = "东莞市高埗镇冼沙村广场北路宝源工业区";
        /// <summary>公司地址</summary>
        [Description("公司地址")]
        public String Address { get { return _Address; } set { _Address = value; } }

        private String _Tel = "0769-23107897 0769-23107080 ";
        /// <summary>联系电话</summary>
        [Description("联系电话")]
        public String Tel { get { return _Tel; } set { _Tel = value; } }

        private String _ICP = "粤ICP备09218017号";
        /// <summary>备案信息</summary>
        [Description("备案信息")]
        public String ICP { get { return _ICP; } set { _ICP = value; } }

        private String _Title = "月无声电子设备有限公司";
        /// <summary>标题</summary>
        [Description("标题")]
        public String Title { get { return _Title; } set { _Title = value; } }

        private String _Keywords = "灌胶机,混合管,AB胶枪,点胶针筒,点胶针头";
        /// <summary>关键字</summary>
        [Description("关键字")]
        public String Keywords { get { return _Keywords; } set { _Keywords = value; } }

        private String _Description = "月无声电子设备有限公司坐落在交通方便的东莞市区,毗邻深圳、广州、佛山、惠州等城市。其主要产品有：双液灌胶机，精准双液点胶机，混合管，点胶机,AB胶枪，点胶针头，AB胶筒，点胶针筒，点胶针头，不锈钢压力桶。月无声的点胶设备适用于：环氧树脂，PU，双组份硅胶和其他双组份流体材料。";
        /// <summary>描述</summary>
        [Description("描述")]
        public String Description { get { return _Description; } set { _Description = value; } }
    }
}
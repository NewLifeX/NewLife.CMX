using NewLife.Xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace UEditor
{

    /// <summary>七牛网盘</summary>
    public class QiniuConfig : QiniuConfig<QiniuConfig> { }

    /// <summary>七牛网盘</summary>
    /// <typeparam name="TSetting"></typeparam>
    [XmlConfigFile("Config/QiniuConfig.config", 15000)]
    public class QiniuConfig<TSetting> : XmlConfig<TSetting> where TSetting : QiniuConfig<TSetting>, new()
    {

        private bool _IsEnable = true;

        /// <summary>是否启用</summary>
        [DisplayName("是否启用")]
        [Description("是否启用云存储")]
        public bool IsEnable { get { return _IsEnable; } set { _IsEnable = value; } }

        private string _ACCESS_KEY = "rrF_ejjCtlOGlmkf3Fs3fsYme-RnFnnVF6oUVyFb";

        /// <summary>ACCESS_KEY</summary>
        [DisplayName("ACCESS_KEY")]
        [Description("ACCESS_KEY")]
        public string ACCESS_KEY { get { return _ACCESS_KEY; } set { _ACCESS_KEY = value; } }

        private string _SECRET_KEY = "W_wmr0Kk8oYH9txmYzOjoBRhpnBD6kwH62YpKaeZ";

        /// <summary>SECRET_KEY</summary>
        [DisplayName("SECRET_KEY")]
        [Description("SECRET_KEY")]
        public string SECRET_KEY { get { return _SECRET_KEY; } set { _SECRET_KEY = value; } }

        private string _bucketName = "zcfileserver";

        /// <summary>BucketName</summary>
        [DisplayName("BucketName")]
        [Description("BucketName")]
        public string BucketName { get { return _bucketName; } set { _bucketName = value; } }

        private string _domain = "7xioaj.com1.z0.glb.clouddn.com";

        /// <summary>Domain</summary>
        [DisplayName("Domain")]
        [Description("Domain")]
        public string Domain { get { return _domain; } set { _domain = value; } }

        public static void QiuniuInit()
        {
            Qiniu.Conf.Config.ACCESS_KEY = Current.ACCESS_KEY;
            Qiniu.Conf.Config.SECRET_KEY = Current.SECRET_KEY;
            Qiniu.Conf.Config.Init();
        }
    }
}

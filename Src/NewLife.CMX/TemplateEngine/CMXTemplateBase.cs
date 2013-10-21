using System;
using System.Collections.Generic;
using System.Text;
using NewLife.CMX.Config;
using XTemplate.Templating;

namespace NewLife.CMX.TemplateEngine
{
    public class CMXTemplateBase : TemplateBase
    {
        #region 属性
        private TemplateConfig _Config;
        /// <summary>模板配置信息</summary>
        public TemplateConfig Config { get { return _Config; } set { _Config = value; } }

        private Dictionary<String, Object> _ArgDic;
        /// <summary>参数字典</summary>
        public Dictionary<String, Object> ArgDic { get { return _ArgDic; } set { _ArgDic = value; } }

        private CMXTemplateInfo _CMXTemplate;
        /// <summary>mo</summary>
        public CMXTemplateInfo CMXTemplate { get { return _CMXTemplate; } set { _CMXTemplate = value; } }
        #endregion

        #region 初始化
        public override void Initialize()
        {
            base.Initialize();

            if (Data.ContainsKey("Config")) Config = (TemplateConfig)Data["Config"];
            if (Data.ContainsKey("ArgDic")) ArgDic = (Dictionary<String, Object>)Data["ArgDic"];
            if (Data.ContainsKey("CMXTemplateInfo")) CMXTemplate = (CMXTemplateInfo)Data["CMXTemplate"];
        }
        #endregion
    }

    public class CMXTemplateInfo
    {
        private String _Name;

        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private String _Content;

        public String Content
        {
            get { return _Content; }
            set { _Content = value; }
        }
    }
}



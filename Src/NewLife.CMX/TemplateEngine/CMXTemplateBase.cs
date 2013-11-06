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

        //private CMXTemplateInfo _TemplateInfo;
        ///// <summary>模板信息</summary>
        //public CMXTemplateInfo TemplateInfo { get { return _TemplateInfo; } set { _TemplateInfo = value; } }

        //private List<CMXTemplateInfo> _Templates;
        ///// <summary>模板信息</summary>
        //public List<CMXTemplateInfo> Templates { get { return _Templates; } set { _Templates = value; } }
        #endregion

        #region 初始化
        public override void Initialize()
        {
            base.Initialize();

            if (Data.ContainsKey("Config")) Config = (TemplateConfig)Data["Config"];
            if (Data.ContainsKey("ArgDic")) ArgDic = (Dictionary<String, Object>)Data["ArgDic"];
            //if (Data.ContainsKey("TemplateInfo")) TemplateInfo = (CMXTemplateInfo)Data["TemplateInfo "];
        }
        #endregion
    }

    //public class CMXTemplateInfo
    //{
    //    private String _Name;
    //    /// <summary>模板名称</summary>
    //    public String Name { get { return _Name; } set { _Name = value; } }

    //    private String _Content;
    //    /// <summary>模板内容</summary>
    //    public String Content { get { return _Content; } set { _Content = value; } }
    //}
}

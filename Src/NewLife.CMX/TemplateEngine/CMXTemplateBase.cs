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

        //private List<ModelDataBase> _ModelDB;
        ///// <summary>模板数据</summary>
        //public List<ModelDataBase> ModelDB { get { return _ModelDB; } set { _ModelDB = value; } }

        #endregion

        #region 初始化
        public override void Initialize()
        {
            base.Initialize();

            if (Data.ContainsKey("Config")) Config = (TemplateConfig)Data["Config"];
            if (Data.ContainsKey("ArgDic")) ArgDic = (Dictionary<String, Object>)Data["ArgDic"];
            //if (Data.ContainsKey("ModelDB")) ModelDB = (List<ModelDataBase>)Data["ModelDB"];
            //if (Data.ContainsKey("TemplateInfo")) TemplateInfo = (CMXTemplateInfo)Data["TemplateInfo "];
        }
        #endregion
    }
}

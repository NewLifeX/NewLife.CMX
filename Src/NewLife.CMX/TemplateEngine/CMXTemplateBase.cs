using System;
using System.Collections.Generic;
using System.Text;
using NewLife.CMX.Config;
using XCode;
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

        private List<IEntity> _ListData;
        /// <summary>数据列表</summary>
        public List<IEntity> ListData { get { return _ListData; } set { _ListData = value; } }

        private List<IEntityTree> _ListCategory;
        /// <summary>分类列表</summary>
        public List<IEntityTree> ListCategory { get { return _ListCategory; } set { _ListCategory = value; } }

        #endregion

        #region 初始化
        public override void Initialize()
        {
            base.Initialize();
            
            if (Data.ContainsKey("Config")) Config = (TemplateConfig)Data["Config"];
            if (Data.ContainsKey("ArgDic")) ArgDic = (Dictionary<String, Object>)Data["ArgDic"];
            if (Data.ContainsKey("ListData")) ListData = (List<IEntity>)Data["ListData"];
            if (Data.ContainsKey("ListCategory")) ListCategory = (List<IEntityTree>)Data["ListCategory"];
        }
        #endregion
    }
}

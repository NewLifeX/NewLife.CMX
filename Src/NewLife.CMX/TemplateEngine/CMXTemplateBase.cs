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

        private Dictionary<String, String> _ArgDic;
        /// <summary>参数字典</summary>
        public Dictionary<String, String> ArgDic { get { return _ArgDic; } set { _ArgDic = value; } }

        private IEntityList _ListEntity;
        /// <summary>数据列表</summary>
        public IEntityList ListEntity { get { return _ListEntity; } set { _ListEntity = value; } }

        private IEntityList _ListCategory;
        /// <summary>分类列表</summary>
        public IEntityList ListCategory { get { return _ListCategory; } set { _ListCategory = value; } }

        private IEntity _Entity;
        /// <summary>实体数据</summary>
        public IEntity Entity { get { return _Entity; } set { _Entity = value; } }
        #endregion

        #region 初始化
        public override void Initialize()
        {
            base.Initialize();

            if (Data.ContainsKey("Config")) Config = (TemplateConfig)Data["Config"];
            if (Data.ContainsKey("ArgDic")) ArgDic = (Dictionary<String, String>)Data["ArgDic"];
            if (Data.ContainsKey("ListEntity")) ListEntity = (IEntityList)Data["ListEntity"];
            if (Data.ContainsKey("ListCategory")) ListCategory = (IEntityList)Data["ListCategory"];
            if (Data.ContainsKey("Entity")) Entity = (IEntity)Data["Entity"];
        }
        #endregion
    }
}

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

        private WebSettingConfig _WebSettingConfig;
        /// <summary>网站配置信息</summary>
        public WebSettingConfig WebSettingConfig { get { return _WebSettingConfig; } set { _WebSettingConfig = value; } }

        private String _LeftMenu;
        /// <summary>左侧导航菜单</summary>
        public String LeftMenu { get { return _LeftMenu; } set { _LeftMenu = value; } }

        private String _Header;
        /// <summary>页头</summary>
        public String Header { get { return _Header; } set { _Header = value; } }

        private String _Foot;
        /// <summary>页脚</summary>
        public String Foot { get { return _Foot; } set { _Foot = value; } }

        private Dictionary<String, String> _ArgDic;
        /// <summary>参数字典</summary>
        public Dictionary<String, String> ArgDic { get { return _ArgDic; } set { _ArgDic = value; } }

        private IEntityList _ListEntity;
        /// <summary>数据列表</summary>
        public IEntityList ListEntity { get { return _ListEntity; } set { _ListEntity = value; } }

        private List<IEntityTree> _ListCategory;
        /// <summary>分类列表</summary>
        public List<IEntityTree> ListCategory { get { return _ListCategory; } set { _ListCategory = value; } }

        private IEntity _Entity;
        /// <summary>实体数据</summary>
        public IEntity Entity { get { return _Entity; } set { _Entity = value; } }

        private String _Suffix;
        /// <summary>扩展名称</summary>
        public String Suffix { get { return _Suffix; } set { _Suffix = value; } }
        #endregion

        #region 初始化
        public override void Initialize()
        {
            base.Initialize();

            if (Data.ContainsKey("Config")) Config = (TemplateConfig)Data["Config"];
            if (Data.ContainsKey("WebSettingConfig")) WebSettingConfig = (WebSettingConfig)Data["WebSettingConfig"];
            if (Data.ContainsKey("Header")) Header = (String)Data["Header"];
            if (Data.ContainsKey("Foot")) Foot = (String)Data["Foot"];
            if (Data.ContainsKey("LeftMenu")) LeftMenu = (String)Data["LeftMenu"];
            if (Data.ContainsKey("ArgDic")) ArgDic = (Dictionary<String, String>)Data["ArgDic"];
            if (Data.ContainsKey("ListEntity")) ListEntity = (IEntityList)Data["ListEntity"];
            if (Data.ContainsKey("ListCategory")) ListCategory = (List<IEntityTree>)Data["ListCategory"];
            if (Data.ContainsKey("Entity")) Entity = (IEntity)Data["Entity"];
            if (Data.ContainsKey("Suffix")) Suffix = (String)Data["Suffix"];
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using NewLife.CMX.Config;
using XTemplate.Templating;

namespace NewLife.CMX.TemplateEngine
{
    public class CMXEngine
    {
        #region 属性
        private TemplateConfig _Config;
        /// <summary>配置</summary>
        public TemplateConfig Config { get { return _Config; } set { _Config = value; } }

        private Dictionary<String, Object> _ArgDic;
        /// <summary>参数字典</summary>
        public Dictionary<String, Object> ArgDic { get { return _ArgDic; } set { _ArgDic = value; } }

        private CMXTemplateInfo _CMXTemplate;
        /// <summary>模型信息</summary>
        public CMXTemplateInfo CMXTemplate { get { return _CMXTemplate; } set { _CMXTemplate = value; } }

        #endregion

        #region 构造
        /// <summary>静态构造</summary>
        static CMXEngine()
        {
            Template.BaseClassName = typeof(CMXTemplateBase).FullName;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="config"></param>
        public CMXEngine(TemplateConfig config)
        {
            Config = config;
        }
        #endregion

        #region 生成
        public String[] Render(String TemplateName)
        {

            return null;
        }

        public String[] RenderAll()
        {

            return null;
        }
        #endregion
    }
}

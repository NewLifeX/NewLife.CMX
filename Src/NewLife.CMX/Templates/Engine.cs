using System;
using System.Collections.Generic;
using System.IO;
using NewLife.CMX.Config;
using XTemplate.Templating;

namespace NewLife.CMX.Templates
{
    /// <summary>模版引擎</summary>
    public class Engine
    {
        #region 属性
        private static Engine _Current;
        /// <summary>当前引擎</summary>
        public static Engine Current
        {
            get
            {
                // 当前使用的模版只有一个，更换模版配置后，需要重新编译模版
                // 在多模版同时使用的项目可以考虑扩展
                if (_Current == null || _Current.Style != TemplateConfig.Current.Style)
                {
                    lock (typeof(Engine))
                    {
                        if (_Current == null || _Current.Style != TemplateConfig.Current.Style)
                        {
                            var eng = new Engine();
                            eng.Init();
                            _Current = eng;
                        }
                    }
                }

                return _Current;
            }
        }

        private String _Style;
        /// <summary>模版样式</summary>
        public String Style { get { return _Style; } set { _Style = value; } }

        private Template _Temp;
        /// <summary>模版</summary>
        public Template Temp { get { return _Temp; } set { _Temp = value; } }
        #endregion

        #region 方法
        void Init()
        {
            Style = TemplateConfig.Current.Style;

            Compile();
        }

        /// <summary>编译所有模版</summary>
        public void Compile()
        {
            var cfg = TemplateConfig.Current;
            var dir = cfg.Root.CombinePath(cfg.Style).GetFullPath();
            var fs = Directory.GetFiles(dir, "*.*", SearchOption.TopDirectoryOnly);

            Template.Debug = cfg.Debug;
            var tmp = new Template();
            // 添加所有模版页面到引擎
            foreach (var item in fs)
            {
                tmp.AddTemplateItem(Path.GetFileName(item), File.ReadAllText(item));
            }
            // 设定模版基类
            foreach (var ti in tmp.Templates)
            {
                if (ti.BaseClassName.IsNullOrWhiteSpace()) ti.BaseClassName = typeof(PageBase).FullName;
            }
            // 写入当前程序集所有命名空间，方便模版直接使用当前程序集的类
            var hs = new HashSet<String>();
            foreach (var item in this.GetType().Assembly.GetTypes())
            {
                if (!String.IsNullOrEmpty(item.Namespace) && !hs.Contains(item.Namespace)) hs.Add(item.Namespace);
            }
            foreach (var ti in tmp.Templates)
            {
                ti.Imports.AddRange(hs);
            }
            tmp.Compile();
            Temp = tmp;
        }

        public String Process(String className, IDictionary<String, Object> data)
        {
            if (Temp == null) throw new Exception("模版未准备就绪！");

            return Temp.Render(className, data);
        }
        #endregion
    }
}
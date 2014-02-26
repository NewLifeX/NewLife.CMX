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
                if (_Current == null)
                {
                    lock (typeof(Engine))
                    {
                        if (_Current == null)
                        {
                            var eng = new Engine();
                            eng.Compile();
                            _Current = eng;
                        }
                    }
                }

                return _Current;
            }
        }

        private Template _Temp;
        /// <summary>模版</summary>
        public Template Temp { get { return _Temp; } set { _Temp = value; } }
        #endregion

        #region 方法
        void Init()
        {
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
            foreach (var item in tmp.Templates)
            {
                if (item.BaseClassName.IsNullOrWhiteSpace()) item.BaseClassName = typeof(PageBase).FullName;
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
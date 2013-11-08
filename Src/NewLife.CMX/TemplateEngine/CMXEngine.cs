using System;
using System.Collections.Generic;
using System.Text;
using NewLife.CMX.Config;
using XTemplate.Templating;
using System.Linq;
using System.IO;
using System.Net;
using System.Web;

namespace NewLife.CMX.TemplateEngine
{
    public class CMXEngine
    {
        #region 属性
        private TemplateConfig _Config;
        /// <summary>模板配置</summary>
        public TemplateConfig Config { get { return _Config; } set { _Config = value; } }

        private Dictionary<String, Object> _ArgDic;
        /// <summary>参数字典</summary>
        public Dictionary<String, Object> ArgDic { get { return _ArgDic; } set { _ArgDic = value; } }

        private List<ModelDataBase> _ModelDB;
        /// <summary>模板数据</summary>
        public List<ModelDataBase> ModelDB { get { return _ModelDB; } set { _ModelDB = value; } }
        #endregion

        #region 构造
        static CMXEngine()
        {
            Template.BaseClassName = typeof(CMXTemplateBase).FullName;
        }

        public CMXEngine(TemplateConfig config)
        {
            Config = config;
        }
        #endregion

        #region 生成
        /// <summary>
        /// 生成模板
        /// </summary>
        /// <param name="TemplateName"></param>
        /// <returns></returns>
        public String Render(String TemplateName)
        {
            var data = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            data["Config"] = Config;
            data["ArgDic"] = ArgDic;
            //data["ModelDB"] = ModelDB;

            #region 获取模板资源文件
            Template.Debug = true;
            Dictionary<String, String> tempdic = new Dictionary<String, String>();

            String WebPath = HttpRuntime.AppDomainAppPath;
            String Templatepath = WebPath.CombinePath(Config.TemplateRootPath, Config.TemplateStyle);

            //校验模板目录
            if (!Directory.Exists(Templatepath)) throw new Exception("指定样式模板不存在！");
            //校验请求文件
            String RequestFile = Templatepath.CombinePath(TemplateName);
            FileInfo fi = new FileInfo(RequestFile);
            if (!fi.Exists) throw new Exception("请求地址不存在!");
            #endregion

            #region 过滤忽略文件
            //获取忽略文件类型数组
            String[] IgnoreExtentList = String.IsNullOrEmpty(Config.IgnoreExtendName) ? new string[] { } : Config.IgnoreExtendName.Split(',');
            //如果是忽略列表中的文件直接返回
            String TempContent = File.ReadAllText(RequestFile, Encoding.UTF8);

            if (IgnoreExtentList.Contains(fi.Extension.Substring(1))) return TempContent;

            tempdic.Add(TemplateName, TempContent);
            #endregion

            #region 生成文件

            List<String> imports = Config.ImportsAssembly.Split(",").ToList();
            //添加程序集引用
            Template.Imports.AddRange(imports);

            Template tt = Template.Create(tempdic);
            //编译文件
            tt.Compile();

            String ResultContent = tt.Render(TemplateName, data);

            if (Config.IsDebug)
            {
                String Outputpath = WebPath.CombinePath(Config.OutputPath, Config.TemplateStyle);
                Outputpath = Outputpath.CombinePath(TemplateName);
                File.WriteAllText(Outputpath, ResultContent, Encoding.UTF8);
            }

            return ResultContent;
            #endregion
        }
        #endregion
    }
}

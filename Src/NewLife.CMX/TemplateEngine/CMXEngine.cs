using System;
using System.Collections.Generic;
using System.Text;
using NewLife.CMX.Config;
using XTemplate.Templating;
using System.Linq;
using System.IO;

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

        private List<CMXTemplateInfo> _Templates;
        /// <summary>参数字典</summary>
        public List<CMXTemplateInfo> Templates { get { return _Templates; } set { _Templates = value; } }
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
        public String[] Render(String TemplateName)
        {
            var data = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            data["Config"] = Config;
            data["ArgDic"] = ArgDic;

            #region 获取模板资源文件
            Template.Debug = Config.IsDebug;
            //Dictionary<String, String> templates = new Dictionary<String, String>();
            Boolean IsCover = Config.IsCover;
            //String TemplatesRootPath = Config.TemplateRootPath;
            //String TemplateStyle = Config.TemplateStyle;
            //String TemplateOutputPath = Config.OutputPath;
            Dictionary<String, String> dic = new Dictionary<string, string>();

            String Templatepath = Config.TemplateRootPath.CombinePath(Config.TemplateStyle);
            String Outputpath = Config.OutputPath.CombinePath(Config.TemplateStyle);

            String[] ExtentList = Config.IgnoreExtendName.Split(',');

            //校验模板目录
            if (!Directory.Exists(Templatepath)) return null;
            //校验模板输出目录
            if (!Directory.Exists(Outputpath)) Directory.CreateDirectory(Outputpath);

            //List<String> TemplateFiles = new List<string>();
            List<String> IgnoreFile = new List<string>();

            List<String> FileList = Directory.GetFiles(Templatepath).ToList<String>();
            #endregion

            #region 分类过滤文件
            //将不需要编译的文件如css,js等文件过滤出来
            foreach (String file in FileList)
            {
                if (ExtentList.Contains((new FileInfo(file)).Extension.Substring(1)))
                {
                    IgnoreFile.Add(file);
                }
                else
                {
                    FileInfo fi = new FileInfo(file);
                    String content = fi.OpenText().ReadToEnd();

                    dic.Add(fi.Name, content);

                    //TemplateFiles.Add(file);
                }
            }
            #endregion

            #region 复制过滤文件
            foreach (String file in IgnoreFile)
            {
                FileInfo fi = new FileInfo(file);
                fi.CopyTo(Outputpath, IsCover);
            }
            #endregion

            #region 生成文件
            Template tt = Template.Create(dic);
            #endregion

            //#region 过滤文件
            ////将不需要编译的文件如css,js等文件过滤出来
            //foreach (String file in templatefiles)
            //{
            //    FileInfo fi = new FileInfo(file);
            //    if (ExtentList.Contains(fi.Extension.Substring(1))) continue;

            //    CMXTemplateInfo cmxt = new CMXTemplateInfo();
            //    cmxt.Name = fi.Name;
            //    cmxt.Content = fi.OpenText().ReadToEnd();
            //    Templates.Add(cmxt);
            //}
            //#endregion



            return null;
        }



        public String[] RenderAll()
        {

            return null;
        }
        #endregion

        #region 辅助方法

        #endregion
    }
}

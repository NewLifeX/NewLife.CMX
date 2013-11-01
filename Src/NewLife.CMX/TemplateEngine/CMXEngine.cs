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

        //private List<CMXTemplateInfo> _Templates;
        ///// <summary>参数字典</summary>
        //public List<CMXTemplateInfo> Templates { get { return _Templates; } set { _Templates = value; } }
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
        public String[] RenderAll()
        {
            var data = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            data["Config"] = Config;
            data["ArgDic"] = ArgDic;

            #region 获取模板资源文件
            Template.Debug = Config.IsDebug;
            //Dictionary<String, String> templates = new Dictionary<String, String>();
            Boolean IsCover = Config.IsCover;
            Dictionary<String, String> tempdic = new Dictionary<string, string>();

            String WebPath = HttpRuntime.AppDomainAppPath;
            String Templatepath = WebPath.CombinePath(Config.TemplateRootPath, Config.TemplateStyle);
            String Outputpath = WebPath.CombinePath(Config.OutputPath, Config.TemplateStyle);


            String[] ExtentList = String.IsNullOrEmpty(Config.IgnoreExtendName) ? new string[] { } : Config.IgnoreExtendName.Split(',');

            //校验模板目录
            if (!Directory.Exists(Templatepath)) throw new Exception("指定样式模板不存在！");
            //校验模板输出目录
            if (!Directory.Exists(Outputpath)) Directory.CreateDirectory(Outputpath);
            //忽略文件
            List<String> IgnoreFiles = new List<string>();
            //获取模板文件夹中的所有文件
            List<String> FileList = Directory.GetFiles(Templatepath).ToList<String>();
            List<String> ChildDirList = Directory.GetDirectories(Templatepath).ToList<String>();
            #endregion

            #region 分类过滤文件
            //将不需要编译的文件如css,js等文件过滤出来
            foreach (String file in FileList)
            {
                if (ExtentList.Contains((new FileInfo(file)).Extension.Substring(1)))
                {
                    IgnoreFiles.Add(file);
                }
                else
                {
                    FileInfo fi = new FileInfo(file);
                    String content = fi.OpenText().ReadToEnd();

                    tempdic.Add(fi.Name, content);
                }
            }
            #endregion

            #region 复制过滤文件以及文件夹
            foreach (String file in IgnoreFiles)
            {
                FileInfo fi = new FileInfo(file);
                fi.CopyTo(Outputpath.CombinePath(fi.Name), IsCover);

            }
            //将样式文件夹下的文件夹自动复制到生成的模板文件中
            //为了方便整理css、js文件
            foreach (String dir in ChildDirList)
            {
                DirectoryInfo di = new DirectoryInfo(dir);
                Directory.Move(dir, Outputpath.CombinePath(di.Name));
            }
            #endregion

            #region 生成文件
            Template tt = Template.Create(tempdic);
            //编译文件
            tt.Compile();

            var rs = new List<String>();
            foreach (TemplateItem item in tt.Templates)
            {
                if (item.Included) continue;

                String filename = Path.GetFileName(item.Name);

                filename = Outputpath.CombinePath(filename);

                if (!IsCover && File.Exists(filename)) continue;
                //生成最终输出内容
                String tempContent = tt.Render(item.Name, data);

                FileInfo fi = new FileInfo(filename);

                String outfilename = fi.FullName.Replace(fi.Extension, ".aspx");

                File.WriteAllText(outfilename, tempContent, Encoding.UTF8);

                rs.Add(tempContent);
            }
            return rs.ToArray();
            #endregion
        }

        public String[] Render(String TemplateName)
        {

            return null;
        }
        #endregion
    }
}

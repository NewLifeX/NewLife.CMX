using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using NewLife.Collections;
using NewLife.Serialization;
using UEditor;

namespace NewLife.CMX.Web.Controllers
{
    public class UEditorController : Controller
    {
        private static readonly String _config_str;
        private static readonly IDictionary<String, Object> _config;
        static UEditorController()
        {
            // 读取配置文件
            var fi = "config.json".AsFile();
            if (fi.Exists)
            {
                var str = fi.ReadText();
                if (!str.IsNullOrEmpty())
                {
                    _config_str = str;

                    // 干掉注释
                    var ss = str.Split("/*", "*/");
                    var sb = Pool.StringBuilder.Get();
                    for (var i = 1; i < ss.Length; i += 2)
                    {
                        sb.Append(ss[i]);
                    }
                    str = sb.Put(true);

                    _config = (new JsonParser(str).Decode() as IDictionary<String, Object>).ToNullable();
                }
            }
        }

        private Int32 GetInt(String key) => _config[key].ToInt();
        private String GetString(String key) => (String)_config[key];
        private String[] GetStringList(String key) => (String[])_config[key];

        // GET: UEditor
        public ActionResult Index(String action)
        {
            var context = System.Web.HttpContext.Current;
            var cfg = UEditor.Setting.Current;

            var act = (action + "").ToLower();
            switch (act)
            {
                case "config": return Config();
                case "uploadimage":
                    new UploadHandler(context, new UploadConfig()
                    {
                        AllowExtensions = GetStringList("imageAllowFiles"),
                        PathFormat = GetString("imagePathFormat"),
                        SizeLimit = GetInt("imageMaxSize"),
                        UploadFieldName = GetString("imageFieldName")
                    }).Process();
                    break;
                case "uploadscrawl":
                    new UploadHandler(context, new UploadConfig()
                    {
                        AllowExtensions = new String[] { ".png" },
                        PathFormat = GetString("scrawlPathFormat"),
                        SizeLimit = GetInt("scrawlMaxSize"),
                        UploadFieldName = GetString("scrawlFieldName"),
                        Base64 = true,
                        Base64Filename = "scrawl.png"
                    }).Process();
                    break;
                case "uploadvideo":
                    new UploadHandler(context, new UploadConfig()
                    {
                        AllowExtensions = GetStringList("videoAllowFiles"),
                        PathFormat = GetString("videoPathFormat"),
                        SizeLimit = GetInt("videoMaxSize"),
                        UploadFieldName = GetString("videoFieldName")
                    }).Process();
                    break;
                case "uploadfile":
                    new UploadHandler(context, new UploadConfig()
                    {
                        AllowExtensions = GetStringList("fileAllowFiles"),
                        PathFormat = GetString("filePathFormat"),
                        SizeLimit = GetInt("fileMaxSize"),
                        UploadFieldName = GetString("fileFieldName")
                    }).Process();
                    break;
                case "listimage":
                    new ListFileManager(context, GetString("imageManagerListPath"), GetStringList("imageManagerAllowFiles")) { ListSize= GetInt("imageManagerListSize") }.Process();
                    break;
                case "listfile":
                    new ListFileManager(context, GetString("fileManagerListPath"), GetStringList("fileManagerAllowFiles")) { ListSize = GetInt("fileManagerListSize") }.Process();
                    break;
                case "catchimage":
                    //return Config();
                    new CrawlerHandler(context) { PathFormat = GetString("catcherPathFormat") }.Process();
                    break;
            }

            return Config();
        }

        public ActionResult Config() => Json(_config, JsonRequestBehavior.AllowGet);
    }
}
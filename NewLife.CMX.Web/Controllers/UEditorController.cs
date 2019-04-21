using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using NewLife.CMX.Editor;
using NewLife.Collections;
using NewLife.Log;
using NewLife.Serialization;

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
        private String[] GetStringList(String key) => (_config[key] as IList<Object>)?.Cast<String>().ToArray();

        public ActionResult Index()
        {
            var act = (Request["action"] + "").ToLower();
            switch (act)
            {
                case "uploadimage": return UploadImage();
                case "uploadscrawl": return UploadScrawl();
                case "uploadvideo": return UploadVideo();
                case "uploadfile": return UploadFile();
                case "listimage": return ListImage();
                case "listfile": return ListFile();
                case "catchimage": return CatchImage();
                case "config":
                default:
                    return Config();
            }
        }

        public ActionResult Config() => Json(_config, JsonRequestBehavior.AllowGet);

        public ActionResult UploadImage()
        {
            var ur = new Uploader
            {
                File = Request.Files[GetString("imageFieldName")],
                AllowExtensions = GetStringList("imageAllowFiles"),
                PathFormat = GetString("imagePathFormat"),
                MaxSize = GetInt("imageMaxSize"),
            };
            var rs = ur.Process();
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadScrawl()
        {
            var ur = new Uploader
            {
                FileBytes = Request[GetString("scrawlFieldName")].ToBase64(),
                AllowExtensions = new String[] { ".png" },
                PathFormat = GetString("scrawlPathFormat"),
                MaxSize = GetInt("scrawlMaxSize"),
                //UploadFieldName = GetString("scrawlFieldName"),
                //Base64 = true,
                Base64Filename = "scrawl.png"
            };
            var rs = ur.Process();
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadVideo()
        {
            var ur = new Uploader
            {
                File = Request.Files[GetString("videoFieldName")],
                AllowExtensions = GetStringList("videoAllowFiles"),
                PathFormat = GetString("videoPathFormat"),
                MaxSize = GetInt("videoMaxSize"),
                //UploadFieldName = GetString("videoFieldName")
            };
            var rs = ur.Process();
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadFile()
        {
            var ur = new Uploader
            {
                File = Request.Files[GetString("fileFieldName")],
                AllowExtensions = GetStringList("fileAllowFiles"),
                PathFormat = GetString("filePathFormat"),
                MaxSize = GetInt("fileMaxSize"),
                //UploadFieldName = GetString("fileFieldName")
            };
            var rs = ur.Process();
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListImage()
        {
            var ff = new ListFile
            {
                PathToList = GetString("imageManagerListPath"),
                SearchExtensions = GetStringList("imageManagerAllowFiles"),
                ListSize = GetInt("imageManagerListSize"),

                Start = Request["start"].ToInt(),
                Size = Request["size"].ToInt(),
            };
            var rs = ff.Process();
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListFile()
        {
            var ff = new ListFile
            {
                PathToList = GetString("fileManagerListPath"),
                SearchExtensions = GetStringList("fileManagerAllowFiles"),
                ListSize = GetInt("fileManagerListSize"),

                Start = Request["start"].ToInt(),
                Size = Request["size"].ToInt(),
            };
            var rs = ff.Process();
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CatchImage()
        {
            var cr = new Crawler
            {
                Sources = Request.Form.GetValues("source[]"),
                PathFormat = GetString("catcherPathFormat"),
            };
            var rs = cr.Process();
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
    }
}
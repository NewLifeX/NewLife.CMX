using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using NewLife.CMX.Editor;
using NewLife.Collections;
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
        private String[] GetStringList(String key) => (String[])_config[key];

        // GET: UEditor
        public ActionResult Index()
        {
            Object rs = null;
            var act = (Request["action"] + "").ToLower();
            switch (act)
            {
                case "uploadimage":
                    var u1 = new Uploader
                    {
                        File = Request.Files["imageFieldName"],
                        AllowExtensions = GetStringList("imageAllowFiles"),
                        PathFormat = GetString("imagePathFormat"),
                        SizeLimit = GetInt("imageMaxSize"),
                        //UploadFieldName = GetString("imageFieldName")
                    };
                    break;
                case "uploadscrawl":
                    var u2 = new Uploader
                    {
                        FileBytes = Request["scrawlFieldName"].ToBase64(),
                        AllowExtensions = new String[] { ".png" },
                        PathFormat = GetString("scrawlPathFormat"),
                        SizeLimit = GetInt("scrawlMaxSize"),
                        //UploadFieldName = GetString("scrawlFieldName"),
                        //Base64 = true,
                        Base64Filename = "scrawl.png"
                    };
                    break;
                case "uploadvideo":
                    var u3 = new Uploader
                    {
                        File = Request.Files["videoFieldName"],
                        AllowExtensions = GetStringList("videoAllowFiles"),
                        PathFormat = GetString("videoPathFormat"),
                        SizeLimit = GetInt("videoMaxSize"),
                        //UploadFieldName = GetString("videoFieldName")
                    };
                    break;
                case "uploadfile":
                    var u4 = new Uploader
                    {
                        File = Request.Files["fileFieldName"],
                        AllowExtensions = GetStringList("fileAllowFiles"),
                        PathFormat = GetString("filePathFormat"),
                        SizeLimit = GetInt("fileMaxSize"),
                        //UploadFieldName = GetString("fileFieldName")
                    };
                    break;
                case "listimage":
                    var ff = new ListFile
                    {
                        PathToList = GetString("imageManagerListPath"),
                        SearchExtensions = GetStringList("imageManagerAllowFiles"),
                        ListSize = GetInt("imageManagerListSize"),

                        Start = Request["start"].ToInt(),
                        Size = Request["size"].ToInt(),
                    };
                    rs = ff.Process();
                    break;
                case "listfile":
                    var f2 = new ListFile
                    {
                        PathToList = GetString("fileManagerListPath"),
                        SearchExtensions = GetStringList("fileManagerAllowFiles"),
                        ListSize = GetInt("fileManagerListSize"),

                        Start = Request["start"].ToInt(),
                        Size = Request["size"].ToInt(),
                    };
                    rs = f2.Process();
                    break;
                case "catchimage":
                    var cr = new Crawler
                    {
                        Sources = Request.Form.GetValues("source[]"),
                        PathFormat = GetString("catcherPathFormat"),
                    };
                    rs = cr.Process();
                    break;
                case "config":
                default:
                    rs = _config; break;
            }

            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Config() => Json(_config, JsonRequestBehavior.AllowGet);

        public ActionResult UploadImage()
        {
            var ur = new Uploader
            {
                File = Request.Files["imageFieldName"],
                AllowExtensions = GetStringList("imageAllowFiles"),
                PathFormat = GetString("imagePathFormat"),
                SizeLimit = GetInt("imageMaxSize"),
                //UploadFieldName = GetString("imageFieldName")
            };
            var rs = ur.Process();
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadScrawl()
        {
            var ur = new Uploader
            {
                FileBytes = Request["scrawlFieldName"].ToBase64(),
                AllowExtensions = new String[] { ".png" },
                PathFormat = GetString("scrawlPathFormat"),
                SizeLimit = GetInt("scrawlMaxSize"),
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
                File = Request.Files["videoFieldName"],
                AllowExtensions = GetStringList("videoAllowFiles"),
                PathFormat = GetString("videoPathFormat"),
                SizeLimit = GetInt("videoMaxSize"),
                //UploadFieldName = GetString("videoFieldName")
            };
            var rs = ur.Process();
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadFile()
        {
            var ur = new Uploader
            {
                File = Request.Files["fileFieldName"],
                AllowExtensions = GetStringList("fileAllowFiles"),
                PathFormat = GetString("filePathFormat"),
                SizeLimit = GetInt("fileMaxSize"),
                //UploadFieldName = GetString("fileFieldName")
            };
            var rs = ur.Process();
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using NewLife.IO;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using NewLife.Log;
using NewLife.Web;

namespace NewLife.CMX.Editor
{
    /// <summary>UEditor 配置信息</summary>
    public class UEditorAjaxCore : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {

            String ac = RequestStr("ac");
            switch (ac)
            {
                case "remote":
                    context.Response.ContentType = "application/x-javascript";
                    context.Response.Write(GetRemoteImage(context));
                    break;
                case "imagemanager":
                    context.Response.ContentType = "application/x-javascript";
                    context.Response.Write(GetimageManager(context));
                    break;
                case "image":
                    context.Response.ContentType = "application/x-javascript";
                    context.Response.Write(ImgUp(context));
                    break;
                case "file":
                    context.Response.ContentType = "application/x-javascript";
                    context.Response.Write(FileUpLoad(context));
                    break;
                case "scraw":
                    context.Response.ContentType = "text/html";
                    context.Response.Write(ScrawlUp(context));
                    break;
                case "getmovie":
                    context.Response.ContentType = "text/html";
                    context.Response.Write(getMovie(context));
                    break;
                case "show":
                    ShowFile(context);
                    break;
                default:
                    context.Response.ContentType = "application/x-javascript";
                    context.Response.Write(LoadConfig(""));
                    break;
            }
        }

        #region 方法
        /// <summary>获取字符型参数</summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static String RequestStr(String name)
        {
            String str = HttpContext.Current.Request[name];
            return String.IsNullOrEmpty(str) ? "" : str;
        }

        /// <summary>
        /// 集合转换字符串
        /// </summary>
        /// <param name="tmpNames"></param>
        /// <returns></returns>
        private string converToString(ArrayList tmpNames)
        {
            String str = String.Empty;
            for (int i = 0, len = tmpNames.Count; i < len; i++)
            {
                str += tmpNames[i] + "ue_separate_ue";
                if (i == tmpNames.Count - 1)
                    str += tmpNames[i];
            }
            return str;
        }
        #endregion

        #region 远程图片保存
        /// <summary>
        /// 远程图片保存
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns></returns>
        public String GetRemoteImage(HttpContext context)
        {
            var config = UEditorConfig.Current;
            //保存文件地址
            String SavePath = context.Server.MapPath(config.UploadPath);
            //文件允许格式
            String[] FileType = config.ImgExtensions;
            //文件大小限制，单位kb
            Int32 FileSize = config.ImgFileSize;
            var uri = context.Server.HtmlEncode(context.Request["upfile"]);
            uri = uri.Replace("&amp;", "&");

            //取得所有图片地址
            var imgUrls = Regex.Split(uri, "ue_separate_ue", RegexOptions.IgnoreCase);
            var tmpNames = new ArrayList();
            var wc = new WebClient();
            HttpWebResponse res;
            String tmpName = String.Empty;
            String imgUrl = String.Empty;
            String currentType = String.Empty;
            try
            {
                for (int i = 0, len = imgUrls.Length; i < len; i++)
                {
                    imgUrl = imgUrls[i];

                    if (imgUrl.Substring(0, 7) != "http://")
                    {
                        tmpNames.Add("error!");
                        continue;
                    }

                    //格式验证
                    int temp = imgUrl.LastIndexOf('.');
                    currentType = imgUrl.Substring(temp).ToLower();
                    if (Array.IndexOf(FileType, currentType) == -1)
                    {
                        tmpNames.Add("error!");
                        continue;
                    }

                    res = (HttpWebResponse)WebRequest.Create(imgUrl).GetResponse();
                    //http检测
                    if (res.ResponseUri.Scheme.ToLower().Trim() != "http")
                    {
                        tmpNames.Add("error!");
                        continue;
                    }
                    //大小验证
                    if (res.ContentLength > FileSize * 1024)
                    {
                        tmpNames.Add("error!");
                        continue;
                    }
                    //死链验证
                    if (res.StatusCode != HttpStatusCode.OK)
                    {
                        tmpNames.Add("error!");
                        continue;
                    }
                    //检查mime类型
                    if (res.ContentType.IndexOf("image") == -1)
                    {
                        tmpNames.Add("error!");
                        continue;
                    }
                    res.Close();

                    //创建保存位置
                    if (!Directory.Exists(SavePath + DateTime.Now.ToString("yyyy-MM-dd")))
                    {
                        Directory.CreateDirectory(SavePath + DateTime.Now.ToString("yyyy-MM-dd"));
                    }

                    //写入文件
                    tmpName = DateTime.Now.ToString("yyyy-MM-dd") + "/" + System.Guid.NewGuid() + currentType;
                    wc.DownloadFile(imgUrl, SavePath + tmpName);
                    tmpNames.Add(tmpName);
                }
            }
            catch (Exception)
            {
                tmpNames.Add("error!");
            }
            finally
            {
                wc.Dispose();
            }
            return "{url:'" + converToString(tmpNames) + "',tip:'远程图片抓取成功！',srcUrl:'" + uri + "'}";
        }
        #endregion

        #region 在线图片管理
        /// <summary>
        /// 在线图片管理
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns></returns>
        public String GetimageManager(HttpContext context)
        {
            var config = UEditorConfig.Current;
            //保存文件地址
            var up = context.Server.MapPath(config.UploadPath);
            //文件允许格式
            var exts = config.ImgExtensions;

            var sb = new StringBuilder();
            if (RequestStr("action") == "get")
            {
                var info = new DirectoryInfo(up);
                //目录验证
                if (info.Exists)
                {
                    var infoArr = info.GetDirectories();
                    foreach (var tmpInfo in infoArr)
                    {
                        foreach (var fi in tmpInfo.GetFiles())
                        {
                            if (Array.IndexOf(exts, fi.Extension) != -1)
                            {
                                sb.Append(tmpInfo.Name + "/" + fi.Name + "ue_separate_ue");
                            }
                        }
                    }
                }
            }
            return sb.ToString();
        }
        #endregion

        #region 图片上传
        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns></returns>
        public String ImgUp(HttpContext context)
        {
            var config = UEditorConfig.Current;
            var up = new UEUploader();
            var info = up.upFile(context, config.UploadPath, config.ImgExtensions, config.ImgFileSize);                               //获取上传状态
            string title = up.getOtherInfo(context, "pictitle");                              //获取图片描述
            string oriName = up.getOtherInfo(context, "fileName");                //获取原始文件名
            return "{'url':'" + info["url"] + "','title':'" + title + "','original':'" + oriName + "','state':'" + info["state"] + "'}";
        }
        #endregion

        #region 文件上传
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns></returns>
        public String FileUpLoad(HttpContext context)
        {
            var config = UEditorConfig.Current;
            var up = new UEUploader();
            var info = up.upFile(context, config.UploadPath, config.FileExtensions, config.FileFileSize);                               //获取上传状态
            return "{'state':'" + info["state"] + "','url':'" + info["url"] + "','fileType':'" + info["currentType"] + "','original':'" + info["originalName"] + "'}";
        }
        #endregion

        #region 涂鸦
        /// <summary>
        /// 涂鸦处理
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns></returns>
        public String ScrawlUp(HttpContext context)
        {
            UEditorConfig Entity = UEditorConfig.Current;
            Hashtable info = new Hashtable();
            UEUploader up = new UEUploader();
            string action = RequestStr("action");
            if (action == "tmpImg")
            {
                string pathbase = Entity.UploadPath + "tmp/";                                                          //保存路径
                info = up.upFile(context, pathbase, Entity.ImgExtensions, Entity.ImgFileSize); //获取上传状态
                return "<script>parent.ue_callback('" + "tmp/" + info["url"] + "','" + info["state"] + "')</script>";
            }
            else
            {
                string tmpPath = Entity.UploadPath + "tmp/";
                info = up.upScrawl(context, Entity.UploadPath, tmpPath, RequestStr("content")); //获取上传状态
                return "{'url':'" + info["url"] + "',state:'" + info["state"] + "'}";
            }
        }
        #endregion

        #region 获取视频
        String getMovie(HttpContext context)
        {
            var key = context.Server.HtmlEncode(context.Request.Form["searchKey"]);
            var type = context.Server.HtmlEncode(context.Request.Form["videoType"]);

            var httpURL = new Uri("http://api.tudou.com/v3/gw?method=item.search&appKey=myKey&format=json&kw=" + key + "&pageNo=1&pageSize=20&channelId=" + type + "&inDays=7&media=v&sort=s");
            var MyWebClient = new WebClient();

            MyWebClient.Credentials = CredentialCache.DefaultCredentials;           //获取或设置用于向Internet资源的请求进行身份验证的网络凭据
            var pageData = MyWebClient.DownloadData(httpURL);

            return Encoding.UTF8.GetString(pageData);
        }
        #endregion

        #region 显示文件
        void ShowFile(HttpContext context)
        {
            var file = context.Request["file"];
            if (String.IsNullOrEmpty(file)) return;

            var config = UEditorConfig.Current;
            //保存文件地址
            var up = context.Server.MapPath(config.UploadPath).GetFullPath();
            var file2 = up.CombinePath(file).GetFullPath();

            //!!! 安全：必须验证路径，否则会爆任何文件
            if (!file2.StartsWith(up, StringComparison.OrdinalIgnoreCase))
            {
                XTrace.WriteLine("安全警告！{0}试图请求{1}，原始访问{2}！", WebHelper.UserHost, file2, file);
                return;
            }

            using (var fs = File.OpenRead(file2))
            {
                fs.CopyTo(context.Response.OutputStream);
            }
        }
        #endregion

        #region 配置
        /// <summary>
        /// 加载皮肤配置
        /// </summary>
        /// <param name="Style">样式</param>
        /// <returns></returns>
        public String LoadConfig(String Style)
        {
            var sb = new StringBuilder();
            var cfg = UEditorConfig.Current;
            sb.Append("(function () {");
            sb.Append(" window.UEDITOR_CONFIG = {");
            sb.Append("UEDITOR_HOME_URL : \"" + cfg.UEditorPath + "\"");
            sb.Append("," + ImageConfig);
            sb.Append("," + FileConfig);
            sb.Append("," + ScrawConfig);
            sb.Append("," + GetRemoteImageConfig);
            sb.Append("," + ImageManagerConfig);
            sb.Append("," + SnapscreenConfig);
            sb.Append("," + WordImageConfig);
            sb.Append("," + GetMovieConfig);
            sb.Append("," + ToolBars);
            sb.Append(",webAppKey:\"" + cfg.BaiduWebAppKey + "\"");
            sb.Append("};})();");
            return sb.ToString();
        }
        /// <summary>
        /// 图片上传配置
        /// </summary>
        public String ImageConfig
        {
            #region
            //            ,imageUrl:URL+"net/imageUp.ashx"             //图片上传提交地址
            // ,imagePath:URL + "net/"                     //图片修正地址，引用了fixedImagePath,如有特殊需求，可自行配置
            ////,imageFieldName:"upfile"                   //图片数据的key,若此处修改，需要在后台对应文件修改对应参数
            // //,compressSide:0                            //等比压缩的基准，确定maxImageSideLength参数的参照对象。0为按照最长边，1为按照宽度，2为按照高度
            // //,maxImageSideLength:900                    //上传图片最大允许的边长，超过会自动等比缩放,不缩放就设置一个比较大的值，更多设置在image.html中
            #endregion
            get
            {
                var Entity = UEditorConfig.Current;
                var imgcfg = new StringBuilder();
                imgcfg.Append("imageUrl:\"" + Entity.ImgUpUrl + "\"");
                imgcfg.Append("," + "imagePath:\"" + Entity.UploadPath + "\"");
                return imgcfg.ToString();
            }
        }
        /// <summary>
        /// 文件上传配置
        /// </summary>
        public String FileConfig
        {
            get
            {
                var Entity = UEditorConfig.Current;
                var Filecfg = new StringBuilder();
                Filecfg.Append("fileUrl:\"" + Entity.FileUrl + "\"");
                Filecfg.Append("," + "filePath:\"" + Entity.UploadPath + "\"");
                return Filecfg.ToString();
            }
        }
        /// <summary>
        /// 涂鸦图片相关配置
        /// </summary>
        public String ScrawConfig
        {
            get
            {
                var Entity = UEditorConfig.Current;
                var Scrawcfg = new StringBuilder();
                Scrawcfg.Append("scrawlUrl:\"" + Entity.ScrawUrl + "\"");
                Scrawcfg.Append("," + "scrawlPath:\"" + Entity.UploadPath + "\"");
                return Scrawcfg.ToString();
            }
        }
        /// <summary>
        /// 远程抓取相关配置
        /// </summary>
        public String GetRemoteImageConfig
        {
            get
            {
                var Entity = UEditorConfig.Current;
                var RemoteCfg = new StringBuilder();
                RemoteCfg.Append("catcherUrl:\"" + Entity.GetRemoteUrl + "\"");
                RemoteCfg.Append("," + "catcherPath:\"" + Entity.UploadPath + "\"");
                return RemoteCfg.ToString();
            }
        }
        /// <summary>
        /// 图片在线管理相关配置
        /// </summary>
        public String ImageManagerConfig
        {
            get
            {
                var Entity = UEditorConfig.Current;
                var ImageManagerCfg = new StringBuilder();
                ImageManagerCfg.Append("imageManagerUrl:\"" + Entity.ImageManagerUrl + "\"");
                ImageManagerCfg.Append("," + "imageManagerPath:\"" + Entity.UploadPath + "\"");
                return ImageManagerCfg.ToString();
            }
        }
        /// <summary>
        /// 屏幕截图配置
        /// </summary>
        public String SnapscreenConfig
        {
            get
            {
                var Entity = UEditorConfig.Current;
                var SnapscreenCfg = new StringBuilder();
                SnapscreenCfg.Append("snapscreenHost:\"" + Entity.SnapscreenHost + "\"");
                SnapscreenCfg.Append("," + "snapscreenServerUrl:\"" + Entity.SnapscreenUrl + "\"");
                SnapscreenCfg.Append("," + "snapscreenPath:\"" + Entity.UploadPath + "\"");
                return SnapscreenCfg.ToString();
            }
        }
        /// <summary>
        /// Word转存配置区
        /// </summary>
        public String WordImageConfig
        {
            get
            {
                UEditorConfig Entity = UEditorConfig.Current;
                StringBuilder WordImageCfg = new StringBuilder();
                WordImageCfg.Append("wordImageUrl:\"" + Entity.ImgUpUrl + "\"");
                WordImageCfg.Append("," + "wordImagePath:\"" + Entity.UploadPath + "\"");
                return WordImageCfg.ToString();
            }
        }
        /// <summary>
        /// 获取视频数据的地址
        /// </summary>
        public String GetMovieConfig
        {
            get
            {
                UEditorConfig Entity = UEditorConfig.Current;
                StringBuilder MovieCfg = new StringBuilder();
                MovieCfg.Append("getMovieUrl:\"" + Entity.MovieUrl + "\"");
                return MovieCfg.ToString();
            }
        }


        public String ToolBars
        {
            get
            {
                var ToolBarsCfg = new StringBuilder();
                ToolBarsCfg.Append("toolbars:[");
                ToolBarsCfg.Append("['fullscreen', 'source', '|', 'undo', 'redo', '|',");
                ToolBarsCfg.Append("'bold', 'italic', 'underline', 'strikethrough', 'superscript', 'subscript', 'removeformat', 'formatmatch','autotypeset','blockquote', 'pasteplain', '|', 'forecolor', 'backcolor', 'insertorderedlist', 'insertunorderedlist','selectall', 'cleardoc', '|',");
                ToolBarsCfg.Append("'rowspacingtop', 'rowspacingbottom','lineheight','|',");
                ToolBarsCfg.Append("'customstyle', 'paragraph', 'fontfamily', 'fontsize', '|',");
                ToolBarsCfg.Append("'directionalityltr', 'directionalityrtl', 'indent', '|',");
                ToolBarsCfg.Append("'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|','touppercase','tolowercase','|',");
                ToolBarsCfg.Append("'link', 'unlink', 'anchor', '|', 'imagenone', 'imageleft', 'imageright','imagecenter', '|',");
                ToolBarsCfg.Append("'insertimage', 'emotion','scrawl', 'insertvideo','music','attachment', 'map', 'gmap', 'insertframe','highlightcode','webapp','pagebreak','template','background', '|',");
                ToolBarsCfg.Append("'horizontal', 'date', 'time', 'spechars','snapscreen', 'wordimage', '|',");
                ToolBarsCfg.Append("'inserttable', 'deletetable', 'insertparagraphbeforetable', 'insertrow', 'deleterow', 'insertcol', 'deletecol', 'mergecells', 'mergeright', 'mergedown', 'splittocells', 'splittorows', 'splittocols', '|',");
                ToolBarsCfg.Append("'print', 'preview', 'searchreplace','help']]");
                return ToolBarsCfg.ToString();
            }
        }
        #endregion

        public bool IsReusable { get { return false; } }
    }
}
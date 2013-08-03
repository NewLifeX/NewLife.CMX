using System;
using System.ComponentModel;
using System.Xml.Serialization;
using NewLife.Xml;

namespace NewLife.CMX.Editor
{
    /// <summary>
    /// 编辑器配置
    /// </summary>
    [XmlConfigFile("Config/UEditor.config", 15000)]
    public class UEditorConfig : XmlConfig<UEditorConfig>
    {
        private String _UEditorPath = "/UEditor/";
        /// <summary>编辑器路径</summary>
        [DisplayName("编辑器路径")]
        [Description("编辑器所在目录，建议用相对路径 默认为根目录下的UEditor")]
        public String UEditorPath { get { return _UEditorPath; } set { _UEditorPath = value; } }

        private String _UploadPath = "/Upload/";
        /// <summary>附件上传目录</summary>
        [DisplayName("附件上传目录")]
        [Description("上传图片或附件的目录，自动创建在网站根目录下")]
        public String UploadPath { get { return _UploadPath; } set { _UploadPath = value; } }

        private String _BaiduWebAppKey;
        /// <summary>百度应用的APIkey</summary>
        [DisplayName("百度应用的APIkey")]
        [Description("百度应用的APIkey，每个站长必须首先去百度官网注册一个key后方能正常使用app功能")]
        public String BaiduWebAppKey { get { return _BaiduWebAppKey; } set { _BaiduWebAppKey = value; } }

        #region 图片上传相关配置
        private String _ImgUpUrl;
        /// <summary>图片上传处理文件</summary>
        [DisplayName("图片上传处理文件")]
        [Description("图片上传处理文件所在目录，建议用相对路径 默认为根目录下的UEditor/UEditorAjax.ashx?ac=image 如自己修改，请带全路径/UEditor/UEditorAjax.ashx?ac=image")]
        public String ImgUpUrl { get { return String.IsNullOrEmpty(_ImgUpUrl) ? _UEditorPath + "UEditorAjax.ashx?ac=image" : _ImgUpUrl; } set { _ImgUpUrl = value; } }

        private String _Imgextension = ".jpg,.gif,.png,.jpeg,.bmp";
        /// <summary>图片上传类型</summary>
        [DisplayName("图片上传类型")]
        [Description("以英文的逗号分隔开，如：“.jpg,.gif,.png,.jpeg,.bmp”")]
        public String ImgExtension { get { return _Imgextension; } set { _Imgextension = value; } }
        /// <summary>图片上传类型</summary>
        [XmlIgnore]
        public String[] ImgExtensions { get { return _Imgextension.Split(","); } }

        private Int32 _ImgFileSize = 3072;
        /// <summary>
        /// 图片上传大小
        /// </summary>
        [DisplayName("图片上传大小")]
        [Description("图片上传大小，文件大小限制，单位kb 默认3072kb")]
        public Int32 ImgFileSize { get { return _ImgFileSize; } set { _ImgFileSize = value; } }
        #endregion

        #region 文件上传相关配置

        private String _FileUrl;

        /// <summary>文件上传处理文件</summary>
        [DisplayName("文件上传处理文件")]
        [Description("文件上传处理文件所在目录，建议用相对路径 默认为根目录下的UEditor/UEditorAjax.ashx?ac=file 如自己修改，请带全路径/UEditor/UEditorAjax.ashx?ac=file")]
        public String FileUrl { get { return String.IsNullOrEmpty(_FileUrl) ? _UEditorPath + "UEditorAjax.ashx?ac=file" : _FileUrl; } set { _FileUrl = value; } }


        private String _FileExtension = ".rar,.doc,.docx,.zip,.pdf,.txt,.swf";
        
        /// <summary>附件上传类型</summary>
        [DisplayName("文件上传类型")]
        [Description("以英文的逗号分隔开，如：“.rar,.doc,.docx,.zip,.pdf,.txt,.swf”")]
        public String FileExtension { get { return _FileExtension; } set { _FileExtension = value; } }
        /// <summary>附件上传类型</summary>
        [XmlIgnore]
        public String[] FileExtensions {get{return FileExtension.Split(",");}}

        private Int32 _FileFileSize = 30720;
        /// <summary>
        /// 附件上传大小
        /// </summary>
        [DisplayName("附件上传大小")]
        [Description("附件上传大小，文件大小限制，单位kb 默认30720kb")]
        public Int32 FileFileSize { get { return _FileFileSize; } set { _FileFileSize = value; } }
        #endregion

        #region 涂鸦图片相关配置

        private String _ScrawUrl;

        /// <summary>涂鸦图片处理文件</summary>
        [DisplayName("涂鸦图片处理文件")]
        [Description("涂鸦图片处理文件所在目录，建议用相对路径 默认为根目录下的UEditor/UEditorAjax.ashx?ac=scraw 如自己修改，请带全路径/UEditor/UEditorAjax.ashx?ac=scraw")]
        public String ScrawUrl { get { return String.IsNullOrEmpty(_ScrawUrl) ? _UEditorPath + "UEditorAjax.ashx?ac=scraw" : _ScrawUrl; } set { _ScrawUrl = value; } }

        #endregion

        #region 远程抓取相关配置

        private String _GetRemoteUrl;

        /// <summary>远程抓取处理文件</summary>
        [DisplayName("远程抓取处理文件")]
        [Description("远程抓取处理文件所在目录，建议用相对路径 默认为根目录下的UEditor/UEditorAjax.ashx?ac=remote 如自己修改，请带全路径/UEditor/UEditorAjax.ashx?ac=remote")]
        public String GetRemoteUrl { get { return String.IsNullOrEmpty(_GetRemoteUrl) ? _UEditorPath + "UEditorAjax.ashx?ac=remote" : _GetRemoteUrl; } set { _GetRemoteUrl = value; } }

        #endregion

        #region 图片在线管理相关配置

        private String _ImageManagerUrl;

        /// <summary>图片在线管理处理文件</summary>
        [DisplayName("图片在线管理处理文件")]
        [Description("图片在线管理处理文件所在目录，建议用相对路径 默认为根目录下的UEditor/UEditorAjax.ashx?ac=imagemanager 如自己修改，请带全路径/UEditor/UEditorAjax.ashx?ac=imagemanager")]
        public String ImageManagerUrl { get { return String.IsNullOrEmpty(_ImageManagerUrl) ? _UEditorPath + "UEditorAjax.ashx?ac=imagemanager" : _ImageManagerUrl; } set { _ImageManagerUrl = value; } }

        #endregion

        #region 获取视频数据相关配置

        private String _MovieUrl;

        /// <summary>获取视频数据处理文件</summary>
        [DisplayName("获取视频数据处理文件")]
        [Description("获取视频数据处理文件所在目录，建议用相对路径 默认为根目录下的UEditor/UEditorAjax.ashx?ac=movie 如自己修改，请带全路径/UEditor/UEditorAjax.ashx?ac=movie")]
        public String MovieUrl { get { return String.IsNullOrEmpty(_MovieUrl) ? _UEditorPath + "UEditorAjax.ashx?ac=movie" : _MovieUrl; } set { _MovieUrl = value; } }

        #endregion

        #region 屏幕截图相关配置

        private String _SnapscreenHost;

        /// <summary>屏幕截图服务器地址</summary>
        [DisplayName("屏幕截图服务器地址")]
        [Description("屏幕截图的server端文件所在的网站地址或者ip，请不要加http://")]
        public String SnapscreenHost { get { return String.IsNullOrEmpty(_SnapscreenHost) ? "127.0.0.1" : _SnapscreenHost; } set { _SnapscreenHost = value; } }


        private String _SnapscreenUrl;

        /// <summary>屏幕截图处理文件</summary>
        [DisplayName("屏幕截图处理文件")]
        [Description("屏幕截图处理文件所在目录，建议用相对路径 默认为根目录下的UEditor/UEditorAjax.ashx?ac=image 如自己修改，请带全路径/UEditor/UEditorAjax.ashx?ac=image")]
        public String SnapscreenUrl { get { return String.IsNullOrEmpty(_SnapscreenUrl) ? _UEditorPath + "UEditorAjax.ashx?ac=image" : _SnapscreenUrl; } set { _SnapscreenUrl = value; } }

        #endregion
    }
}
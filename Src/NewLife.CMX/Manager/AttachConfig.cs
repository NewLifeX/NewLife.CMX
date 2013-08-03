using System;
using System.ComponentModel;
using NewLife.Xml;

namespace NewLife.CMX
{
    /// <summary>附件设置</summary>
    [XmlConfigFile("Config/Attach.config", 15000)]
    public class AttachConfig : XmlConfig<AttachConfig>
    {
        private String _Path = "../../Upload";
        /// <summary>附件上传目录</summary>
        [DisplayName("附件上传目录")]
        [Description("上传图片或附件的目录，自动创建在网站根目录下")]
        public String Path { get { return _Path; } set { _Path = value; } }

        private String _extension = "jpg,gif,rar";
        /// <summary>附件上传类型</summary>
        [DisplayName("附件上传类型")]
        [Description("以英文的逗号分隔开，如：“jpg,gif,rar”")]
        public String Extension { get { return _extension; } set { _extension = value; } }

        private Int32 _save;
        /// <summary>附件保存方式</summary>
        [DisplayName("附件保存方式")]
        [Description("附件保存方式")]
        public Int32 SaveMode { get { return _save; } set { _save = value; } }

        private Int32 _filesize = 5 * 1024 * 1024;
        /// <summary>文件上传大小</summary>
        [DisplayName("文件上传大小")]
        [Description("单位KB。超过设置的文件大小不予上传，0不限制")]
        public Int32 FileSize { get { return _filesize; } set { _filesize = value; } }

        private Int32 _imgsize = 5 * 1024 * 1024;
        /// <summary>图片上传大小</summary>
        [DisplayName("图片上传大小")]
        [Description("单位KB。超过设置的图片大小不予上传，0不限制")]
        public Int32 SiteAttachimgsize { get { return _imgsize; } set { _imgsize = value; } }

        private Int32 _imgmaxheight;
        /// <summary>图片最大尺寸高</summary>
        [DisplayName("图片最大尺寸高")]
        [Description("设置图片高和宽，超出自动裁剪，0为不受限制")]
        public Int32 ImgMaxHeight { get { return _imgmaxheight; } set { _imgmaxheight = value; } }

        private Int32 _imgmaxwidth;
        /// <summary>图片最大尺寸宽</summary>
        [DisplayName("图片最大尺寸宽")]
        [Description("设置图片高和宽，超出自动裁剪，0为不受限制")]
        public Int32 ImgMaxWidth { get { return _imgmaxwidth; } set { _imgmaxwidth = value; } }

        private Int32 _Thumbnailheight;
        /// <summary>生成缩略图大小高</summary>
        [DisplayName("生成缩略图大小高")]
        [Description("图片生成缩略图高和宽，0为不生成")]
        public Int32 ThumbnailHeight { get { return _Thumbnailheight; } set { _Thumbnailheight = value; } }

        private Int32 _Thumbnailwidth;
        /// <summary>生成缩略图大小宽</summary>
        [DisplayName("生成缩略图大小宽")]
        [Description("图片生成缩略图高和宽，0为不生成")]
        public Int32 ThumbnailWidth { get { return _Thumbnailwidth; } set { _Thumbnailwidth = value; } }

        private Int32 _Watermarktype;
        /// <summary>图片水印类型</summary>
        [DisplayName("图片水印类型")]
        [Description("图片水印类型")]
        public Int32 WatermarkType { get { return _Watermarktype; } set { _Watermarktype = value; } }

        private Int32 _Watermarkposition;
        /// <summary>图片水印位置</summary>
        [DisplayName("图片水印位置")]
        [Description("图片水印位置")]
        public Int32 WatermarkPosition { get { return _Watermarkposition; } set { _Watermarkposition = value; } }

        private Int32 _Watermarkimgquality;
        /// <summary>图片生成质量</summary>
        [DisplayName("图片生成质量")]
        [Description("只适用于加水印的jpeg格式图片.取值范围 0-100, 0质量最低, 100质量最高, 默认80")]
        public Int32 WatermarkImgQuality { get { return _Watermarkimgquality; } set { _Watermarkimgquality = value; } }

        private String _Watermarkpic = "";
        /// <summary>图片水印文件</summary>
        [DisplayName("图片水印文件")]
        [Description("需存放在站点目录下，如图片不存在将使用文字水印")]
        public String WatermarkPic { get { return _Watermarkpic; } set { _Watermarkpic = value; } }

        private Int32 _Watermarktransparency;
        /// <summary>水印透明度</summary>
        [DisplayName("水印透明度")]
        [Description("取值范围1--10 (10为不透明)")]
        public Int32 WatermarkTransparency { get { return _Watermarktransparency; } set { _Watermarktransparency = value; } }

        private String _Watermarktext = "";
        /// <summary>水印文字</summary>
        [DisplayName("水印文字")]
        [Description("文字水印的内容")]
        public String WatermarkText { get { return _Watermarktext; } set { _Watermarktext = value; } }

        private String _Watermarkfont = "";
        /// <summary>文字字体</summary>
        [DisplayName("文字字体")]
        [Description("文字水印的字体和大小")]
        public String WatermarkFont { get { return _Watermarkfont; } set { _Watermarkfont = value; } }

        private Int32 _Watermarkfontsize;
        /// <summary>文字字体大小</summary>
        [DisplayName("文字字体大小")]
        [Description("文字水印的字体和大小")]
        public Int32 WatermarkFontSize { get { return _Watermarkfontsize; } set { _Watermarkfontsize = value; } }

        #region 构造
        /// <summary>
        /// 实例化
        /// </summary>
        public AttachConfig()
        {
            //Path = "SiteAttachPath";
            //Extension = "SiteAttachextension";
            //SaveMode = 0;
            //FileSize = 0;
            //SiteAttachimgsize = 0;
            //ImgMaxHeight = 0;
            //ImgMaxWidth = 0;
            //ThumbnailHeight = 0;
            //ThumbnailWidth = 0;
            //WatermarkType = 0;
            //WatermarkPosition = 0;
            //WatermarkImgQuality = 0;
            //SaveMode = 0;
            //FileSize = 0;
            //SiteAttachimgsize = 0;
            //ImgMaxHeight = 0;
            //ImgMaxWidth = 0;
            //ThumbnailHeight = 0;
            //ThumbnailWidth = 0;
            //WatermarkType = 0;
            //WatermarkPosition = 0;
            //WatermarkImgQuality = 0;
            //WatermarkPic = "SiteWatermarkpic";
            //WatermarkTransparency = 0;
            //WatermarkText = "SiteWatermarktext";
            //WatermarkFont = "SiteWatermarkfont";
            //WatermarkFontSize = 0;
        }

        //static AttachConfig() { }
        #endregion
    }
}
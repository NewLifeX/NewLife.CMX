using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using NewLife.Xml;

namespace NewLife.CMX.Config
{
    [XmlConfigFile("config/DefaultArticle.config", 15000)]
    public class CMXDefaultArticleModelConfig : CMXmlConfig<CMXDefaultArticleModelConfig>
    {
        private String _CategroyClassName = "Article";
        /// <summary>类名</summary>
        [Description("类名")]
        public String ClassName { get { return _CategroyClassName; } set { _CategroyClassName = value; } }

        private String _CategoryName = "ArticleCategory";
        /// <summary>分类名</summary>
        [Description("分类名")]
        public String CategoryName { get { return _CategoryName; } set { _CategoryName = value; } }

        private String _TitleTemplatePath = "Article.aspx";
        /// <summary>标题页路径</summary>
        [Description("标题页路径")]
        public String TitleTemplatePath { get { return _TitleTemplatePath; } set { _TitleTemplatePath = value; } }

        private String _CategoryTemplatePath = "ArticleCategory.aspx";
        /// <summary>分类页路径</summary>
        [Description("分类页路径")]
        public String CategoryTemplatePath { get { return _CategoryTemplatePath; } set { _CategoryTemplatePath = value; } }

        private String _ListModelPath = "ArticleModelList.html";
        /// <summary>列表模板路径</summary>
        [Description("列表模板路径")]
        public String ListModelPath { get { return _ListModelPath; } set { _ListModelPath = value; } }

        private String _ContentModelPath = "ArticleModelContent.html";
        /// <summary>内容模板路径</summary>
        [Description("列表模板路径")]
        public String ContentModelPath { get { return _ContentModelPath; } set { _ContentModelPath = value; } }

        private String _Suffix = "";
        /// <summary>扩展名称</summary>
        [Description("扩展名称")]
        public String Suffix { get { return _Suffix; } set { _Suffix = value; } }
    }

    [XmlConfigFile("config/DefaultProduct.config", 15000)]
    public class CMXDefaultProductModelConfig : CMXmlConfig<CMXDefaultProductModelConfig>
    {
        private String _CategroyClassName = "Product";
        /// <summary>类名</summary>
        [Description("类名")]
        public String ClassName { get { return _CategroyClassName; } set { _CategroyClassName = value; } }

        private String _CategoryName = "ProductCategory";
        /// <summary>分类名</summary>
        [Description("分类名")]
        public String CategoryName { get { return _CategoryName; } set { _CategoryName = value; } }

        private String _TitleTemplatePath = "Product.aspx";
        /// <summary>标题页路径</summary>
        [Description("标题页路径")]
        public String TitleTemplatePath { get { return _TitleTemplatePath; } set { _TitleTemplatePath = value; } }

        private String _CategoryTemplatePath = "ProductCategory.aspx";
        /// <summary>分类页路径</summary>
        [Description("分类页路径")]
        public String CategoryTemplatePath { get { return _CategoryTemplatePath; } set { _CategoryTemplatePath = value; } }

        private String _ListModelPath = "ProductMolelList.html";
        /// <summary>列表模板路径</summary>
        [Description("列表模板路径")]
        public String ListModelPath { get { return _ListModelPath; } set { _ListModelPath = value; } }

        private String _ContentModelPath = "ProductModelContent.html";
        /// <summary>内容模板路径</summary>
        [Description("列表模板路径")]
        public String ContentModelPath { get { return _ContentModelPath; } set { _ContentModelPath = value; } }

        private String _Suffix = "";
        /// <summary>扩展名称</summary>
        [Description("扩展名称")]
        public String Suffix { get { return _Suffix; } set { _Suffix = value; } }
    }

    [XmlConfigFile("config/DefaultText.config", 15000)]
    public class CMXDefaultTextModelConfig : CMXmlConfig<CMXDefaultTextModelConfig>
    {
        private String _CategroyClassName = "Text";
        /// <summary>类名</summary>
        [Description("类名")]
        public String ClassName { get { return _CategroyClassName; } set { _CategroyClassName = value; } }

        private String _CategoryName = "TextCategory";
        /// <summary>分类名</summary>
        [Description("分类名")]
        public String CategoryName { get { return _CategoryName; } set { _CategoryName = value; } }

        private String _TitleTemplatePath = "Text.aspx";
        /// <summary>标题页路径</summary>
        [Description("标题页路径")]
        public String TitleTemplatePath { get { return _TitleTemplatePath; } set { _TitleTemplatePath = value; } }

        private String _CategoryTemplatePath = "TextCategory.aspx";
        /// <summary>分类页路径</summary>
        [Description("分类页路径")]
        public String CategoryTemplatePath { get { return _CategoryTemplatePath; } set { _CategoryTemplatePath = value; } }

        private String _ListModelPath = "TextModelList.html";
        /// <summary>列表模板路径</summary>
        [Description("列表模板路径")]
        public String ListModelPath { get { return _ListModelPath; } set { _ListModelPath = value; } }

        private String _ContentModelPath = "TextModelContent.html";
        /// <summary>内容模板路径</summary>
        [Description("列表模板路径")]
        public String ContentModelPath { get { return _ContentModelPath; } set { _ContentModelPath = value; } }

        private String _Suffix = "";
        /// <summary>扩展名称</summary>
        [Description("扩展名称")]
        public String Suffix { get { return _Suffix; } set { _Suffix = value; } }
    }
}

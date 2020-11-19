using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace NewLife.CMX
{
    /// <summary>文章</summary>
    [Serializable]
    [DataObject]
    [Description("文章")]
    [BindIndex("IU_Article_InfoID", true, "InfoID")]
    [BindTable("Article", Description = "文章", ConnName = "CMX", DbType = DatabaseType.SqlServer)]
    public partial class Article
    {
        #region 属性
        private Int32 _ID;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("ID", "编号", "")]
        public Int32 ID { get => _ID; set { if (OnPropertyChanging("ID", value)) { _ID = value; OnPropertyChanged("ID"); } } }

        private Int32 _InfoID;
        /// <summary>标题</summary>
        [DisplayName("标题")]
        [Description("标题")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("InfoID", "标题", "")]
        public Int32 InfoID { get => _InfoID; set { if (OnPropertyChanging("InfoID", value)) { _InfoID = value; OnPropertyChanged("InfoID"); } } }

        private Int32 _SourceID;
        /// <summary>来源</summary>
        [DisplayName("来源")]
        [Description("来源")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("SourceID", "来源", "")]
        public Int32 SourceID { get => _SourceID; set { if (OnPropertyChanging("SourceID", value)) { _SourceID = value; OnPropertyChanged("SourceID"); } } }

        private String _SourceName;
        /// <summary>来源名称</summary>
        [DisplayName("来源名称")]
        [Description("来源名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("SourceName", "来源名称", "")]
        public String SourceName { get => _SourceName; set { if (OnPropertyChanging("SourceName", value)) { _SourceName = value; OnPropertyChanged("SourceName"); } } }

        private String _SourceUrl;
        /// <summary>来源地址</summary>
        [DisplayName("来源地址")]
        [Description("来源地址")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("SourceUrl", "来源地址", "")]
        public String SourceUrl { get => _SourceUrl; set { if (OnPropertyChanging("SourceUrl", value)) { _SourceUrl = value; OnPropertyChanged("SourceUrl"); } } }

        private Boolean _Top;
        /// <summary>置顶</summary>
        [DisplayName("置顶")]
        [Description("置顶")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Top", "置顶", "")]
        public Boolean Top { get => _Top; set { if (OnPropertyChanging("Top", value)) { _Top = value; OnPropertyChanged("Top"); } } }

        private Boolean _Recommend;
        /// <summary>推荐</summary>
        [DisplayName("推荐")]
        [Description("推荐")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Recommend", "推荐", "")]
        public Boolean Recommend { get => _Recommend; set { if (OnPropertyChanging("Recommend", value)) { _Recommend = value; OnPropertyChanged("Recommend"); } } }

        private Boolean _Hot;
        /// <summary>热门</summary>
        [DisplayName("热门")]
        [Description("热门")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Hot", "热门", "")]
        public Boolean Hot { get => _Hot; set { if (OnPropertyChanging("Hot", value)) { _Hot = value; OnPropertyChanged("Hot"); } } }

        private Boolean _Slide;
        /// <summary>幻灯片</summary>
        [DisplayName("幻灯片")]
        [Description("幻灯片")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Slide", "幻灯片", "")]
        public Boolean Slide { get => _Slide; set { if (OnPropertyChanging("Slide", value)) { _Slide = value; OnPropertyChanged("Slide"); } } }

        private String _Cover;
        /// <summary>封面</summary>
        [DisplayName("封面")]
        [Description("封面")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Cover", "封面", "")]
        public String Cover { get => _Cover; set { if (OnPropertyChanging("Cover", value)) { _Cover = value; OnPropertyChanged("Cover"); } } }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        public override Object this[String name]
        {
            get
            {
                switch (name)
                {
                    case "ID": return _ID;
                    case "InfoID": return _InfoID;
                    case "SourceID": return _SourceID;
                    case "SourceName": return _SourceName;
                    case "SourceUrl": return _SourceUrl;
                    case "Top": return _Top;
                    case "Recommend": return _Recommend;
                    case "Hot": return _Hot;
                    case "Slide": return _Slide;
                    case "Cover": return _Cover;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "ID": _ID = value.ToInt(); break;
                    case "InfoID": _InfoID = value.ToInt(); break;
                    case "SourceID": _SourceID = value.ToInt(); break;
                    case "SourceName": _SourceName = Convert.ToString(value); break;
                    case "SourceUrl": _SourceUrl = Convert.ToString(value); break;
                    case "Top": _Top = value.ToBoolean(); break;
                    case "Recommend": _Recommend = value.ToBoolean(); break;
                    case "Hot": _Hot = value.ToBoolean(); break;
                    case "Slide": _Slide = value.ToBoolean(); break;
                    case "Cover": _Cover = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得文章字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field ID = FindByName("ID");

            /// <summary>标题</summary>
            public static readonly Field InfoID = FindByName("InfoID");

            /// <summary>来源</summary>
            public static readonly Field SourceID = FindByName("SourceID");

            /// <summary>来源名称</summary>
            public static readonly Field SourceName = FindByName("SourceName");

            /// <summary>来源地址</summary>
            public static readonly Field SourceUrl = FindByName("SourceUrl");

            /// <summary>置顶</summary>
            public static readonly Field Top = FindByName("Top");

            /// <summary>推荐</summary>
            public static readonly Field Recommend = FindByName("Recommend");

            /// <summary>热门</summary>
            public static readonly Field Hot = FindByName("Hot");

            /// <summary>幻灯片</summary>
            public static readonly Field Slide = FindByName("Slide");

            /// <summary>封面</summary>
            public static readonly Field Cover = FindByName("Cover");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得文章字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String ID = "ID";

            /// <summary>标题</summary>
            public const String InfoID = "InfoID";

            /// <summary>来源</summary>
            public const String SourceID = "SourceID";

            /// <summary>来源名称</summary>
            public const String SourceName = "SourceName";

            /// <summary>来源地址</summary>
            public const String SourceUrl = "SourceUrl";

            /// <summary>置顶</summary>
            public const String Top = "Top";

            /// <summary>推荐</summary>
            public const String Recommend = "Recommend";

            /// <summary>热门</summary>
            public const String Hot = "Hot";

            /// <summary>幻灯片</summary>
            public const String Slide = "Slide";

            /// <summary>封面</summary>
            public const String Cover = "Cover";
        }
        #endregion
    }
}
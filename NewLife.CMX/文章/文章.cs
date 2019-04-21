﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    [BindRelation("SourceID", false, "Source", "ID")]
    [BindTable("Article", Description = "文章", ConnName = "CMX", DbType = DatabaseType.SqlServer)]
    public partial class Article : IArticle
    {
        #region 属性
        private Int32 _SourceID;
        /// <summary>来源</summary>
        [DisplayName("来源")]
        [Description("来源")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(1, "SourceID", "来源", null, "int", 10, 0, false)]
        public virtual Int32 SourceID
        {
            get { return _SourceID; }
            set { if (OnPropertyChanging(__.SourceID, value)) { _SourceID = value; OnPropertyChanged(__.SourceID); } }
        }

        private String _SourceName;
        /// <summary>来源名称</summary>
        [DisplayName("来源名称")]
        [Description("来源名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(2, "SourceName", "来源名称", null, "nvarchar(50)", 0, 0, true)]
        public virtual String SourceName
        {
            get { return _SourceName; }
            set { if (OnPropertyChanging(__.SourceName, value)) { _SourceName = value; OnPropertyChanged(__.SourceName); } }
        }

        private String _SourceUrl;
        /// <summary>来源地址</summary>
        [DisplayName("来源地址")]
        [Description("来源地址")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(3, "SourceUrl", "来源地址", null, "nvarchar(50)", 0, 0, true)]
        public virtual String SourceUrl
        {
            get { return _SourceUrl; }
            set { if (OnPropertyChanging(__.SourceUrl, value)) { _SourceUrl = value; OnPropertyChanged(__.SourceUrl); } }
        }

        private Boolean _Top;
        /// <summary>置顶</summary>
        [DisplayName("置顶")]
        [Description("置顶")]
        [DataObjectField(false, false, true, 1)]
        [BindColumn(4, "Top", "置顶", null, "bit", 0, 0, false)]
        public virtual Boolean Top
        {
            get { return _Top; }
            set { if (OnPropertyChanging(__.Top, value)) { _Top = value; OnPropertyChanged(__.Top); } }
        }

        private Boolean _Recommend;
        /// <summary>推荐</summary>
        [DisplayName("推荐")]
        [Description("推荐")]
        [DataObjectField(false, false, true, 1)]
        [BindColumn(5, "Recommend", "推荐", null, "bit", 0, 0, false)]
        public virtual Boolean Recommend
        {
            get { return _Recommend; }
            set { if (OnPropertyChanging(__.Recommend, value)) { _Recommend = value; OnPropertyChanged(__.Recommend); } }
        }

        private Boolean _Hot;
        /// <summary>热门</summary>
        [DisplayName("热门")]
        [Description("热门")]
        [DataObjectField(false, false, true, 1)]
        [BindColumn(6, "Hot", "热门", null, "bit", 0, 0, false)]
        public virtual Boolean Hot
        {
            get { return _Hot; }
            set { if (OnPropertyChanging(__.Hot, value)) { _Hot = value; OnPropertyChanged(__.Hot); } }
        }

        private Boolean _Slide;
        /// <summary>幻灯片</summary>
        [DisplayName("幻灯片")]
        [Description("幻灯片")]
        [DataObjectField(false, false, true, 1)]
        [BindColumn(7, "Slide", "幻灯片", null, "bit", 0, 0, false)]
        public virtual Boolean Slide
        {
            get { return _Slide; }
            set { if (OnPropertyChanging(__.Slide, value)) { _Slide = value; OnPropertyChanged(__.Slide); } }
        }

        private String _Cover;
        /// <summary>封面</summary>
        [DisplayName("封面")]
        [Description("封面")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(8, "Cover", "封面", null, "nvarchar(200)", 0, 0, true)]
        public virtual String Cover
        {
            get { return _Cover; }
            set { if (OnPropertyChanging(__.Cover, value)) { _Cover = value; OnPropertyChanged(__.Cover); } }
        }
        #endregion

        #region 获取/设置 字段值
        /// <summary>
        /// 获取/设置 字段值。
        /// 一个索引，基类使用反射实现。
        /// 派生实体类可重写该索引，以避免反射带来的性能损耗
        /// </summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        public override Object this[String name]
        {
            get
            {
                switch (name)
                {
                    case __.SourceID : return _SourceID;
                    case __.SourceName : return _SourceName;
                    case __.SourceUrl : return _SourceUrl;
                    case __.Top : return _Top;
                    case __.Recommend : return _Recommend;
                    case __.Hot : return _Hot;
                    case __.Slide : return _Slide;
                    case __.Cover : return _Cover;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.SourceID : _SourceID = Convert.ToInt32(value); break;
                    case __.SourceName : _SourceName = Convert.ToString(value); break;
                    case __.SourceUrl : _SourceUrl = Convert.ToString(value); break;
                    case __.Top : _Top = Convert.ToBoolean(value); break;
                    case __.Recommend : _Recommend = Convert.ToBoolean(value); break;
                    case __.Hot : _Hot = Convert.ToBoolean(value); break;
                    case __.Slide : _Slide = Convert.ToBoolean(value); break;
                    case __.Cover : _Cover = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得文章字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary>分类</summary>
            public static readonly Field CategoryID = FindByName(__.CategoryID);

            ///<summary>分类名称</summary>
            public static readonly Field CategoryName = FindByName(__.CategoryName);

            ///<summary>标题</summary>
            public static readonly Field Title = FindByName(__.Title);

            ///<summary>最新版本</summary>
            public static readonly Field Version = FindByName(__.Version);

            ///<summary>访问统计</summary>
            public static readonly Field StatisticsID = FindByName(__.StatisticsID);

            ///<summary>访问量。由统计表同步过来</summary>
            public static readonly Field Views = FindByName(__.Views);

            ///<summary>发布时间</summary>
            public static readonly Field PublishTime = FindByName(__.PublishTime);

            ///<summary>创建人</summary>
            public static readonly Field CreateUserID = FindByName(__.CreateUserID);

            ///<summary>创建时间</summary>
            public static readonly Field CreateTime = FindByName(__.CreateTime);

            ///<summary>更新人</summary>
            public static readonly Field UpdateUserID = FindByName(__.UpdateUserID);

            ///<summary>更新时间</summary>
            public static readonly Field UpdateTime = FindByName(__.UpdateTime);

            ///<summary>备注</summary>
            public static readonly Field Remark = FindByName(__.Remark);

            ///<summary>来源</summary>
            public static readonly Field SourceID = FindByName(__.SourceID);

            ///<summary>来源名称</summary>
            public static readonly Field SourceName = FindByName(__.SourceName);

            ///<summary>来源地址</summary>
            public static readonly Field SourceUrl = FindByName(__.SourceUrl);

            ///<summary>置顶</summary>
            public static readonly Field Top = FindByName(__.Top);

            ///<summary>推荐</summary>
            public static readonly Field Recommend = FindByName(__.Recommend);

            ///<summary>热门</summary>
            public static readonly Field Hot = FindByName(__.Hot);

            ///<summary>幻灯片</summary>
            public static readonly Field Slide = FindByName(__.Slide);

            ///<summary>封面</summary>
            public static readonly Field Cover = FindByName(__.Cover);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得文章字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary>编号</summary>
            public const String ID = "ID";

            ///<summary>分类</summary>
            public const String CategoryID = "CategoryID";

            ///<summary>分类名称</summary>
            public const String CategoryName = "CategoryName";

            ///<summary>标题</summary>
            public const String Title = "Title";

            ///<summary>最新版本</summary>
            public const String Version = "Version";

            ///<summary>访问统计</summary>
            public const String StatisticsID = "StatisticsID";

            ///<summary>访问量。由统计表同步过来</summary>
            public const String Views = "Views";

            ///<summary>发布时间</summary>
            public const String PublishTime = "PublishTime";

            ///<summary>创建人</summary>
            public const String CreateUserID = "CreateUserID";

            ///<summary>创建时间</summary>
            public const String CreateTime = "CreateTime";

            ///<summary>更新人</summary>
            public const String UpdateUserID = "UpdateUserID";

            ///<summary>更新时间</summary>
            public const String UpdateTime = "UpdateTime";

            ///<summary>备注</summary>
            public const String Remark = "Remark";

            ///<summary>来源</summary>
            public const String SourceID = "SourceID";

            ///<summary>来源名称</summary>
            public const String SourceName = "SourceName";

            ///<summary>来源地址</summary>
            public const String SourceUrl = "SourceUrl";

            ///<summary>置顶</summary>
            public const String Top = "Top";

            ///<summary>推荐</summary>
            public const String Recommend = "Recommend";

            ///<summary>热门</summary>
            public const String Hot = "Hot";

            ///<summary>幻灯片</summary>
            public const String Slide = "Slide";

            ///<summary>封面</summary>
            public const String Cover = "Cover";

        }
        #endregion
    }

    /// <summary>文章接口</summary>
    public partial interface IArticle : IEntityTitle
    {
        #region 属性
        /// <summary>来源</summary>
        Int32 SourceID { get; set; }

        /// <summary>来源名称</summary>
        String SourceName { get; set; }

        /// <summary>来源地址</summary>
        String SourceUrl { get; set; }

        /// <summary>置顶</summary>
        Boolean Top { get; set; }

        /// <summary>推荐</summary>
        Boolean Recommend { get; set; }

        /// <summary>热门</summary>
        Boolean Hot { get; set; }

        /// <summary>幻灯片</summary>
        Boolean Slide { get; set; }

        /// <summary>封面</summary>
        String Cover { get; set; }
        #endregion
    }
}
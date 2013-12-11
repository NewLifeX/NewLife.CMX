﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace NewLife.CMX
{
    /// <summary>产品内容</summary>
    [Serializable]
    [DataObject]
    [Description("产品内容")]
    [BindIndex("IX_ProductContent_ParentID", false, "ParentID")]
    [BindIndex("IU_ProductContent_ParentID_Version", true, "ParentID,Version")]
    [BindTable("ProductContent", Description = "产品内容", ConnName = "CMX", DbType = DatabaseType.SqlServer)]
    public partial class ProductContent : IProductContent
    {
        #region 属性
        private Int32 _ID;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 10)]
        [BindColumn(1, "ID", "编号", null, "int", 10, 0, false)]
        public virtual Int32 ID
        {
            get { return _ID; }
            set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } }
        }

        private Int32 _ParentID;
        /// <summary>主题</summary>
        [DisplayName("主题")]
        [Description("主题")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(2, "ParentID", "主题", null, "int", 10, 0, false)]
        public virtual Int32 ParentID
        {
            get { return _ParentID; }
            set { if (OnPropertyChanging(__.ParentID, value)) { _ParentID = value; OnPropertyChanged(__.ParentID); } }
        }

        private String _Title;
        /// <summary>标题</summary>
        [DisplayName("标题")]
        [Description("标题")]
        [DataObjectField(false, false, false, 200)]
        [BindColumn(3, "Title", "标题", null, "nvarchar(200)", 0, 0, true)]
        public virtual String Title
        {
            get { return _Title; }
            set { if (OnPropertyChanging(__.Title, value)) { _Title = value; OnPropertyChanged(__.Title); } }
        }

        private String _Specification;
        /// <summary>规格参数</summary>
        [DisplayName("规格参数")]
        [Description("规格参数")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn(4, "Specification", "规格参数", null, "ntext", 0, 0, true)]
        public virtual String Specification
        {
            get { return _Specification; }
            set { if (OnPropertyChanging(__.Specification, value)) { _Specification = value; OnPropertyChanged(__.Specification); } }
        }

        private String _Feature;
        /// <summary>功能特点</summary>
        [DisplayName("功能特点")]
        [Description("功能特点")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn(5, "Feature", "功能特点", null, "ntext", 0, 0, true)]
        public virtual String Feature
        {
            get { return _Feature; }
            set { if (OnPropertyChanging(__.Feature, value)) { _Feature = value; OnPropertyChanged(__.Feature); } }
        }

        private String _App;
        /// <summary>推荐应用</summary>
        [DisplayName("推荐应用")]
        [Description("推荐应用")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn(6, "App", "推荐应用", null, "ntext", 0, 0, true)]
        public virtual String App
        {
            get { return _App; }
            set { if (OnPropertyChanging(__.App, value)) { _App = value; OnPropertyChanged(__.App); } }
        }

        private String _Fitting;
        /// <summary>相关配件</summary>
        [DisplayName("相关配件")]
        [Description("相关配件")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn(7, "Fitting", "相关配件", null, "ntext", 0, 0, true)]
        public virtual String Fitting
        {
            get { return _Fitting; }
            set { if (OnPropertyChanging(__.Fitting, value)) { _Fitting = value; OnPropertyChanged(__.Fitting); } }
        }

        private String _Video;
        /// <summary>产品视频</summary>
        [DisplayName("产品视频")]
        [Description("产品视频")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn(8, "Video", "产品视频", null, "ntext", 0, 0, true)]
        public virtual String Video
        {
            get { return _Video; }
            set { if (OnPropertyChanging(__.Video, value)) { _Video = value; OnPropertyChanged(__.Video); } }
        }

        private Int32 _Version;
        /// <summary>版本</summary>
        [DisplayName("版本")]
        [Description("版本")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(9, "Version", "版本", null, "int", 10, 0, false)]
        public virtual Int32 Version
        {
            get { return _Version; }
            set { if (OnPropertyChanging(__.Version, value)) { _Version = value; OnPropertyChanged(__.Version); } }
        }

        private Int32 _CreateUserID;
        /// <summary>创建人</summary>
        [DisplayName("创建人")]
        [Description("创建人")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(10, "CreateUserID", "创建人", null, "int", 10, 0, false)]
        public virtual Int32 CreateUserID
        {
            get { return _CreateUserID; }
            set { if (OnPropertyChanging(__.CreateUserID, value)) { _CreateUserID = value; OnPropertyChanged(__.CreateUserID); } }
        }

        private String _CreateUserName;
        /// <summary>创建人</summary>
        [DisplayName("创建人")]
        [Description("创建人")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(11, "CreateUserName", "创建人", null, "nvarchar(50)", 0, 0, true)]
        public virtual String CreateUserName
        {
            get { return _CreateUserName; }
            set { if (OnPropertyChanging(__.CreateUserName, value)) { _CreateUserName = value; OnPropertyChanged(__.CreateUserName); } }
        }

        private DateTime _CreateTime;
        /// <summary>创建时间</summary>
        [DisplayName("创建时间")]
        [Description("创建时间")]
        [DataObjectField(false, false, true, 3)]
        [BindColumn(12, "CreateTime", "创建时间", null, "datetime", 3, 0, false)]
        public virtual DateTime CreateTime
        {
            get { return _CreateTime; }
            set { if (OnPropertyChanging(__.CreateTime, value)) { _CreateTime = value; OnPropertyChanged(__.CreateTime); } }
        }

        private String _Content;
        /// <summary>内容</summary>
        [DisplayName("内容")]
        [Description("内容")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn(13, "Content", "内容", null, "ntext", 0, 0, true)]
        public virtual String Content
        {
            get { return _Content; }
            set { if (OnPropertyChanging(__.Content, value)) { _Content = value; OnPropertyChanged(__.Content); } }
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
                    case __.ID : return _ID;
                    case __.ParentID : return _ParentID;
                    case __.Title : return _Title;
                    case __.Specification : return _Specification;
                    case __.Feature : return _Feature;
                    case __.App : return _App;
                    case __.Fitting : return _Fitting;
                    case __.Video : return _Video;
                    case __.Version : return _Version;
                    case __.CreateUserID : return _CreateUserID;
                    case __.CreateUserName : return _CreateUserName;
                    case __.CreateTime : return _CreateTime;
                    case __.Content : return _Content;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.ParentID : _ParentID = Convert.ToInt32(value); break;
                    case __.Title : _Title = Convert.ToString(value); break;
                    case __.Specification : _Specification = Convert.ToString(value); break;
                    case __.Feature : _Feature = Convert.ToString(value); break;
                    case __.App : _App = Convert.ToString(value); break;
                    case __.Fitting : _Fitting = Convert.ToString(value); break;
                    case __.Video : _Video = Convert.ToString(value); break;
                    case __.Version : _Version = Convert.ToInt32(value); break;
                    case __.CreateUserID : _CreateUserID = Convert.ToInt32(value); break;
                    case __.CreateUserName : _CreateUserName = Convert.ToString(value); break;
                    case __.CreateTime : _CreateTime = Convert.ToDateTime(value); break;
                    case __.Content : _Content = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得产品内容字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary>主题</summary>
            public static readonly Field ParentID = FindByName(__.ParentID);

            ///<summary>标题</summary>
            public static readonly Field Title = FindByName(__.Title);

            ///<summary>规格参数</summary>
            public static readonly Field Specification = FindByName(__.Specification);

            ///<summary>功能特点</summary>
            public static readonly Field Feature = FindByName(__.Feature);

            ///<summary>推荐应用</summary>
            public static readonly Field App = FindByName(__.App);

            ///<summary>相关配件</summary>
            public static readonly Field Fitting = FindByName(__.Fitting);

            ///<summary>产品视频</summary>
            public static readonly Field Video = FindByName(__.Video);

            ///<summary>版本</summary>
            public static readonly Field Version = FindByName(__.Version);

            ///<summary>创建人</summary>
            public static readonly Field CreateUserID = FindByName(__.CreateUserID);

            ///<summary>创建人</summary>
            public static readonly Field CreateUserName = FindByName(__.CreateUserName);

            ///<summary>创建时间</summary>
            public static readonly Field CreateTime = FindByName(__.CreateTime);

            ///<summary>内容</summary>
            public static readonly Field Content = FindByName(__.Content);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得产品内容字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary>编号</summary>
            public const String ID = "ID";

            ///<summary>主题</summary>
            public const String ParentID = "ParentID";

            ///<summary>标题</summary>
            public const String Title = "Title";

            ///<summary>规格参数</summary>
            public const String Specification = "Specification";

            ///<summary>功能特点</summary>
            public const String Feature = "Feature";

            ///<summary>推荐应用</summary>
            public const String App = "App";

            ///<summary>相关配件</summary>
            public const String Fitting = "Fitting";

            ///<summary>产品视频</summary>
            public const String Video = "Video";

            ///<summary>版本</summary>
            public const String Version = "Version";

            ///<summary>创建人</summary>
            public const String CreateUserID = "CreateUserID";

            ///<summary>创建人</summary>
            public const String CreateUserName = "CreateUserName";

            ///<summary>创建时间</summary>
            public const String CreateTime = "CreateTime";

            ///<summary>内容</summary>
            public const String Content = "Content";

        }
        #endregion
    }

    /// <summary>产品内容接口</summary>
    public partial interface IProductContent
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>主题</summary>
        Int32 ParentID { get; set; }

        /// <summary>标题</summary>
        String Title { get; set; }

        /// <summary>规格参数</summary>
        String Specification { get; set; }

        /// <summary>功能特点</summary>
        String Feature { get; set; }

        /// <summary>推荐应用</summary>
        String App { get; set; }

        /// <summary>相关配件</summary>
        String Fitting { get; set; }

        /// <summary>产品视频</summary>
        String Video { get; set; }

        /// <summary>版本</summary>
        Int32 Version { get; set; }

        /// <summary>创建人</summary>
        Int32 CreateUserID { get; set; }

        /// <summary>创建人</summary>
        String CreateUserName { get; set; }

        /// <summary>创建时间</summary>
        DateTime CreateTime { get; set; }

        /// <summary>内容</summary>
        String Content { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
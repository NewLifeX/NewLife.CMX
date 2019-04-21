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
    [BindTable("ProductContent", Description = "产品内容", ConnName = "CMX", DbType = DatabaseType.SqlServer)]
    public partial class ProductContent : IProductContent
    {
        #region 属性
        private String _Specification;
        /// <summary>规格参数</summary>
        [DisplayName("规格参数")]
        [Description("规格参数")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn(1, "Specification", "规格参数", null, "ntext", 0, 0, true)]
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
        [BindColumn(2, "Feature", "功能特点", null, "ntext", 0, 0, true)]
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
        [BindColumn(3, "App", "推荐应用", null, "ntext", 0, 0, true)]
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
        [BindColumn(4, "Fitting", "相关配件", null, "ntext", 0, 0, true)]
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
        [BindColumn(5, "Video", "产品视频", null, "ntext", 0, 0, true)]
        public virtual String Video
        {
            get { return _Video; }
            set { if (OnPropertyChanging(__.Video, value)) { _Video = value; OnPropertyChanged(__.Video); } }
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
                    case __.Specification : return _Specification;
                    case __.Feature : return _Feature;
                    case __.App : return _App;
                    case __.Fitting : return _Fitting;
                    case __.Video : return _Video;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Specification : _Specification = Convert.ToString(value); break;
                    case __.Feature : _Feature = Convert.ToString(value); break;
                    case __.App : _App = Convert.ToString(value); break;
                    case __.Fitting : _Fitting = Convert.ToString(value); break;
                    case __.Video : _Video = Convert.ToString(value); break;
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

        }
        #endregion
    }

    /// <summary>产品内容接口</summary>
    public partial interface IProductContent : IEntityContent
    {
        #region 属性
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
        #endregion
    }
}
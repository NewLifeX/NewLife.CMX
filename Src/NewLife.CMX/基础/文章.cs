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
    [BindIndex("IX_Article_TitleID", false, "TitleID")]
    [BindIndex("IX_Article_TitleID_Page", true, "TitleID,Page")]
    [BindTable("Article", Description = "文章", ConnName = "CMX", DbType = DatabaseType.SqlServer)]
    public partial class Article : IArticle
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

        private Int32 _TitleID;
        /// <summary>标题</summary>
        [DisplayName("标题")]
        [Description("标题")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(2, "TitleID", "标题", null, "int", 10, 0, false)]
        public virtual Int32 TitleID
        {
            get { return _TitleID; }
            set { if (OnPropertyChanging(__.TitleID, value)) { _TitleID = value; OnPropertyChanged(__.TitleID); } }
        }

        private Int32 _Page;
        /// <summary>页码</summary>
        [DisplayName("页码")]
        [Description("页码")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(3, "Page", "页码", null, "int", 10, 0, false)]
        public virtual Int32 Page
        {
            get { return _Page; }
            set { if (OnPropertyChanging(__.Page, value)) { _Page = value; OnPropertyChanged(__.Page); } }
        }

        private String _Content;
        /// <summary>内容</summary>
        [DisplayName("内容")]
        [Description("内容")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn(4, "Content", "内容", null, "ntext", 0, 0, true)]
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
                    case __.TitleID : return _TitleID;
                    case __.Page : return _Page;
                    case __.Content : return _Content;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.TitleID : _TitleID = Convert.ToInt32(value); break;
                    case __.Page : _Page = Convert.ToInt32(value); break;
                    case __.Content : _Content = Convert.ToString(value); break;
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

            ///<summary>标题</summary>
            public static readonly Field TitleID = FindByName(__.TitleID);

            ///<summary>页码</summary>
            public static readonly Field Page = FindByName(__.Page);

            ///<summary>内容</summary>
            public static readonly Field Content = FindByName(__.Content);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得文章字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary>编号</summary>
            public const String ID = "ID";

            ///<summary>标题</summary>
            public const String TitleID = "TitleID";

            ///<summary>页码</summary>
            public const String Page = "Page";

            ///<summary>内容</summary>
            public const String Content = "Content";

        }
        #endregion
    }

    /// <summary>文章接口</summary>
    public partial interface IArticle
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>标题</summary>
        Int32 TitleID { get; set; }

        /// <summary>页码</summary>
        Int32 Page { get; set; }

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
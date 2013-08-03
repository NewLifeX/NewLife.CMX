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
    [BindIndex("IX_Article_SubjectID", false, "SubjectID")]
    [BindIndex("IX_Article_SubjectID_Version", false, "SubjectID,Version")]
    [BindIndex("IX_Article_SubjectID_Version_Page", true, "SubjectID,Version,Page")]
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

        private Int32 _SubjectID;
        /// <summary>标题</summary>
        [DisplayName("标题")]
        [Description("标题")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(2, "SubjectID", "标题", null, "int", 10, 0, false)]
        public virtual Int32 SubjectID
        {
            get { return _SubjectID; }
            set { if (OnPropertyChanging(__.SubjectID, value)) { _SubjectID = value; OnPropertyChanged(__.SubjectID); } }
        }

        private String _Name;
        /// <summary>标题</summary>
        [DisplayName("标题")]
        [Description("标题")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(3, "Name", "标题", null, "nvarchar(50)", 0, 0, true)]
        public virtual String Name
        {
            get { return _Name; }
            set { if (OnPropertyChanging(__.Name, value)) { _Name = value; OnPropertyChanged(__.Name); } }
        }

        private Int32 _Version;
        /// <summary>版本</summary>
        [DisplayName("版本")]
        [Description("版本")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(4, "Version", "版本", null, "int", 10, 0, false)]
        public virtual Int32 Version
        {
            get { return _Version; }
            set { if (OnPropertyChanging(__.Version, value)) { _Version = value; OnPropertyChanged(__.Version); } }
        }

        private Int32 _Page;
        /// <summary>页码</summary>
        [DisplayName("页码")]
        [Description("页码")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(5, "Page", "页码", null, "int", 10, 0, false)]
        public virtual Int32 Page
        {
            get { return _Page; }
            set { if (OnPropertyChanging(__.Page, value)) { _Page = value; OnPropertyChanged(__.Page); } }
        }

        private Int32 _UpdateUser;
        /// <summary>更新人</summary>
        [DisplayName("更新人")]
        [Description("更新人")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(6, "UpdateUser", "更新人", null, "int", 10, 0, false)]
        public virtual Int32 UpdateUser
        {
            get { return _UpdateUser; }
            set { if (OnPropertyChanging(__.UpdateUser, value)) { _UpdateUser = value; OnPropertyChanged(__.UpdateUser); } }
        }

        private String _UpdateName;
        /// <summary>更新人</summary>
        [DisplayName("更新人")]
        [Description("更新人")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(7, "UpdateName", "更新人", null, "nvarchar(50)", 0, 0, true)]
        public virtual String UpdateName
        {
            get { return _UpdateName; }
            set { if (OnPropertyChanging(__.UpdateName, value)) { _UpdateName = value; OnPropertyChanged(__.UpdateName); } }
        }

        private DateTime _UpdateTime;
        /// <summary>更新时间</summary>
        [DisplayName("更新时间")]
        [Description("更新时间")]
        [DataObjectField(false, false, true, 3)]
        [BindColumn(8, "UpdateTime", "更新时间", null, "datetime", 3, 0, false)]
        public virtual DateTime UpdateTime
        {
            get { return _UpdateTime; }
            set { if (OnPropertyChanging(__.UpdateTime, value)) { _UpdateTime = value; OnPropertyChanged(__.UpdateTime); } }
        }

        private String _Content;
        /// <summary>内容</summary>
        [DisplayName("内容")]
        [Description("内容")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn(9, "Content", "内容", null, "ntext", 0, 0, true)]
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
                    case __.SubjectID : return _SubjectID;
                    case __.Name : return _Name;
                    case __.Version : return _Version;
                    case __.Page : return _Page;
                    case __.UpdateUser : return _UpdateUser;
                    case __.UpdateName : return _UpdateName;
                    case __.UpdateTime : return _UpdateTime;
                    case __.Content : return _Content;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.SubjectID : _SubjectID = Convert.ToInt32(value); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.Version : _Version = Convert.ToInt32(value); break;
                    case __.Page : _Page = Convert.ToInt32(value); break;
                    case __.UpdateUser : _UpdateUser = Convert.ToInt32(value); break;
                    case __.UpdateName : _UpdateName = Convert.ToString(value); break;
                    case __.UpdateTime : _UpdateTime = Convert.ToDateTime(value); break;
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
            public static readonly Field SubjectID = FindByName(__.SubjectID);

            ///<summary>标题</summary>
            public static readonly Field Name = FindByName(__.Name);

            ///<summary>版本</summary>
            public static readonly Field Version = FindByName(__.Version);

            ///<summary>页码</summary>
            public static readonly Field Page = FindByName(__.Page);

            ///<summary>更新人</summary>
            public static readonly Field UpdateUser = FindByName(__.UpdateUser);

            ///<summary>更新人</summary>
            public static readonly Field UpdateName = FindByName(__.UpdateName);

            ///<summary>更新时间</summary>
            public static readonly Field UpdateTime = FindByName(__.UpdateTime);

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
            public const String SubjectID = "SubjectID";

            ///<summary>标题</summary>
            public const String Name = "Name";

            ///<summary>版本</summary>
            public const String Version = "Version";

            ///<summary>页码</summary>
            public const String Page = "Page";

            ///<summary>更新人</summary>
            public const String UpdateUser = "UpdateUser";

            ///<summary>更新人</summary>
            public const String UpdateName = "UpdateName";

            ///<summary>更新时间</summary>
            public const String UpdateTime = "UpdateTime";

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
        Int32 SubjectID { get; set; }

        /// <summary>标题</summary>
        String Name { get; set; }

        /// <summary>版本</summary>
        Int32 Version { get; set; }

        /// <summary>页码</summary>
        Int32 Page { get; set; }

        /// <summary>更新人</summary>
        Int32 UpdateUser { get; set; }

        /// <summary>更新人</summary>
        String UpdateName { get; set; }

        /// <summary>更新时间</summary>
        DateTime UpdateTime { get; set; }

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
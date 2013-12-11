﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace NewLife.CMX
{
    /// <summary>文本</summary>
    [Serializable]
    [DataObject]
    [Description("文本")]
    [BindIndex("IX_Text_CategoryID", false, "CategoryID")]
    [BindTable("Text", Description = "文本", ConnName = "CMX", DbType = DatabaseType.SqlServer)]
    public partial class Text : IText
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

        private Int32 _CategoryID;
        /// <summary>分类</summary>
        [DisplayName("分类")]
        [Description("分类")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(2, "CategoryID", "分类", null, "int", 10, 0, false)]
        public virtual Int32 CategoryID
        {
            get { return _CategoryID; }
            set { if (OnPropertyChanging(__.CategoryID, value)) { _CategoryID = value; OnPropertyChanged(__.CategoryID); } }
        }

        private String _CategoryName;
        /// <summary>分类名称</summary>
        [DisplayName("分类名称")]
        [Description("分类名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(3, "CategoryName", "分类名称", null, "nvarchar(50)", 0, 0, true)]
        public virtual String CategoryName
        {
            get { return _CategoryName; }
            set { if (OnPropertyChanging(__.CategoryName, value)) { _CategoryName = value; OnPropertyChanged(__.CategoryName); } }
        }

        private String _Title;
        /// <summary>标题</summary>
        [DisplayName("标题")]
        [Description("标题")]
        [DataObjectField(false, false, false, 200)]
        [BindColumn(4, "Title", "标题", null, "nvarchar(200)", 0, 0, true)]
        public virtual String Title
        {
            get { return _Title; }
            set { if (OnPropertyChanging(__.Title, value)) { _Title = value; OnPropertyChanged(__.Title); } }
        }

        private Int32 _Version;
        /// <summary>最新版本</summary>
        [DisplayName("最新版本")]
        [Description("最新版本")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(5, "Version", "最新版本", null, "int", 10, 0, false)]
        public virtual Int32 Version
        {
            get { return _Version; }
            set { if (OnPropertyChanging(__.Version, value)) { _Version = value; OnPropertyChanged(__.Version); } }
        }

        private Int32 _StatisticsID;
        /// <summary>访问统计</summary>
        [DisplayName("访问统计")]
        [Description("访问统计")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(6, "StatisticsID", "访问统计", null, "int", 10, 0, false)]
        public virtual Int32 StatisticsID
        {
            get { return _StatisticsID; }
            set { if (OnPropertyChanging(__.StatisticsID, value)) { _StatisticsID = value; OnPropertyChanged(__.StatisticsID); } }
        }

        private Int32 _CreateUserID;
        /// <summary>创建人</summary>
        [DisplayName("创建人")]
        [Description("创建人")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(7, "CreateUserID", "创建人", null, "int", 10, 0, false)]
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
        [BindColumn(8, "CreateUserName", "创建人", null, "nvarchar(50)", 0, 0, true)]
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
        [BindColumn(9, "CreateTime", "创建时间", null, "datetime", 3, 0, false)]
        public virtual DateTime CreateTime
        {
            get { return _CreateTime; }
            set { if (OnPropertyChanging(__.CreateTime, value)) { _CreateTime = value; OnPropertyChanged(__.CreateTime); } }
        }

        private Int32 _UpdateUserID;
        /// <summary>更新人</summary>
        [DisplayName("更新人")]
        [Description("更新人")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(10, "UpdateUserID", "更新人", null, "int", 10, 0, false)]
        public virtual Int32 UpdateUserID
        {
            get { return _UpdateUserID; }
            set { if (OnPropertyChanging(__.UpdateUserID, value)) { _UpdateUserID = value; OnPropertyChanged(__.UpdateUserID); } }
        }

        private String _UpdateUserName;
        /// <summary>更新人</summary>
        [DisplayName("更新人")]
        [Description("更新人")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(11, "UpdateUserName", "更新人", null, "nvarchar(50)", 0, 0, true)]
        public virtual String UpdateUserName
        {
            get { return _UpdateUserName; }
            set { if (OnPropertyChanging(__.UpdateUserName, value)) { _UpdateUserName = value; OnPropertyChanged(__.UpdateUserName); } }
        }

        private DateTime _UpdateTime;
        /// <summary>更新时间</summary>
        [DisplayName("更新时间")]
        [Description("更新时间")]
        [DataObjectField(false, false, true, 3)]
        [BindColumn(12, "UpdateTime", "更新时间", null, "datetime", 3, 0, false)]
        public virtual DateTime UpdateTime
        {
            get { return _UpdateTime; }
            set { if (OnPropertyChanging(__.UpdateTime, value)) { _UpdateTime = value; OnPropertyChanged(__.UpdateTime); } }
        }

        private String _Remark;
        /// <summary>备注</summary>
        [DisplayName("备注")]
        [Description("备注")]
        [DataObjectField(false, false, true, 500)]
        [BindColumn(13, "Remark", "备注", null, "nvarchar(500)", 0, 0, true)]
        public virtual String Remark
        {
            get { return _Remark; }
            set { if (OnPropertyChanging(__.Remark, value)) { _Remark = value; OnPropertyChanged(__.Remark); } }
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
                    case __.CategoryID : return _CategoryID;
                    case __.CategoryName : return _CategoryName;
                    case __.Title : return _Title;
                    case __.Version : return _Version;
                    case __.StatisticsID : return _StatisticsID;
                    case __.CreateUserID : return _CreateUserID;
                    case __.CreateUserName : return _CreateUserName;
                    case __.CreateTime : return _CreateTime;
                    case __.UpdateUserID : return _UpdateUserID;
                    case __.UpdateUserName : return _UpdateUserName;
                    case __.UpdateTime : return _UpdateTime;
                    case __.Remark : return _Remark;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.CategoryID : _CategoryID = Convert.ToInt32(value); break;
                    case __.CategoryName : _CategoryName = Convert.ToString(value); break;
                    case __.Title : _Title = Convert.ToString(value); break;
                    case __.Version : _Version = Convert.ToInt32(value); break;
                    case __.StatisticsID : _StatisticsID = Convert.ToInt32(value); break;
                    case __.CreateUserID : _CreateUserID = Convert.ToInt32(value); break;
                    case __.CreateUserName : _CreateUserName = Convert.ToString(value); break;
                    case __.CreateTime : _CreateTime = Convert.ToDateTime(value); break;
                    case __.UpdateUserID : _UpdateUserID = Convert.ToInt32(value); break;
                    case __.UpdateUserName : _UpdateUserName = Convert.ToString(value); break;
                    case __.UpdateTime : _UpdateTime = Convert.ToDateTime(value); break;
                    case __.Remark : _Remark = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得文本字段信息的快捷方式</summary>
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

            ///<summary>创建人</summary>
            public static readonly Field CreateUserID = FindByName(__.CreateUserID);

            ///<summary>创建人</summary>
            public static readonly Field CreateUserName = FindByName(__.CreateUserName);

            ///<summary>创建时间</summary>
            public static readonly Field CreateTime = FindByName(__.CreateTime);

            ///<summary>更新人</summary>
            public static readonly Field UpdateUserID = FindByName(__.UpdateUserID);

            ///<summary>更新人</summary>
            public static readonly Field UpdateUserName = FindByName(__.UpdateUserName);

            ///<summary>更新时间</summary>
            public static readonly Field UpdateTime = FindByName(__.UpdateTime);

            ///<summary>备注</summary>
            public static readonly Field Remark = FindByName(__.Remark);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得文本字段名称的快捷方式</summary>
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

            ///<summary>创建人</summary>
            public const String CreateUserID = "CreateUserID";

            ///<summary>创建人</summary>
            public const String CreateUserName = "CreateUserName";

            ///<summary>创建时间</summary>
            public const String CreateTime = "CreateTime";

            ///<summary>更新人</summary>
            public const String UpdateUserID = "UpdateUserID";

            ///<summary>更新人</summary>
            public const String UpdateUserName = "UpdateUserName";

            ///<summary>更新时间</summary>
            public const String UpdateTime = "UpdateTime";

            ///<summary>备注</summary>
            public const String Remark = "Remark";

        }
        #endregion
    }

    /// <summary>文本接口</summary>
    public partial interface IText
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>分类</summary>
        Int32 CategoryID { get; set; }

        /// <summary>分类名称</summary>
        String CategoryName { get; set; }

        /// <summary>标题</summary>
        String Title { get; set; }

        /// <summary>最新版本</summary>
        Int32 Version { get; set; }

        /// <summary>访问统计</summary>
        Int32 StatisticsID { get; set; }

        /// <summary>创建人</summary>
        Int32 CreateUserID { get; set; }

        /// <summary>创建人</summary>
        String CreateUserName { get; set; }

        /// <summary>创建时间</summary>
        DateTime CreateTime { get; set; }

        /// <summary>更新人</summary>
        Int32 UpdateUserID { get; set; }

        /// <summary>更新人</summary>
        String UpdateUserName { get; set; }

        /// <summary>更新时间</summary>
        DateTime UpdateTime { get; set; }

        /// <summary>备注</summary>
        String Remark { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
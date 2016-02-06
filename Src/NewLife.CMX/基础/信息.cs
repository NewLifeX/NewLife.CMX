﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace NewLife.CMX
{
    /// <summary>信息</summary>
    [Serializable]
    [DataObject]
    [Description("信息")]
    [BindIndex("IX_Info_ModelID", false, "ModelID")]
    [BindIndex("IX_Info_ExtendID", false, "ExtendID")]
    [BindIndex("IX_Info_CategoryID", false, "CategoryID")]
    [BindIndex("IX_Info_PublishTime", false, "PublishTime")]
    [BindTable("Info", Description = "信息", ConnName = "CMX", DbType = DatabaseType.None)]
    public abstract partial class Info<TEntity> : IInfo
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

        private Int32 _ModelID;
        /// <summary>模型</summary>
        [DisplayName("模型")]
        [Description("模型")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(2, "ModelID", "模型", null, "int", 10, 0, false)]
        public virtual Int32 ModelID
        {
            get { return _ModelID; }
            set { if (OnPropertyChanging(__.ModelID, value)) { _ModelID = value; OnPropertyChanged(__.ModelID); } }
        }

        private Int32 _CategoryID;
        /// <summary>分类</summary>
        [DisplayName("分类")]
        [Description("分类")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(3, "CategoryID", "分类", null, "int", 10, 0, false)]
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
        [BindColumn(4, "CategoryName", "分类名称", null, "nvarchar(50)", 0, 0, true)]
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
        [BindColumn(5, "Title", "标题", null, "nvarchar(200)", 0, 0, true, Master=true)]
        public virtual String Title
        {
            get { return _Title; }
            set { if (OnPropertyChanging(__.Title, value)) { _Title = value; OnPropertyChanged(__.Title); } }
        }

        private Int32 _ExtendID;
        /// <summary>扩展</summary>
        [DisplayName("扩展")]
        [Description("扩展")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(6, "ExtendID", "扩展", null, "int", 10, 0, false)]
        public virtual Int32 ExtendID
        {
            get { return _ExtendID; }
            set { if (OnPropertyChanging(__.ExtendID, value)) { _ExtendID = value; OnPropertyChanged(__.ExtendID); } }
        }

        private Int32 _Version;
        /// <summary>最新版本</summary>
        [DisplayName("最新版本")]
        [Description("最新版本")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(7, "Version", "最新版本", null, "int", 10, 0, false)]
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
        [BindColumn(8, "StatisticsID", "访问统计", null, "int", 10, 0, false)]
        public virtual Int32 StatisticsID
        {
            get { return _StatisticsID; }
            set { if (OnPropertyChanging(__.StatisticsID, value)) { _StatisticsID = value; OnPropertyChanged(__.StatisticsID); } }
        }

        private Int32 _Views;
        /// <summary>访问量。由统计表同步过来</summary>
        [DisplayName("访问量")]
        [Description("访问量。由统计表同步过来")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(9, "Views", "访问量。由统计表同步过来", null, "int", 10, 0, false)]
        public virtual Int32 Views
        {
            get { return _Views; }
            set { if (OnPropertyChanging(__.Views, value)) { _Views = value; OnPropertyChanged(__.Views); } }
        }

        private String _Summary;
        /// <summary>摘要</summary>
        [DisplayName("摘要")]
        [Description("摘要")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(10, "Summary", "摘要", null, "nvarchar(200)", 0, 0, true)]
        public virtual String Summary
        {
            get { return _Summary; }
            set { if (OnPropertyChanging(__.Summary, value)) { _Summary = value; OnPropertyChanged(__.Summary); } }
        }

        private DateTime _PublishTime;
        /// <summary>发布时间</summary>
        [DisplayName("发布时间")]
        [Description("发布时间")]
        [DataObjectField(false, false, true, 3)]
        [BindColumn(11, "PublishTime", "发布时间", null, "datetime", 3, 0, false)]
        public virtual DateTime PublishTime
        {
            get { return _PublishTime; }
            set { if (OnPropertyChanging(__.PublishTime, value)) { _PublishTime = value; OnPropertyChanged(__.PublishTime); } }
        }

        private String _Publisher;
        /// <summary>发布人</summary>
        [DisplayName("发布人")]
        [Description("发布人")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(12, "Publisher", "发布人", null, "nvarchar(50)", 0, 0, true)]
        public virtual String Publisher
        {
            get { return _Publisher; }
            set { if (OnPropertyChanging(__.Publisher, value)) { _Publisher = value; OnPropertyChanged(__.Publisher); } }
        }

        private Int32 _CreateUserID;
        /// <summary>创建人</summary>
        [DisplayName("创建人")]
        [Description("创建人")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(13, "CreateUserID", "创建人", null, "int", 10, 0, false)]
        public virtual Int32 CreateUserID
        {
            get { return _CreateUserID; }
            set { if (OnPropertyChanging(__.CreateUserID, value)) { _CreateUserID = value; OnPropertyChanged(__.CreateUserID); } }
        }

        private DateTime _CreateTime;
        /// <summary>创建时间</summary>
        [DisplayName("创建时间")]
        [Description("创建时间")]
        [DataObjectField(false, false, true, 3)]
        [BindColumn(14, "CreateTime", "创建时间", null, "datetime", 3, 0, false)]
        public virtual DateTime CreateTime
        {
            get { return _CreateTime; }
            set { if (OnPropertyChanging(__.CreateTime, value)) { _CreateTime = value; OnPropertyChanged(__.CreateTime); } }
        }

        private String _CreateIP;
        /// <summary>创建地址</summary>
        [DisplayName("创建地址")]
        [Description("创建地址")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(15, "CreateIP", "创建地址", null, "nvarchar(50)", 0, 0, true)]
        public virtual String CreateIP
        {
            get { return _CreateIP; }
            set { if (OnPropertyChanging(__.CreateIP, value)) { _CreateIP = value; OnPropertyChanged(__.CreateIP); } }
        }

        private Int32 _UpdateUserID;
        /// <summary>更新人</summary>
        [DisplayName("更新人")]
        [Description("更新人")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(16, "UpdateUserID", "更新人", null, "int", 10, 0, false)]
        public virtual Int32 UpdateUserID
        {
            get { return _UpdateUserID; }
            set { if (OnPropertyChanging(__.UpdateUserID, value)) { _UpdateUserID = value; OnPropertyChanged(__.UpdateUserID); } }
        }

        private DateTime _UpdateTime;
        /// <summary>更新时间</summary>
        [DisplayName("更新时间")]
        [Description("更新时间")]
        [DataObjectField(false, false, true, 3)]
        [BindColumn(17, "UpdateTime", "更新时间", null, "datetime", 3, 0, false)]
        public virtual DateTime UpdateTime
        {
            get { return _UpdateTime; }
            set { if (OnPropertyChanging(__.UpdateTime, value)) { _UpdateTime = value; OnPropertyChanged(__.UpdateTime); } }
        }

        private String _UpdateIP;
        /// <summary>更新地址</summary>
        [DisplayName("更新地址")]
        [Description("更新地址")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(18, "UpdateIP", "更新地址", null, "nvarchar(50)", 0, 0, true)]
        public virtual String UpdateIP
        {
            get { return _UpdateIP; }
            set { if (OnPropertyChanging(__.UpdateIP, value)) { _UpdateIP = value; OnPropertyChanged(__.UpdateIP); } }
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
                    case __.ModelID : return _ModelID;
                    case __.CategoryID : return _CategoryID;
                    case __.CategoryName : return _CategoryName;
                    case __.Title : return _Title;
                    case __.ExtendID : return _ExtendID;
                    case __.Version : return _Version;
                    case __.StatisticsID : return _StatisticsID;
                    case __.Views : return _Views;
                    case __.Summary : return _Summary;
                    case __.PublishTime : return _PublishTime;
                    case __.Publisher : return _Publisher;
                    case __.CreateUserID : return _CreateUserID;
                    case __.CreateTime : return _CreateTime;
                    case __.CreateIP : return _CreateIP;
                    case __.UpdateUserID : return _UpdateUserID;
                    case __.UpdateTime : return _UpdateTime;
                    case __.UpdateIP : return _UpdateIP;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.ModelID : _ModelID = Convert.ToInt32(value); break;
                    case __.CategoryID : _CategoryID = Convert.ToInt32(value); break;
                    case __.CategoryName : _CategoryName = Convert.ToString(value); break;
                    case __.Title : _Title = Convert.ToString(value); break;
                    case __.ExtendID : _ExtendID = Convert.ToInt32(value); break;
                    case __.Version : _Version = Convert.ToInt32(value); break;
                    case __.StatisticsID : _StatisticsID = Convert.ToInt32(value); break;
                    case __.Views : _Views = Convert.ToInt32(value); break;
                    case __.Summary : _Summary = Convert.ToString(value); break;
                    case __.PublishTime : _PublishTime = Convert.ToDateTime(value); break;
                    case __.Publisher : _Publisher = Convert.ToString(value); break;
                    case __.CreateUserID : _CreateUserID = Convert.ToInt32(value); break;
                    case __.CreateTime : _CreateTime = Convert.ToDateTime(value); break;
                    case __.CreateIP : _CreateIP = Convert.ToString(value); break;
                    case __.UpdateUserID : _UpdateUserID = Convert.ToInt32(value); break;
                    case __.UpdateTime : _UpdateTime = Convert.ToDateTime(value); break;
                    case __.UpdateIP : _UpdateIP = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得信息字段信息的快捷方式</summary>
        partial class _
        {
            ///<summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary>模型</summary>
            public static readonly Field ModelID = FindByName(__.ModelID);

            ///<summary>分类</summary>
            public static readonly Field CategoryID = FindByName(__.CategoryID);

            ///<summary>分类名称</summary>
            public static readonly Field CategoryName = FindByName(__.CategoryName);

            ///<summary>标题</summary>
            public static readonly Field Title = FindByName(__.Title);

            ///<summary>扩展</summary>
            public static readonly Field ExtendID = FindByName(__.ExtendID);

            ///<summary>最新版本</summary>
            public static readonly Field Version = FindByName(__.Version);

            ///<summary>访问统计</summary>
            public static readonly Field StatisticsID = FindByName(__.StatisticsID);

            ///<summary>访问量。由统计表同步过来</summary>
            public static readonly Field Views = FindByName(__.Views);

            ///<summary>摘要</summary>
            public static readonly Field Summary = FindByName(__.Summary);

            ///<summary>发布时间</summary>
            public static readonly Field PublishTime = FindByName(__.PublishTime);

            ///<summary>发布人</summary>
            public static readonly Field Publisher = FindByName(__.Publisher);

            ///<summary>创建人</summary>
            public static readonly Field CreateUserID = FindByName(__.CreateUserID);

            ///<summary>创建时间</summary>
            public static readonly Field CreateTime = FindByName(__.CreateTime);

            ///<summary>创建地址</summary>
            public static readonly Field CreateIP = FindByName(__.CreateIP);

            ///<summary>更新人</summary>
            public static readonly Field UpdateUserID = FindByName(__.UpdateUserID);

            ///<summary>更新时间</summary>
            public static readonly Field UpdateTime = FindByName(__.UpdateTime);

            ///<summary>更新地址</summary>
            public static readonly Field UpdateIP = FindByName(__.UpdateIP);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得信息字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary>编号</summary>
            public const String ID = "ID";

            ///<summary>模型</summary>
            public const String ModelID = "ModelID";

            ///<summary>分类</summary>
            public const String CategoryID = "CategoryID";

            ///<summary>分类名称</summary>
            public const String CategoryName = "CategoryName";

            ///<summary>标题</summary>
            public const String Title = "Title";

            ///<summary>扩展</summary>
            public const String ExtendID = "ExtendID";

            ///<summary>最新版本</summary>
            public const String Version = "Version";

            ///<summary>访问统计</summary>
            public const String StatisticsID = "StatisticsID";

            ///<summary>访问量。由统计表同步过来</summary>
            public const String Views = "Views";

            ///<summary>摘要</summary>
            public const String Summary = "Summary";

            ///<summary>发布时间</summary>
            public const String PublishTime = "PublishTime";

            ///<summary>发布人</summary>
            public const String Publisher = "Publisher";

            ///<summary>创建人</summary>
            public const String CreateUserID = "CreateUserID";

            ///<summary>创建时间</summary>
            public const String CreateTime = "CreateTime";

            ///<summary>创建地址</summary>
            public const String CreateIP = "CreateIP";

            ///<summary>更新人</summary>
            public const String UpdateUserID = "UpdateUserID";

            ///<summary>更新时间</summary>
            public const String UpdateTime = "UpdateTime";

            ///<summary>更新地址</summary>
            public const String UpdateIP = "UpdateIP";

        }
        #endregion
    }

    /// <summary>信息接口</summary>
    public partial interface IInfo
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>模型</summary>
        Int32 ModelID { get; set; }

        /// <summary>分类</summary>
        Int32 CategoryID { get; set; }

        /// <summary>分类名称</summary>
        String CategoryName { get; set; }

        /// <summary>标题</summary>
        String Title { get; set; }

        /// <summary>扩展</summary>
        Int32 ExtendID { get; set; }

        /// <summary>最新版本</summary>
        Int32 Version { get; set; }

        /// <summary>访问统计</summary>
        Int32 StatisticsID { get; set; }

        /// <summary>访问量。由统计表同步过来</summary>
        Int32 Views { get; set; }

        /// <summary>摘要</summary>
        String Summary { get; set; }

        /// <summary>发布时间</summary>
        DateTime PublishTime { get; set; }

        /// <summary>发布人</summary>
        String Publisher { get; set; }

        /// <summary>创建人</summary>
        Int32 CreateUserID { get; set; }

        /// <summary>创建时间</summary>
        DateTime CreateTime { get; set; }

        /// <summary>创建地址</summary>
        String CreateIP { get; set; }

        /// <summary>更新人</summary>
        Int32 UpdateUserID { get; set; }

        /// <summary>更新时间</summary>
        DateTime UpdateTime { get; set; }

        /// <summary>更新地址</summary>
        String UpdateIP { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
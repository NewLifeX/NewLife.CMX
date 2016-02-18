﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace NewLife.CMX
{
    /// <summary>内容</summary>
    [Serializable]
    [DataObject]
    [Description("内容")]
    [BindIndex("IX_Content_ModelID", false, "ModelID")]
    [BindIndex("IX_Content_InfoID", false, "InfoID")]
    [BindIndex("IU_Content_InfoID_Version", true, "InfoID,Version")]
    [BindTable("Content", Description = "内容", ConnName = "CMX", DbType = DatabaseType.None)]
    public abstract partial class Content<TEntity> : IContent
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

        private Int32 _InfoID;
        /// <summary>主题</summary>
        [DisplayName("主题")]
        [Description("主题")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(3, "InfoID", "主题", null, "int", 10, 0, false)]
        public virtual Int32 InfoID
        {
            get { return _InfoID; }
            set { if (OnPropertyChanging(__.InfoID, value)) { _InfoID = value; OnPropertyChanged(__.InfoID); } }
        }

        private String _Title;
        /// <summary>标题</summary>
        [DisplayName("标题")]
        [Description("标题")]
        [DataObjectField(false, false, false, 200)]
        [BindColumn(4, "Title", "标题", null, "nvarchar(200)", 0, 0, true, Master=true)]
        public virtual String Title
        {
            get { return _Title; }
            set { if (OnPropertyChanging(__.Title, value)) { _Title = value; OnPropertyChanged(__.Title); } }
        }

        private Int32 _Version;
        /// <summary>版本</summary>
        [DisplayName("版本")]
        [Description("版本")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(5, "Version", "版本", null, "int", 10, 0, false)]
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
        [BindColumn(6, "CreateUserID", "创建人", null, "int", 10, 0, false)]
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
        [BindColumn(7, "CreateTime", "创建时间", null, "datetime", 3, 0, false)]
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
        [BindColumn(8, "CreateIP", "创建地址", null, "nvarchar(50)", 0, 0, true)]
        public virtual String CreateIP
        {
            get { return _CreateIP; }
            set { if (OnPropertyChanging(__.CreateIP, value)) { _CreateIP = value; OnPropertyChanged(__.CreateIP); } }
        }

        private String _Html;
        /// <summary>内容</summary>
        [DisplayName("内容")]
        [Description("内容")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn(9, "Html", "内容", null, "ntext", 0, 0, true)]
        public virtual String Html
        {
            get { return _Html; }
            set { if (OnPropertyChanging(__.Html, value)) { _Html = value; OnPropertyChanged(__.Html); } }
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
                    case __.InfoID : return _InfoID;
                    case __.Title : return _Title;
                    case __.Version : return _Version;
                    case __.CreateUserID : return _CreateUserID;
                    case __.CreateTime : return _CreateTime;
                    case __.CreateIP : return _CreateIP;
                    case __.Html : return _Html;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.ModelID : _ModelID = Convert.ToInt32(value); break;
                    case __.InfoID : _InfoID = Convert.ToInt32(value); break;
                    case __.Title : _Title = Convert.ToString(value); break;
                    case __.Version : _Version = Convert.ToInt32(value); break;
                    case __.CreateUserID : _CreateUserID = Convert.ToInt32(value); break;
                    case __.CreateTime : _CreateTime = Convert.ToDateTime(value); break;
                    case __.CreateIP : _CreateIP = Convert.ToString(value); break;
                    case __.Html : _Html = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得内容字段信息的快捷方式</summary>
        partial class _
        {
            ///<summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary>模型</summary>
            public static readonly Field ModelID = FindByName(__.ModelID);

            ///<summary>主题</summary>
            public static readonly Field InfoID = FindByName(__.InfoID);

            ///<summary>标题</summary>
            public static readonly Field Title = FindByName(__.Title);

            ///<summary>版本</summary>
            public static readonly Field Version = FindByName(__.Version);

            ///<summary>创建人</summary>
            public static readonly Field CreateUserID = FindByName(__.CreateUserID);

            ///<summary>创建时间</summary>
            public static readonly Field CreateTime = FindByName(__.CreateTime);

            ///<summary>创建地址</summary>
            public static readonly Field CreateIP = FindByName(__.CreateIP);

            ///<summary>内容</summary>
            public static readonly Field Html = FindByName(__.Html);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得内容字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary>编号</summary>
            public const String ID = "ID";

            ///<summary>模型</summary>
            public const String ModelID = "ModelID";

            ///<summary>主题</summary>
            public const String InfoID = "InfoID";

            ///<summary>标题</summary>
            public const String Title = "Title";

            ///<summary>版本</summary>
            public const String Version = "Version";

            ///<summary>创建人</summary>
            public const String CreateUserID = "CreateUserID";

            ///<summary>创建时间</summary>
            public const String CreateTime = "CreateTime";

            ///<summary>创建地址</summary>
            public const String CreateIP = "CreateIP";

            ///<summary>内容</summary>
            public const String Html = "Html";

        }
        #endregion
    }

    /// <summary>内容接口</summary>
    public partial interface IContent
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>模型</summary>
        Int32 ModelID { get; set; }

        /// <summary>主题</summary>
        Int32 InfoID { get; set; }

        /// <summary>标题</summary>
        String Title { get; set; }

        /// <summary>版本</summary>
        Int32 Version { get; set; }

        /// <summary>创建人</summary>
        Int32 CreateUserID { get; set; }

        /// <summary>创建时间</summary>
        DateTime CreateTime { get; set; }

        /// <summary>创建地址</summary>
        String CreateIP { get; set; }

        /// <summary>内容</summary>
        String Html { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
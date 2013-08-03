﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace NewLife.CMX
{
    /// <summary>标题</summary>
    [Serializable]
    [DataObject]
    [Description("标题")]
    [BindIndex("IX_Title_SubjectID_Version", true, "SubjectID,Version")]
    [BindIndex("IX_Title_SubjectID", false, "SubjectID")]
    [BindTable("Title", Description = "标题", ConnName = "CMX", DbType = DatabaseType.SqlServer)]
    public partial class Title : ITitle
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
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得标题字段信息的快捷方式</summary>
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

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得标题字段名称的快捷方式</summary>
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

        }
        #endregion
    }

    /// <summary>标题接口</summary>
    public partial interface ITitle
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
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
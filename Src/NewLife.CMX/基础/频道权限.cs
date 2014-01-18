﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace NewLife.CMX
{
    /// <summary>频道权限</summary>
    [Serializable]
    [DataObject]
    [Description("频道权限")]
    [BindIndex("IX_ChannelRole_RoleID", false, "RoleID")]
    [BindTable("ChannelRole", Description = "频道权限", ConnName = "CMX", DbType = DatabaseType.SqlServer)]
    public partial class ChannelRole : IChannelRole
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

        private Int32 _RoleID;
        /// <summary>角色ID</summary>
        [DisplayName("角色ID")]
        [Description("角色ID")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(2, "RoleID", "角色ID", null, "int", 10, 0, false)]
        public virtual Int32 RoleID
        {
            get { return _RoleID; }
            set { if (OnPropertyChanging(__.RoleID, value)) { _RoleID = value; OnPropertyChanged(__.RoleID); } }
        }

        private Int32 _ChannelID;
        /// <summary>频道ID</summary>
        [DisplayName("频道ID")]
        [Description("频道ID")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(3, "ChannelID", "频道ID", null, "int", 10, 0, false)]
        public virtual Int32 ChannelID
        {
            get { return _ChannelID; }
            set { if (OnPropertyChanging(__.ChannelID, value)) { _ChannelID = value; OnPropertyChanged(__.ChannelID); } }
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
                    case __.RoleID : return _RoleID;
                    case __.ChannelID : return _ChannelID;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.RoleID : _RoleID = Convert.ToInt32(value); break;
                    case __.ChannelID : _ChannelID = Convert.ToInt32(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得频道权限字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary>角色ID</summary>
            public static readonly Field RoleID = FindByName(__.RoleID);

            ///<summary>频道ID</summary>
            public static readonly Field ChannelID = FindByName(__.ChannelID);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得频道权限字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary>编号</summary>
            public const String ID = "ID";

            ///<summary>角色ID</summary>
            public const String RoleID = "RoleID";

            ///<summary>频道ID</summary>
            public const String ChannelID = "ChannelID";

        }
        #endregion
    }

    /// <summary>频道权限接口</summary>
    public partial interface IChannelRole
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>角色ID</summary>
        Int32 RoleID { get; set; }

        /// <summary>频道ID</summary>
        Int32 ChannelID { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
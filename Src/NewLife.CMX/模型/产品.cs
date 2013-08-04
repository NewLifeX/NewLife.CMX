﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace NewLife.CMX
{
    /// <summary>产品</summary>
    [Serializable]
    [DataObject]
    [Description("产品")]
    [BindTable("Product", Description = "产品", ConnName = "CMX", DbType = DatabaseType.SqlServer)]
    public partial class Product : IProduct
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
        /// <summary>主题</summary>
        [DisplayName("主题")]
        [Description("主题")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(2, "SubjectID", "主题", null, "int", 10, 0, false)]
        public virtual Int32 SubjectID
        {
            get { return _SubjectID; }
            set { if (OnPropertyChanging(__.SubjectID, value)) { _SubjectID = value; OnPropertyChanged(__.SubjectID); } }
        }

        private String _Title;
        /// <summary>标题</summary>
        [DisplayName("标题")]
        [Description("标题")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(3, "Title", "标题", null, "nvarchar(50)", 0, 0, true)]
        public virtual String Title
        {
            get { return _Title; }
            set { if (OnPropertyChanging(__.Title, value)) { _Title = value; OnPropertyChanged(__.Title); } }
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
                    case __.Title : return _Title;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.SubjectID : _SubjectID = Convert.ToInt32(value); break;
                    case __.Title : _Title = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得产品字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary>主题</summary>
            public static readonly Field SubjectID = FindByName(__.SubjectID);

            ///<summary>标题</summary>
            public static readonly Field Title = FindByName(__.Title);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得产品字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary>编号</summary>
            public const String ID = "ID";

            ///<summary>主题</summary>
            public const String SubjectID = "SubjectID";

            ///<summary>标题</summary>
            public const String Title = "Title";

        }
        #endregion
    }

    /// <summary>产品接口</summary>
    public partial interface IProduct
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>主题</summary>
        Int32 SubjectID { get; set; }

        /// <summary>标题</summary>
        String Title { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
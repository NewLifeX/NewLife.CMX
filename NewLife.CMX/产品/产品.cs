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
        private Decimal _Price;
        /// <summary>价格</summary>
        [DisplayName("价格")]
        [Description("价格")]
        [DataObjectField(false, false, true, 19)]
        [BindColumn(1, "Price", "价格", null, "money", 0, 0, false)]
        public virtual Decimal Price
        {
            get { return _Price; }
            set { if (OnPropertyChanging(__.Price, value)) { _Price = value; OnPropertyChanged(__.Price); } }
        }

        private String _PhotoPath;
        /// <summary>图片路径</summary>
        [DisplayName("图片路径")]
        [Description("图片路径")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(2, "PhotoPath", "图片路径", null, "nvarchar(200)", 0, 0, true)]
        public virtual String PhotoPath
        {
            get { return _PhotoPath; }
            set { if (OnPropertyChanging(__.PhotoPath, value)) { _PhotoPath = value; OnPropertyChanged(__.PhotoPath); } }
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
                    case __.Price : return _Price;
                    case __.PhotoPath : return _PhotoPath;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Price : _Price = Convert.ToDecimal(value); break;
                    case __.PhotoPath : _PhotoPath = Convert.ToString(value); break;
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

            ///<summary>访问量。由统计表同步过来</summary>
            public static readonly Field Views = FindByName(__.Views);

            ///<summary>发布时间</summary>
            public static readonly Field PublishTime = FindByName(__.PublishTime);

            ///<summary>创建人</summary>
            public static readonly Field CreateUserID = FindByName(__.CreateUserID);

            ///<summary>创建时间</summary>
            public static readonly Field CreateTime = FindByName(__.CreateTime);

            ///<summary>更新人</summary>
            public static readonly Field UpdateUserID = FindByName(__.UpdateUserID);

            ///<summary>更新时间</summary>
            public static readonly Field UpdateTime = FindByName(__.UpdateTime);

            ///<summary>备注</summary>
            public static readonly Field Remark = FindByName(__.Remark);

            ///<summary>价格</summary>
            public static readonly Field Price = FindByName(__.Price);

            ///<summary>图片路径</summary>
            public static readonly Field PhotoPath = FindByName(__.PhotoPath);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得产品字段名称的快捷方式</summary>
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

            ///<summary>访问量。由统计表同步过来</summary>
            public const String Views = "Views";

            ///<summary>发布时间</summary>
            public const String PublishTime = "PublishTime";

            ///<summary>创建人</summary>
            public const String CreateUserID = "CreateUserID";

            ///<summary>创建时间</summary>
            public const String CreateTime = "CreateTime";

            ///<summary>更新人</summary>
            public const String UpdateUserID = "UpdateUserID";

            ///<summary>更新时间</summary>
            public const String UpdateTime = "UpdateTime";

            ///<summary>备注</summary>
            public const String Remark = "Remark";

            ///<summary>价格</summary>
            public const String Price = "Price";

            ///<summary>图片路径</summary>
            public const String PhotoPath = "PhotoPath";

        }
        #endregion
    }

    /// <summary>产品接口</summary>
    public partial interface IProduct : IEntityTitle
    {
        #region 属性
        /// <summary>价格</summary>
        Decimal Price { get; set; }

        /// <summary>图片路径</summary>
        String PhotoPath { get; set; }
        #endregion
    }
}
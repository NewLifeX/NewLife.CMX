﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace NewLife.CMX
{
    /// <summary>产品统计</summary>
    [Serializable]
    [DataObject]
    [Description("产品统计")]
    [BindTable("ProductStatistics", Description = "产品统计", ConnName = "CMX", DbType = DatabaseType.SqlServer)]
    public partial class ProductStatistics : IProductStatistics
    {
        #region 属性       
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
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得产品统计字段信息的快捷方式</summary>
        public partial class _
        {
            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得产品统计字段名称的快捷方式</summary>
        partial class __
        {
        }
        #endregion
    }

    /// <summary>产品统计接口</summary>
    public partial interface IProductStatistics
    {
        #region 属性    
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
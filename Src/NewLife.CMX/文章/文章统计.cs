﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace NewLife.CMX
{
    /// <summary>文章统计</summary>
    [Serializable]
    [DataObject]
    [Description("文章统计")]
    [BindTable("ArticleStatistics", Description = "文章统计", ConnName = "CMX", DbType = DatabaseType.SqlServer)]
    public partial class ArticleStatistics : IArticleStatistics
    {
        #region 属性
        #endregion


        #region 字段名
        /// <summary>取得文章统计字段信息的快捷方式</summary>
        public partial class _
        {
            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得文章统计字段名称的快捷方式</summary>
        partial class __
        {
        }
        #endregion
    }

    /// <summary>文章统计接口</summary>
    public partial interface IArticleStatistics
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
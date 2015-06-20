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
    [BindTable("Text", Description = "文本", ConnName = "CMX", DbType = DatabaseType.SqlServer)]
    public partial class Text : IText
    {
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

        }
        #endregion
    }

    /// <summary>文本接口</summary>
    public partial interface IText : IEntityTitle
    {
    }
}
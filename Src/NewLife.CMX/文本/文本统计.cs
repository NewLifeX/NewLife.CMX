﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace NewLife.CMX
{
    /// <summary>文本统计</summary>
    [Serializable]
    [DataObject]
    [Description("文本统计")]
    [BindTable("TextStatistics", Description = "文本统计", ConnName = "CMX", DbType = DatabaseType.SqlServer)]
    public partial class TextStatistics : ITextStatistics
    {
        #region 字段名
        /// <summary>取得文本统计字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary>总数</summary>
            public static readonly Field Total = FindByName(__.Total);

            ///<summary>今天</summary>
            public static readonly Field Today = FindByName(__.Today);

            ///<summary>昨天</summary>
            public static readonly Field Yesterday = FindByName(__.Yesterday);

            ///<summary>本周</summary>
            public static readonly Field ThisWeek = FindByName(__.ThisWeek);

            ///<summary>上周</summary>
            public static readonly Field LastWeek = FindByName(__.LastWeek);

            ///<summary>本月</summary>
            public static readonly Field ThisMonth = FindByName(__.ThisMonth);

            ///<summary>上月</summary>
            public static readonly Field LastMonth = FindByName(__.LastMonth);

            ///<summary>本年</summary>
            public static readonly Field ThisYear = FindByName(__.ThisYear);

            ///<summary>去年</summary>
            public static readonly Field LastYear = FindByName(__.LastYear);

            ///<summary>最后时间</summary>
            public static readonly Field LastTime = FindByName(__.LastTime);

            ///<summary>最后IP</summary>
            public static readonly Field LastIP = FindByName(__.LastIP);

            ///<summary>备注</summary>
            public static readonly Field Remark = FindByName(__.Remark);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得文本统计字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary>编号</summary>
            public const String ID = "ID";

            ///<summary>总数</summary>
            public const String Total = "Total";

            ///<summary>今天</summary>
            public const String Today = "Today";

            ///<summary>昨天</summary>
            public const String Yesterday = "Yesterday";

            ///<summary>本周</summary>
            public const String ThisWeek = "ThisWeek";

            ///<summary>上周</summary>
            public const String LastWeek = "LastWeek";

            ///<summary>本月</summary>
            public const String ThisMonth = "ThisMonth";

            ///<summary>上月</summary>
            public const String LastMonth = "LastMonth";

            ///<summary>本年</summary>
            public const String ThisYear = "ThisYear";

            ///<summary>去年</summary>
            public const String LastYear = "LastYear";

            ///<summary>最后时间</summary>
            public const String LastTime = "LastTime";

            ///<summary>最后IP</summary>
            public const String LastIP = "LastIP";

            ///<summary>备注</summary>
            public const String Remark = "Remark";

        }
        #endregion
    }

    /// <summary>文本统计接口</summary>
    public partial interface ITextStatistics : IStatistics
    {
    }
}
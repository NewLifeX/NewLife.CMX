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
    }

    /// <summary>文章统计接口</summary>
    public partial interface IArticleStatistics
    {
    }
}
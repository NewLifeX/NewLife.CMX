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
    }

    /// <summary>文本统计接口</summary>
    public partial interface ITextStatistics
    {
    }
}
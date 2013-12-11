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
    }

    /// <summary>产品统计接口</summary>
    public partial interface IProductStatistics
    {
    }
}
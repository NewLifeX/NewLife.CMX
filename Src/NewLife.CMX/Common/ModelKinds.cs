using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NewLife.CMX.Common
{
    /// <summary>模型类型</summary>
    public enum ModelKinds
    {
        /// <summary>文本</summary>
        [Description("文本")]
        SimpleText,

        /// <summary>文章</summary>
        [Description("文章")]
        Article,

        /// <summary>产品</summary>
        [Description("产品")]
        Product
    }
}
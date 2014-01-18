﻿using System;
using System.ComponentModel;
using XCode;

namespace NewLife.CMX
{
    /// <summary>产品分类</summary>
    public partial class ProductCategory : EntityCategory<ProductCategory>
    {
        #region 对象操作﻿
        #endregion

        #region 扩展属性﻿
        /// <summary>父级名称</summary>
        public String ParentName { get { return Parent != null ? Parent.Name : ""; } }
        #endregion

        #region 扩展查询﻿
       
        #endregion

        #region 高级查询
        #endregion

        #region 扩展操作
        #endregion

        #region 业务
        #endregion
    }
}
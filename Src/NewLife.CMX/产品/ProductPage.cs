using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using XTemplate.Templating;
using NewLife.CMX.Templates;

namespace NewLife.CMX
{
    /// <summary>产品列表模版基类。模版生成类继承于此类</summary>
    public class ProductListPage : PageBase
    {
    }

    /// <summary>产品模版基类。模版生成类继承于此类</summary>
    public class ProductPage : PageBase
    {
        #region 属性
        private Product _Entity;
        /// <summary>实体对象</summary>
        public Product Entity { get { return _Entity; } set { _Entity = value; } }
        #endregion
    }
}
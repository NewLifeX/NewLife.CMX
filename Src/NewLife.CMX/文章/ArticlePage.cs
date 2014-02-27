using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using XTemplate.Templating;
using NewLife.CMX.Templates;

namespace NewLife.CMX
{
    /// <summary>文章列表模版基类。首页模版生成类继承于此类</summary>
    public class ArticleListPage : PageBase
    {
    }

    /// <summary>文章模版基类。首页模版生成类继承于此类</summary>
    public class ArticlePage : PageBase
    {
        #region 属性
        private Article _Entity;
        /// <summary>实体对象</summary>
        public Article Entity { get { return _Entity; } set { _Entity = value; } }
        #endregion
    }
}
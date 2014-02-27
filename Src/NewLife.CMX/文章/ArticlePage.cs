using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using XTemplate.Templating;
using NewLife.CMX.Templates;
using XCode;

namespace NewLife.CMX
{
    /// <summary>文章列表模版基类。首页模版生成类继承于此类</summary>
    public class ArticleListPage : PageBase
    {
        #region 属性
        private EntityList<Article> _Entities;
        /// <summary>实体对象集合</summary>
        public EntityList<Article> Entities { get { return _Entities; } set { _Entities = value; } }

        private ArticleCategory _Category;
        /// <summary>分类</summary>
        public ArticleCategory Category { get { return _Category; } set { _Category = value; } }

        private String _CurrentPageClassName;
        /// <summary>页面基类名称</summary>
        public String CurrentPageClassName { get { return _CurrentPageClassName; } set { _CurrentPageClassName = value; } }

        private Channel _CurrentChannel;
        /// <summary>当前频道</summary>
        public Channel CurrentChannel { get { return _CurrentChannel; } set { _CurrentChannel = value; } }

        //private String _ChannelName;
        ///// <summary>当前频道名称</summary>
        //public String ChannelName { get { return _ChannelName; } set { _ChannelName = value; } }

        //private String _ChannelSuffix;
        ///// <summary>当前频道扩展名称</summary>
        //public String ChannelSuffix { get { return _ChannelSuffix; } set { _ChannelSuffix = value; } }

        private Int32 _PageCount;
        /// <summary>页面记录数</summary>
        public Int32 PageCount { get { return _PageCount; } set { _PageCount = value; } }

        private Int32 _CurrentPageNo;
        /// <summary>当前页码</summary>
        public Int32 CurrentPageNo { get { return _CurrentPageNo; } set { _CurrentPageNo = value; } }

        private String _BeforePage;
        /// <summary>前页</summary>
        public String BeforePage { get { return _BeforePage; } set { _BeforePage = value; } }

        private String _NextPage;
        /// <summary>后页</summary>
        public String NextPage { get { return _NextPage; } set { _NextPage = value; } }

        #endregion
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
/*
 * XCoder v5.1.4992.36291
 * 作者：nnhy/X
 * 时间：2013-09-01 20:13:59
 * 版权：版权所有 (C) 新生命开发团队 2002~2013
*/
﻿using System;

namespace NewLife.CMX
{
    /// <summary>文章内容</summary>
    public partial class ArticleContent : EntityContent<ArticleContent>
    {
        #region 对象操作﻿
        #endregion

        #region 扩展属性﻿
        private Article _Article;
        /// <summary>文章</summary>
        public Article Article
        {
            get
            {
                if (_Article == null && ParentID > 0 && !Dirtys.ContainsKey("Article"))
                {
                    _Article = Article.FindByID(ParentID);
                    Dirtys["Article"] = true;
                }
                return _Article;
            }
            set { _Article = value; }
        }

        //public String Suffix { get { return Article == null ? "" : Article.ChannelSuffix; } }
        //private String _Suffix;
        ///// <summary></summary>
        //public String Suffix
        //{
        //    get { return _Suffix; }
        //    set { _Suffix = value; }
        //}

        private Int32 _Views;
        /// <summary>点击次数</summary>
        public Int32 Views
        {
            get
            {
                if (Article != null && !Dirtys.ContainsKey("Views"))
                {
                    _Views = Article.Views;
                    Dirtys["Views"] = true;
                }
                return _Views;
            }
        }
        #endregion

        #region 扩展查询﻿
        #endregion

        #region 高级查询
        // 以下为自定义高级查询的例子

        ///// <summary>
        ///// 查询满足条件的记录集，分页、排序
        ///// </summary>
        ///// <param name="key">关键字</param>
        ///// <param name="orderClause">排序，不带Order By</param>
        ///// <param name="startRowIndex">开始行，0表示第一行</param>
        ///// <param name="maximumRows">最大返回行数，0表示所有行</param>
        ///// <returns>实体集</returns>
        //[DataObjectMethod(DataObjectMethodType.Select, true)]
        //public static EntityList<ArticleContent> Search(String key, String orderClause, Int32 startRowIndex, Int32 maximumRows)
        //{
        //    return FindAll(SearchWhere(key), orderClause, null, startRowIndex, maximumRows);
        //}

        ///// <summary>
        ///// 查询满足条件的记录总数，分页和排序无效，带参数是因为ObjectDataSource要求它跟Search统一
        ///// </summary>
        ///// <param name="key">关键字</param>
        ///// <param name="orderClause">排序，不带Order By</param>
        ///// <param name="startRowIndex">开始行，0表示第一行</param>
        ///// <param name="maximumRows">最大返回行数，0表示所有行</param>
        ///// <returns>记录数</returns>
        //public static Int32 SearchCount(String key, String orderClause, Int32 startRowIndex, Int32 maximumRows)
        //{
        //    return FindCount(SearchWhere(key), null, null, 0, 0);
        //}

        /// <summary>构造搜索条件</summary>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        private static String SearchWhere(String key)
        {
            // WhereExpression重载&和|运算符，作为And和Or的替代
            // SearchWhereByKeys系列方法用于构建针对字符串字段的模糊搜索
            var exp = SearchWhereByKeys(key, null);

            // 以下仅为演示，Field（继承自FieldItem）重载了==、!=、>、<、>=、<=等运算符（第4行）
            //if (userid > 0) exp &= _.OperatorID == userid;
            //if (isSign != null) exp &= _.IsSign == isSign.Value;
            //if (start > DateTime.MinValue) exp &= _.OccurTime >= start;
            //if (end > DateTime.MinValue) exp &= _.OccurTime < end.AddDays(1).Date;

            return exp;
        }
        #endregion

        #region 扩展操作
        #endregion

        #region 业务
        #endregion
    }
}
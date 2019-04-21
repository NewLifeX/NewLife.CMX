/*
 * XCoder v5.1.4992.36291
 * 作者：nnhy/X
 * 时间：2013-09-01 20:13:59
 * 版权：版权所有 (C) 新生命开发团队 2002~2013
*/
﻿using System;
using System.ComponentModel;
using XCode;

namespace NewLife.CMX
{
    /// <summary>文本</summary>
    public partial class Text : EntityTitle<Text, TextCategory, TextContent, TextStatistics>
    {
        #region 对象操作﻿
        #endregion

        #region 扩展属性﻿
        //private String _ConentTxt;
        ///// <summary></summary>
        //public String ConentTxt
        //{
        //    get
        //    {
        //        if (_ConentTxt == null && !Dirtys.ContainsKey("ConentTxt"))
        //        {
        //            _ConentTxt = Content.Content ?? "";
        //            Dirtys["ConentTxt"] = true;
        //        }
        //        return _ConentTxt;
        //    }
        //    set { _ConentTxt = value; }
        //}
        #endregion

        #region 扩展查询﻿
        #endregion

        #region 高级查询
        /// <summary>查找指定分类下的单文本，如果不存在，则自动创建分类和单文本</summary>
        /// <param name="categoryPath"></param>
        /// <returns></returns>
        public static Text FindByCategory(String categoryPath)
        {
            var list = GetTitles(categoryPath, 1, 1);
            if (list.Count > 0) return list[0];

            var cat = FindOrCreateCategory(null, categoryPath);

            // 创建默认单文本
            var entity = new Text();
            entity.CategoryID = cat.ID;
            entity.Title = cat.Name;
            entity.ContentText = cat.Name;
            entity.Insert();

            return entity;
        }
        #endregion

        #region 扩展操作
        #endregion

        #region 业务
        #endregion
    }
}
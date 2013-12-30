/*
 * XCoder v4.3.2011.0920
 * 作者：X/X-PC
 * 时间：2011-10-27 10:48:06
 * 版权：版权所有 (C) 新生命开发团队 2011
*/
﻿using System;
using System.ComponentModel;
using XCode;

namespace NewLife.CMX
{
    /// <summary>基础信息</summary>
    public partial class BaseInfo<TEntity> : Entity<TEntity> where TEntity : BaseInfo<TEntity>, new()
    {
        #region 扩展查询﻿
        /// <summary>根据信息名称查找</summary>
        /// <param name="name">信息名称</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static TEntity FindByName(String name)
        {
            if (Meta.Count >= 1000)
                return Find(new String[] { _.Name }, new Object[] { name });
            else // 实体缓存
                return Meta.Cache.Entities.Find(_.Name, name);
            // 单对象缓存
            //return Meta.SingleCache[name];
        }

        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static TEntity FindByID(Int32 id)
        {
            if (Meta.Count >= 1000)
                return Find(new String[] { _.ID }, new Object[] { id });
            else // 实体缓存
                return Meta.Cache.Entities.Find(_.ID, id);
            // 单对象缓存
            //return Meta.SingleCache[id];
        }
        #endregion
    }
}
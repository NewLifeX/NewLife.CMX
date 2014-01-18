using System;
using System.Collections.Generic;
using System.Text;
using XCode;

namespace NewLife.CMX
{
    public interface ICategoryOperate : IEntityOperate
    {
        /// <summary>查询子类以及子类的ID如果子类不是最终类，返回的时候ID会被改为负数</summary>
        /// <param name="parentKey"></param>
        /// <param name="deepth"></param>
        /// <returns></returns>
        Dictionary<Int32, String> FindChildNameAndIDByNoParent(Int32 parentKey, Int32 deepth);
    }

    class CategoryOperate<TEntity> : Entity<TEntity>.EntityOperate, ICategoryOperate where TEntity : EntityCategory<TEntity>, new()
    {
        /// <summary>查询子类以及子类的ID如果子类不是最终类，返回的时候ID会被改为负数</summary>
        /// <param name="parentKey"></param>
        /// <param name="deepth"></param>
        /// <returns></returns>
        public Dictionary<Int32, String> FindChildNameAndIDByNoParent(Int32 parentKey, Int32 deepth)
        {
            return EntityCategory<TEntity>.FindChildNameAndIDByNoParent(parentKey, deepth);
        }
    }
}
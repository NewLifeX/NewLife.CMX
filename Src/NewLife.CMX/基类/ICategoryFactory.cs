using System;
using System.Linq;
using System.Collections.Generic;
using XCode;

namespace NewLife.CMX
{
    /// <summary>分类实体工厂</summary>
    public interface ICategoryFactory : IEntityOperate
    {
        IEntityCategory Root { get; }

        IEntityCategory FindByID(Int32 id);

        IEntityCategory FindByName(String name);

        /// <summary>查询子类以及子类的ID如果子类不是最终类，返回的时候ID会被改为负数</summary>
        /// <param name="parentKey"></param>
        /// <param name="deepth"></param>
        /// <returns></returns>
        Dictionary<Int32, String> FindChildNameAndIDByNoParent(Int32 parentKey, Int32 deepth);
    }

    class CategoryFactory<TEntity> : Entity<TEntity>.EntityOperate, ICategoryFactory where TEntity : EntityCategory<TEntity>, new()
    {
        public IEntityCategory Root { get { return EntityCategory<TEntity>.Root; } }

        public virtual IEntityCategory FindByID(Int32 id)
        {
            return EntityCategory<TEntity>.FindByID(id);
        }

        public IEntityCategory FindByName(String name) { return EntityCategory<TEntity>.FindAllByName(name).ToList().FirstOrDefault(); }

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
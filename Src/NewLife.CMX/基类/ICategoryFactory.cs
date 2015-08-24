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

        IEntityCategory FindByCode(String code);

        IEntityCategory FindByPath(String path);

        ///// <summary>查询子类以及子类的ID如果子类不是最终类，返回的时候ID会被改为负数</summary>
        ///// <param name="parentKey"></param>
        ///// <param name="deepth"></param>
        ///// <returns></returns>
        //Dictionary<Int32, String> FindChildNameAndIDByNoParent(Int32 parentKey, Int32 deepth);
    }

    class CategoryFactory<TEntity> : Entity<TEntity>.EntityOperate, ICategoryFactory where TEntity : EntityCategory<TEntity>, new()
    {
        public IEntityCategory Root { get { return EntityCategory<TEntity>.Root; } }

        public virtual IEntityCategory FindByID(Int32 id)
        {
            return EntityCategory<TEntity>.FindByID(id);
        }

        public IEntityCategory FindByName(String name) { return EntityCategory<TEntity>.FindAllByName(name).ToList().FirstOrDefault(); }

        public IEntityCategory FindByCode(String code) { return EntityCategory<TEntity>.FindByCode(code); }

        public IEntityCategory FindByPath(String path)
        {
            return EntityCategory<TEntity>.Root.FindByPath(path);
        }

        ///// <summary>查询子类以及子类的ID如果子类不是最终类，返回的时候ID会被改为负数</summary>
        ///// <param name="parentKey"></param>
        ///// <param name="deepth"></param>
        ///// <returns></returns>
        //public Dictionary<Int32, String> FindChildNameAndIDByNoParent(Int32 parentKey, Int32 deepth)
        //{
        //    return EntityCategory<TEntity>.FindChildNameAndIDByNoParent(parentKey, deepth);
        //}
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using XCode;

namespace NewLife.CMX
{
    /// <summary>内容实体工厂</summary>
    public interface IContentFactory : IEntityOperate
    {
        IEntityContent FindByID(Int32 id);
        IEntityContent FindLastByParentID(Int32 id);
    }

    class ContentFactory<TEntity> : Entity<TEntity>.EntityOperate, IContentFactory where TEntity : EntityContent<TEntity>, new()
    {
        public virtual IEntityContent FindByID(Int32 id)
        {
            return EntityContent<TEntity>.FindByID(id);
        }

        public virtual IEntityContent FindLastByParentID(Int32 id)
        {
            return EntityContent<TEntity>.FindLastByParentID(id);
        }

    }
}
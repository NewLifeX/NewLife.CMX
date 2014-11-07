using System;
using System.Collections.Generic;
using System.Text;
using XCode;

namespace NewLife.CMX
{
    /// <summary>标题实体工厂</summary>
    public interface ITitleFactory : IEntityOperate
    {
        IEntityTitle FindByID(Int32 id);
    }

    class TitleFactory<TEntity> : Entity<TEntity>.EntityOperate, ITitleFactory where TEntity : EntityTitle<TEntity>, new()
    {
        public virtual IEntityTitle FindByID(Int32 id)
        {
            return EntityTitle<TEntity>.FindByID(id);
        }

    }
}
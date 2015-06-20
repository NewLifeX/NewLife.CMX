using System;
using System.Collections.Generic;
using System.Linq;
using NewLife.Data;
using XCode;

namespace NewLife.CMX
{
    /// <summary>标题实体工厂</summary>
    public interface ITitleFactory : IEntityOperate
    {
        IEntityTitle FindByID(Int32 id);

        IList<IEntityTitle> GetTitles(Int32 categoryid, Int32 pageIndex = 1, Int32 pageCount = 10);
        Int32 GetTitleCount(Int32 categoryId);

        IList<IEntityTitle> GetTitles(Int32 categoryid, PageParameter pager);
    }

    class TitleFactory<TEntity> : Entity<TEntity>.EntityOperate, ITitleFactory where TEntity : EntityTitle<TEntity>, new()
    {
        public virtual IEntityTitle FindByID(Int32 id)
        {
            return EntityTitle<TEntity>.FindByID(id);
        }

        public IList<IEntityTitle> GetTitles(Int32 categoryid, Int32 pageIndex = 1, Int32 pageCount = 10)
        {
            return EntityTitle<TEntity>.GetTitles(categoryid, pageIndex, pageCount).ToList().Cast<IEntityTitle>().ToList();
        }

        public int GetTitleCount(int categoryId)
        {
            return EntityTitle<TEntity>.GetTitleCount(categoryId);
        }

        public IList<IEntityTitle> GetTitles(Int32 categoryid, PageParameter pager)
        {
            return EntityTitle<TEntity>.GetTitles(categoryid, pager).ToList().Cast<IEntityTitle>().ToList();
        }
    }
}
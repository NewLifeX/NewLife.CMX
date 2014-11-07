using System;
using System.Collections.Generic;
using System.Text;
using XCode;

namespace NewLife.CMX
{
    /// <summary>统计实体工厂</summary>
    public interface IStatisticsFactory : IEntityOperate
    {
        IStatistics FindByID(Int32 id);
    }

    class StatisticsFactory<TEntity> : Entity<TEntity>.EntityOperate, IStatisticsFactory where TEntity : Statistics<TEntity>, new()
    {
        public virtual IStatistics FindByID(Int32 id)
        {
            return Statistics<TEntity>.FindByID(id);
        }

    }
}
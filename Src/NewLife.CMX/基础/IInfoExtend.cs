using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XCode;

namespace NewLife.CMX
{
    public interface IInfoExtend
    {
    }

    public class InfoExtend<TEntity> : Entity<TEntity>, IInfoExtend where TEntity : InfoExtend<TEntity>, new()
    {

    }
}
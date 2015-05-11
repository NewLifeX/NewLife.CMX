using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewLife.Cube;
using XCode;

namespace NewLife.CMX.Web
{
    public class TitleController<TEntity> : EntityController<TEntity> where TEntity : EntityTitle<TEntity>, new()
    {
    }
}
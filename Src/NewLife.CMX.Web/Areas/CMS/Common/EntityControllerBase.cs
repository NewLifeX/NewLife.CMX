using System;
using System.Collections.Generic;
using System.Linq;
using NewLife.Cube;
using XCode;
using XCode.Configuration;

namespace NewLife.CMX.Web
{
    public class EntityControllerBase<TEntity> : EntityController<TEntity> where TEntity : Entity<TEntity>, new()
    {
        static EntityControllerBase()
        {
            // 过滤掉一些字段
            ListFields.Replace("CreateUserID", "CreateUserName")
                .Replace("UpdateUserID", "UpdateUserName");
            FormFields.Replace("CreateUserID", "CreateUserName")
                .Replace("UpdateUserID", "UpdateUserName")
                .RemoveField("CreateTime")
                .RemoveField("UpdateTime");
        }
    }
}
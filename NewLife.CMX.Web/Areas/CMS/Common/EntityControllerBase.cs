﻿using NewLife.Cube;
using XCode;

namespace NewLife.CMX.Web
{
    public class EntityControllerBase<TEntity> : EntityController<TEntity> where TEntity : Entity<TEntity>, new()
    {
        static EntityControllerBase()
        {
            // 过滤掉一些字段
            ListFields.Replace("CreateUserID", "CreateUser")
                .Replace("UpdateUserID", "UpdateUser");

            AddFormFields.RemoveCreateField();
            AddFormFields.RemoveUpdateField();

            EditFormFields.RemoveCreateField();
            EditFormFields.RemoveUpdateField();

            DetailFields.RemoveCreateField();
            DetailFields.RemoveUpdateField();
        }
    }
}
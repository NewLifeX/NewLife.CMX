using System;
using System.Collections.Generic;
using System.Text;
using NewLife.CommonEntity;
using XCode;

namespace NewLife.CMX
{
    /// <summary>实体基类</summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EntityBase<TEntity> : Entity<TEntity>, IUserInfo, ITimeInfo where TEntity : EntityBase<TEntity>, new()
    {
        #region 静态引用
        class __
        {
            public const String CreateUserID = "CreateUserID";
            public const String UpdateUserID = "UpdateUserID";
            public const String CreateUserName = "CreateUserName";
            public const String UpdateUserName = "UpdateUserName";
            public const String CreateTime = "CreateTime";
            public const String UpdateTime = "UpdateTime";
        }
        #endregion

        #region 验证数据
        public override void Valid(bool isNew)
        {
            if (!isNew && !HasDirty) return;

            base.Valid(isNew);

            var fs = Meta.FieldNames;

            // 当前登录用户
            var user = ManageProvider.Provider.Current;
            if (user != null)
            {
                //var ui = this as IUserInfo;
                //if (isNew && fs.Contains(__.CreateUserID) && !Dirtys[__.CreateUserID]) ui.CreateUserID = user.ID;
                //if (fs.Contains(__.UpdateUserID) && !Dirtys[__.UpdateUserID]) ui.UpdateUserID = user.ID;
                if (isNew)
                {
                    SetDirtyItem(__.CreateUserID, user.ID);
                    SetDirtyItem(__.CreateUserName, user + "");
                }
                SetDirtyItem(__.UpdateUserID, user.ID);
                SetDirtyItem(__.UpdateUserName, user + "");
            }
            //var ti = this as ITimeInfo;
            //if (isNew && fs.Contains(__.CreateTime) && !Dirtys[__.CreateTime]) ti.CreateTime = DateTime.Now;
            //if (fs.Contains(__.UpdateTime) && !Dirtys[__.UpdateTime]) ti.UpdateTime = DateTime.Now;
            if (isNew) SetDirtyItem(__.CreateTime, DateTime.Now);
            SetDirtyItem(__.UpdateTime, DateTime.Now);
        }

        /// <summary>设置脏数据项。如果某个键存在并且数据没有脏，则设置</summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        private void SetDirtyItem(String name, Object value)
        {
            if (Meta.FieldNames.Contains(name) && !Dirtys[name]) SetItem(name, value);
        }
        #endregion

        #region 扩展属性
        private IManageUser _CreateUser;
        /// <summary>创建人</summary>
        public IManageUser CreateUser
        {
            get
            {
                var CreateUserID = this[__.CreateUserID].ToInt();
                if (_CreateUser == null && CreateUserID > 0 && !Dirtys.ContainsKey("CreateUser"))
                {
                    _CreateUser = ManageProvider.Provider.FindByID(CreateUserID);
                    Dirtys["CreateUser"] = true;
                }
                return _CreateUser;
            }
            set { _CreateUser = value; }
        }

        /// <summary>创建人名称</summary>
        public String CreateUserName { get { return CreateUser + ""; } }

        private IManageUser _UpdateUser;
        /// <summary>更新人</summary>
        public IManageUser UpdateUser
        {
            get
            {
                var UpdateUserID = this[__.UpdateUserID].ToInt();
                if (_UpdateUser == null && UpdateUserID > 0 && !Dirtys.ContainsKey("UpdateUser"))
                {
                    _UpdateUser = ManageProvider.Provider.FindByID(UpdateUserID);
                    Dirtys["UpdateUser"] = true;
                }
                return _UpdateUser;
            }
            set { _UpdateUser = value; }
        }

        /// <summary>更新人名称</summary>
        public String UpdateUserName { get { return UpdateUser + ""; } }

        Int32 IUserInfo.CreateUserID { get { return (Int32)this[__.CreateUserID]; } set { SetItem(__.CreateUserID, value); } }
        Int32 IUserInfo.UpdateUserID { get { return (Int32)this[__.UpdateUserID]; } set { SetItem(__.UpdateUserID, value); } }

        DateTime ITimeInfo.CreateTime { get { return (DateTime)this[__.CreateTime]; } set { SetItem(__.CreateTime, value); } }
        DateTime ITimeInfo.UpdateTime { get { return (DateTime)this[__.UpdateTime]; } set { SetItem(__.UpdateTime, value); } }
        #endregion
    }

    /// <summary>用户信息接口。包含创建用户和更新用户</summary>
    public interface IUserInfo
    {
        /// <summary>创建用户ID</summary>
        Int32 CreateUserID { get; set; }

        /// <summary>创建用户</summary>
        IManageUser CreateUser { get; set; }

        /// <summary>创建用户名</summary>
        String CreateUserName { get; }

        /// <summary>更新用户ID</summary>
        Int32 UpdateUserID { get; set; }

        /// <summary>更新用户</summary>
        IManageUser UpdateUser { get; set; }

        /// <summary>更新用户名</summary>
        String UpdateUserName { get; }
    }

    /// <summary>时间信息接口。包含创建时间和更新时间</summary>
    public interface ITimeInfo
    {
        /// <summary>创建时间</summary>
        DateTime CreateTime { get; set; }

        /// <summary>更新时间</summary>
        DateTime UpdateTime { get; set; }
    }
}
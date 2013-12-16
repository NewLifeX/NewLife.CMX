/*
 * XCoder v5.1.4992.36291
 * 作者：nnhy/X
 * 时间：2013-09-01 20:13:59
 * 版权：版权所有 (C) 新生命开发团队 2002~2013
*/
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using NewLife.CommonEntity;
using NewLife.Log;
using NewLife.Web;
using XCode;
using XCode.Configuration;
using NewLife.Reflection;

namespace NewLife.CMX
{
    /// <summary>模型</summary>
    /// <remarks>模型。默认有文章、文本、产品三种模型，可以扩展增加。</remarks>
    public partial class Model : Entity<Model>
    {
        #region 对象操作﻿
        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
            // 这里验证参数范围，建议抛出参数异常，指定参数名，前端用户界面可以捕获参数异常并聚焦到对应的参数输入框
            //if (String.IsNullOrEmpty(Name)) throw new ArgumentNullException(__.Name, _.Name.DisplayName + "无效！");
            //if (!isNew && ID < 1) throw new ArgumentOutOfRangeException(__.ID, _.ID.DisplayName + "必须大于0！");

            // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
            base.Valid(isNew);
            // 在新插入数据或者修改了指定字段时进行唯一性验证，CheckExist内部抛出参数异常
            //if (isNew || Dirtys[__.Name]) CheckExist(__.Name);

            if (isNew && !Dirtys[__.CreateUserID]) CreateUserID = Admin.Current.ID;
            if (!Dirtys[__.UpdateUserID]) UpdateUserID = Admin.Current.ID;
            if (isNew && !Dirtys[__.CreateTime]) CreateTime = DateTime.Now;
            if (!Dirtys[__.UpdateTime]) UpdateTime = DateTime.Now;
        }

        /// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void InitData()
        {
            base.InitData();

            // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
            // Meta.Count是快速取得表记录数
            if (Meta.Count > 0) return;

            // 需要注意的是，如果该方法调用了其它实体类的首次数据库操作，目标实体类的数据初始化将会在同一个线程完成
            if (XTrace.Debug) XTrace.WriteLine("开始初始化{0}[{1}]数据……", typeof(Model).Name, Meta.Table.DataTable.DisplayName);

            //Add("文本");
            //Add("文章");
            //Add("产品");
            foreach (var item in AssemblyX.FindAllPlugins(typeof(IEntityTitle), true))
            {
                var dis = item.GetCustomAttribute<DisplayNameAttribute>();
                var des = item.GetCustomAttribute<DescriptionAttribute>();

                var entity = new Model();
                entity.Name = dis != null ? dis.DisplayName : (des != null ? des.Description : item.Name);
                entity.ClassName = item.FullName;
                entity.Enable = true;
                entity.Save();
            }

            if (XTrace.Debug) XTrace.WriteLine("完成初始化{0}[{1}]数据！", typeof(Model).Name, Meta.Table.DataTable.DisplayName);
        }
        #endregion

        #region 扩展属性﻿
        private Admin _CreateUser;
        /// <summary>创建人</summary>
        public Admin CreateUser
        {
            get
            {
                if (_CreateUser == null && CreateUserID > 0 && !Dirtys.ContainsKey("CreateUser"))
                {
                    _CreateUser = Admin.FindByID(CreateUserID);
                    Dirtys["CreateUser"] = true;
                }
                return _CreateUser;
            }
            set { _CreateUser = value; }
        }

        /// <summary>创建人名称</summary>
        public String CreateUserName { get { return CreateUser != null ? CreateUser.DisplayName : ""; } }

        private Admin _UpdateUser;
        /// <summary>更新人</summary>
        public Admin UpdateUser
        {
            get
            {
                if (_UpdateUser == null && UpdateUserID > 0 && !Dirtys.ContainsKey("UpdateUser"))
                {
                    _UpdateUser = Admin.FindByID(UpdateUserID);
                    Dirtys["UpdateUser"] = true;
                }
                return _UpdateUser;
            }
            set { _UpdateUser = value; }
        }

        /// <summary>更新人名称</summary>
        public String UpdateUserName { get { return UpdateUser != null ? UpdateUser.DisplayName : ""; } }
        #endregion

        #region 扩展查询﻿
        /// <summary>根据ID查询</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Model FindByID(Int32 id)
        {
            if (Meta.Count >= 1000)
                return Find(__.ID, id);
            else
                return Meta.Cache.Entities.Find(__.ID, id);
        }

        /// <summary>根据名称查找</summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Model FindByName(String name)
        {
            if (Meta.Count >= 1000)
                return Find(__.Name, name);
            else // 实体缓存
                return Meta.Cache.Entities.Find(__.Name, name);
            // 单对象缓存
            //return Meta.SingleCache[name];
        }
        #endregion

        #region 高级查询
        #endregion

        #region 扩展操作
        #endregion

        #region 业务
        public static Model Add(String name)
        {
            var entity = new Model();
            entity.Name = name;
            entity.Enable = true;
            entity.Save();

            return entity;
        }
        #endregion
    }
}
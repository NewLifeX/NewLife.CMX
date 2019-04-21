/*
 * XCoder v6.0.5096.30596
 * 作者：nnhy/X2
 * 时间：2013-12-14 17:00:07
 * 版权：版权所有 (C) 新生命开发团队 2002~2013
*/
﻿using System;
using System.ComponentModel;
using System.Linq;
using XCode;
using XCode.Cache;
using XCode.Membership;

namespace NewLife.CMX
{
    /// <summary>实体内容</summary>
    public partial class EntityContent<TEntity> : Entity<TEntity> where TEntity : EntityContent<TEntity>, new()
    {
        #region 对象操作﻿
        /// <summary>根据ParentID缓存最后版本</summary>
        static SingleEntityCache<Int32, TEntity> _cache;

        static EntityContent()
        {
            // 用于引发基类的静态构造函数，所有层次的泛型实体类都应该有一个
            TEntity entity = new TEntity();

            EntityFactory.Register(typeof(TEntity), new ContentFactory<TEntity>());

            _cache = new SingleEntityCache<int, TEntity>();
            _cache.AllowNull = true;
            _cache.AutoSave = false;
            _cache.FindKeyMethod = id =>
            {
                var list = FindAllByName(__.ParentID, id, _.Version.Desc(), 0, 1);
                return list.Count > 0 ? list[0] : null;
            };
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
            // 这里验证参数范围，建议抛出参数异常，指定参数名，前端用户界面可以捕获参数异常并聚焦到对应的参数输入框
            //if (String.IsNullOrEmpty(Name)) throw new ArgumentNullException(__.Name, _.Name.DisplayName + "无效！");
            //if (!isNew && ID < 1) throw new ArgumentOutOfRangeException(__.ID, _.ID.DisplayName + "必须大于0！");

            // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
            base.Valid(isNew);

            var mp = ManageProvider.Provider;
            // 创建者信息
            if (isNew && !Dirtys[__.CreateTime])
            {
                CreateTime = DateTime.Now;
                if (mp.Current != null)
                {
                    CreateUserID = mp.Current.ID;
                    CreateUserName = mp.Current.ToString();
                }
            }
        }

        /// <summary>不允许修改</summary>
        /// <returns></returns>
        public override int Update()
        {
            // 重载非OnXXX版本是为了让实体类内部可以直接调用OnXXX越过这里的检查
            throw new XException(Meta.Table.DataTable.DisplayName + "不允许修改！");
            //return base.Update();
        }

        /// <summary>不允许修改</summary>
        /// <returns></returns>
        public override int Delete()
        {
            // 重载非OnXXX版本是为了让实体类内部可以直接调用OnXXX越过这里的检查
            throw new XException(Meta.Table.DataTable.DisplayName + "不允许删除！");
            //return base.Delete();
        }
        #endregion

        #region 扩展属性﻿
        #endregion

        #region 扩展查询﻿
        /// <summary>根据ID查询</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static TEntity FindByID(Int32 id)
        {
            if (Meta.Count >= 1000)
                return Meta.SingleCache[id];
            else
                return Meta.Cache.Entities.Find(__.ID, id);
        }

        /// <summary>根据主题查找</summary>
        /// <param name="parentid">主题</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EntityList<TEntity> FindAllByParentID(Int32 parentid)
        {
            if (Meta.Count >= 1000)
                return FindAll(__.ParentID, parentid);
            else // 实体缓存
                return Meta.Cache.Entities.FindAll(__.ParentID, parentid);
        }

        /// <summary>根据主题、版本查找</summary>
        /// <param name="parentid">主题</param>
        /// <param name="version">版本</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static TEntity FindByParentIDAndVersion(Int32 parentid, Int32 version)
        {
            if (Meta.Count >= 1000)
                return Find(new String[] { __.ParentID, __.Version }, new Object[] { parentid, version });
            else // 实体缓存
                return Meta.Cache.Entities.Find(e => e.ParentID == parentid && e.Version == version);
        }

        /// <summary>根据ParentID查询最后版本</summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static TEntity FindLastByParentID(Int32 parentid)
        {
            if (Meta.Count >= 1000)
                return _cache[parentid];
            else
                return Meta.Cache.Entities.FindAll(__.ParentID, parentid).Sort(__.Version, true).ToList().FirstOrDefault();
        }
        #endregion

        #region 高级查询
        #endregion

        #region 扩展操作
        /// <summary>根据父级编号删除对应内容</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Int32 DeleteByParentID(Int32 id)
        {
            var rs = 0;
            var list = FindAllByParentID(id);
            foreach (var item in list)
            {
                rs += item.OnDelete();
            }
            return rs;
        }
        #endregion

        #region 业务
        #endregion
    }
}
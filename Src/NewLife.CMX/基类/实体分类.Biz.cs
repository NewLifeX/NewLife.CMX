/*
 * XCoder v6.0.5096.30596
 * 作者：nnhy/X2
 * 时间：2013-12-14 17:00:07
 * 版权：版权所有 (C) 新生命开发团队 2002~2013
*/
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using NewLife.Log;
using XCode;

namespace NewLife.CMX
{
    /// <summary>实体分类</summary>
    public partial class EntityCategory<TEntity> : EntityTree<TEntity> where TEntity : EntityCategory<TEntity>, new()
    {
        #region 对象操作﻿
        static EntityCategory()
        {
            // 用于引发基类的静态构造函数，所有层次的泛型实体类都应该有一个
            TEntity entity = new TEntity();

            EntityFactory.Register(typeof(TEntity), new CategoryFactory<TEntity>());
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
            // 这里验证参数范围，建议抛出参数异常，指定参数名，前端用户界面可以捕获参数异常并聚焦到对应的参数输入框
            if (Channel == null) throw new ArgumentNullException(__.ChannelID, _.ChannelID.DisplayName + "无效！");
            //if (!isNew && ID < 1) throw new ArgumentOutOfRangeException(__.ID, _.ID.DisplayName + "必须大于0！");

            // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
            base.Valid(isNew);

            // 在新插入数据或者修改了指定字段时进行唯一性验证，CheckExist内部抛出参数异常
            //if (isNew || Dirtys[__.Name]) CheckExist(__.Name);

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
            if (XTrace.Debug) XTrace.WriteLine("开始初始化{0}[{1}]数据……", typeof(TEntity).Name, Meta.Table.DataTable.DisplayName);

            // 找到频道
            var provider = ModelProvider.Get<TEntity>();
            var model = Model.FindByName(provider.Name);
            var chn = Channel.FindBySuffixAndModel(null, model.ID);

            var entity = new TEntity();
            entity.Name = "默认" + Meta.Table.Description;
            entity.ChannelID = chn.ID;
            entity.Insert();

            entity = new TEntity { ParentID = entity.ID };
            entity.Name = "二级分类";
            entity.ChannelID = chn.ID;
            entity.Insert();

            if (XTrace.Debug) XTrace.WriteLine("完成初始化{0}[{1}]数据！", typeof(TEntity).Name, Meta.Table.DataTable.DisplayName);
        }
        #endregion

        #region 扩展属性﻿
        /// <summary>当前分类所在频道</summary>
        [BindRelation("ChannelID", false, "Channel", "ID")]
        public Channel Channel { get { return Channel.FindByID(this.ChannelID); } }

        [DisplayName("频道名")]
        public String ChannelName { get { return Channel != null ? Channel.DisplayName : null; } }
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

        /// <summary>根据名称查找</summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EntityList<TEntity> FindAllByName(String name)
        {
            if (Meta.Count >= 1000)
                return FindAll(__.Name, name);
            else // 实体缓存
                return Meta.Cache.Entities.FindAll(__.Name, name);
        }

        /// <summary>根据父类查找</summary>
        /// <param name="parentid">父类</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EntityList<TEntity> FindAllByParentID(Int32 parentid)
        {
            if (Meta.Count >= 1000)
                return FindAll(__.ParentID, parentid);
            else // 实体缓存
                return Meta.Cache.Entities.FindAll(__.ParentID, parentid);
        }

        /// <summary>根据名称、父类查找</summary>
        /// <param name="name">名称</param>
        /// <param name="parentid">父类</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static TEntity FindByNameAndParentID(String name, Int32 parentid)
        {
            if (Meta.Count >= 1000)
                return Find(new String[] { _.Name, _.ParentID }, new Object[] { name, parentid });
            else // 实体缓存
                return Meta.Cache.Entities.Find(e => e.Name == name && e.ParentID == parentid);
        }

        ///// <summary>查询所有子孙类以及子孙类的ID如果子类不是最终类，返回的时候ID会被改为负数</summary>
        ///// <param name="parentKey"></param>
        ///// <returns></returns>
        //public static Dictionary<Int32, String> FindAllChildsNameAndIDByNoParent(Int32 parentKey)
        //{
        //    var entity = Meta.Factory.Default as TEntity;
        //    var list = FindAllChildsNoParent(parentKey);
        //    var dic = new Dictionary<Int32, String>();

        //    foreach (var item in list)
        //    {
        //        //if (item.IsEnd)
        //        //    dic.Add(item.ID, item.TreeNodeName);
        //        //else
        //        dic.Add(-item.ID, item.TreeNodeName);
        //    }

        //    return dic;
        //}

        ///// <summary>查询子类以及子类的ID如果子类不是最终类，返回的时候ID会被改为负数</summary>
        ///// <param name="parentKey"></param>
        ///// <returns></returns>
        //public static Dictionary<Int32, String> FindChildNameAndIDByNoParent(Int32 parentKey, Int32 deepth)
        //{
        //    var entity = Meta.Factory.Default as TEntity;
        //    var list = FindAllChildsNoParent(parentKey);
        //    var dic = new Dictionary<Int32, String>();

        //    foreach (TEntity item in list)
        //    {
        //        if (item.Deepth > deepth) continue;

        //        //if (item.IsEnd)
        //        //    dic.Add(item.ID, item.TreeNodeName);
        //        //else
        //        dic.Add(-item.ID, item.TreeNodeName);
        //    }
        //    return dic;
        //}

        ///// <summary>查询所有不是终节点的节点</summary>
        ///// <param name="parentkey"></param>
        ///// <returns></returns>
        //public static EntityList<TEntity> FindAllByNoEnd(Int32 parentkey)
        //{
        //    var entitylist = FindAllChildsByParent(parentkey);
        //    entitylist.RemoveAll(e => e.IsEnd);
        //    return entitylist;
        //}

        ///// <summary>
        ///// 根据分类ID查询，RootDeepth当前分类的父级根目录级别，DisDeepth决定显示分类层级
        ///// 如：RootDeepth=1， DisDeepth=2 则表示 从当前分类所属父级分类的深度为1的分类开始，显示 深度为1和2的两级分类内容
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="RootDeepth"></param>
        ///// <param name="DisDeepth"></param>
        ///// <param name="IsContainParent"></param>
        ///// <returns></returns>
        //public static EntityList<TEntity> FindAllByIDAndDeepth(Int32 id, Int32 RootDeepth, Int32 DisDeepth, Boolean IsContainParent)
        //{
        //    var currentEntity = FindByID(id);
        //    TEntity pentity;

        //    if (RootDeepth != 0 && currentEntity.AllParents.Count > 0)
        //        pentity = currentEntity.AllParents.Find(e => e.Deepth == RootDeepth);
        //    else
        //        pentity = Root;

        //    //var CategoryEntities = pentity.AllChilds.FindAll(e => e.Deepth == DisDeepth);
        //    var CategoryEntities = FindAllChildsByParent(pentity.ID);

        //    CategoryEntities.RemoveAll(e => e.Deepth > DisDeepth);

        //    if (!IsContainParent) CategoryEntities.Remove(CategoryEntities.ToList().First());
        //    //if (IsContainParent) CategoryEntities.Add(pentity);

        //    return CategoryEntities;
        //}

        ///// <summary>
        ///// 根据ID以及深度查询父类
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="RootDeepth"></param>
        ///// <returns></returns>
        //public static TEntity FindParentByIDAndDeepth(Int32 id, Int32 RootDeepth)
        //{
        //    var entity = FindByID(id);

        //    if (id == 0 || entity.Parent == null || RootDeepth == 0) return entity = Root;

        //    return entity.AllParents.Find(e => e.Deepth == RootDeepth);
        //}
        #endregion

        #region 高级查询
        public IList<IEntityTitle> GetTitles(Int32 pageIndex = 1, Int32 pageCount = 10)
        {
            var provider = ModelProvider.Get(this.GetType());
            return provider.TitleFactory.GetTitles(ID, pageIndex, pageCount);
        }

        public IEntityTitle FindTitle(int id)
        {
            var provider = ModelProvider.Get(this.GetType());
            return provider.TitleFactory.FindByID(id);
        }

        #endregion

        #region 扩展操作
        #endregion

        #region 业务
        #endregion
    }

    partial interface IEntityCategory
    {
        Channel Channel { get; }

        IList<IEntityTitle> GetTitles(Int32 pageIndex = 1, Int32 pageCount = 10);

        IEntityTitle FindTitle(Int32 id);
    }
}
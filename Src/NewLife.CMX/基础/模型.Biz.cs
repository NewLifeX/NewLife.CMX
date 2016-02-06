/*
 * XCoder v5.1.4992.36291
 * 作者：nnhy/X
 * 时间：2013-09-01 20:13:59
 * 版权：版权所有 (C) 新生命开发团队 2002~2013
*/
﻿using System;
using System.ComponentModel;
using NewLife.Common;
using NewLife.Log;
using XCode.Membership;
using NewLife.Reflection;

namespace NewLife.CMX
{
    /// <summary>模型</summary>
    /// <remarks>模型。默认有文章、文本、产品三种模型，可以扩展增加。</remarks>
    public partial class Model : UserTimeEntity<Model>
    {
        #region 对象操作﻿
        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
            // 这里验证参数范围，建议抛出参数异常，指定参数名，前端用户界面可以捕获参数异常并聚焦到对应的参数输入框
            if (String.IsNullOrEmpty(Name)) throw new ArgumentNullException(__.Name, _.Name.DisplayName + "无效！");

            // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
            base.Valid(isNew);

            if (String.IsNullOrEmpty(DisplayName)) DisplayName = Name;
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
            Scan();
            if (XTrace.Debug) XTrace.WriteLine("完成初始化{0}[{1}]数据！", typeof(Model).Name, Meta.Table.DataTable.DisplayName);
        }

        protected override int OnDelete()
        {
            //var count = Channel.FindCountByModel(ID);
            //if (count > 0) throw new XException("该模型下有{0}个频道，禁止删除！", count);

            return base.OnDelete();
        }
        #endregion

        #region 扩展属性﻿
        ///// <summary>标题页面</summary>
        //public String TitleUrl { get { return String.Format("CMX/{0}/{0}.aspx", Provider.TitleType.Name); } }

        ///// <summary>分类页面</summary>
        //public String CategoryUrl { get { return String.Format("CMX/{0}/{1}.aspx", Provider.TitleType.Name, Provider.CategoryType.Name); } }
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
            // 实体缓存
            var entity = Meta.Cache.Entities.Find(__.Name, name);
            if (entity == null) entity = Meta.Cache.Entities.Find(__.DisplayName, name);
            return entity;
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

        /// <summary>扫描所有模型提供者，并添加到数据库</summary>
        /// <returns></returns>
        public static Int32 Scan()
        {
            var count = 0;

            foreach (var item in typeof(IInfoExtend).GetAllSubclasses(true))
            {
                var entity = FindByName(item.Name);
                if (entity == null) entity = new Model();

                entity.Name = item.Name;
                entity.DisplayName = item.GetDisplayName() ?? item.GetDescription();
                entity.ProviderName = item.FullName;

                //entity.InfoTemplate = String.Format("CMX/{0}.aspx", entity.Name);
                //entity.CategoryTemplate = String.Format("CMX/{0}/{1}.aspx", entity.Name, entity.Name);
                entity.Enable = true;
                entity.Save();

                count++;
            }

            return count;
        }
        #endregion

        #region 模型提供者
        //private IModelProvider _Provider;
        ///// <summary>模型提供者</summary>
        //public IModelProvider Provider
        //{
        //    get
        //    {
        //        if (_Provider == null)
        //        {
        //            if (!ModelProvider.Providers.TryGetValue(ProviderName, out _Provider)) throw new XException("找不到模型提供者{0}", ProviderName);
        //        }
        //        return _Provider;
        //    }
        //}
        #endregion
    }
}
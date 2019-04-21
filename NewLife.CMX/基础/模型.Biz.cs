/*
 * XCoder v5.1.4992.36291
 * 作者：nnhy/X
 * 时间：2013-09-01 20:13:59
 * 版权：版权所有 (C) 新生命开发团队 2002~2013
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife.Log;
using NewLife.Model;
using NewLife.Reflection;
using XCode;
using XCode.Membership;

namespace NewLife.CMX
{
    /// <summary>模型</summary>
    /// <remarks>模型。默认有文章、文本、产品三种模型，可以扩展增加。</remarks>
    public partial class Model : LogEntity<Model> 
    {
        #region 对象操作﻿
        static Model()
        {
            Meta.Modules.Add<UserModule>();
            Meta.Modules.Add<TimeModule>();
            Meta.Modules.Add<IPModule>();
        }

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
            if (XTrace.Debug) XTrace.WriteLine("开始初始化{0}[{1}]数据……", Meta.ThisType.Name, Meta.Table.DataTable.DisplayName);
            Scan();
            if (XTrace.Debug) XTrace.WriteLine("完成初始化{0}[{1}]数据！", Meta.ThisType.Name, Meta.Table.DataTable.DisplayName);
        }

        /// <summary>检查是否允许删除</summary>
        /// <returns></returns>
        protected override Int32 OnDelete()
        {
            var count = Category.FindAllWithCache().ToList().Count(e => e.ModelID == ID);
            if (count > 0) throw new XException("该模型下有{0}个分类，禁止删除！", count);

            return base.OnDelete();
        }
        #endregion

        #region 扩展属性﻿
        /// <summary>创建人</summary>
        [XmlIgnore, ScriptIgnore]
        [DisplayName("创建人")]
        public IManageUser CreateUser { get { return Extends.Get(nameof(CreateUser), k => ManageProvider.Provider.FindByID(CreateUserID)); } }

        /// <summary>创建人名称</summary>
        [XmlIgnore, ScriptIgnore]
        [DisplayName("创建人")]
        [Map(__.CreateUserID)]
        public String CreateUserName { get { return CreateUser + ""; } }

        /// <summary>更新人</summary>
        [XmlIgnore, ScriptIgnore]
        [DisplayName("更新人")]
        public IManageUser UpdateUser { get { return Extends.Get(nameof(UpdateUser), k => ManageProvider.Provider.FindByID(UpdateUserID)); } }

        /// <summary>更新人名称</summary>
        [XmlIgnore, ScriptIgnore]
        [DisplayName("更新人")]
        [Map(__.UpdateUserID)]
        public String UpdateUserName { get { return UpdateUser + ""; } }
        #endregion

        #region 扩展查询﻿
        /// <summary>根据ID查询</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Model FindByID(Int32 id)
        {
            if (id <= 0) return null;

            if (Meta.Count >= 1000)
                return Find(__.ID, id);
            else
                return Meta.Cache.Entities.FirstOrDefault(e => e.ID == id);
        }

        /// <summary>根据名称查找</summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Model FindByName(String name)
        {
            if (name.IsNullOrEmpty()) return null;

            // 实体缓存
            var entity = Meta.Cache.Entities.FirstOrDefault(e => e.Name == name);
            if (entity == null) entity = Meta.Cache.Entities.FirstOrDefault(e => e.DisplayName == name);
            return entity;
        }
        #endregion

        #region 高级查询
        /// <summary>获取所有有效模型</summary>
        /// <returns></returns>
        public static List<Model> GetAll()
        {
            //return FindAllWithCache(__.Enable, true).Sort(__.ID, false);
            return FindAllWithCache().ToList().Where(e => e.Enable).OrderBy(e => e.ID).ToList();
        }

        /// <summary>获取当前模型的顶级分类</summary>
        /// <returns></returns>
        public IList<ICategory> GetTopCategories()
        {
            // 过滤得到该模型的所有分类，然后按照深度排序
            var list = Category.FindAllWithCache().ToList().Where(e => e.ModelID == ID);
            if (list.Any())
            {
                var min = list.Min(e => e.Deepth);
                return list.Where(e => e.Deepth == min).Cast<ICategory>().ToList();
            }
            return new List<ICategory>();
        }
        #endregion

        #region 扩展操作
        /// <summary>显示友好名称</summary>
        /// <returns></returns>
        public override String ToString()
        {
            if (!DisplayName.IsNullOrEmpty())
                return DisplayName;
            else
                return Name;
        }
        #endregion

        #region 业务
        /// <summary>添加模型</summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Model Add(String name)
        {
            var entity = new Model()
            {
                Name = name,
                Enable = true
            };
            entity.Save();

            return entity;
        }

        /// <summary>扫描所有模型提供者，并添加到数据库</summary>
        /// <returns></returns>
        public static Int32 Scan()
        {
            var count = 0;

            // 预设顺序
            var ms = "Text,Article,Photo,Video,Product,Down".Split(",");

            foreach (var item in typeof(IInfoExtend).GetAllSubclasses(true).OrderBy(e => Array.IndexOf(ms, e.Name)))
            {
                //var entity = FindByName(item.Name);
                var entity = Find(__.Name, item.Name);
                if (entity == null) entity = new Model();

                entity.Name = item.Name;
                entity.DisplayName = item.GetDisplayName() ?? item.GetDescription();
                entity.ProviderName = item.FullName;

                // 默认初始化路径
                entity.IndexTemplate = "~/Views/{0}/Index.cshtml".F(entity.Name);
                entity.CategoryTemplate = "~/Views/{0}/Category.cshtml".F(entity.Name);
                entity.InfoTemplate = "~/Views/{0}/Info.cshtml".F(entity.Name);

                entity.Enable = true;
                entity.Save();

                count++;
            }

            return count;
        }
        #endregion
    }

    partial interface IModel
    {
        /// <summary>获取当前模型的顶级分类</summary>
        /// <returns></returns>
        IList<ICategory> GetTopCategories();
    }
}
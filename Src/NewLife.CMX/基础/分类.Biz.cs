/*
 * XCoder v6.6.5879.6460
 * 作者：Stone/X
 * 时间：2016-02-06 17:22:55
 * 版权：版权所有 (C) 新生命开发团队 2002~2016
*/
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using NewLife.Log;
using NewLife.Web;
﻿using NewLife.Data;
using XCode;
using XCode.Configuration;
using XCode.Membership;
using System.IO;
using System.Linq;

namespace NewLife.CMX
{
    /// <summary>分类</summary>
    [ModelCheckMode(ModelCheckModes.CheckTableWhenFirstUse)]
    public class Category : Category<Category> { }

    /// <summary>分类</summary>
    public partial class Category<TEntity> : EntityTree<TEntity>, ICategory where TEntity : Category<TEntity>, new()
    {
        #region 对象操作

        static Category()
        {
            // 用于引发基类的静态构造函数，所有层次的泛型实体类都应该有一个
            TEntity entity = new TEntity();
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;

            // 这里验证参数范围，建议抛出参数异常，指定参数名，前端用户界面可以捕获参数异常并聚焦到对应的参数输入框
            if (Model == null) throw new ArgumentNullException(__.ModelID, _.ModelID.DisplayName + "无效！");
            //if (String.IsNullOrEmpty(Name)) throw new ArgumentNullException(_.Name, _.Name.DisplayName + "无效！");
            //if (!isNew && ID < 1) throw new ArgumentOutOfRangeException(_.ID, _.ID.DisplayName + "必须大于0！");

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
            var fn = "../InitData/{0}.json".F(Meta.TableName).GetFullPath();
            if (File.Exists(fn))
            {
                if (XTrace.Debug) XTrace.WriteLine("使用数据初始化文件【{0}】初始化{1}[{2}]数据……", fn, typeof(TEntity).Name, Meta.Table.DataTable.DisplayName);

                var list = EntityList<TEntity>.FromJson(File.ReadAllText(fn, Encoding.UTF8));
                var queue = new Queue<TEntity>(list);
                while (queue.Count > 0)
                {
                    var item = queue.Dequeue();
                    item.Save();
                    if (item.Childrens != null && item.Childrens.Count > 0)
                    {
                        foreach (var child in item.Childrens)
                        {
                            child.ParentID = item.ID;
                            queue.Enqueue(child);
                        }
                    }
                }
            }
            else
            {
                // 遍历模型
                NewLife.CMX.Model.Meta.Session.WaitForInitData();

                foreach (var item in NewLife.CMX.Model.FindAllWithCache())
                {
                    var entity = new TEntity
                    {
                        Name = item.DisplayName ?? item.Name,
                        ModelID = item.ID
                    };
                    entity.Insert();

                    entity = new TEntity
                    {
                        ParentID = entity.ID,
                        Name = "二级" + item.DisplayName ?? item.Name,
                        ModelID = item.ID
                    };
                    entity.Insert();
                }
            }
            if (XTrace.Debug) XTrace.WriteLine("完成初始化{0}[{1}]数据！", typeof(TEntity).Name, Meta.Table.DataTable.DisplayName);
        }

        ///// <summary>已重载。基类先调用Valid(true)验证数据，然后在事务保护内调用OnInsert</summary>
        ///// <returns></returns>
        //public override Int32 Insert()
        //{
        //    return base.Insert();
        //}

        ///// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        ///// <returns></returns>
        //protected override Int32 OnInsert()
        //{
        //    return base.OnInsert();
        //}

        #endregion

        #region 扩展属性
        [NonSerialized]
        private IModel _Model;
        /// <summary>该分类所对应的模型</summary>
        [XmlIgnore]
        [BindRelation("ModelID", false, "Model", "ID")]
        public IModel Model
        {
            get
            {
                if (_Model == null && ModelID > 0 && !Dirtys.ContainsKey("Model"))
                {
                    _Model = NewLife.CMX.Model.FindByID(ModelID);
                    Dirtys["Model"] = true;
                }
                return _Model;
            }
            set { _Model = value; }
        }

        /// <summary>该分类所对应的模型名称</summary>
        [XmlIgnore]
        [DisplayName("模型名")]
        public String ModelName { get { return Model != null ? Model.Name : String.Empty; } }

        public EntityList<TEntity> Childrens { get; set; }
        #endregion

        #region 扩展查询
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
      ﻿
        /// <summary>根据名称查找</summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static TEntity FindByName(String name)
        {
            if (Meta.Count >= 1000)
                return Find(__.Name, name);
            else // 实体缓存
                return Meta.Cache.Entities.Find(__.Name, name);
        }
      ﻿
        /// <summary>根据代码查找</summary>
        /// <param name="code">代码</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static TEntity FindByCode(String code)
        {
            if (Meta.Count >= 1000)
                return Find(__.Code, code);
            else // 实体缓存
                return Meta.Cache.Entities.Find(__.Code, code);
        }

        ///// <summary>根据父类查找</summary>
        ///// <param name="parentid">父类</param>
        ///// <returns></returns>
        //[DataObjectMethod(DataObjectMethodType.Select, false)]
        //public static EntityList<TEntity> FindAllByParentID(Int32 parentid)
        //{
        //    if (Meta.Count >= 1000)
        //        return FindAll(__.ParentID, parentid);
        //    else // 实体缓存
        //        return Meta.Cache.Entities.FindAll(__.ParentID, parentid);
        //}
        #endregion

        #region 高级查询
        public IList<IInfo> GetTitles(Int32 pageIndex = 1, Int32 pageSize = 10)
        {
            //var provider = ModelProvider.Get(this.GetType());
            //return provider.TitleFactory.GetTitles(ID, pageIndex, pageSize);
            var pager = new PageParameter { PageIndex = pageIndex, PageSize = pageSize };

            return GetTitles(pager);
        }

        //public int GetTitleCount()
        //{
        //    var provider = ModelProvider.Get(this.GetType());
        //    return provider.TitleFactory.GetTitleCount(this.ID);
        //}

        public IList<IInfo> GetTitles(PageParameter pager)
        {
            //var provider = ModelProvider.Get(this.GetType());
            //return provider.TitleFactory.GetTitles(ID, pager);

            //todo GetTitles(PageParameter pager)

            return Info.Search(0, ID, null, pager).ToList().Cast<IInfo>().ToList();
        }

        //public IEntityTitle FindTitle(int id)
        //{
        //    var provider = ModelProvider.Get(this.GetType());
        //    return provider.TitleFactory.FindByID(id);
        //}

        #endregion

        #region 扩展操作
        #endregion

        #region 业务
        #endregion

        #region 模版
        /// <summary>获取模版。就近原则</summary>
        /// <param name="kind">模版类型。Category/Title/Detail</param>
        /// <returns></returns>
        public String GetTemplate(String kind)
        {
            if (String.IsNullOrEmpty(kind)) kind = "category";

            kind = (kind + "").Trim().ToLower();

            var tmp = "";
            switch (kind)
            {
                case "category":
                    tmp = CategoryTemplate;
                    break;
                case "title":
                    tmp = InfoTemplate;
                    break;
                default:
                    break;
            }
            if (!tmp.IsNullOrWhiteSpace()) return tmp;

            // 找上一级
            if (ParentID > 0 && Parent != null) return Parent.GetTemplate(kind);

            // 如果顶级分类，则找频道
            if (Model != null)
            {
                switch (kind)
                {
                    case "category":
                        tmp = Model.CategoryTemplate;
                        break;
                    case "title":
                        tmp = Model.InfoTemplate;
                        break;
                    default:
                        break;
                }
                if (!tmp.IsNullOrWhiteSpace()) return tmp;
            }

            return null;
        }

        /// <summary>获取分类模版</summary>
        /// <returns></returns>
        public String GetCategoryTemplate() { return GetTemplate("category"); }

        /// <summary>获取标题模版</summary>
        /// <returns></returns>
        public String GetTitleTemplate() { return GetTemplate("title"); }
        #endregion
    }

    partial interface ICategory
    {
        IModel Model { get; }

        IList<IInfo> GetTitles(Int32 pageIndex = 1, Int32 pageCount = 10);
        //Int32 GetTitleCount();

        IList<IInfo> GetTitles(PageParameter pager);

        //IInfo FindTitle(Int32 id);

        /// <summary>获取分类模版</summary>
        /// <returns></returns>
        String GetCategoryTemplate();

        /// <summary>获取标题模版</summary>
        /// <returns></returns>
        String GetTitleTemplate();
    }
}
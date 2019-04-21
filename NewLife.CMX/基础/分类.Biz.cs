/*
 * XCoder v6.6.5879.6460
 * 作者：Stone/X
 * 时间：2016-02-06 17:22:55
 * 版权：版权所有 (C) 新生命开发团队 2002~2016
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife.Collections;
using NewLife.Common;
using NewLife.Data;
using NewLife.Log;
using XCode;
using XCode.Membership;

namespace NewLife.CMX
{
    /// <summary>分类</summary>
    public partial class Category : EntityTree<Category>, ICategory
    {
        #region 对象操作
        static Category()
        {
            Meta.Modules.Add<UserModule>();
            Meta.Modules.Add<TimeModule>();
            Meta.Modules.Add<IPModule>();
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;

            // 这里验证参数范围，建议抛出参数异常，指定参数名，前端用户界面可以捕获参数异常并聚焦到对应的参数输入框
            if (Name.IsNullOrEmpty()) throw new ArgumentNullException(_.Name, _.Name.DisplayName + "无效！");
            if (Model == null) throw new ArgumentNullException(__.ModelID, _.ModelID.DisplayName + "无效！");

            // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
            base.Valid(isNew);

            if (Dirtys[__.Code])
            {
                // 已设置代码，检查是否唯一
                if (!Code.IsNullOrEmpty()) CheckExist(__.Code);
            }
            else if (isNew)
            {
                // 未设置代码，自动生成
                var code = PinYin.GetFirst(Name);
                Code = code;
                for (var i = 2; i < 100; i++)
                {
                    if (!Exist(__.Code)) break;

                    Code = code + i;
                }
            }
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
            if (XTrace.Debug) XTrace.WriteLine("开始初始化{0}[{1}]数据……", typeof(Category).Name, Meta.Table.DataTable.DisplayName);

            // 遍历模型
            CMX.Model.Meta.Session.WaitForInitData();

            var sort = 100;
            foreach (var item in CMX.Model.FindAllWithCache())
            {
                var entity = new Category
                {
                    Name = (item.DisplayName ?? item.Name),
                    ModelID = item.ID
                };
                entity.Sort = sort--;
                entity.Insert();

                entity = new Category
                {
                    ParentID = entity.ID,
                    Name = "二级" + item.DisplayName ?? item.Name,
                    ModelID = item.ID,
                    Code = entity.Code + "2",
                };
                entity.Insert();
            }

            if (XTrace.Debug) XTrace.WriteLine("完成初始化{0}[{1}]数据！", typeof(Category).Name, Meta.Table.DataTable.DisplayName);
        }
        #endregion

        #region 扩展属性
        /// <summary>该分类所对应的模型</summary>
        [XmlIgnore, ScriptIgnore]
        public IModel Model => Extends.Get(nameof(Model), k => NewLife.CMX.Model.FindByID(ModelID));

        /// <summary>该分类所对应的模型名称</summary>
        [XmlIgnore, ScriptIgnore]
        [DisplayName("模型")]
        [Map(__.ModelID, typeof(Model), "ID")]
        public String ModelName => Model + "";
        #endregion

        #region 扩展查询
        /// <summary>根据ID查询</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Category FindByID(Int32 id)
        {
            if (id <= 0) return null;

            if (Meta.Count >= 1000)
                return Meta.SingleCache[id];
            else
                return Meta.Cache.Find(e => e.ID == id);
        }

        /// <summary>根据名称查找</summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Category FindByName(String name)
        {
            if (name.IsNullOrEmpty()) return null;

            if (Meta.Count >= 1000)
                return Find(__.Name, name);
            else // 实体缓存
                return Meta.Cache.Find(e => e.Name == name);
        }

        /// <summary>根据代码查找</summary>
        /// <param name="code">代码</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Category FindByCode(String code)
        {
            if (code.IsNullOrEmpty()) return null;

            if (Meta.Count >= 1000)
                return Find(__.Code, code);
            else // 实体缓存
                return Meta.Cache.Find(e => e.Code == code);
        }
        #endregion

        #region 高级查询
        /// <summary>获取该分类以及子孙分类的所有有效信息</summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<IInfo> GetInfos(Int32 pageIndex = 1, Int32 pageSize = 10)
        {
            var pager = new PageParameter { PageIndex = pageIndex, PageSize = pageSize };

            return GetInfos(pager);
        }

        DictionaryCache<String, IList<IInfo>> _cache = new DictionaryCache<String, IList<IInfo>>()
        {
            Period = 60,
        };
        /// <summary>获取该分类以及子孙分类的所有有效信息。带60秒异步缓存</summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        public IList<IInfo> GetInfos(PageParameter pager)
        {
            var key = "{0}-{1}".F(ID, pager.GetKey());
            return _cache.GetItem(key, k => Info.Search(0, ID, null, pager).Cast<IInfo>().ToList());
        }
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
            if (kind.IsNullOrEmpty()) kind = "category";

            kind = (kind + "").Trim().ToLower();

            var tmp = "";
            switch (kind)
            {
                case "category":
                    tmp = CategoryTemplate;
                    break;
                case "info":
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
                    case "info":
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
        public String GetCategoryTemplate() => GetTemplate("category");

        /// <summary>获取标题模版</summary>
        /// <returns></returns>
        public String GetInfoTemplate() => GetTemplate("info");
        #endregion
    }

    partial interface ICategory : IEntityTree
    {
        /// <summary>模型</summary>
        IModel Model { get; }

        /// <summary>获取该分类以及子孙分类的所有有效信息</summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        IList<IInfo> GetInfos(Int32 pageIndex = 1, Int32 pageCount = 10);

        /// <summary>获取该分类以及子孙分类的所有有效信息</summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        IList<IInfo> GetInfos(PageParameter pager);

        /// <summary>获取分类模版</summary>
        /// <returns></returns>
        String GetCategoryTemplate();

        /// <summary>获取信息模版</summary>
        /// <returns></returns>
        String GetInfoTemplate();
    }
}
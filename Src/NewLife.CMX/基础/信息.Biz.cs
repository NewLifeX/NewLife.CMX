/*
 * XCoder v6.6.5879.6460
 * 作者：Stone/X
 * 时间：2016-02-06 19:22:33
 * 版权：版权所有 (C) 新生命开发团队 2002~2016
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife.Data;
using NewLife.Log;
using NewLife.Model;
using NewLife.Reflection;
using XCode;
using XCode.Membership;

namespace NewLife.CMX
{
    /// <summary>信息</summary>
    [ModelCheckMode(ModelCheckModes.CheckTableWhenFirstUse)]
    public class Info : Info<Info> { }

    /// <summary>信息</summary>
    public partial class Info<TEntity> : Entity<TEntity> where TEntity : Info<TEntity>, new()
    {
        #region 对象操作
        static Info()
        {
            // 用于引发基类的静态构造函数，所有层次的泛型实体类都应该有一个
            var entity = new TEntity();

            Meta.Factory.AdditionalFields.Add(__.Views);

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

            // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
            base.Valid(isNew);

            // 自动保存分类名称
            if (!Dirtys[__.CategoryName] && Category != null) CategoryName = Category.Name;

            // 发布时间为创建时间
            if ((isNew || PublishTime.Year < 2000) && !Dirtys[__.PublishTime]) PublishTime = CreateTime;
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

            // 遍历分类
            NewLife.CMX.Category.Meta.Session.WaitForInitData();

            var sb = new StringBuilder();
            for (var i = 0; i < 20; i++)
            {
                sb.AppendLine("新生命开发团队，学无先后达者为师<br>");
            }
            var txt = sb.ToString();

            foreach (var item in NewLife.CMX.Category.FindAllWithCache())
            {
                var entity = new TEntity()
                {
                    ModelID = item.ModelID,
                    CategoryID = item.ID,
                    CategoryName = item.Name,
                    Title = "{0}信息".F(item.Name),
                    ContentText = txt,
                    ExtendID = 0
                };
                entity.Insert();
            }

            if (XTrace.Debug) XTrace.WriteLine("完成初始化{0}[{1}]数据！", typeof(TEntity).Name, Meta.Table.DataTable.DisplayName);
        }

        /// <summary>同步插入统计信息和内容信息</summary>
        /// <returns></returns>
        protected override Int32 OnInsert()
        {
            if (!Dirtys["Version"]) Version = 1;

            var num = 0;
            // 保存统计
            var stat = Statistics;
            if (stat != null) (stat as IEntity).Insert();

            StatisticsID = stat.ID;

            num += base.OnInsert();

            // 保存内容
            //HelperTool.SaveModelContent(typeof(TContent), Version, ChannelSuffix, this, null);
            var entity = Content;
            entity.InfoID = ID;
            entity.Title = Title;
            entity.Version = Version;
            num += (entity as IEntity).Insert();

            return num;
        }

        /// <summary>同步更新内容，但不同步更新统计</summary>
        protected override Int32 OnUpdate()
        {
            var rs = 0;
            // 如果内容数据有修改，插入新内容
            //if ((Content as IEntity).Dirtys.Any(e => e.Value))
            if ((Content as IEntity).Dirtys.Count > 0)
            {
                //由于原先的添加方法存在BUG无法给ParentID赋值，给字段赋引用类型值时无法触发脏数据更新标记
                //故创建新的对象
                var entity = (Content as IEntity).CloneEntity(true) as IContent;
                entity.ID = 0;
                entity.InfoID = ID;
                entity.Version++;
                entity.Title = Title;
                rs += (entity as IEntity).Insert();

                Version = entity.Version;
            }

            // 同步访问量
            if (HasDirty) Views = Statistics.Total;

            return rs + base.OnUpdate();
        }

        /// <summary>已重载。关联删除内容和统计</summary>
        /// <returns></returns>
        protected override Int32 OnDelete()
        {
            // 删内容
            NewLife.CMX.Content.DeleteByParentID(ID);

            // 删统计
            (Statistics as IEntity).Delete();

            return base.OnDelete();
        }
        #endregion

        #region 扩展属性
        /// <summary>该分类所对应的模型</summary>
        [XmlIgnore, ScriptIgnore]
        public IModel Model { get { return Extends.Get(nameof(Model), k => ModelX.FindByID(ModelID)); } }

        /// <summary>该分类所对应的模型名称</summary>
        [XmlIgnore, ScriptIgnore]
        [DisplayName("模型")]
        [Map(__.ModelID, typeof(ModelX), "ID")]
        public String ModelName { get { return Model + ""; } }

        /// <summary>分类</summary>
        [XmlIgnore, ScriptIgnore]
        public ICategory Category { get { return Extends.Get(nameof(Category), k => NewLife.CMX.Category.FindByID(CategoryID)); } }

        ///// <summary>分类</summary>
        //[XmlIgnore, ScriptIgnore]
        //[DisplayName("分类")]
        //[Map(__.CategoryID, typeof(Category), "ID")]
        //public String CategoryName { get { return Category + ""; } }

        /// <summary>内容</summary>
        [XmlIgnore, ScriptIgnore]
        public IContent Content { get { return Extends.Get(nameof(Content), k => NewLife.CMX.Content.FindLastByParentID(ID) ?? new Content { InfoID = ID }); } }

        /// <summary>内容文本</summary>
        public String ContentText { get { return Content?.Html; } set { Content.Html = value; } }

        /// <summary>扩展</summary>
        public IInfoExtend Ext
        {
            get
            {
                return Extends.Get(nameof(Ext), k =>
                {
                    // 根据分类找到模型，再找到对应实体类
                    if (Model != null)
                    {
                        var type = Model.ProviderName.GetTypeEx();
                        if (type != null)
                        {
                            // 反射调用FindByID方法
                            //var entity = type.Invoke("FindByID", ExtendID);
                            var fact = EntityFactory.CreateOperate(type);
                            var entity = fact.FindByKey(ExtendID);
                            return entity as IInfoExtend;
                        }
                    }
                    return null;
                });
            }
        }

        /// <summary>统计</summary>
        [XmlIgnore, ScriptIgnore]
        public IStatistics Statistics
        {
            get
            {
                return Extends.Get(nameof(Statistics), k =>
                {
                    var st = NewLife.CMX.Statistics.FindByID(StatisticsID);
                    if (st == null)
                    {
                        st = new Statistics();
                        st.Insert();

                        StatisticsID = st.ID;
                    }
                    return st;
                });
            }
        }

        /// <summary>统计</summary>
        [XmlIgnore, ScriptIgnore]
        [DisplayName("访问统计")]
        [Map(__.StatisticsID, typeof(Statistics), "ID")]
        public String StatisticsText { get { return Statistics?.Text; } }

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

        #region 扩展查询
        /// <summary>根据ID查询</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static TEntity FindByID(Int32 id)
        {
            if (id <= 0) return null;

            if (Meta.Count >= 1000)
                return Meta.SingleCache[id];
            else
                return Meta.Cache.Entities.Find(__.ID, id);
        }

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

        /// <summary>根据模型查找</summary>
        /// <param name="modelid">模型</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static IList<TEntity> FindAllByModelID(Int32 modelid)
        {
            if (Meta.Count >= 1000)
                return FindAll(__.ModelID, modelid);
            else // 实体缓存
                return Meta.Cache.Entities.FindAll(__.ModelID, modelid);
        }

        /// <summary>根据扩展查找</summary>
        /// <param name="extendid">扩展</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static IList<TEntity> FindAllByExtendID(Int32 extendid)
        {
            if (Meta.Count >= 1000)
                return FindAll(__.ExtendID, extendid);
            else // 实体缓存
                return Meta.Cache.Entities.FindAll(__.ExtendID, extendid);
        }

        /// <summary>根据分类查找</summary>
        /// <param name="categoryid">分类</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static IList<TEntity> FindAllByCategoryID(Int32 categoryid)
        {
            if (Meta.Count >= 1000)
                return FindAll(__.CategoryID, categoryid);
            else // 实体缓存
                return Meta.Cache.Entities.FindAll(__.CategoryID, categoryid);
        }

        /// <summary>根据发布时间查找</summary>
        /// <param name="publishtime">发布时间</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static IList<TEntity> FindAllByPublishTime(DateTime publishtime)
        {
            if (Meta.Count >= 1000)
                return FindAll(__.PublishTime, publishtime);
            else // 实体缓存
                return Meta.Cache.Entities.FindAll(__.PublishTime, publishtime);
        }

        #endregion

        #region 高级查询
        /// <summary>根据关键字搜索指定模型和分类下的信息</summary>
        /// <param name="modelid"></param>
        /// <param name="categoryid"></param>
        /// <param name="key"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IList<TEntity> Search(Int32 modelid, Int32 categoryid, String key, PageParameter param)
        {
            var exp = new WhereExpression();

            if (categoryid > 0)
            {
                var cat = NewLife.CMX.Category.FindByID(categoryid);
                if (cat != null) exp &= _.CategoryID.In(cat.MyAllChilds.Select(e => e.ID));
            }
            else if (modelid > 0)
                exp &= _.ModelID == modelid;

            if (!key.IsNullOrEmpty()) exp &= SearchWhereByKeys(key, null, null);

            return FindAll(exp, param);
        }
        #endregion

        #region 扩展操作
        #endregion

        #region 业务
        #endregion
    }

    partial interface IInfo //: IUserInfo
    {
        /// <summary>模型</summary>
        IModel Model { get; }

        /// <summary>当前主题的分类</summary>
        ICategory Category { get; }

        /// <summary>扩展信息</summary>
        IInfoExtend Ext { get; }

        /// <summary>主要内容</summary>
        String ContentText { get; set; }

        /// <summary>统计</summary>
        IStatistics Statistics { get; }

        /// <summary>创建者</summary>
        String CreateUserName { get; }
    }
}
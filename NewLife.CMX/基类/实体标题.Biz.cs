﻿/*
 * XCoder v6.0.5096.30596
 * 作者：nnhy/X2
 * 时间：2013-12-14 17:00:07
 * 版权：版权所有 (C) 新生命开发团队 2002~2013
*/
﻿using System;
using System.ComponentModel;
using System.Linq;
using NewLife.Data;
using NewLife.Log;
using NewLife.Web;
using XCode;
using XCode.Membership;

namespace NewLife.CMX
{
    /// <summary>实体标题</summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TCategory"></typeparam>
    /// <typeparam name="TContent"></typeparam>
    public abstract class EntityTitle<TEntity, TCategory, TContent, TStatistics> : EntityTitle<TEntity>
        where TEntity : EntityTitle<TEntity>, new()
        where TCategory : EntityCategory<TCategory>, new()
        where TContent : EntityContent<TContent>, new()
        where TStatistics : Statistics<TStatistics>, new()
    {
        #region 对象操作﻿
        public override void Valid(bool isNew)
        {
            base.Valid(isNew);

            // 自动保存分类名称
            if (Meta.FieldNames.Contains("CategoryName"))
            {
                if (!Dirtys["CategoryName"] && Category != null) SetItem("CategoryName", Category.Name);
            }

            // 发布时间为创建时间
            //if (!Dirtys[__.PublishTime]) PublishTime = DateTime.Now;
            if (Meta.FieldNames.Contains("PublishTime"))
            {
                var dt = (DateTime)this["PublishTime"];
                if ((isNew || dt.Year < 2000) && !Dirtys["PublishTime"]) SetItem("PublishTime", CreateTime);
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
            if (XTrace.Debug) XTrace.WriteLine("开始初始化{0}[{1}]数据……", typeof(TEntity).Name, Meta.Table.DataTable.DisplayName);

            // 配套的分类
            //EntityCategory<TCategory>.Meta.WaitForInitData();
            var cat = EntityCategory<TCategory>.FindAllWithCache().ToList().FirstOrDefault();
            var des = typeof(TEntity).GetCustomAttribute<DescriptionAttribute>();

            var entity = new TEntity();
            entity.CategoryID = cat.ID;
            entity.CategoryName = cat.Name;
            entity.Title = des.Description + "1";
            entity.Insert();

            //entity.Title = des.Description + "2";
            //entity.Insert();

            if (XTrace.Debug) XTrace.WriteLine("完成初始化{0}[{1}]数据！", typeof(TEntity).Name, Meta.Table.DataTable.DisplayName);
        }
        #endregion

        #region 扩展属性
        private TCategory _Category;
        /// <summary>分类</summary>
        public TCategory Category
        {
            get
            {
                if (_Category == null && CategoryID > 0 && !Dirtys.ContainsKey("Category"))
                {
                    _Category = EntityCategory<TCategory>.FindByID(CategoryID);
                    Dirtys["Category"] = true;
                }
                return _Category;
            }
            //set { _Category = value; }
        }

        protected internal override IEntityCategory Category_ { get { return Category; } }

        private TContent _Content;
        /// <summary>内容</summary>
        public TContent Content
        {
            get
            {
                if (_Content == null)
                {
                    _Content = EntityContent<TContent>.FindLastByParentID(ID);
                    if (_Content == null) _Content = new TContent { ParentID = ID };
                }
                return _Content;
            }
            //set { _Content = value; }
        }

        /// <summary>内容文本</summary>
        public override String ContentText { get { return Content != null ? Content.Content : ""; } set { Content.Content = value; } }

        private TStatistics _Statistics;
        /// <summary>统计</summary>
        public TStatistics Statistics
        {
            get
            {
                if (_Statistics == null && !Dirtys.ContainsKey("Statistics"))
                {
                    _Statistics = Statistics<TStatistics>.FindByID(StatisticsID);
                    if (_Statistics == null) _Statistics = new TStatistics();
                    Dirtys["Statistics"] = true;
                }
                return _Statistics;
            }
            //set { _Statistics = value; }
        }

        protected internal override IStatistics Statistics_ { get { return Statistics; } }

        private String _StatisticsText;
        /// <summary>统计文本</summary>
        [DisplayName("访问统计")]
        public String StatisticsText
        {
            get
            {
                if (_StatisticsText == null && !Dirtys.ContainsKey("StatisticsText"))
                {
                    _StatisticsText = Statistics != null ? Statistics.Text : null;
                    Dirtys["StatisticsText"] = true;
                }
                return _StatisticsText;
            }
        }
        #endregion

        #region 对象操作
        /// <summary>同步插入统计信息和内容信息</summary>
        /// <returns></returns>
        protected override Int32 OnInsert()
        {
            if (!Dirtys["Version"]) Version = 1;

            var num = 0;
            // 保存统计
            var stat = Statistics;
            if (stat != null) stat.Insert();

            this.StatisticsID = stat.ID;

            num += base.OnInsert();

            // 保存内容
            //HelperTool.SaveModelContent(typeof(TContent), Version, ChannelSuffix, this, null);
            var entity = Content;
            entity.ParentID = ID;
            entity.Title = Title;
            entity.Version = Version;
            num += entity.Insert();

            return num;
        }

        /// <summary>同步更新内容，但不同步更新统计</summary>
        protected override int OnUpdate()
        {
            var rs = 0;
            // 如果内容数据有修改，插入新内容
            //if ((Content as IEntity).Dirtys.Any(e => e.Value))
            if ((Content as IEntity).Dirtys.Count > 0)
            {
                //由于原先的添加方法存在BUG无法给ParentID赋值，给字段赋引用类型值时无法触发脏数据更新标记
                //故创建新的对象
                var entity = Content.CloneEntity(true);
                entity.ID = 0;
                entity.ParentID = ID;
                entity.Version++;
                entity.Title = Title;
                rs += entity.Insert();

                this.Version = entity.Version;
            }

            // 同步访问量
            if (HasDirty) Views = Statistics.Total;

            return rs + base.OnUpdate();
        }

        protected override int OnDelete()
        {
            // 删内容
            //Content.Delete();
            EntityContent<TContent>.DeleteByParentID(ID);

            // 删统计
            Statistics.Delete();

            return base.OnDelete();
        }
        #endregion

        #region 业务
        /// <summary>增加访问量</summary>
        public override void IncView()
        {
            Statistics<TStatistics>.Increment(StatisticsID);
        }
        #endregion
    }

    /// <summary>实体标题</summary>
    public partial class EntityTitle<TEntity> : UserTimeEntity<TEntity> where TEntity : EntityTitle<TEntity>, new()
    {
        #region 对象操作﻿
        static EntityTitle()
        {
            // 用于引发基类的静态构造函数，所有层次的泛型实体类都应该有一个
            TEntity entity = new TEntity();

            EntityFactory.Register(typeof(TEntity), new TitleFactory<TEntity>());

            Meta.Factory.AdditionalFields.Add(__.Views);
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
            // 没有数据改动的Update直接跳过
            if (!isNew && !HasDirty) return;

            // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
            base.Valid(isNew);

            //var mp = ManageProvider.Provider;
            //// 创建者信息
            //if (isNew && !Dirtys[__.CreateTime])
            //{
            //    CreateTime = DateTime.Now;
            //    if (mp.Current != null)
            //    {
            //        CreateUserID = mp.Current.ID;
            //        CreateUserName = mp.Current.ToString();
            //    }
            //}
            //// 更新者信息
            //if (!Dirtys[__.UpdateTime])
            //{
            //    UpdateTime = DateTime.Now;
            //    if (mp.Current != null)
            //    {
            //        UpdateUserID = mp.Current.ID;
            //        UpdateUserName = mp.Current.ToString();
            //    }
            //}
        }
        #endregion

        #region 扩展属性﻿
        /// <summary>当前主题的分类</summary>
        IEntityCategory IEntityTitle.Category { get { return Category_; } }

        protected internal abstract IEntityCategory Category_ { get; }

        /// <summary>内容文本</summary>
        public abstract String ContentText { get; set; }

        /// <summary>当前主题的统计</summary>
        IStatistics IEntityTitle.Statistics { get { return Statistics_; } }

        protected internal abstract IStatistics Statistics_ { get; }

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

        /// <summary>根据分类查找</summary>
        /// <param name="categoryid">分类</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EntityList<TEntity> FindAllByCategoryID(Int32 categoryid, Int32 start = 0, Int32 max = 10)
        {
            if (Meta.Count >= 1000)
                return FindAll(_.CategoryID == categoryid, null, null, start, max);
            else // 实体缓存
                return Meta.Cache.Entities.FindAll(__.CategoryID, categoryid).Page(start, max);
        }

        public static EntityList<TEntity> GetPages(Int32 categoryId, Int32 pageIndex, Int32 pageSize,
            out Int32 recordCount)
        {
            var where = _.CategoryID == categoryId;
            recordCount = FindCount(where);
            return FindAll(where, _.ID.Desc(), null, (pageIndex - 1) * pageSize, pageSize);
        }
        #endregion

        #region 高级查询
        public static EntityList<TEntity> Search(Int32 categoryid, Pager p)
        {
            var exp = _.CategoryID == categoryid;
            exp.SetStrict();

            return FindAll(exp, p);
        }

        //public static EntityList<TEntity> GetTitles(Int32 categoryid, Int32 pageIndex = 1, Int32 pageCount = 10)
        //{
        //    if (pageIndex <= 0) pageIndex = 1;

        //    var provider = ModelProvider.Get(typeof(TEntity));
        //    var cat = provider.CategoryFactory.FindByID(categoryid);
        //    var childs = cat.FindAllChildsExcept(null).Cast<IEntityCategory>();
        //    // 只要末级节点
        //    childs = childs.Where(e => e.Childs.Count == 0);

        //    var exp = _.CategoryID.In(childs.Select(e => e.ID));
        //    //exp.SetStrict();

        //    return FindAll(exp, null, null, (pageIndex - 1) * pageCount, pageCount);
        //}

        //public static Int32 GetTitleCount(Int32 categoryid)
        //{
        //    var provider = ModelProvider.Get(typeof(TEntity));
        //    var cat = provider.CategoryFactory.FindByID(categoryid);
        //    var childs = cat.FindAllChildsExcept(null).Cast<IEntityCategory>();
        //    // 只要末级节点
        //    childs = childs.Where(e => e.Childs.Count == 0);

        //    var exp = _.CategoryID.In(childs.Select(e => e.ID));
        //    return FindCount(exp);
        //    //return FindCount(_.CategoryID, categoryid);
        //}

        public static EntityList<TEntity> GetTitles(Int32 categoryid, PageParameter pager)
        {
            var provider = ModelProvider.Get(typeof(TEntity));
            var cat = provider.CategoryFactory.FindByID(categoryid);
            var childs = cat.FindAllChildsExcept(null).Cast<IEntityCategory>();
            // 只要末级节点
            childs = childs.Where(e => e.Childs.Count == 0);

            var exp = _.CategoryID.In(childs.Select(e => e.ID));
            //exp.SetStrict();

            // 发布时间未到
            //exp &= _.PublishTime <= DateTime.Now;
            // 为了避免给一级缓存带来压力，不要实时更新
            var dt = DateTime.Now;
            dt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
            exp &= _.PublishTime <= dt;

            // 按照发布时间降序
            if (pager.Sort.IsNullOrWhiteSpace()) pager.Sort = _.PublishTime.Desc();

            return FindAll(exp, pager);
        }
        #endregion

        #region 扩展操作
        #endregion

        #region 业务
        /// <summary>增加访问量</summary>
        public abstract void IncView();

        /// <summary>获取标题列表</summary>
        /// <param name="categoryPath"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static EntityList<TEntity> GetTitles(String categoryPath, Int32 pageIndex = 1, Int32 pageSize = 10)
        {
            return GetTitles(null, categoryPath, pageIndex, pageSize);
        }

        /// <summary>获取标题列表</summary>
        /// <param name="channelName">频道</param>
        /// <param name="categoryPath"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static EntityList<TEntity> GetTitles(String channelName, String categoryPath, Int32 pageIndex = 1, Int32 pageSize = 10)
        {
            var cat = FindOrCreateCategory(channelName, categoryPath);
            var pager = new PageParameter { PageIndex = pageIndex, PageSize = pageSize };

            return GetTitles(cat.ID, pager);
        }

        /// <summary>查找或创建分类</summary>
        /// <param name="channelName"></param>
        /// <param name="categoryPath"></param>
        /// <returns></returns>
        protected static IEntityCategory FindOrCreateCategory(String channelName, String categoryPath)
        {
            var chn = Channel.FindByName(channelName);
            if (chn == null)
            {
                var provider = ModelProvider.Get<TEntity>();
                var model = Model.FindByName(provider.Name);
                chn = Channel.FindAllWithCache().ToList().FirstOrDefault(e => e.ModelID == model.ID);
            }
            if (chn == null) throw new XException("设计错误！在实体[{0}]中无法找到频道[{1}]", typeof(TEntity).FullName, channelName);

            return chn.FindCategory(categoryPath);
        }
        #endregion
    }

    partial interface IEntityTitle : IUserInfo
    {
        /// <summary>当前主题的分类</summary>
        IEntityCategory Category { get; }

        /// <summary>主要内容</summary>
        String ContentText { get; set; }

        IStatistics Statistics { get; }
    }
}
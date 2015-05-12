/*
 * XCoder v6.0.5096.30596
 * 作者：nnhy/X2
 * 时间：2013-12-14 17:00:07
 * 版权：版权所有 (C) 新生命开发团队 2002~2013
*/
﻿using System;
using System.ComponentModel;
using System.Linq;
using NewLife.Log;
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
            set { _Category = value; }
        }

        private TContent _Content;
        /// <summary>内容</summary>
        public TContent Content
        {
            get
            {
                if (_Content == null && !Dirtys.ContainsKey("Content"))
                {
                    _Content = EntityContent<TContent>.FindLastByParentID(ID);
                    if (_Content == null) _Content = new TContent { ParentID = ID };
                    Dirtys["Content"] = true;
                }
                return _Content;
            }
            set { _Content = value; }
        }

        private String _ContentText;
        /// <summary>内容文本</summary>
        public String ContentText
        {
            get
            {
                if (_ContentText == null && !Dirtys.ContainsKey("ContentText"))
                {
                    _ContentText = Content.Content ?? "";
                    Dirtys["ContentText"] = true;
                }
                return _ContentText;
            }
            set { _ContentText = value; Content.Content = value; }
        }

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
            set { _Statistics = value; }
        }

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
    public partial class EntityTitle<TEntity> : EntityBase<TEntity> where TEntity : EntityTitle<TEntity>, new()
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
            // 更新者信息
            if (!Dirtys[__.UpdateTime])
            {
                UpdateTime = DateTime.Now;
                if (mp.Current != null)
                {
                    UpdateUserID = mp.Current.ID;
                    UpdateUserName = mp.Current.ToString();
                }
            }
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
        #endregion

        #region 高级查询
        #endregion

        #region 扩展操作
        #endregion

        #region 业务
        /// <summary>增加访问量</summary>
        public abstract void IncView();
        #endregion
    }
}
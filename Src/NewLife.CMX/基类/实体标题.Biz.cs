/*
 * XCoder v6.0.5096.30596
 * 作者：nnhy/X2
 * 时间：2013-12-14 17:00:07
 * 版权：版权所有 (C) 新生命开发团队 2002~2013
*/
﻿using System;
using System.ComponentModel;
using System.Linq;
using NewLife.CMX.Tool;
using NewLife.CommonEntity;
using NewLife.Log;
using XCode;

namespace NewLife.CMX
{
    /// <summary>实体标题</summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TCategory"></typeparam>
    /// <typeparam name="TContent"></typeparam>
    public abstract class EntityTitle<TEntity, TCategory, TContent> : EntityTitle<TEntity>
        where TEntity : EntityTitle<TEntity>, new()
        where TCategory : EntityCategory<TCategory>, new()
        where TContent : EntityContent<TContent>, new()
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
            EntityCategory<TCategory>.Meta.WaitForInitData();
            var cat = EntityCategory<TCategory>.FindAllWithCache().ToList().FirstOrDefault() as IEntityCategory;
            var des = typeof(TEntity).GetCustomAttribute<DescriptionAttribute>();

            var entity = new TEntity();
            entity.CategoryID = cat.ID;
            entity.CategoryName = cat.Name;
            entity.Title = des.Description + "1";
            entity.Insert();

            entity.Title = des.Description + "2";
            entity.Insert();

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
                    try
                    {
                        EntityFactory.CreateOperate(typeof(TCategory)).TableName += ChannelSuffix;
                        _Category = EntityCategory<TCategory>.FindByID(CategoryID);
                        if (_Category == null) _Category = new TCategory();
                        Dirtys["Category"] = true;
                    }
                    finally
                    {
                        EntityFactory.CreateOperate(typeof(TCategory)).TableName = "";
                    }
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
                    try
                    {
                        EntityFactory.CreateOperate(typeof(TContent)).TableName += ChannelSuffix;
                        _Content = EntityContent<TContent>.FindLastByParentID(ID);
                        if (_Content == null) _Content = new TContent();
                        Dirtys["Content"] = true;
                    }
                    finally
                    {
                        EntityFactory.CreateOperate(typeof(TContent)).TableName = "";
                    }
                }
                return _Content;
            }
            set { _Content = value; }
        }
        #endregion

        #region 对象操作
        /// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        /// <returns></returns>
        protected override Int32 OnInsert()
        {
            if (!Dirtys["Version"]) Version = 1;

            Int32 num = base.OnInsert();

            HelperTool.SaveModelContent(typeof(TContent), Version, ChannelSuffix, this, null);

            return num;
        }

        /// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        protected override int OnUpdate()
        {
            if (Dirtys["Content"])
            {
                if (!Dirtys["Version"]) Version++;

                HelperTool.SaveModelContent(typeof(TContent), Version, ChannelSuffix, this, null);
            }

            return base.OnUpdate();
        }

        protected override int OnDelete()
        {
            return base.OnDelete();
        }
        #endregion

        #region 频道
        /// <summary>采用线程静态，避免影响其它线程</summary>
        [ThreadStatic]
        private static String ChannelSuffix;

        /// <summary>设置频道后缀，指定标题、分类、内容数据表，仅本线程有效</summary>
        /// <param name="suffix"></param>
        public static void SetChannelSuffix(String suffix)
        {
            //if (ChannelSuffix == suffix) return;

            ChannelSuffix = suffix;

            Meta.TableName = Meta.Table.TableName + suffix;
            EntityCategory<TCategory>.Meta.TableName = EntityCategory<TCategory>.Meta.Table.TableName + suffix;
            EntityContent<TContent>.Meta.TableName = EntityContent<TContent>.Meta.Table.TableName + suffix;
        }

        private Channel _Channel;
        /// <summary>频道</summary>
        public Channel Channel
        {
            get
            {
                if (_Channel == null && ChannelSuffix != null && !Dirtys.ContainsKey("Channel"))
                {
                    _Channel = Channel.FindBySuffix(ChannelSuffix);
                    Dirtys["Channel"] = true;
                }
                return _Channel;
            }
            set { _Channel = value; }
        }

        /// <summary>频道名</summary>
        public String ChannelName { get { return Channel != null ? Channel.Name : null; } }
        #endregion
    }

    /// <summary>实体标题</summary>
    public partial class EntityTitle<TEntity> : Entity<TEntity> where TEntity : EntityTitle<TEntity>, new()
    {
        #region 对象操作﻿
        static EntityTitle()
        {
            // 用于引发基类的静态构造函数，所有层次的泛型实体类都应该有一个
            TEntity entity = new TEntity();
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
            // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
            base.Valid(isNew);

            var mp = ManageProvider.Provider;
            if (isNew && !Dirtys[__.CreateTime])
            {
                CreateTime = DateTime.Now;
                CreateUserID = mp.Current.ID;
                CreateUserName = mp.Current.ToString();
            }
            if (!Dirtys[__.UpdateTime])
            {
                UpdateTime = DateTime.Now;
                UpdateUserID = mp.Current.ID;
                UpdateUserName = mp.Current.ToString();
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
        public static EntityList<TEntity> FindAllByCategoryID(Int32 categoryid)
        {
            if (Meta.Count >= 1000)
                return FindAll(__.CategoryID, categoryid);
            else // 实体缓存
                return Meta.Cache.Entities.FindAll(__.CategoryID, categoryid);
        }
        #endregion

        #region 高级查询
        // 以下为自定义高级查询的例子

        ///// <summary>
        ///// 查询满足条件的记录集，分页、排序
        ///// </summary>
        ///// <param name="key">关键字</param>
        ///// <param name="orderClause">排序，不带Order By</param>
        ///// <param name="startRowIndex">开始行，0表示第一行</param>
        ///// <param name="maximumRows">最大返回行数，0表示所有行</param>
        ///// <returns>实体集</returns>
        //[DataObjectMethod(DataObjectMethodType.Select, true)]
        //public static EntityList<TEntity> Search(String key, String orderClause, Int32 startRowIndex, Int32 maximumRows)
        //{
        //    return FindAll(SearchWhere(key), orderClause, null, startRowIndex, maximumRows);
        //}

        ///// <summary>
        ///// 查询满足条件的记录总数，分页和排序无效，带参数是因为ObjectDataSource要求它跟Search统一
        ///// </summary>
        ///// <param name="key">关键字</param>
        ///// <param name="orderClause">排序，不带Order By</param>
        ///// <param name="startRowIndex">开始行，0表示第一行</param>
        ///// <param name="maximumRows">最大返回行数，0表示所有行</param>
        ///// <returns>记录数</returns>
        //public static Int32 SearchCount(String key, String orderClause, Int32 startRowIndex, Int32 maximumRows)
        //{
        //    return FindCount(SearchWhere(key), null, null, 0, 0);
        //}

        /// <summary>构造搜索条件</summary>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        private static String SearchWhere(String key)
        {
            // WhereExpression重载&和|运算符，作为And和Or的替代
            // SearchWhereByKeys系列方法用于构建针对字符串字段的模糊搜索
            var exp = SearchWhereByKeys(key, null);

            // 以下仅为演示，Field（继承自FieldItem）重载了==、!=、>、<、>=、<=等运算符（第4行）
            //if (userid > 0) exp &= _.OperatorID == userid;
            //if (isSign != null) exp &= _.IsSign == isSign.Value;
            //if (start > DateTime.MinValue) exp &= _.OccurTime >= start;
            //if (end > DateTime.MinValue) exp &= _.OccurTime < end.AddDays(1).Date;

            return exp;
        }
        #endregion

        #region 扩展操作
        #endregion

        #region 业务
        #endregion
    }
}
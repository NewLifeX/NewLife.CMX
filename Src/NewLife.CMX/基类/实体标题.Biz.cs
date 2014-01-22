/*
 * XCoder v6.0.5096.30596
 * 作者：nnhy/X2
 * 时间：2013-12-14 17:00:07
 * 版权：版权所有 (C) 新生命开发团队 2002~2013
*/
﻿using System;
using System.ComponentModel;
using System.Linq;
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
            //EntityCategory<TCategory>.Meta.WaitForInitData();
            var cat = EntityCategory<TCategory>.FindAllWithCache().ToList().FirstOrDefault() as IEntityCategory;
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
                    if (_Content == null) _Content = new TContent();
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
        #endregion

        #region 对象操作
        /// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        /// <returns></returns>
        protected override Int32 OnInsert()
        {
            if (!Dirtys["Version"]) Version = 1;

            Int32 num = base.OnInsert();

            //HelperTool.SaveModelContent(typeof(TContent), Version, ChannelSuffix, this, null);
            var entity = Content;
            entity.ParentID = ID;
            entity.Title = Title;
            entity.Version = Version;
            entity.Insert();

            return num;
        }

        /// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        protected override int OnUpdate()
        {
            //if (Dirtys["Content"])
            //{
            //    if (!Dirtys["Version"]) Version++;

            //    HelperTool.SaveModelContent(typeof(TContent), Version, ChannelSuffix, this, null);
            //}

            // 如果内容数据有修改，插入新内容
            //if ((Content as IEntity).Dirtys.Any(e => e.Value))
            if ((Content as IEntity).Dirtys.Count > 0)
            {
                //由于原先的添加方法存在BUG无法给ParentID赋值，给字段赋引用类型值时无法触发脏数据更新标记
                //故创建新的对象
                var newEntity = Content.CloneEntity(true);
                newEntity.ID = 0;
                newEntity.ParentID = ID;
                newEntity.Version++;
                newEntity.Title = Title;
                newEntity.CreateUserID = Admin.Current.ID;
                newEntity.Insert();

                //Content.ID = 0;
                //Content.ParentID = ID;
                //Content.Version++;
                //Content.Title = Title;
                //Content.CreateUserID = Admin.Current.ID;
                //Content.Save();

                //this.Version = Content.Version;
                this.Version = newEntity.Version;
            }

            return base.OnUpdate();
        }

        protected override int OnDelete()
        {
            Content.Delete();

            return base.OnDelete();
        }
        #endregion

        #region 频道
        ///// <summary>采用线程静态，避免影响其它线程</summary>
        //[ThreadStatic]
        //private static String ChannelSuffix;

        ///// <summary>设置频道后缀，指定标题、分类、内容数据表，仅本线程有效</summary>
        ///// <param name="suffix"></param>
        //public static void SetChannelSuffix(String suffix)
        //{
        //    //if (ChannelSuffix == suffix) return;

        //    ChannelSuffix = suffix;

        //    Meta.TableName = Meta.Table.TableName + suffix;
        //    EntityCategory<TCategory>.Meta.TableName = EntityCategory<TCategory>.Meta.Table.TableName + suffix;
        //    EntityContent<TContent>.Meta.TableName = EntityContent<TContent>.Meta.Table.TableName + suffix;
        //}

        //private Channel _Channel;
        ///// <summary>频道</summary>
        //public Channel Channel
        //{
        //    get
        //    {
        //        if (_Channel == null && !Dirtys.ContainsKey("Channel"))
        //        {
        //            _Channel = Channel.FindByID(ModelProvider<TEntity, TCategory, TContent>.CurrentChannel);
        //            Dirtys["Channel"] = true;
        //        }
        //        return _Channel;
        //    }
        //}

        ///// <summary>频道名</summary>
        //public String ChannelName { get { return Channel != null ? Channel.Name : null; } }
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
                if (mp.Current != null)
                {
                    CreateUserID = mp.Current.ID;
                    CreateUserName = mp.Current.ToString();
                }
            }
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
        public static EntityList<TEntity> FindAllByCategoryID(Int32 categoryid)
        {
            if (Meta.Count >= 1000)
                return FindAll(__.CategoryID, categoryid);
            else // 实体缓存
                return Meta.Cache.Entities.FindAll(__.CategoryID, categoryid);
        }
        #endregion

        #region 高级查询
        #endregion

        #region 扩展操作
        #endregion

        #region 业务
        #endregion
    }
}
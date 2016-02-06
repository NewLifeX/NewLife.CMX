/*
 * XCoder v6.6.5879.6460
 * 作者：Stone/X
 * 时间：2016-02-06 19:22:33
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

namespace NewLife.CMX
{
    /// <summary>信息</summary>
    [ModelCheckMode(ModelCheckModes.CheckTableWhenFirstUse)]
    public class Info : Info<Info> { }
    
    /// <summary>信息</summary>
    public partial class Info<TEntity> : Entity<TEntity> where TEntity : Info<TEntity>, new()
    {
        #region 对象操作
            ﻿
        static Info()
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
            //if (String.IsNullOrEmpty(Name)) throw new ArgumentNullException(_.Name, _.Name.DisplayName + "无效！");
            //if (!isNew && ID < 1) throw new ArgumentOutOfRangeException(_.ID, _.ID.DisplayName + "必须大于0！");

            // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
            base.Valid(isNew);

            // 在新插入数据或者修改了指定字段时进行唯一性验证，CheckExist内部抛出参数异常
            //if (isNew || Dirtys[__.Name]) CheckExist(__.Name);
            
            if (isNew && !Dirtys[__.CreateTime]) CreateTime = DateTime.Now;
            if (!Dirtys[__.CreateIP]) CreateIP = WebHelper.UserHost;
            if (!Dirtys[__.UpdateTime]) UpdateTime = DateTime.Now;
            if (!Dirtys[__.UpdateIP]) UpdateIP = WebHelper.UserHost;
        }

        ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected override void InitData()
        //{
        //    base.InitData();

        //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        //    // Meta.Count是快速取得表记录数
        //    if (Meta.Count > 0) return;

        //    // 需要注意的是，如果该方法调用了其它实体类的首次数据库操作，目标实体类的数据初始化将会在同一个线程完成
        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化{0}[{1}]数据……", typeof(TEntity).Name, Meta.Table.DataTable.DisplayName);

        //    var entity = new Info();
        //    entity.ModelID = 0;
        //    entity.CategoryID = 0;
        //    entity.CategoryName = "abc";
        //    entity.Title = "abc";
        //    entity.ExtendID = 0;
        //    entity.Version = 0;
        //    entity.StatisticsID = 0;
        //    entity.Views = 0;
        //    entity.Summary = "abc";
        //    entity.PublishTime = DateTime.Now;
        //    entity.Publisher = "abc";
        //    entity.CreateUserID = 0;
        //    entity.CreateTime = DateTime.Now;
        //    entity.CreateIP = "abc";
        //    entity.UpdateUserID = 0;
        //    entity.UpdateTime = DateTime.Now;
        //    entity.UpdateIP = "abc";
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化{0}[{1}]数据！", typeof(TEntity).Name, Meta.Table.DataTable.DisplayName);
        //}


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
            ﻿

        #endregion

        #region 扩展查询
            ﻿
        /// <summary>根据模型查找</summary>
        /// <param name="modelid">模型</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EntityList<TEntity> FindAllByModelID(Int32 modelid)
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
        public static EntityList<TEntity> FindAllByExtendID(Int32 extendid)
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
        public static EntityList<TEntity> FindAllByCategoryID(Int32 categoryid)
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
        public static EntityList<TEntity> FindAllByPublishTime(DateTime publishtime)
        {
            if (Meta.Count >= 1000)
                return FindAll(__.PublishTime, publishtime);
            else // 实体缓存
                return Meta.Cache.Entities.FindAll(__.PublishTime, publishtime);
        }

        #endregion

        #region 高级查询
        // 以下为自定义高级查询的例子

        /// <summary>查询满足条件的记录集，分页、排序</summary>
        /// <param name="userid">用户编号</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="key">关键字</param>
        /// <param name="param">分页排序参数，同时返回满足条件的总记录数</param>
        /// <returns>实体集</returns>
        public static EntityList<TEntity> Search(Int32 userid, DateTime start, DateTime end, String key, PageParameter param)
        {
            // WhereExpression重载&和|运算符，作为And和Or的替代
            // SearchWhereByKeys系列方法用于构建针对字符串字段的模糊搜索，第二个参数可指定要搜索的字段
            var exp = SearchWhereByKeys(key, null, null);

            // 以下仅为演示，Field（继承自FieldItem）重载了==、!=、>、<、>=、<=等运算符
            //if (userid > 0) exp &= _.OperatorID == userid;
            //if (isSign != null) exp &= _.IsSign == isSign.Value;
            //exp &= _.OccurTime.Between(start, end); // 大于等于start，小于end，当start/end大于MinValue时有效

            return FindAll(exp, param);
        }
        #endregion

        #region 扩展操作
        #endregion

        #region 业务
        #endregion
    }
}
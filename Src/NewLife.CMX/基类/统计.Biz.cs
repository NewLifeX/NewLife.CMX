/*
 * XCoder v6.0.5096.30596
 * 作者：nnhy/X2
 * 时间：2013-12-14 17:00:07
 * 版权：版权所有 (C) 新生命开发团队 2002~2013
*/
﻿using System;
using System.ComponentModel;
using NewLife.Web;
using XCode;
using System.Web;
using System.Text;

namespace NewLife.CMX
{
    /// <summary>统计</summary>
    public partial class Statistics<TEntity> : Entity<TEntity> where TEntity : Statistics<TEntity>, new()
    {
        #region 对象操作﻿
        static Statistics()
        {
            // 用于引发基类的静态构造函数，所有层次的泛型实体类都应该有一个
            TEntity entity = new TEntity();

            EntityFactory.Register(typeof(TEntity), new StatisticsFactory<TEntity>());

            Meta.Factory.AdditionalFields.Add(__.Total);
            Meta.Factory.AdditionalFields.Add(__.Today);
            Meta.Factory.AdditionalFields.Add(__.ThisWeek);
            Meta.Factory.AdditionalFields.Add(__.ThisMonth);
            Meta.Factory.AdditionalFields.Add(__.ThisYear);

            // 自动保存时间60秒
            var sc = Meta.Session.SingleCache;
            sc.AutoSave = true;
            sc.Expriod = 60;
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
            // 这里验证参数范围，建议抛出参数异常，指定参数名，前端用户界面可以捕获参数异常并聚焦到对应的参数输入框
            //if (String.IsNullOrEmpty(Name)) throw new ArgumentNullException(_.Name, _.Name.DisplayName + "无效！");
            //if (!isNew && ID < 1) throw new ArgumentOutOfRangeException(_.ID, _.ID.DisplayName + "必须大于0！");

            // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
            base.Valid(isNew);

            // 在新插入数据或者修改了指定字段时进行唯一性验证，CheckExist内部抛出参数异常
            //if (isNew || Dirtys[__.Name]) CheckExist(__.Name);

            if (!Dirtys[__.LastIP]) LastIP = WebHelper.UserHost;
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

        #region 扩展属性﻿
        /// <summary>文本形式表示统计信息</summary>
        public String Text
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var item in Meta.Fields)
                {
                    if (item.PrimaryKey) continue;
                    if (item.Type != typeof(Int32)) continue;

                    if (sb.Length > 0) sb.Append(" ");
                    if (item.Type == typeof(Int32))
                        sb.Append("{0}{1:n0}".F(item.DisplayName, this[item.Name]));
                    else
                        sb.Append("{0}{1}".F(item.DisplayName, this[item.Name]));
                }
                return sb.ToString();
            }
        }
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
        #endregion

        #region 高级查询
        #endregion

        #region 扩展操作
        #endregion

        #region 业务
        /// <summary>增加计数</summary>
        /// <param name="remark">备注</param>
        public void Increment(String remark)
        {
            lock (this)
            {
                Total++;

                var last = LastTime;
                var now = DateTime.Now;
                LastTime = now;

                // 有记录，判断是否过了一天（周、月、年）
                if (last > DateTime.MinValue)
                {
                    // 去掉时分秒，避免其带来差异
                    last = last.Date;
                    now = now.Date;

                    // 是否同一天
                    Int32 diff = (now - last).Days;
                    if (diff != 0)
                    {
                        Yesterday = diff == 1 ? Today : 0;
                        Today = 0;

                        // 是否同一周
                        diff = Math.Abs(diff);
                        if (diff >= 7)
                        {
                            // 肯定不是同一周
                            LastWeek = diff <= 14 && now.DayOfWeek >= last.DayOfWeek ? ThisWeek : 0;
                            ThisWeek = 0;
                        }
                        else
                        {
                            // 当前的星期数小于上次的星期数，并且两者在七天之内，表明是新的一周了
                            if (now.DayOfWeek < last.DayOfWeek)
                            {
                                LastWeek = ThisWeek;
                                ThisWeek = 0;
                            }
                        }

                        // 是否同一个月
                        diff = now.Year * 12 + now.Month - (last.Year * 12 + last.Month);
                        if (diff != 0)
                        {
                            // 是否刚好过了一个月
                            LastMonth = diff == 1 ? ThisMonth : 0;
                            ThisMonth = 0;

                            // 是否同一年
                            diff = now.Year - last.Year;
                            if (diff != 0)
                            {
                                // 是否刚好过了一年
                                LastYear = diff == 1 ? ThisYear : 0;
                                ThisYear = 0;
                            }
                        }
                    }
                }

                Today++;
                ThisWeek++;
                ThisMonth++;
                ThisYear++;

                //if (HttpContext.Current != null && HttpContext.Current.Request != null)
                //    LastIP = HttpContext.Current.Request.UserHostAddress;
                LastIP = WebHelper.UserHost;
                if (!String.IsNullOrEmpty(remark)) Remark = remark;
            }
        }

        /// <summary>增加指定编号的计数</summary>
        /// <param name="id"></param>
        public static void Increment(Int32 id)
        {
            var entity = FindByID(id);
            if (entity != null) entity.Increment(null);
        }
        #endregion
    }
}
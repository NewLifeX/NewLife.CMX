/*
 * XCoder v6.0.5096.30596
 * 作者：nnhy/X2
 * 时间：2013-12-14 17:00:07
 * 版权：版权所有 (C) 新生命开发团队 2002~2013
*/
using System;
using System.ComponentModel;
using System.Text;
using NewLife.Web;
using XCode;

namespace NewLife.CMX
{
    /// <summary>统计</summary>
    [ModelCheckMode(ModelCheckModes.CheckTableWhenFirstUse)]
    public class Statistics : Statistics<Statistics> { }

    /// <summary>统计</summary>
    public partial class Statistics<TEntity> : Entity<TEntity> where TEntity : Statistics<TEntity>, new()
    {
        #region 对象操作﻿
        static Statistics()
        {
            // 用于引发基类的静态构造函数，所有层次的泛型实体类都应该有一个
            var entity = new TEntity();

            Meta.Factory.AdditionalFields.Add(__.Total);
            Meta.Factory.AdditionalFields.Add(__.Today);
            Meta.Factory.AdditionalFields.Add(__.ThisWeek);
            Meta.Factory.AdditionalFields.Add(__.ThisMonth);
            Meta.Factory.AdditionalFields.Add(__.ThisYear);
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
            if (!HasDirty) return;

            // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
            base.Valid(isNew);

            if (!Dirtys[__.LastIP]) LastIP = WebHelper.UserHost;
        }
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
            if (id <= 0) return null;

            //if (Meta.Count >= 1000)
            return Meta.SingleCache[id];
            //else
            //    return Meta.Cache.Entities.Find(__.ID, id);
        }
        #endregion

        #region 高级查询
        #endregion

        #region 扩展操作
        /// <summary>已重载。</summary>
        /// <returns></returns>
        public override String ToString() { return Text; }
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

    partial interface IStatistics
    {
        /// <summary>统计信息的文本表示</summary>
        String Text { get; }

        /// <summary>增加计数</summary>
        /// <param name="remark">备注</param>
        void Increment(String remark);

    }
}
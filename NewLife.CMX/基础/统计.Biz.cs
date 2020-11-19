/*
 * XCoder v6.0.5096.30596
 * 作者：nnhy/X2
 * 时间：2013-12-14 17:00:07
 * 版权：版权所有 (C) 新生命开发团队 2002~2013
*/
using System;
using System.ComponentModel;
using System.Text;
using XCode;
using XCode.Membership;

namespace NewLife.CMX
{
    /// <summary>统计</summary>
    public partial class Statistics : Entity<Statistics>
    {
        #region 对象操作﻿
        static Statistics()
        {
            var df = Meta.Factory.AdditionalFields;
            df.Add(__.Total);
            df.Add(__.Today);
            df.Add(__.ThisWeek);
            df.Add(__.ThisMonth);
            df.Add(__.ThisYear);
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
            if (!HasDirty) return;

            // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
            base.Valid(isNew);

            if (!Dirtys[__.LastIP]) LastIP = ManageProvider.UserHost;
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
                        sb.AppendFormat("{0}{1:n0}", item.DisplayName, this[item.Name]);
                    else
                        sb.AppendFormat("{0}{1}", item.DisplayName, this[item.Name]);
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
        public static Statistics FindByID(Int32 id)
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
        public override String ToString() => Text;
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
                    var diff = (now - last).Days;
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

                LastIP = ManageProvider.UserHost;
                if (!String.IsNullOrEmpty(remark)) Remark = remark;

                SaveAsync(10);
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
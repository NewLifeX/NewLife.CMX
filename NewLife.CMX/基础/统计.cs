using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace NewLife.CMX
{
    /// <summary>统计</summary>
    [Serializable]
    [DataObject]
    [Description("统计")]
    [BindTable("Statistics", Description = "统计", ConnName = "CMX", DbType = DatabaseType.None)]
    public partial class Statistics : IStatistics
    {
        #region 属性
        private Int32 _ID;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("ID", "编号", "")]
        public Int32 ID { get { return _ID; } set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } } }

        private Int32 _Total;
        /// <summary>总数</summary>
        [DisplayName("总数")]
        [Description("总数")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Total", "总数", "")]
        public Int32 Total { get { return _Total; } set { if (OnPropertyChanging(__.Total, value)) { _Total = value; OnPropertyChanged(__.Total); } } }

        private Int32 _Today;
        /// <summary>今天</summary>
        [DisplayName("今天")]
        [Description("今天")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Today", "今天", "")]
        public Int32 Today { get { return _Today; } set { if (OnPropertyChanging(__.Today, value)) { _Today = value; OnPropertyChanged(__.Today); } } }

        private Int32 _Yesterday;
        /// <summary>昨天</summary>
        [DisplayName("昨天")]
        [Description("昨天")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Yesterday", "昨天", "")]
        public Int32 Yesterday { get { return _Yesterday; } set { if (OnPropertyChanging(__.Yesterday, value)) { _Yesterday = value; OnPropertyChanged(__.Yesterday); } } }

        private Int32 _ThisWeek;
        /// <summary>本周</summary>
        [DisplayName("本周")]
        [Description("本周")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ThisWeek", "本周", "")]
        public Int32 ThisWeek { get { return _ThisWeek; } set { if (OnPropertyChanging(__.ThisWeek, value)) { _ThisWeek = value; OnPropertyChanged(__.ThisWeek); } } }

        private Int32 _LastWeek;
        /// <summary>上周</summary>
        [DisplayName("上周")]
        [Description("上周")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("LastWeek", "上周", "")]
        public Int32 LastWeek { get { return _LastWeek; } set { if (OnPropertyChanging(__.LastWeek, value)) { _LastWeek = value; OnPropertyChanged(__.LastWeek); } } }

        private Int32 _ThisMonth;
        /// <summary>本月</summary>
        [DisplayName("本月")]
        [Description("本月")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ThisMonth", "本月", "")]
        public Int32 ThisMonth { get { return _ThisMonth; } set { if (OnPropertyChanging(__.ThisMonth, value)) { _ThisMonth = value; OnPropertyChanged(__.ThisMonth); } } }

        private Int32 _LastMonth;
        /// <summary>上月</summary>
        [DisplayName("上月")]
        [Description("上月")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("LastMonth", "上月", "")]
        public Int32 LastMonth { get { return _LastMonth; } set { if (OnPropertyChanging(__.LastMonth, value)) { _LastMonth = value; OnPropertyChanged(__.LastMonth); } } }

        private Int32 _ThisYear;
        /// <summary>本年</summary>
        [DisplayName("本年")]
        [Description("本年")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ThisYear", "本年", "")]
        public Int32 ThisYear { get { return _ThisYear; } set { if (OnPropertyChanging(__.ThisYear, value)) { _ThisYear = value; OnPropertyChanged(__.ThisYear); } } }

        private Int32 _LastYear;
        /// <summary>去年</summary>
        [DisplayName("去年")]
        [Description("去年")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("LastYear", "去年", "")]
        public Int32 LastYear { get { return _LastYear; } set { if (OnPropertyChanging(__.LastYear, value)) { _LastYear = value; OnPropertyChanged(__.LastYear); } } }

        private DateTime _LastTime;
        /// <summary>最后时间</summary>
        [DisplayName("最后时间")]
        [Description("最后时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("LastTime", "最后时间", "")]
        public DateTime LastTime { get { return _LastTime; } set { if (OnPropertyChanging(__.LastTime, value)) { _LastTime = value; OnPropertyChanged(__.LastTime); } } }

        private String _LastIP;
        /// <summary>最后IP</summary>
        [DisplayName("最后IP")]
        [Description("最后IP")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("LastIP", "最后IP", "")]
        public String LastIP { get { return _LastIP; } set { if (OnPropertyChanging(__.LastIP, value)) { _LastIP = value; OnPropertyChanged(__.LastIP); } } }

        private String _Remark;
        /// <summary>备注</summary>
        [DisplayName("备注")]
        [Description("备注")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Remark", "备注", "")]
        public String Remark { get { return _Remark; } set { if (OnPropertyChanging(__.Remark, value)) { _Remark = value; OnPropertyChanged(__.Remark); } } }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        public override Object this[String name]
        {
            get
            {
                switch (name)
                {
                    case __.ID : return _ID;
                    case __.Total : return _Total;
                    case __.Today : return _Today;
                    case __.Yesterday : return _Yesterday;
                    case __.ThisWeek : return _ThisWeek;
                    case __.LastWeek : return _LastWeek;
                    case __.ThisMonth : return _ThisMonth;
                    case __.LastMonth : return _LastMonth;
                    case __.ThisYear : return _ThisYear;
                    case __.LastYear : return _LastYear;
                    case __.LastTime : return _LastTime;
                    case __.LastIP : return _LastIP;
                    case __.Remark : return _Remark;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = value.ToInt(); break;
                    case __.Total : _Total = value.ToInt(); break;
                    case __.Today : _Today = value.ToInt(); break;
                    case __.Yesterday : _Yesterday = value.ToInt(); break;
                    case __.ThisWeek : _ThisWeek = value.ToInt(); break;
                    case __.LastWeek : _LastWeek = value.ToInt(); break;
                    case __.ThisMonth : _ThisMonth = value.ToInt(); break;
                    case __.LastMonth : _LastMonth = value.ToInt(); break;
                    case __.ThisYear : _ThisYear = value.ToInt(); break;
                    case __.LastYear : _LastYear = value.ToInt(); break;
                    case __.LastTime : _LastTime = value.ToDateTime(); break;
                    case __.LastIP : _LastIP = Convert.ToString(value); break;
                    case __.Remark : _Remark = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得统计字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            /// <summary>总数</summary>
            public static readonly Field Total = FindByName(__.Total);

            /// <summary>今天</summary>
            public static readonly Field Today = FindByName(__.Today);

            /// <summary>昨天</summary>
            public static readonly Field Yesterday = FindByName(__.Yesterday);

            /// <summary>本周</summary>
            public static readonly Field ThisWeek = FindByName(__.ThisWeek);

            /// <summary>上周</summary>
            public static readonly Field LastWeek = FindByName(__.LastWeek);

            /// <summary>本月</summary>
            public static readonly Field ThisMonth = FindByName(__.ThisMonth);

            /// <summary>上月</summary>
            public static readonly Field LastMonth = FindByName(__.LastMonth);

            /// <summary>本年</summary>
            public static readonly Field ThisYear = FindByName(__.ThisYear);

            /// <summary>去年</summary>
            public static readonly Field LastYear = FindByName(__.LastYear);

            /// <summary>最后时间</summary>
            public static readonly Field LastTime = FindByName(__.LastTime);

            /// <summary>最后IP</summary>
            public static readonly Field LastIP = FindByName(__.LastIP);

            /// <summary>备注</summary>
            public static readonly Field Remark = FindByName(__.Remark);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得统计字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String ID = "ID";

            /// <summary>总数</summary>
            public const String Total = "Total";

            /// <summary>今天</summary>
            public const String Today = "Today";

            /// <summary>昨天</summary>
            public const String Yesterday = "Yesterday";

            /// <summary>本周</summary>
            public const String ThisWeek = "ThisWeek";

            /// <summary>上周</summary>
            public const String LastWeek = "LastWeek";

            /// <summary>本月</summary>
            public const String ThisMonth = "ThisMonth";

            /// <summary>上月</summary>
            public const String LastMonth = "LastMonth";

            /// <summary>本年</summary>
            public const String ThisYear = "ThisYear";

            /// <summary>去年</summary>
            public const String LastYear = "LastYear";

            /// <summary>最后时间</summary>
            public const String LastTime = "LastTime";

            /// <summary>最后IP</summary>
            public const String LastIP = "LastIP";

            /// <summary>备注</summary>
            public const String Remark = "Remark";
        }
        #endregion
    }

    /// <summary>统计接口</summary>
    public partial interface IStatistics
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>总数</summary>
        Int32 Total { get; set; }

        /// <summary>今天</summary>
        Int32 Today { get; set; }

        /// <summary>昨天</summary>
        Int32 Yesterday { get; set; }

        /// <summary>本周</summary>
        Int32 ThisWeek { get; set; }

        /// <summary>上周</summary>
        Int32 LastWeek { get; set; }

        /// <summary>本月</summary>
        Int32 ThisMonth { get; set; }

        /// <summary>上月</summary>
        Int32 LastMonth { get; set; }

        /// <summary>本年</summary>
        Int32 ThisYear { get; set; }

        /// <summary>去年</summary>
        Int32 LastYear { get; set; }

        /// <summary>最后时间</summary>
        DateTime LastTime { get; set; }

        /// <summary>最后IP</summary>
        String LastIP { get; set; }

        /// <summary>备注</summary>
        String Remark { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
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
    public partial class Statistics
    {
        #region 属性
        private Int32 _ID;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("ID", "编号", "")]
        public Int32 ID { get => _ID; set { if (OnPropertyChanging("ID", value)) { _ID = value; OnPropertyChanged("ID"); } } }

        private Int32 _Total;
        /// <summary>总数</summary>
        [DisplayName("总数")]
        [Description("总数")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Total", "总数", "")]
        public Int32 Total { get => _Total; set { if (OnPropertyChanging("Total", value)) { _Total = value; OnPropertyChanged("Total"); } } }

        private Int32 _Today;
        /// <summary>今天</summary>
        [DisplayName("今天")]
        [Description("今天")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Today", "今天", "")]
        public Int32 Today { get => _Today; set { if (OnPropertyChanging("Today", value)) { _Today = value; OnPropertyChanged("Today"); } } }

        private Int32 _Yesterday;
        /// <summary>昨天</summary>
        [DisplayName("昨天")]
        [Description("昨天")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Yesterday", "昨天", "")]
        public Int32 Yesterday { get => _Yesterday; set { if (OnPropertyChanging("Yesterday", value)) { _Yesterday = value; OnPropertyChanged("Yesterday"); } } }

        private Int32 _ThisWeek;
        /// <summary>本周</summary>
        [DisplayName("本周")]
        [Description("本周")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ThisWeek", "本周", "")]
        public Int32 ThisWeek { get => _ThisWeek; set { if (OnPropertyChanging("ThisWeek", value)) { _ThisWeek = value; OnPropertyChanged("ThisWeek"); } } }

        private Int32 _LastWeek;
        /// <summary>上周</summary>
        [DisplayName("上周")]
        [Description("上周")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("LastWeek", "上周", "")]
        public Int32 LastWeek { get => _LastWeek; set { if (OnPropertyChanging("LastWeek", value)) { _LastWeek = value; OnPropertyChanged("LastWeek"); } } }

        private Int32 _ThisMonth;
        /// <summary>本月</summary>
        [DisplayName("本月")]
        [Description("本月")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ThisMonth", "本月", "")]
        public Int32 ThisMonth { get => _ThisMonth; set { if (OnPropertyChanging("ThisMonth", value)) { _ThisMonth = value; OnPropertyChanged("ThisMonth"); } } }

        private Int32 _LastMonth;
        /// <summary>上月</summary>
        [DisplayName("上月")]
        [Description("上月")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("LastMonth", "上月", "")]
        public Int32 LastMonth { get => _LastMonth; set { if (OnPropertyChanging("LastMonth", value)) { _LastMonth = value; OnPropertyChanged("LastMonth"); } } }

        private Int32 _ThisYear;
        /// <summary>本年</summary>
        [DisplayName("本年")]
        [Description("本年")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ThisYear", "本年", "")]
        public Int32 ThisYear { get => _ThisYear; set { if (OnPropertyChanging("ThisYear", value)) { _ThisYear = value; OnPropertyChanged("ThisYear"); } } }

        private Int32 _LastYear;
        /// <summary>去年</summary>
        [DisplayName("去年")]
        [Description("去年")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("LastYear", "去年", "")]
        public Int32 LastYear { get => _LastYear; set { if (OnPropertyChanging("LastYear", value)) { _LastYear = value; OnPropertyChanged("LastYear"); } } }

        private DateTime _LastTime;
        /// <summary>最后时间</summary>
        [DisplayName("最后时间")]
        [Description("最后时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("LastTime", "最后时间", "")]
        public DateTime LastTime { get => _LastTime; set { if (OnPropertyChanging("LastTime", value)) { _LastTime = value; OnPropertyChanged("LastTime"); } } }

        private String _LastIP;
        /// <summary>最后IP</summary>
        [DisplayName("最后IP")]
        [Description("最后IP")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("LastIP", "最后IP", "")]
        public String LastIP { get => _LastIP; set { if (OnPropertyChanging("LastIP", value)) { _LastIP = value; OnPropertyChanged("LastIP"); } } }

        private String _Remark;
        /// <summary>备注</summary>
        [DisplayName("备注")]
        [Description("备注")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Remark", "备注", "")]
        public String Remark { get => _Remark; set { if (OnPropertyChanging("Remark", value)) { _Remark = value; OnPropertyChanged("Remark"); } } }
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
                    case "ID": return _ID;
                    case "Total": return _Total;
                    case "Today": return _Today;
                    case "Yesterday": return _Yesterday;
                    case "ThisWeek": return _ThisWeek;
                    case "LastWeek": return _LastWeek;
                    case "ThisMonth": return _ThisMonth;
                    case "LastMonth": return _LastMonth;
                    case "ThisYear": return _ThisYear;
                    case "LastYear": return _LastYear;
                    case "LastTime": return _LastTime;
                    case "LastIP": return _LastIP;
                    case "Remark": return _Remark;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "ID": _ID = value.ToInt(); break;
                    case "Total": _Total = value.ToInt(); break;
                    case "Today": _Today = value.ToInt(); break;
                    case "Yesterday": _Yesterday = value.ToInt(); break;
                    case "ThisWeek": _ThisWeek = value.ToInt(); break;
                    case "LastWeek": _LastWeek = value.ToInt(); break;
                    case "ThisMonth": _ThisMonth = value.ToInt(); break;
                    case "LastMonth": _LastMonth = value.ToInt(); break;
                    case "ThisYear": _ThisYear = value.ToInt(); break;
                    case "LastYear": _LastYear = value.ToInt(); break;
                    case "LastTime": _LastTime = value.ToDateTime(); break;
                    case "LastIP": _LastIP = Convert.ToString(value); break;
                    case "Remark": _Remark = Convert.ToString(value); break;
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
            public static readonly Field ID = FindByName("ID");

            /// <summary>总数</summary>
            public static readonly Field Total = FindByName("Total");

            /// <summary>今天</summary>
            public static readonly Field Today = FindByName("Today");

            /// <summary>昨天</summary>
            public static readonly Field Yesterday = FindByName("Yesterday");

            /// <summary>本周</summary>
            public static readonly Field ThisWeek = FindByName("ThisWeek");

            /// <summary>上周</summary>
            public static readonly Field LastWeek = FindByName("LastWeek");

            /// <summary>本月</summary>
            public static readonly Field ThisMonth = FindByName("ThisMonth");

            /// <summary>上月</summary>
            public static readonly Field LastMonth = FindByName("LastMonth");

            /// <summary>本年</summary>
            public static readonly Field ThisYear = FindByName("ThisYear");

            /// <summary>去年</summary>
            public static readonly Field LastYear = FindByName("LastYear");

            /// <summary>最后时间</summary>
            public static readonly Field LastTime = FindByName("LastTime");

            /// <summary>最后IP</summary>
            public static readonly Field LastIP = FindByName("LastIP");

            /// <summary>备注</summary>
            public static readonly Field Remark = FindByName("Remark");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
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
}
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
    /// <summary>信息</summary>
    [Serializable]
    [DataObject]
    [Description("信息")]
    [BindIndex("IX_Info_Title", false, "Title")]
    [BindIndex("IX_Info_ModelID", false, "ModelID")]
    [BindIndex("IX_Info_CategoryID_Code", false, "CategoryID,Code")]
    [BindIndex("IX_Info_PublishTime", false, "PublishTime")]
    [BindTable("Info", Description = "信息", ConnName = "CMX", DbType = DatabaseType.None)]
    public partial class Info
    {
        #region 属性
        private Int32 _ID;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("ID", "编号", "")]
        public Int32 ID { get => _ID; set { if (OnPropertyChanging("ID", value)) { _ID = value; OnPropertyChanged("ID"); } } }

        private Int32 _ModelID;
        /// <summary>模型</summary>
        [DisplayName("模型")]
        [Description("模型")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ModelID", "模型", "")]
        public Int32 ModelID { get => _ModelID; set { if (OnPropertyChanging("ModelID", value)) { _ModelID = value; OnPropertyChanged("ModelID"); } } }

        private Int32 _CategoryID;
        /// <summary>分类</summary>
        [DisplayName("分类")]
        [Description("分类")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("CategoryID", "分类", "")]
        public Int32 CategoryID { get => _CategoryID; set { if (OnPropertyChanging("CategoryID", value)) { _CategoryID = value; OnPropertyChanged("CategoryID"); } } }

        private String _Title;
        /// <summary>标题</summary>
        [DisplayName("标题")]
        [Description("标题")]
        [DataObjectField(false, false, false, 200)]
        [BindColumn("Title", "标题", "", Master = true)]
        public String Title { get => _Title; set { if (OnPropertyChanging("Title", value)) { _Title = value; OnPropertyChanged("Title"); } } }

        private String _Code;
        /// <summary>编码。全局唯一的路由识别名，英文名</summary>
        [DisplayName("编码")]
        [Description("编码。全局唯一的路由识别名，英文名")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Code", "编码。全局唯一的路由识别名，英文名", "")]
        public String Code { get => _Code; set { if (OnPropertyChanging("Code", value)) { _Code = value; OnPropertyChanged("Code"); } } }

        private Int32 _Version;
        /// <summary>最新版本</summary>
        [DisplayName("最新版本")]
        [Description("最新版本")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Version", "最新版本", "")]
        public Int32 Version { get => _Version; set { if (OnPropertyChanging("Version", value)) { _Version = value; OnPropertyChanged("Version"); } } }

        private Int32 _StatisticsID;
        /// <summary>访问统计</summary>
        [DisplayName("访问统计")]
        [Description("访问统计")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("StatisticsID", "访问统计", "")]
        public Int32 StatisticsID { get => _StatisticsID; set { if (OnPropertyChanging("StatisticsID", value)) { _StatisticsID = value; OnPropertyChanged("StatisticsID"); } } }

        private Int32 _Views;
        /// <summary>访问量。由统计表同步过来</summary>
        [DisplayName("访问量")]
        [Description("访问量。由统计表同步过来")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Views", "访问量。由统计表同步过来", "")]
        public Int32 Views { get => _Views; set { if (OnPropertyChanging("Views", value)) { _Views = value; OnPropertyChanged("Views"); } } }

        private Int32 _Sort;
        /// <summary>排序。较大值在前</summary>
        [DisplayName("排序")]
        [Description("排序。较大值在前")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Sort", "排序。较大值在前", "")]
        public Int32 Sort { get => _Sort; set { if (OnPropertyChanging("Sort", value)) { _Sort = value; OnPropertyChanged("Sort"); } } }

        private String _Image;
        /// <summary>图片</summary>
        [DisplayName("图片")]
        [Description("图片")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Image", "图片", "")]
        public String Image { get => _Image; set { if (OnPropertyChanging("Image", value)) { _Image = value; OnPropertyChanged("Image"); } } }

        private String _Summary;
        /// <summary>摘要</summary>
        [DisplayName("摘要")]
        [Description("摘要")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Summary", "摘要", "")]
        public String Summary { get => _Summary; set { if (OnPropertyChanging("Summary", value)) { _Summary = value; OnPropertyChanged("Summary"); } } }

        private String _Publisher;
        /// <summary>发布人</summary>
        [DisplayName("发布人")]
        [Description("发布人")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Publisher", "发布人", "")]
        public String Publisher { get => _Publisher; set { if (OnPropertyChanging("Publisher", value)) { _Publisher = value; OnPropertyChanged("Publisher"); } } }

        private DateTime _PublishTime;
        /// <summary>发布时间</summary>
        [DisplayName("发布时间")]
        [Description("发布时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("PublishTime", "发布时间", "")]
        public DateTime PublishTime { get => _PublishTime; set { if (OnPropertyChanging("PublishTime", value)) { _PublishTime = value; OnPropertyChanged("PublishTime"); } } }

        private String _CreateUser;
        /// <summary>创建人</summary>
        [DisplayName("创建人")]
        [Description("创建人")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("CreateUser", "创建人", "")]
        public String CreateUser { get => _CreateUser; set { if (OnPropertyChanging("CreateUser", value)) { _CreateUser = value; OnPropertyChanged("CreateUser"); } } }

        private Int32 _CreateUserID;
        /// <summary>创建者</summary>
        [DisplayName("创建者")]
        [Description("创建者")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("CreateUserID", "创建者", "")]
        public Int32 CreateUserID { get => _CreateUserID; set { if (OnPropertyChanging("CreateUserID", value)) { _CreateUserID = value; OnPropertyChanged("CreateUserID"); } } }

        private DateTime _CreateTime;
        /// <summary>创建时间</summary>
        [DisplayName("创建时间")]
        [Description("创建时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("CreateTime", "创建时间", "")]
        public DateTime CreateTime { get => _CreateTime; set { if (OnPropertyChanging("CreateTime", value)) { _CreateTime = value; OnPropertyChanged("CreateTime"); } } }

        private String _CreateIP;
        /// <summary>创建地址</summary>
        [DisplayName("创建地址")]
        [Description("创建地址")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("CreateIP", "创建地址", "")]
        public String CreateIP { get => _CreateIP; set { if (OnPropertyChanging("CreateIP", value)) { _CreateIP = value; OnPropertyChanged("CreateIP"); } } }

        private String _UpdateUser;
        /// <summary>更新人</summary>
        [DisplayName("更新人")]
        [Description("更新人")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("UpdateUser", "更新人", "")]
        public String UpdateUser { get => _UpdateUser; set { if (OnPropertyChanging("UpdateUser", value)) { _UpdateUser = value; OnPropertyChanged("UpdateUser"); } } }

        private Int32 _UpdateUserID;
        /// <summary>更新者</summary>
        [DisplayName("更新者")]
        [Description("更新者")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("UpdateUserID", "更新者", "")]
        public Int32 UpdateUserID { get => _UpdateUserID; set { if (OnPropertyChanging("UpdateUserID", value)) { _UpdateUserID = value; OnPropertyChanged("UpdateUserID"); } } }

        private DateTime _UpdateTime;
        /// <summary>更新时间</summary>
        [DisplayName("更新时间")]
        [Description("更新时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("UpdateTime", "更新时间", "")]
        public DateTime UpdateTime { get => _UpdateTime; set { if (OnPropertyChanging("UpdateTime", value)) { _UpdateTime = value; OnPropertyChanged("UpdateTime"); } } }

        private String _UpdateIP;
        /// <summary>更新地址</summary>
        [DisplayName("更新地址")]
        [Description("更新地址")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("UpdateIP", "更新地址", "")]
        public String UpdateIP { get => _UpdateIP; set { if (OnPropertyChanging("UpdateIP", value)) { _UpdateIP = value; OnPropertyChanged("UpdateIP"); } } }
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
                    case "ModelID": return _ModelID;
                    case "CategoryID": return _CategoryID;
                    case "Title": return _Title;
                    case "Code": return _Code;
                    case "Version": return _Version;
                    case "StatisticsID": return _StatisticsID;
                    case "Views": return _Views;
                    case "Sort": return _Sort;
                    case "Image": return _Image;
                    case "Summary": return _Summary;
                    case "Publisher": return _Publisher;
                    case "PublishTime": return _PublishTime;
                    case "CreateUser": return _CreateUser;
                    case "CreateUserID": return _CreateUserID;
                    case "CreateTime": return _CreateTime;
                    case "CreateIP": return _CreateIP;
                    case "UpdateUser": return _UpdateUser;
                    case "UpdateUserID": return _UpdateUserID;
                    case "UpdateTime": return _UpdateTime;
                    case "UpdateIP": return _UpdateIP;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "ID": _ID = value.ToInt(); break;
                    case "ModelID": _ModelID = value.ToInt(); break;
                    case "CategoryID": _CategoryID = value.ToInt(); break;
                    case "Title": _Title = Convert.ToString(value); break;
                    case "Code": _Code = Convert.ToString(value); break;
                    case "Version": _Version = value.ToInt(); break;
                    case "StatisticsID": _StatisticsID = value.ToInt(); break;
                    case "Views": _Views = value.ToInt(); break;
                    case "Sort": _Sort = value.ToInt(); break;
                    case "Image": _Image = Convert.ToString(value); break;
                    case "Summary": _Summary = Convert.ToString(value); break;
                    case "Publisher": _Publisher = Convert.ToString(value); break;
                    case "PublishTime": _PublishTime = value.ToDateTime(); break;
                    case "CreateUser": _CreateUser = Convert.ToString(value); break;
                    case "CreateUserID": _CreateUserID = value.ToInt(); break;
                    case "CreateTime": _CreateTime = value.ToDateTime(); break;
                    case "CreateIP": _CreateIP = Convert.ToString(value); break;
                    case "UpdateUser": _UpdateUser = Convert.ToString(value); break;
                    case "UpdateUserID": _UpdateUserID = value.ToInt(); break;
                    case "UpdateTime": _UpdateTime = value.ToDateTime(); break;
                    case "UpdateIP": _UpdateIP = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得信息字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field ID = FindByName("ID");

            /// <summary>模型</summary>
            public static readonly Field ModelID = FindByName("ModelID");

            /// <summary>分类</summary>
            public static readonly Field CategoryID = FindByName("CategoryID");

            /// <summary>标题</summary>
            public static readonly Field Title = FindByName("Title");

            /// <summary>编码。全局唯一的路由识别名，英文名</summary>
            public static readonly Field Code = FindByName("Code");

            /// <summary>最新版本</summary>
            public static readonly Field Version = FindByName("Version");

            /// <summary>访问统计</summary>
            public static readonly Field StatisticsID = FindByName("StatisticsID");

            /// <summary>访问量。由统计表同步过来</summary>
            public static readonly Field Views = FindByName("Views");

            /// <summary>排序。较大值在前</summary>
            public static readonly Field Sort = FindByName("Sort");

            /// <summary>图片</summary>
            public static readonly Field Image = FindByName("Image");

            /// <summary>摘要</summary>
            public static readonly Field Summary = FindByName("Summary");

            /// <summary>发布人</summary>
            public static readonly Field Publisher = FindByName("Publisher");

            /// <summary>发布时间</summary>
            public static readonly Field PublishTime = FindByName("PublishTime");

            /// <summary>创建人</summary>
            public static readonly Field CreateUser = FindByName("CreateUser");

            /// <summary>创建者</summary>
            public static readonly Field CreateUserID = FindByName("CreateUserID");

            /// <summary>创建时间</summary>
            public static readonly Field CreateTime = FindByName("CreateTime");

            /// <summary>创建地址</summary>
            public static readonly Field CreateIP = FindByName("CreateIP");

            /// <summary>更新人</summary>
            public static readonly Field UpdateUser = FindByName("UpdateUser");

            /// <summary>更新者</summary>
            public static readonly Field UpdateUserID = FindByName("UpdateUserID");

            /// <summary>更新时间</summary>
            public static readonly Field UpdateTime = FindByName("UpdateTime");

            /// <summary>更新地址</summary>
            public static readonly Field UpdateIP = FindByName("UpdateIP");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得信息字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String ID = "ID";

            /// <summary>模型</summary>
            public const String ModelID = "ModelID";

            /// <summary>分类</summary>
            public const String CategoryID = "CategoryID";

            /// <summary>标题</summary>
            public const String Title = "Title";

            /// <summary>编码。全局唯一的路由识别名，英文名</summary>
            public const String Code = "Code";

            /// <summary>最新版本</summary>
            public const String Version = "Version";

            /// <summary>访问统计</summary>
            public const String StatisticsID = "StatisticsID";

            /// <summary>访问量。由统计表同步过来</summary>
            public const String Views = "Views";

            /// <summary>排序。较大值在前</summary>
            public const String Sort = "Sort";

            /// <summary>图片</summary>
            public const String Image = "Image";

            /// <summary>摘要</summary>
            public const String Summary = "Summary";

            /// <summary>发布人</summary>
            public const String Publisher = "Publisher";

            /// <summary>发布时间</summary>
            public const String PublishTime = "PublishTime";

            /// <summary>创建人</summary>
            public const String CreateUser = "CreateUser";

            /// <summary>创建者</summary>
            public const String CreateUserID = "CreateUserID";

            /// <summary>创建时间</summary>
            public const String CreateTime = "CreateTime";

            /// <summary>创建地址</summary>
            public const String CreateIP = "CreateIP";

            /// <summary>更新人</summary>
            public const String UpdateUser = "UpdateUser";

            /// <summary>更新者</summary>
            public const String UpdateUserID = "UpdateUserID";

            /// <summary>更新时间</summary>
            public const String UpdateTime = "UpdateTime";

            /// <summary>更新地址</summary>
            public const String UpdateIP = "UpdateIP";
        }
        #endregion
    }
}
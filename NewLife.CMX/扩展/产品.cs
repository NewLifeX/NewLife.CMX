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
    /// <summary>产品</summary>
    [Serializable]
    [DataObject]
    [Description("产品")]
    [BindIndex("IU_Product_InfoID", true, "InfoID")]
    [BindTable("Product", Description = "产品", ConnName = "CMX", DbType = DatabaseType.SqlServer)]
    public partial class Product
    {
        #region 属性
        private Int32 _ID;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("ID", "编号", "")]
        public Int32 ID { get => _ID; set { if (OnPropertyChanging("ID", value)) { _ID = value; OnPropertyChanged("ID"); } } }

        private Int32 _InfoID;
        /// <summary>标题</summary>
        [DisplayName("标题")]
        [Description("标题")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("InfoID", "标题", "")]
        public Int32 InfoID { get => _InfoID; set { if (OnPropertyChanging("InfoID", value)) { _InfoID = value; OnPropertyChanged("InfoID"); } } }

        private Decimal _Price;
        /// <summary>价格</summary>
        [DisplayName("价格")]
        [Description("价格")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Price", "价格", "")]
        public Decimal Price { get => _Price; set { if (OnPropertyChanging("Price", value)) { _Price = value; OnPropertyChanged("Price"); } } }

        private String _PhotoPath;
        /// <summary>图片路径</summary>
        [DisplayName("图片路径")]
        [Description("图片路径")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("PhotoPath", "图片路径", "")]
        public String PhotoPath { get => _PhotoPath; set { if (OnPropertyChanging("PhotoPath", value)) { _PhotoPath = value; OnPropertyChanged("PhotoPath"); } } }

        private String _Specification;
        /// <summary>规格参数</summary>
        [DisplayName("规格参数")]
        [Description("规格参数")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Specification", "规格参数", "")]
        public String Specification { get => _Specification; set { if (OnPropertyChanging("Specification", value)) { _Specification = value; OnPropertyChanged("Specification"); } } }

        private String _Feature;
        /// <summary>功能特点</summary>
        [DisplayName("功能特点")]
        [Description("功能特点")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Feature", "功能特点", "")]
        public String Feature { get => _Feature; set { if (OnPropertyChanging("Feature", value)) { _Feature = value; OnPropertyChanged("Feature"); } } }

        private String _App;
        /// <summary>推荐应用</summary>
        [DisplayName("推荐应用")]
        [Description("推荐应用")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("App", "推荐应用", "")]
        public String App { get => _App; set { if (OnPropertyChanging("App", value)) { _App = value; OnPropertyChanged("App"); } } }

        private String _Fitting;
        /// <summary>相关配件</summary>
        [DisplayName("相关配件")]
        [Description("相关配件")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Fitting", "相关配件", "")]
        public String Fitting { get => _Fitting; set { if (OnPropertyChanging("Fitting", value)) { _Fitting = value; OnPropertyChanged("Fitting"); } } }

        private String _Video;
        /// <summary>产品视频</summary>
        [DisplayName("产品视频")]
        [Description("产品视频")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Video", "产品视频", "")]
        public String Video { get => _Video; set { if (OnPropertyChanging("Video", value)) { _Video = value; OnPropertyChanged("Video"); } } }
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
                    case "InfoID": return _InfoID;
                    case "Price": return _Price;
                    case "PhotoPath": return _PhotoPath;
                    case "Specification": return _Specification;
                    case "Feature": return _Feature;
                    case "App": return _App;
                    case "Fitting": return _Fitting;
                    case "Video": return _Video;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "ID": _ID = value.ToInt(); break;
                    case "InfoID": _InfoID = value.ToInt(); break;
                    case "Price": _Price = Convert.ToDecimal(value); break;
                    case "PhotoPath": _PhotoPath = Convert.ToString(value); break;
                    case "Specification": _Specification = Convert.ToString(value); break;
                    case "Feature": _Feature = Convert.ToString(value); break;
                    case "App": _App = Convert.ToString(value); break;
                    case "Fitting": _Fitting = Convert.ToString(value); break;
                    case "Video": _Video = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得产品字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field ID = FindByName("ID");

            /// <summary>标题</summary>
            public static readonly Field InfoID = FindByName("InfoID");

            /// <summary>价格</summary>
            public static readonly Field Price = FindByName("Price");

            /// <summary>图片路径</summary>
            public static readonly Field PhotoPath = FindByName("PhotoPath");

            /// <summary>规格参数</summary>
            public static readonly Field Specification = FindByName("Specification");

            /// <summary>功能特点</summary>
            public static readonly Field Feature = FindByName("Feature");

            /// <summary>推荐应用</summary>
            public static readonly Field App = FindByName("App");

            /// <summary>相关配件</summary>
            public static readonly Field Fitting = FindByName("Fitting");

            /// <summary>产品视频</summary>
            public static readonly Field Video = FindByName("Video");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得产品字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String ID = "ID";

            /// <summary>标题</summary>
            public const String InfoID = "InfoID";

            /// <summary>价格</summary>
            public const String Price = "Price";

            /// <summary>图片路径</summary>
            public const String PhotoPath = "PhotoPath";

            /// <summary>规格参数</summary>
            public const String Specification = "Specification";

            /// <summary>功能特点</summary>
            public const String Feature = "Feature";

            /// <summary>推荐应用</summary>
            public const String App = "App";

            /// <summary>相关配件</summary>
            public const String Fitting = "Fitting";

            /// <summary>产品视频</summary>
            public const String Video = "Video";
        }
        #endregion
    }
}
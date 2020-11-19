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
    /// <summary>分类</summary>
    [Serializable]
    [DataObject]
    [Description("分类")]
    [BindIndex("IU_Category_Name", true, "Name")]
    [BindIndex("IX_Category_Code", false, "Code")]
    [BindIndex("IX_Category_ParentID", false, "ParentID")]
    [BindTable("Category", Description = "分类", ConnName = "CMX", DbType = DatabaseType.None)]
    public partial class Category
    {
        #region 属性
        private Int32 _ID;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("ID", "编号", "")]
        public Int32 ID { get => _ID; set { if (OnPropertyChanging("ID", value)) { _ID = value; OnPropertyChanged("ID"); } } }

        private String _Name;
        /// <summary>名称</summary>
        [DisplayName("名称")]
        [Description("名称")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn("Name", "名称", "", Master = true)]
        public String Name { get => _Name; set { if (OnPropertyChanging("Name", value)) { _Name = value; OnPropertyChanged("Name"); } } }

        private String _Code;
        /// <summary>编码。全局唯一的路由识别名，英文名</summary>
        [DisplayName("编码")]
        [Description("编码。全局唯一的路由识别名，英文名")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Code", "编码。全局唯一的路由识别名，英文名", "")]
        public String Code { get => _Code; set { if (OnPropertyChanging("Code", value)) { _Code = value; OnPropertyChanged("Code"); } } }

        private Int32 _ParentID;
        /// <summary>父类</summary>
        [DisplayName("父类")]
        [Description("父类")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ParentID", "父类", "")]
        public Int32 ParentID { get => _ParentID; set { if (OnPropertyChanging("ParentID", value)) { _ParentID = value; OnPropertyChanged("ParentID"); } } }

        private Int32 _ModelID;
        /// <summary>模型</summary>
        [DisplayName("模型")]
        [Description("模型")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ModelID", "模型", "")]
        public Int32 ModelID { get => _ModelID; set { if (OnPropertyChanging("ModelID", value)) { _ModelID = value; OnPropertyChanged("ModelID"); } } }

        private Int32 _Sort;
        /// <summary>排序</summary>
        [DisplayName("排序")]
        [Description("排序")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Sort", "排序", "")]
        public Int32 Sort { get => _Sort; set { if (OnPropertyChanging("Sort", value)) { _Sort = value; OnPropertyChanged("Sort"); } } }

        private Int32 _Num;
        /// <summary>数量</summary>
        [DisplayName("数量")]
        [Description("数量")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Num", "数量", "")]
        public Int32 Num { get => _Num; set { if (OnPropertyChanging("Num", value)) { _Num = value; OnPropertyChanged("Num"); } } }

        private String _Image;
        /// <summary>图标</summary>
        [DisplayName("图标")]
        [Description("图标")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Image", "图标", "")]
        public String Image { get => _Image; set { if (OnPropertyChanging("Image", value)) { _Image = value; OnPropertyChanged("Image"); } } }

        private String _CategoryTemplate;
        /// <summary>分类页模版。本分类专属列表页</summary>
        [DisplayName("分类页模版")]
        [Description("分类页模版。本分类专属列表页")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("CategoryTemplate", "分类页模版。本分类专属列表页", "")]
        public String CategoryTemplate { get => _CategoryTemplate; set { if (OnPropertyChanging("CategoryTemplate", value)) { _CategoryTemplate = value; OnPropertyChanged("CategoryTemplate"); } } }

        private String _InfoTemplate;
        /// <summary>信息页模版。本分类专属内容页</summary>
        [DisplayName("信息页模版")]
        [Description("信息页模版。本分类专属内容页")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("InfoTemplate", "信息页模版。本分类专属内容页", "")]
        public String InfoTemplate { get => _InfoTemplate; set { if (OnPropertyChanging("InfoTemplate", value)) { _InfoTemplate = value; OnPropertyChanged("InfoTemplate"); } } }

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
                    case "Name": return _Name;
                    case "Code": return _Code;
                    case "ParentID": return _ParentID;
                    case "ModelID": return _ModelID;
                    case "Sort": return _Sort;
                    case "Num": return _Num;
                    case "Image": return _Image;
                    case "CategoryTemplate": return _CategoryTemplate;
                    case "InfoTemplate": return _InfoTemplate;
                    case "CreateUser": return _CreateUser;
                    case "CreateUserID": return _CreateUserID;
                    case "CreateTime": return _CreateTime;
                    case "CreateIP": return _CreateIP;
                    case "UpdateUser": return _UpdateUser;
                    case "UpdateUserID": return _UpdateUserID;
                    case "UpdateTime": return _UpdateTime;
                    case "UpdateIP": return _UpdateIP;
                    case "Remark": return _Remark;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "ID": _ID = value.ToInt(); break;
                    case "Name": _Name = Convert.ToString(value); break;
                    case "Code": _Code = Convert.ToString(value); break;
                    case "ParentID": _ParentID = value.ToInt(); break;
                    case "ModelID": _ModelID = value.ToInt(); break;
                    case "Sort": _Sort = value.ToInt(); break;
                    case "Num": _Num = value.ToInt(); break;
                    case "Image": _Image = Convert.ToString(value); break;
                    case "CategoryTemplate": _CategoryTemplate = Convert.ToString(value); break;
                    case "InfoTemplate": _InfoTemplate = Convert.ToString(value); break;
                    case "CreateUser": _CreateUser = Convert.ToString(value); break;
                    case "CreateUserID": _CreateUserID = value.ToInt(); break;
                    case "CreateTime": _CreateTime = value.ToDateTime(); break;
                    case "CreateIP": _CreateIP = Convert.ToString(value); break;
                    case "UpdateUser": _UpdateUser = Convert.ToString(value); break;
                    case "UpdateUserID": _UpdateUserID = value.ToInt(); break;
                    case "UpdateTime": _UpdateTime = value.ToDateTime(); break;
                    case "UpdateIP": _UpdateIP = Convert.ToString(value); break;
                    case "Remark": _Remark = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得分类字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field ID = FindByName("ID");

            /// <summary>名称</summary>
            public static readonly Field Name = FindByName("Name");

            /// <summary>编码。全局唯一的路由识别名，英文名</summary>
            public static readonly Field Code = FindByName("Code");

            /// <summary>父类</summary>
            public static readonly Field ParentID = FindByName("ParentID");

            /// <summary>模型</summary>
            public static readonly Field ModelID = FindByName("ModelID");

            /// <summary>排序</summary>
            public static readonly Field Sort = FindByName("Sort");

            /// <summary>数量</summary>
            public static readonly Field Num = FindByName("Num");

            /// <summary>图标</summary>
            public static readonly Field Image = FindByName("Image");

            /// <summary>分类页模版。本分类专属列表页</summary>
            public static readonly Field CategoryTemplate = FindByName("CategoryTemplate");

            /// <summary>信息页模版。本分类专属内容页</summary>
            public static readonly Field InfoTemplate = FindByName("InfoTemplate");

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

            /// <summary>备注</summary>
            public static readonly Field Remark = FindByName("Remark");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得分类字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String ID = "ID";

            /// <summary>名称</summary>
            public const String Name = "Name";

            /// <summary>编码。全局唯一的路由识别名，英文名</summary>
            public const String Code = "Code";

            /// <summary>父类</summary>
            public const String ParentID = "ParentID";

            /// <summary>模型</summary>
            public const String ModelID = "ModelID";

            /// <summary>排序</summary>
            public const String Sort = "Sort";

            /// <summary>数量</summary>
            public const String Num = "Num";

            /// <summary>图标</summary>
            public const String Image = "Image";

            /// <summary>分类页模版。本分类专属列表页</summary>
            public const String CategoryTemplate = "CategoryTemplate";

            /// <summary>信息页模版。本分类专属内容页</summary>
            public const String InfoTemplate = "InfoTemplate";

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

            /// <summary>备注</summary>
            public const String Remark = "Remark";
        }
        #endregion
    }
}
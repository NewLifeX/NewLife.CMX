using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class Category : ICategory
    {
        #region 属性
        private Int32 _ID;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("ID", "编号", "")]
        public Int32 ID { get { return _ID; } set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } } }

        private String _Name;
        /// <summary>名称</summary>
        [DisplayName("名称")]
        [Description("名称")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn("Name", "名称", "", Master = true)]
        public String Name { get { return _Name; } set { if (OnPropertyChanging(__.Name, value)) { _Name = value; OnPropertyChanged(__.Name); } } }

        private String _Code;
        /// <summary>编码。全局唯一的路由识别名，英文名</summary>
        [DisplayName("编码")]
        [Description("编码。全局唯一的路由识别名，英文名")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Code", "编码。全局唯一的路由识别名，英文名", "")]
        public String Code { get { return _Code; } set { if (OnPropertyChanging(__.Code, value)) { _Code = value; OnPropertyChanged(__.Code); } } }

        private Int32 _ParentID;
        /// <summary>父类</summary>
        [DisplayName("父类")]
        [Description("父类")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ParentID", "父类", "")]
        public Int32 ParentID { get { return _ParentID; } set { if (OnPropertyChanging(__.ParentID, value)) { _ParentID = value; OnPropertyChanged(__.ParentID); } } }

        private Int32 _ModelID;
        /// <summary>模型</summary>
        [DisplayName("模型")]
        [Description("模型")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ModelID", "模型", "")]
        public Int32 ModelID { get { return _ModelID; } set { if (OnPropertyChanging(__.ModelID, value)) { _ModelID = value; OnPropertyChanged(__.ModelID); } } }

        private Int32 _Sort;
        /// <summary>排序</summary>
        [DisplayName("排序")]
        [Description("排序")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Sort", "排序", "")]
        public Int32 Sort { get { return _Sort; } set { if (OnPropertyChanging(__.Sort, value)) { _Sort = value; OnPropertyChanged(__.Sort); } } }

        private Int32 _Num;
        /// <summary>数量</summary>
        [DisplayName("数量")]
        [Description("数量")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Num", "数量", "")]
        public Int32 Num { get { return _Num; } set { if (OnPropertyChanging(__.Num, value)) { _Num = value; OnPropertyChanged(__.Num); } } }

        private String _Image;
        /// <summary>图标</summary>
        [DisplayName("图标")]
        [Description("图标")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Image", "图标", "")]
        public String Image { get { return _Image; } set { if (OnPropertyChanging(__.Image, value)) { _Image = value; OnPropertyChanged(__.Image); } } }

        private String _CategoryTemplate;
        /// <summary>分类页模版。本分类专属列表页</summary>
        [DisplayName("分类页模版")]
        [Description("分类页模版。本分类专属列表页")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("CategoryTemplate", "分类页模版。本分类专属列表页", "")]
        public String CategoryTemplate { get { return _CategoryTemplate; } set { if (OnPropertyChanging(__.CategoryTemplate, value)) { _CategoryTemplate = value; OnPropertyChanged(__.CategoryTemplate); } } }

        private String _InfoTemplate;
        /// <summary>信息页模版。本分类专属内容页</summary>
        [DisplayName("信息页模版")]
        [Description("信息页模版。本分类专属内容页")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("InfoTemplate", "信息页模版。本分类专属内容页", "")]
        public String InfoTemplate { get { return _InfoTemplate; } set { if (OnPropertyChanging(__.InfoTemplate, value)) { _InfoTemplate = value; OnPropertyChanged(__.InfoTemplate); } } }

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
                    case __.Name : return _Name;
                    case __.Code : return _Code;
                    case __.ParentID : return _ParentID;
                    case __.ModelID : return _ModelID;
                    case __.Sort : return _Sort;
                    case __.Num : return _Num;
                    case __.Image : return _Image;
                    case __.CategoryTemplate : return _CategoryTemplate;
                    case __.InfoTemplate : return _InfoTemplate;
                    case __.Remark : return _Remark;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.Code : _Code = Convert.ToString(value); break;
                    case __.ParentID : _ParentID = Convert.ToInt32(value); break;
                    case __.ModelID : _ModelID = Convert.ToInt32(value); break;
                    case __.Sort : _Sort = Convert.ToInt32(value); break;
                    case __.Num : _Num = Convert.ToInt32(value); break;
                    case __.Image : _Image = Convert.ToString(value); break;
                    case __.CategoryTemplate : _CategoryTemplate = Convert.ToString(value); break;
                    case __.InfoTemplate : _InfoTemplate = Convert.ToString(value); break;
                    case __.Remark : _Remark = Convert.ToString(value); break;
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
            public static readonly Field ID = FindByName(__.ID);

            /// <summary>名称</summary>
            public static readonly Field Name = FindByName(__.Name);

            /// <summary>编码。全局唯一的路由识别名，英文名</summary>
            public static readonly Field Code = FindByName(__.Code);

            /// <summary>父类</summary>
            public static readonly Field ParentID = FindByName(__.ParentID);

            /// <summary>模型</summary>
            public static readonly Field ModelID = FindByName(__.ModelID);

            /// <summary>排序</summary>
            public static readonly Field Sort = FindByName(__.Sort);

            /// <summary>数量</summary>
            public static readonly Field Num = FindByName(__.Num);

            /// <summary>图标</summary>
            public static readonly Field Image = FindByName(__.Image);

            /// <summary>分类页模版。本分类专属列表页</summary>
            public static readonly Field CategoryTemplate = FindByName(__.CategoryTemplate);

            /// <summary>信息页模版。本分类专属内容页</summary>
            public static readonly Field InfoTemplate = FindByName(__.InfoTemplate);

            /// <summary>备注</summary>
            public static readonly Field Remark = FindByName(__.Remark);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
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

            /// <summary>备注</summary>
            public const String Remark = "Remark";
        }
        #endregion
    }

    /// <summary>分类接口</summary>
    public partial interface ICategory
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>名称</summary>
        String Name { get; set; }

        /// <summary>编码。全局唯一的路由识别名，英文名</summary>
        String Code { get; set; }

        /// <summary>父类</summary>
        Int32 ParentID { get; set; }

        /// <summary>模型</summary>
        Int32 ModelID { get; set; }

        /// <summary>排序</summary>
        Int32 Sort { get; set; }

        /// <summary>数量</summary>
        Int32 Num { get; set; }

        /// <summary>图标</summary>
        String Image { get; set; }

        /// <summary>分类页模版。本分类专属列表页</summary>
        String CategoryTemplate { get; set; }

        /// <summary>信息页模版。本分类专属内容页</summary>
        String InfoTemplate { get; set; }

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
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace NewLife.CMX
{
    /// <summary>模型</summary>
    /// <remarks>模型。默认有文章、文本、产品三种模型，可以扩展增加。</remarks>
    [Serializable]
    [DataObject]
    [Description("模型。默认有文章、文本、产品三种模型，可以扩展增加。")]
    [BindIndex("IU_Model_Name", true, "Name")]
    [BindTable("Model", Description = "模型。默认有文章、文本、产品三种模型，可以扩展增加。", ConnName = "CMX", DbType = DatabaseType.SqlServer)]
    public partial class Model : IModel
    {
        #region 属性
        private Int32 _ID;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 10)]
        [BindColumn(1, "ID", "编号", null, "int", 10, 0, false)]
        public virtual Int32 ID
        {
            get { return _ID; }
            set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } }
        }

        private String _Name;
        /// <summary>名称</summary>
        [DisplayName("名称")]
        [Description("名称")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(2, "Name", "名称", null, "nvarchar(50)", 0, 0, true)]
        public virtual String Name
        {
            get { return _Name; }
            set { if (OnPropertyChanging(__.Name, value)) { _Name = value; OnPropertyChanged(__.Name); } }
        }

        private Boolean _Enable;
        /// <summary>启用</summary>
        [DisplayName("启用")]
        [Description("启用")]
        [DataObjectField(false, false, true, 1)]
        [BindColumn(3, "Enable", "启用", null, "bit", 0, 0, false)]
        public virtual Boolean Enable
        {
            get { return _Enable; }
            set { if (OnPropertyChanging(__.Enable, value)) { _Enable = value; OnPropertyChanged(__.Enable); } }
        }

        private Int32 _CreateUserID;
        /// <summary>创建人ID</summary>
        [DisplayName("创建人ID")]
        [Description("创建人ID")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(4, "CreateUserID", "创建人ID", null, "int", 10, 0, false)]
        public virtual Int32 CreateUserID
        {
            get { return _CreateUserID; }
            set { if (OnPropertyChanging(__.CreateUserID, value)) { _CreateUserID = value; OnPropertyChanged(__.CreateUserID); } }
        }

        private DateTime _CreateTime;
        /// <summary>创建时间</summary>
        [DisplayName("创建时间")]
        [Description("创建时间")]
        [DataObjectField(false, false, true, 3)]
        [BindColumn(5, "CreateTime", "创建时间", null, "datetime", 3, 0, false)]
        public virtual DateTime CreateTime
        {
            get { return _CreateTime; }
            set { if (OnPropertyChanging(__.CreateTime, value)) { _CreateTime = value; OnPropertyChanged(__.CreateTime); } }
        }

        private Int32 _UpdateUserID;
        /// <summary>更新人ID</summary>
        [DisplayName("更新人ID")]
        [Description("更新人ID")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(6, "UpdateUserID", "更新人ID", null, "int", 10, 0, false)]
        public virtual Int32 UpdateUserID
        {
            get { return _UpdateUserID; }
            set { if (OnPropertyChanging(__.UpdateUserID, value)) { _UpdateUserID = value; OnPropertyChanged(__.UpdateUserID); } }
        }

        private DateTime _UpdateTime;
        /// <summary>更新时间</summary>
        [DisplayName("更新时间")]
        [Description("更新时间")]
        [DataObjectField(false, false, true, 3)]
        [BindColumn(7, "UpdateTime", "更新时间", null, "datetime", 3, 0, false)]
        public virtual DateTime UpdateTime
        {
            get { return _UpdateTime; }
            set { if (OnPropertyChanging(__.UpdateTime, value)) { _UpdateTime = value; OnPropertyChanged(__.UpdateTime); } }
        }

        private String _Remark;
        /// <summary>备注</summary>
        [DisplayName("备注")]
        [Description("备注")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(8, "Remark", "备注", null, "nvarchar(200)", 0, 0, true)]
        public virtual String Remark
        {
            get { return _Remark; }
            set { if (OnPropertyChanging(__.Remark, value)) { _Remark = value; OnPropertyChanged(__.Remark); } }
        }

        private String _TitleTemplatePath;
        /// <summary>标题页模版</summary>
        [DisplayName("标题页模版")]
        [Description("标题页模版")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(9, "TitleTemplatePath", "标题页模版", null, "nvarchar(200)", 0, 0, true)]
        public virtual String TitleTemplatePath
        {
            get { return _TitleTemplatePath; }
            set { if (OnPropertyChanging(__.TitleTemplatePath, value)) { _TitleTemplatePath = value; OnPropertyChanged(__.TitleTemplatePath); } }
        }

        private String _CategoryTemplatePath;
        /// <summary>分类页模版</summary>
        [DisplayName("分类页模版")]
        [Description("分类页模版")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(10, "CategoryTemplatePath", "分类页模版", null, "nvarchar(200)", 0, 0, true)]
        public virtual String CategoryTemplatePath
        {
            get { return _CategoryTemplatePath; }
            set { if (OnPropertyChanging(__.CategoryTemplatePath, value)) { _CategoryTemplatePath = value; OnPropertyChanged(__.CategoryTemplatePath); } }
        }

        private String _ClassName;
        /// <summary>类名</summary>
        [DisplayName("类名")]
        [Description("类名")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(11, "ClassName", "类名", null, "nvarchar(200)", 0, 0, true)]
        public virtual String ClassName
        {
            get { return _ClassName; }
            set { if (OnPropertyChanging(__.ClassName, value)) { _ClassName = value; OnPropertyChanged(__.ClassName); } }
        }
        #endregion

        #region 获取/设置 字段值
        /// <summary>
        /// 获取/设置 字段值。
        /// 一个索引，基类使用反射实现。
        /// 派生实体类可重写该索引，以避免反射带来的性能损耗
        /// </summary>
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
                    case __.Enable : return _Enable;
                    case __.CreateUserID : return _CreateUserID;
                    case __.CreateTime : return _CreateTime;
                    case __.UpdateUserID : return _UpdateUserID;
                    case __.UpdateTime : return _UpdateTime;
                    case __.Remark : return _Remark;
                    case __.TitleTemplatePath : return _TitleTemplatePath;
                    case __.CategoryTemplatePath : return _CategoryTemplatePath;
                    case __.ClassName : return _ClassName;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.Enable : _Enable = Convert.ToBoolean(value); break;
                    case __.CreateUserID : _CreateUserID = Convert.ToInt32(value); break;
                    case __.CreateTime : _CreateTime = Convert.ToDateTime(value); break;
                    case __.UpdateUserID : _UpdateUserID = Convert.ToInt32(value); break;
                    case __.UpdateTime : _UpdateTime = Convert.ToDateTime(value); break;
                    case __.Remark : _Remark = Convert.ToString(value); break;
                    case __.TitleTemplatePath : _TitleTemplatePath = Convert.ToString(value); break;
                    case __.CategoryTemplatePath : _CategoryTemplatePath = Convert.ToString(value); break;
                    case __.ClassName : _ClassName = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得模型字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary>名称</summary>
            public static readonly Field Name = FindByName(__.Name);

            ///<summary>启用</summary>
            public static readonly Field Enable = FindByName(__.Enable);

            ///<summary>创建人ID</summary>
            public static readonly Field CreateUserID = FindByName(__.CreateUserID);

            ///<summary>创建时间</summary>
            public static readonly Field CreateTime = FindByName(__.CreateTime);

            ///<summary>更新人ID</summary>
            public static readonly Field UpdateUserID = FindByName(__.UpdateUserID);

            ///<summary>更新时间</summary>
            public static readonly Field UpdateTime = FindByName(__.UpdateTime);

            ///<summary>备注</summary>
            public static readonly Field Remark = FindByName(__.Remark);

            ///<summary>标题页模版</summary>
            public static readonly Field TitleTemplatePath = FindByName(__.TitleTemplatePath);

            ///<summary>分类页模版</summary>
            public static readonly Field CategoryTemplatePath = FindByName(__.CategoryTemplatePath);

            ///<summary>类名</summary>
            public static readonly Field ClassName = FindByName(__.ClassName);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得模型字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary>编号</summary>
            public const String ID = "ID";

            ///<summary>名称</summary>
            public const String Name = "Name";

            ///<summary>启用</summary>
            public const String Enable = "Enable";

            ///<summary>创建人ID</summary>
            public const String CreateUserID = "CreateUserID";

            ///<summary>创建时间</summary>
            public const String CreateTime = "CreateTime";

            ///<summary>更新人ID</summary>
            public const String UpdateUserID = "UpdateUserID";

            ///<summary>更新时间</summary>
            public const String UpdateTime = "UpdateTime";

            ///<summary>备注</summary>
            public const String Remark = "Remark";

            ///<summary>标题页模版</summary>
            public const String TitleTemplatePath = "TitleTemplatePath";

            ///<summary>分类页模版</summary>
            public const String CategoryTemplatePath = "CategoryTemplatePath";

            ///<summary>类名</summary>
            public const String ClassName = "ClassName";

        }
        #endregion
    }

    /// <summary>模型接口</summary>
    /// <remarks>模型。默认有文章、文本、产品三种模型，可以扩展增加。</remarks>
    public partial interface IModel
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>名称</summary>
        String Name { get; set; }

        /// <summary>启用</summary>
        Boolean Enable { get; set; }

        /// <summary>创建人ID</summary>
        Int32 CreateUserID { get; set; }

        /// <summary>创建时间</summary>
        DateTime CreateTime { get; set; }

        /// <summary>更新人ID</summary>
        Int32 UpdateUserID { get; set; }

        /// <summary>更新时间</summary>
        DateTime UpdateTime { get; set; }

        /// <summary>备注</summary>
        String Remark { get; set; }

        /// <summary>标题页模版</summary>
        String TitleTemplatePath { get; set; }

        /// <summary>分类页模版</summary>
        String CategoryTemplatePath { get; set; }

        /// <summary>类名</summary>
        String ClassName { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
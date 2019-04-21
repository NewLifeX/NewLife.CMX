﻿﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace NewLife.CMX
{
    /// <summary>频道</summary>
    [Serializable]
    [DataObject]
    [Description("频道")]
    [BindIndex("IU_Channel_Name", true, "Name")]
    [BindIndex("IU_Channel_ModelID_Suffix", true, "ModelID,Suffix")]
    [BindIndex("IX_Channel_ModelID", false, "ModelID")]
    [BindRelation("ModelID", false, "Model", "ID")]
    [BindTable("Channel", Description = "频道", ConnName = "CMX", DbType = DatabaseType.SqlServer)]
    public partial class Channel : IChannel
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

        private String _DisplayName;
        /// <summary>显示名</summary>
        [DisplayName("显示名")]
        [Description("显示名")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(3, "DisplayName", "显示名", null, "nvarchar(50)", 0, 0, true, Master=true)]
        public virtual String DisplayName
        {
            get { return _DisplayName; }
            set { if (OnPropertyChanging(__.DisplayName, value)) { _DisplayName = value; OnPropertyChanged(__.DisplayName); } }
        }

        private Int32 _ModelID;
        /// <summary>模型</summary>
        [DisplayName("模型")]
        [Description("模型")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(4, "ModelID", "模型", null, "int", 10, 0, false)]
        public virtual Int32 ModelID
        {
            get { return _ModelID; }
            set { if (OnPropertyChanging(__.ModelID, value)) { _ModelID = value; OnPropertyChanged(__.ModelID); } }
        }

        private String _Suffix;
        /// <summary>后缀。默认频道后缀为空，扩展频道必须有不同的表后缀</summary>
        [DisplayName("后缀")]
        [Description("后缀。默认频道后缀为空，扩展频道必须有不同的表后缀")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(5, "Suffix", "后缀。默认频道后缀为空，扩展频道必须有不同的表后缀", null, "nvarchar(50)", 0, 0, true)]
        public virtual String Suffix
        {
            get { return _Suffix; }
            set { if (OnPropertyChanging(__.Suffix, value)) { _Suffix = value; OnPropertyChanged(__.Suffix); } }
        }

        private Boolean _Enable;
        /// <summary>启用</summary>
        [DisplayName("启用")]
        [Description("启用")]
        [DataObjectField(false, false, true, 1)]
        [BindColumn(6, "Enable", "启用", null, "bit", 0, 0, false)]
        public virtual Boolean Enable
        {
            get { return _Enable; }
            set { if (OnPropertyChanging(__.Enable, value)) { _Enable = value; OnPropertyChanged(__.Enable); } }
        }

        private String _Roles;
        /// <summary>角色列表</summary>
        [DisplayName("角色列表")]
        [Description("角色列表")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(7, "Roles", "角色列表", null, "nvarchar(50)", 0, 0, true)]
        public virtual String Roles
        {
            get { return _Roles; }
            set { if (OnPropertyChanging(__.Roles, value)) { _Roles = value; OnPropertyChanged(__.Roles); } }
        }

        private String _IndexTemplate;
        /// <summary>索引页模版。本频道专属首页</summary>
        [DisplayName("索引页模版")]
        [Description("索引页模版。本频道专属首页")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(8, "IndexTemplate", "索引页模版。本频道专属首页", null, "nvarchar(200)", 0, 0, true)]
        public virtual String IndexTemplate
        {
            get { return _IndexTemplate; }
            set { if (OnPropertyChanging(__.IndexTemplate, value)) { _IndexTemplate = value; OnPropertyChanged(__.IndexTemplate); } }
        }

        private String _CategoryTemplate;
        /// <summary>分类页模版。本频道专属列表页</summary>
        [DisplayName("分类页模版")]
        [Description("分类页模版。本频道专属列表页")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(9, "CategoryTemplate", "分类页模版。本频道专属列表页", null, "nvarchar(200)", 0, 0, true)]
        public virtual String CategoryTemplate
        {
            get { return _CategoryTemplate; }
            set { if (OnPropertyChanging(__.CategoryTemplate, value)) { _CategoryTemplate = value; OnPropertyChanged(__.CategoryTemplate); } }
        }

        private String _TitleTemplate;
        /// <summary>标题页模版。本频道专属内容页</summary>
        [DisplayName("标题页模版")]
        [Description("标题页模版。本频道专属内容页")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(10, "TitleTemplate", "标题页模版。本频道专属内容页", null, "nvarchar(200)", 0, 0, true)]
        public virtual String TitleTemplate
        {
            get { return _TitleTemplate; }
            set { if (OnPropertyChanging(__.TitleTemplate, value)) { _TitleTemplate = value; OnPropertyChanged(__.TitleTemplate); } }
        }

        private Int32 _CreateUserID;
        /// <summary>创建人</summary>
        [DisplayName("创建人")]
        [Description("创建人")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(11, "CreateUserID", "创建人", null, "int", 10, 0, false)]
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
        [BindColumn(12, "CreateTime", "创建时间", null, "datetime", 3, 0, false)]
        public virtual DateTime CreateTime
        {
            get { return _CreateTime; }
            set { if (OnPropertyChanging(__.CreateTime, value)) { _CreateTime = value; OnPropertyChanged(__.CreateTime); } }
        }

        private Int32 _UpdateUserID;
        /// <summary>更新人</summary>
        [DisplayName("更新人")]
        [Description("更新人")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(13, "UpdateUserID", "更新人", null, "int", 10, 0, false)]
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
        [BindColumn(14, "UpdateTime", "更新时间", null, "datetime", 3, 0, false)]
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
        [BindColumn(15, "Remark", "备注", null, "nvarchar(200)", 0, 0, true)]
        public virtual String Remark
        {
            get { return _Remark; }
            set { if (OnPropertyChanging(__.Remark, value)) { _Remark = value; OnPropertyChanged(__.Remark); } }
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
                    case __.DisplayName : return _DisplayName;
                    case __.ModelID : return _ModelID;
                    case __.Suffix : return _Suffix;
                    case __.Enable : return _Enable;
                    case __.Roles : return _Roles;
                    case __.IndexTemplate : return _IndexTemplate;
                    case __.CategoryTemplate : return _CategoryTemplate;
                    case __.TitleTemplate : return _TitleTemplate;
                    case __.CreateUserID : return _CreateUserID;
                    case __.CreateTime : return _CreateTime;
                    case __.UpdateUserID : return _UpdateUserID;
                    case __.UpdateTime : return _UpdateTime;
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
                    case __.DisplayName : _DisplayName = Convert.ToString(value); break;
                    case __.ModelID : _ModelID = Convert.ToInt32(value); break;
                    case __.Suffix : _Suffix = Convert.ToString(value); break;
                    case __.Enable : _Enable = Convert.ToBoolean(value); break;
                    case __.Roles : _Roles = Convert.ToString(value); break;
                    case __.IndexTemplate : _IndexTemplate = Convert.ToString(value); break;
                    case __.CategoryTemplate : _CategoryTemplate = Convert.ToString(value); break;
                    case __.TitleTemplate : _TitleTemplate = Convert.ToString(value); break;
                    case __.CreateUserID : _CreateUserID = Convert.ToInt32(value); break;
                    case __.CreateTime : _CreateTime = Convert.ToDateTime(value); break;
                    case __.UpdateUserID : _UpdateUserID = Convert.ToInt32(value); break;
                    case __.UpdateTime : _UpdateTime = Convert.ToDateTime(value); break;
                    case __.Remark : _Remark = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得频道字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary>名称</summary>
            public static readonly Field Name = FindByName(__.Name);

            ///<summary>显示名</summary>
            public static readonly Field DisplayName = FindByName(__.DisplayName);

            ///<summary>模型</summary>
            public static readonly Field ModelID = FindByName(__.ModelID);

            ///<summary>后缀。默认频道后缀为空，扩展频道必须有不同的表后缀</summary>
            public static readonly Field Suffix = FindByName(__.Suffix);

            ///<summary>启用</summary>
            public static readonly Field Enable = FindByName(__.Enable);

            ///<summary>角色列表</summary>
            public static readonly Field Roles = FindByName(__.Roles);

            ///<summary>索引页模版。本频道专属首页</summary>
            public static readonly Field IndexTemplate = FindByName(__.IndexTemplate);

            ///<summary>分类页模版。本频道专属列表页</summary>
            public static readonly Field CategoryTemplate = FindByName(__.CategoryTemplate);

            ///<summary>标题页模版。本频道专属内容页</summary>
            public static readonly Field TitleTemplate = FindByName(__.TitleTemplate);

            ///<summary>创建人</summary>
            public static readonly Field CreateUserID = FindByName(__.CreateUserID);

            ///<summary>创建时间</summary>
            public static readonly Field CreateTime = FindByName(__.CreateTime);

            ///<summary>更新人</summary>
            public static readonly Field UpdateUserID = FindByName(__.UpdateUserID);

            ///<summary>更新时间</summary>
            public static readonly Field UpdateTime = FindByName(__.UpdateTime);

            ///<summary>备注</summary>
            public static readonly Field Remark = FindByName(__.Remark);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得频道字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary>编号</summary>
            public const String ID = "ID";

            ///<summary>名称</summary>
            public const String Name = "Name";

            ///<summary>显示名</summary>
            public const String DisplayName = "DisplayName";

            ///<summary>模型</summary>
            public const String ModelID = "ModelID";

            ///<summary>后缀。默认频道后缀为空，扩展频道必须有不同的表后缀</summary>
            public const String Suffix = "Suffix";

            ///<summary>启用</summary>
            public const String Enable = "Enable";

            ///<summary>角色列表</summary>
            public const String Roles = "Roles";

            ///<summary>索引页模版。本频道专属首页</summary>
            public const String IndexTemplate = "IndexTemplate";

            ///<summary>分类页模版。本频道专属列表页</summary>
            public const String CategoryTemplate = "CategoryTemplate";

            ///<summary>标题页模版。本频道专属内容页</summary>
            public const String TitleTemplate = "TitleTemplate";

            ///<summary>创建人</summary>
            public const String CreateUserID = "CreateUserID";

            ///<summary>创建时间</summary>
            public const String CreateTime = "CreateTime";

            ///<summary>更新人</summary>
            public const String UpdateUserID = "UpdateUserID";

            ///<summary>更新时间</summary>
            public const String UpdateTime = "UpdateTime";

            ///<summary>备注</summary>
            public const String Remark = "Remark";

        }
        #endregion
    }

    /// <summary>频道接口</summary>
    public partial interface IChannel
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>名称</summary>
        String Name { get; set; }

        /// <summary>显示名</summary>
        String DisplayName { get; set; }

        /// <summary>模型</summary>
        Int32 ModelID { get; set; }

        /// <summary>后缀。默认频道后缀为空，扩展频道必须有不同的表后缀</summary>
        String Suffix { get; set; }

        /// <summary>启用</summary>
        Boolean Enable { get; set; }

        /// <summary>角色列表</summary>
        String Roles { get; set; }

        /// <summary>索引页模版。本频道专属首页</summary>
        String IndexTemplate { get; set; }

        /// <summary>分类页模版。本频道专属列表页</summary>
        String CategoryTemplate { get; set; }

        /// <summary>标题页模版。本频道专属内容页</summary>
        String TitleTemplate { get; set; }

        /// <summary>创建人</summary>
        Int32 CreateUserID { get; set; }

        /// <summary>创建时间</summary>
        DateTime CreateTime { get; set; }

        /// <summary>更新人</summary>
        Int32 UpdateUserID { get; set; }

        /// <summary>更新时间</summary>
        DateTime UpdateTime { get; set; }

        /// <summary>备注</summary>
        String Remark { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
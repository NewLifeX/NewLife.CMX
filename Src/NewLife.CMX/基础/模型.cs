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
    [Serializable]
    [DataObject]
    [Description("模型")]
    [BindIndex("IU_Model_Name", true, "Name")]
    [BindTable("Model", Description = "模型", ConnName = "CMX", DbType = DatabaseType.SqlServer)]
    public abstract partial class Model<TEntity> : IModel
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

        private String _ProviderName;
        /// <summary>提供者</summary>
        [DisplayName("提供者")]
        [Description("提供者")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(4, "ProviderName", "提供者", null, "nvarchar(50)", 0, 0, true)]
        public virtual String ProviderName
        {
            get { return _ProviderName; }
            set { if (OnPropertyChanging(__.ProviderName, value)) { _ProviderName = value; OnPropertyChanged(__.ProviderName); } }
        }

        private Boolean _Enable;
        /// <summary>启用</summary>
        [DisplayName("启用")]
        [Description("启用")]
        [DataObjectField(false, false, true, 1)]
        [BindColumn(5, "Enable", "启用", null, "bit", 0, 0, false)]
        public virtual Boolean Enable
        {
            get { return _Enable; }
            set { if (OnPropertyChanging(__.Enable, value)) { _Enable = value; OnPropertyChanged(__.Enable); } }
        }

        private String _IndexTemplate;
        /// <summary>索引页模版。本模型专属首页</summary>
        [DisplayName("索引页模版")]
        [Description("索引页模版。本模型专属首页")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(6, "IndexTemplate", "索引页模版。本模型专属首页", null, "nvarchar(200)", 0, 0, true)]
        public virtual String IndexTemplate
        {
            get { return _IndexTemplate; }
            set { if (OnPropertyChanging(__.IndexTemplate, value)) { _IndexTemplate = value; OnPropertyChanged(__.IndexTemplate); } }
        }

        private String _CategoryTemplate;
        /// <summary>分类页模版。本模型专属列表页</summary>
        [DisplayName("分类页模版")]
        [Description("分类页模版。本模型专属列表页")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(7, "CategoryTemplate", "分类页模版。本模型专属列表页", null, "nvarchar(200)", 0, 0, true)]
        public virtual String CategoryTemplate
        {
            get { return _CategoryTemplate; }
            set { if (OnPropertyChanging(__.CategoryTemplate, value)) { _CategoryTemplate = value; OnPropertyChanged(__.CategoryTemplate); } }
        }

        private String _InfoTemplate;
        /// <summary>信息页模版。本模型专属内容页</summary>
        [DisplayName("信息页模版")]
        [Description("信息页模版。本模型专属内容页")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(8, "InfoTemplate", "信息页模版。本模型专属内容页", null, "nvarchar(200)", 0, 0, true)]
        public virtual String InfoTemplate
        {
            get { return _InfoTemplate; }
            set { if (OnPropertyChanging(__.InfoTemplate, value)) { _InfoTemplate = value; OnPropertyChanged(__.InfoTemplate); } }
        }

        private Int32 _CreateUserID;
        /// <summary>创建人</summary>
        [DisplayName("创建人")]
        [Description("创建人")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(9, "CreateUserID", "创建人", null, "int", 10, 0, false)]
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
        [BindColumn(10, "CreateTime", "创建时间", null, "datetime", 3, 0, false)]
        public virtual DateTime CreateTime
        {
            get { return _CreateTime; }
            set { if (OnPropertyChanging(__.CreateTime, value)) { _CreateTime = value; OnPropertyChanged(__.CreateTime); } }
        }

        private String _CreateIP;
        /// <summary>创建地址</summary>
        [DisplayName("创建地址")]
        [Description("创建地址")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(11, "CreateIP", "创建地址", null, "nvarchar(50)", 0, 0, true)]
        public virtual String CreateIP
        {
            get { return _CreateIP; }
            set { if (OnPropertyChanging(__.CreateIP, value)) { _CreateIP = value; OnPropertyChanged(__.CreateIP); } }
        }

        private Int32 _UpdateUserID;
        /// <summary>更新人</summary>
        [DisplayName("更新人")]
        [Description("更新人")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(12, "UpdateUserID", "更新人", null, "int", 10, 0, false)]
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
        [BindColumn(13, "UpdateTime", "更新时间", null, "datetime", 3, 0, false)]
        public virtual DateTime UpdateTime
        {
            get { return _UpdateTime; }
            set { if (OnPropertyChanging(__.UpdateTime, value)) { _UpdateTime = value; OnPropertyChanged(__.UpdateTime); } }
        }

        private String _UpdateIP;
        /// <summary>更新地址</summary>
        [DisplayName("更新地址")]
        [Description("更新地址")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(14, "UpdateIP", "更新地址", null, "nvarchar(50)", 0, 0, true)]
        public virtual String UpdateIP
        {
            get { return _UpdateIP; }
            set { if (OnPropertyChanging(__.UpdateIP, value)) { _UpdateIP = value; OnPropertyChanged(__.UpdateIP); } }
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
                    case __.ProviderName : return _ProviderName;
                    case __.Enable : return _Enable;
                    case __.IndexTemplate : return _IndexTemplate;
                    case __.CategoryTemplate : return _CategoryTemplate;
                    case __.InfoTemplate : return _InfoTemplate;
                    case __.CreateUserID : return _CreateUserID;
                    case __.CreateTime : return _CreateTime;
                    case __.CreateIP : return _CreateIP;
                    case __.UpdateUserID : return _UpdateUserID;
                    case __.UpdateTime : return _UpdateTime;
                    case __.UpdateIP : return _UpdateIP;
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
                    case __.ProviderName : _ProviderName = Convert.ToString(value); break;
                    case __.Enable : _Enable = Convert.ToBoolean(value); break;
                    case __.IndexTemplate : _IndexTemplate = Convert.ToString(value); break;
                    case __.CategoryTemplate : _CategoryTemplate = Convert.ToString(value); break;
                    case __.InfoTemplate : _InfoTemplate = Convert.ToString(value); break;
                    case __.CreateUserID : _CreateUserID = Convert.ToInt32(value); break;
                    case __.CreateTime : _CreateTime = Convert.ToDateTime(value); break;
                    case __.CreateIP : _CreateIP = Convert.ToString(value); break;
                    case __.UpdateUserID : _UpdateUserID = Convert.ToInt32(value); break;
                    case __.UpdateTime : _UpdateTime = Convert.ToDateTime(value); break;
                    case __.UpdateIP : _UpdateIP = Convert.ToString(value); break;
                    case __.Remark : _Remark = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得模型字段信息的快捷方式</summary>
        partial class _
        {
            ///<summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary>名称</summary>
            public static readonly Field Name = FindByName(__.Name);

            ///<summary>显示名</summary>
            public static readonly Field DisplayName = FindByName(__.DisplayName);

            ///<summary>提供者</summary>
            public static readonly Field ProviderName = FindByName(__.ProviderName);

            ///<summary>启用</summary>
            public static readonly Field Enable = FindByName(__.Enable);

            ///<summary>索引页模版。本模型专属首页</summary>
            public static readonly Field IndexTemplate = FindByName(__.IndexTemplate);

            ///<summary>分类页模版。本模型专属列表页</summary>
            public static readonly Field CategoryTemplate = FindByName(__.CategoryTemplate);

            ///<summary>信息页模版。本模型专属内容页</summary>
            public static readonly Field InfoTemplate = FindByName(__.InfoTemplate);

            ///<summary>创建人</summary>
            public static readonly Field CreateUserID = FindByName(__.CreateUserID);

            ///<summary>创建时间</summary>
            public static readonly Field CreateTime = FindByName(__.CreateTime);

            ///<summary>创建地址</summary>
            public static readonly Field CreateIP = FindByName(__.CreateIP);

            ///<summary>更新人</summary>
            public static readonly Field UpdateUserID = FindByName(__.UpdateUserID);

            ///<summary>更新时间</summary>
            public static readonly Field UpdateTime = FindByName(__.UpdateTime);

            ///<summary>更新地址</summary>
            public static readonly Field UpdateIP = FindByName(__.UpdateIP);

            ///<summary>备注</summary>
            public static readonly Field Remark = FindByName(__.Remark);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得模型字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary>编号</summary>
            public const String ID = "ID";

            ///<summary>名称</summary>
            public const String Name = "Name";

            ///<summary>显示名</summary>
            public const String DisplayName = "DisplayName";

            ///<summary>提供者</summary>
            public const String ProviderName = "ProviderName";

            ///<summary>启用</summary>
            public const String Enable = "Enable";

            ///<summary>索引页模版。本模型专属首页</summary>
            public const String IndexTemplate = "IndexTemplate";

            ///<summary>分类页模版。本模型专属列表页</summary>
            public const String CategoryTemplate = "CategoryTemplate";

            ///<summary>信息页模版。本模型专属内容页</summary>
            public const String InfoTemplate = "InfoTemplate";

            ///<summary>创建人</summary>
            public const String CreateUserID = "CreateUserID";

            ///<summary>创建时间</summary>
            public const String CreateTime = "CreateTime";

            ///<summary>创建地址</summary>
            public const String CreateIP = "CreateIP";

            ///<summary>更新人</summary>
            public const String UpdateUserID = "UpdateUserID";

            ///<summary>更新时间</summary>
            public const String UpdateTime = "UpdateTime";

            ///<summary>更新地址</summary>
            public const String UpdateIP = "UpdateIP";

            ///<summary>备注</summary>
            public const String Remark = "Remark";

        }
        #endregion
    }

    /// <summary>模型接口</summary>
    public partial interface IModel
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>名称</summary>
        String Name { get; set; }

        /// <summary>显示名</summary>
        String DisplayName { get; set; }

        /// <summary>提供者</summary>
        String ProviderName { get; set; }

        /// <summary>启用</summary>
        Boolean Enable { get; set; }

        /// <summary>索引页模版。本模型专属首页</summary>
        String IndexTemplate { get; set; }

        /// <summary>分类页模版。本模型专属列表页</summary>
        String CategoryTemplate { get; set; }

        /// <summary>信息页模版。本模型专属内容页</summary>
        String InfoTemplate { get; set; }

        /// <summary>创建人</summary>
        Int32 CreateUserID { get; set; }

        /// <summary>创建时间</summary>
        DateTime CreateTime { get; set; }

        /// <summary>创建地址</summary>
        String CreateIP { get; set; }

        /// <summary>更新人</summary>
        Int32 UpdateUserID { get; set; }

        /// <summary>更新时间</summary>
        DateTime UpdateTime { get; set; }

        /// <summary>更新地址</summary>
        String UpdateIP { get; set; }

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
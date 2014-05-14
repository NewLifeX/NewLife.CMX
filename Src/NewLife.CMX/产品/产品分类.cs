using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace NewLife.CMX
{
    /// <summary>产品分类</summary>
    [Serializable]
    [DataObject]
    [Description("产品分类")]
    [BindTable("ProductCategory", Description = "产品分类", ConnName = "CMX", DbType = DatabaseType.SqlServer)]
    public partial class ProductCategory : IProductCategory
    {
        #region 字段名
        /// <summary>取得产品分类字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary>名称</summary>
            public static readonly Field Name = FindByName(__.Name);

            ///<summary>父类</summary>
            public static readonly Field ParentID = FindByName(__.ParentID);

            ///<summary>是否最终分类</summary>
            public static readonly Field IsEnd = FindByName(__.IsEnd);

            ///<summary>排序</summary>
            public static readonly Field Sort = FindByName(__.Sort);

            ///<summary>数量</summary>
            public static readonly Field Num = FindByName(__.Num);

            ///<summary>分类页模版。前台分类页</summary>
            public static readonly Field CategoryTemplate = FindByName(__.CategoryTemplate);

            ///<summary>标题页模版。前台列表页</summary>
            public static readonly Field TitleTemplate = FindByName(__.TitleTemplate);

            ///<summary>内容页模版。前台内容页</summary>
            public static readonly Field ContentTemplate = FindByName(__.ContentTemplate);

            ///<summary>备注</summary>
            public static readonly Field Remark = FindByName(__.Remark);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得产品分类字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary>编号</summary>
            public const String ID = "ID";

            ///<summary>名称</summary>
            public const String Name = "Name";

            ///<summary>父类</summary>
            public const String ParentID = "ParentID";

            ///<summary>是否最终分类</summary>
            public const String IsEnd = "IsEnd";

            ///<summary>排序</summary>
            public const String Sort = "Sort";

            ///<summary>数量</summary>
            public const String Num = "Num";

            ///<summary>分类页模版。前台分类页</summary>
            public const String CategoryTemplate = "CategoryTemplate";

            ///<summary>标题页模版。前台列表页</summary>
            public const String TitleTemplate = "TitleTemplate";

            ///<summary>内容页模版。前台内容页</summary>
            public const String ContentTemplate = "ContentTemplate";

            ///<summary>备注</summary>
            public const String Remark = "Remark";

        }
        #endregion
    }

    /// <summary>产品分类接口</summary>
    public partial interface IProductCategory : IEntityCategory
    {
    }
}
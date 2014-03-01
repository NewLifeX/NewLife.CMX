using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace NewLife.CMX
{
    /// <summary>文本内容</summary>
    [Serializable]
    [DataObject]
    [Description("文本内容")]
    [BindTable("TextContent", Description = "文本内容", ConnName = "CMX", DbType = DatabaseType.SqlServer)]
    public partial class TextContent : ITextContent
    {
        #region 字段名
        /// <summary>取得文本内容字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary>主题</summary>
            public static readonly Field ParentID = FindByName(__.ParentID);

            ///<summary>标题</summary>
            public static readonly Field Title = FindByName(__.Title);

            ///<summary>版本</summary>
            public static readonly Field Version = FindByName(__.Version);

            ///<summary>创建人</summary>
            public static readonly Field CreateUserID = FindByName(__.CreateUserID);

            ///<summary>创建人</summary>
            public static readonly Field CreateUserName = FindByName(__.CreateUserName);

            ///<summary>创建时间</summary>
            public static readonly Field CreateTime = FindByName(__.CreateTime);

            ///<summary>内容</summary>
            public static readonly Field Content = FindByName(__.Content);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得文本内容字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary>编号</summary>
            public const String ID = "ID";

            ///<summary>主题</summary>
            public const String ParentID = "ParentID";

            ///<summary>标题</summary>
            public const String Title = "Title";

            ///<summary>版本</summary>
            public const String Version = "Version";

            ///<summary>创建人</summary>
            public const String CreateUserID = "CreateUserID";

            ///<summary>创建人</summary>
            public const String CreateUserName = "CreateUserName";

            ///<summary>创建时间</summary>
            public const String CreateTime = "CreateTime";

            ///<summary>内容</summary>
            public const String Content = "Content";

        }
        #endregion
    }

    /// <summary>文本内容接口</summary>
    public partial interface ITextContent : IEntityContent
    {
    }
}
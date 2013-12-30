using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NewLife.CMX
{
    /// <summary>模型提供者接口。所有模型必须有提供者接口实现</summary>
    public interface IModelProvider
    {
        #region 属性
        /// <summary>模型名称</summary>
        String Name { get; }

        /// <summary>标题实体类</summary>
        Type TitleType { get; }

        /// <summary>分类实体类</summary>
        Type CategoryType { get; }

        /// <summary>内容实体类</summary>
        Type ContentType { get; }
        #endregion
    }

    /// <summary>模型提供者泛型基类</summary>
    /// <typeparam name="TTitle"></typeparam>
    /// <typeparam name="TCategory"></typeparam>
    /// <typeparam name="TContent"></typeparam>
    public abstract class ModelProvider<TTitle, TCategory, TContent> : IModelProvider
    {
        #region 属性
        private String _Name;
        /// <summary>模型名称</summary>
        public virtual String Name
        {
            get
            {
                if (_Name == null)
                {
                    var type = typeof(TTitle);
                    _Name = type.GetCustomAttributeValue<DisplayNameAttribute, String>();
                    if (String.IsNullOrEmpty(_Name)) _Name = type.GetCustomAttributeValue<DescriptionAttribute, String>();
                    if (String.IsNullOrEmpty(_Name)) _Name = type.Name;
                }
                return _Name;
            }
        }

        /// <summary>标题实体类</summary>
        public virtual Type TitleType { get { return typeof(TTitle); } }

        /// <summary>分类实体类</summary>
        public virtual Type CategoryType { get { return typeof(TCategory); } }

        /// <summary>内容实体类</summary>
        public virtual Type ContentType { get { return typeof(TContent); } }
        #endregion
    }
}
using System;
using System.ComponentModel;

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

        #region 当前频道
        /// <summary>当前频道</summary>
        Int32 CurrentChannel { get; set; }
        #endregion
    }

    /// <summary>模型提供者泛型基类</summary>
    /// <typeparam name="TTitle"></typeparam>
    /// <typeparam name="TCategory"></typeparam>
    /// <typeparam name="TContent"></typeparam>
    public abstract class ModelProvider<TTitle, TCategory, TContent> : IModelProvider
        where TTitle : EntityTitle<TTitle>, new()
        where TCategory : EntityCategory<TCategory>, new()
        where TContent : EntityContent<TContent>, new()
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

        #region 当前频道
        [ThreadStatic]
        private static Int32 _Current;
        /// <summary>当前频道</summary>
        public static Int32 CurrentChannel
        {
            get { return _Current; }
            set
            {
                if (value != 0)
                {
                    _Current = value;

                    var chn = Channel.FindByID(value);
                    if (chn != null)
                    {
                        var suffix = chn.Suffix;
                        EntityTitle<TTitle>.Meta.TableName = EntityTitle<TTitle>.Meta.Table.TableName + suffix;
                        EntityCategory<TCategory>.Meta.TableName = EntityCategory<TCategory>.Meta.Table.TableName + suffix;
                        EntityContent<TContent>.Meta.TableName = EntityContent<TContent>.Meta.Table.TableName + suffix;
                    }
                }
                else
                {
                    EntityTitle<TTitle>.Meta.TableName = null;
                    EntityCategory<TCategory>.Meta.TableName = null;
                    EntityContent<TContent>.Meta.TableName = null;
                }
            }
        }

        Int32 IModelProvider.CurrentChannel { get { return CurrentChannel; } set { CurrentChannel = value; } }
        #endregion
    }
}
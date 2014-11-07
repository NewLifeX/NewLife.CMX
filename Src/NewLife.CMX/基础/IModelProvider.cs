using System;
using System.Collections.Generic;
using System.ComponentModel;
using NewLife.Reflection;
using XCode;

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

        /// <summary>统计实体类</summary>
        Type StatisticsType { get; }
        #endregion

        #region 工厂
        /// <summary>标题实体工厂</summary>
        ITitleFactory TitleFactory { get; }

        /// <summary>标题实体工厂</summary>
        ICategoryFactory CategoryFactory { get; }

        /// <summary>内容实体工厂</summary>
        IContentFactory ContentFactory { get; }

        /// <summary>统计实体工厂</summary>
        IStatisticsFactory StatisticsFactory { get; }

        #endregion

        #region 当前频道
        /// <summary>当前频道。主要用于切换当前所在频道，直接影响当前模型各实体类在本线程所需要操作的频道数据表</summary>
        Int32 CurrentChannel { get; set; }
        #endregion

        #region 方法
        #endregion
    }

    /// <summary>模型提供者</summary>
    public class ModelProvider
    {
        #region 静态全局方法
        private static Dictionary<String, IModelProvider> _Providers;
        /// <summary>模型提供者集合</summary>
        public static Dictionary<String, IModelProvider> Providers
        {
            get
            {
                if (_Providers == null)
                {
                    var dic = new Dictionary<String, IModelProvider>(StringComparer.InvariantCultureIgnoreCase);
                    foreach (var item in typeof(IModelProvider).GetAllSubclasses(true))
                    {
                        var model = item.CreateInstance() as IModelProvider;
                        dic.Add(item.FullName, model);
                    }
                    _Providers = dic;
                }
                return _Providers;
            }
        }

        /// <summary>根据类型查找该类型所属模型提供者</summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IModelProvider Get(Type type)
        {
            foreach (var model in Providers.Values)
            {
                if (model.TitleType == type ||
                    model.CategoryType == type ||
                    model.ContentType == type ||
                    model.StatisticsType == type) return model;
            }
            return null;
        }

        /// <summary>根据类型查找该类型所属模型提供者</summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IModelProvider Get<T>() { return Get(typeof(T)); }

        #endregion
    }

    /// <summary>模型提供者泛型基类</summary>
    /// <typeparam name="TTitle"></typeparam>
    /// <typeparam name="TCategory"></typeparam>
    /// <typeparam name="TContent"></typeparam>
    /// <typeparam name="TStatistics"></typeparam>
    public abstract class ModelProvider<TTitle, TCategory, TContent, TStatistics> : ModelProvider, IModelProvider
        where TTitle : EntityTitle<TTitle>, new()
        where TCategory : EntityCategory<TCategory>, new()
        where TContent : EntityContent<TContent>, new()
        where TStatistics : Statistics<TStatistics>, new()
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

        /// <summary>统计实体类</summary>
        public virtual Type StatisticsType { get { return typeof(TStatistics); } }
        #endregion

        #region 工厂
        private ITitleFactory _TitleFactory;
        /// <summary>标题实体工厂</summary>
        public virtual ITitleFactory TitleFactory
        {
            get
            {
                return _TitleFactory ?? (_TitleFactory = EntityFactory.CreateOperate(TitleType) as ITitleFactory);
            }
        }

        private ICategoryFactory _CategoryFactory;
        /// <summary>标题实体工厂</summary>
        public virtual ICategoryFactory CategoryFactory
        {
            get
            {
                return _CategoryFactory ?? (_CategoryFactory = EntityFactory.CreateOperate(CategoryType) as ICategoryFactory);
            }
        }

        private IContentFactory _ContentFactory;
        /// <summary>内容实体工厂</summary>
        public virtual IContentFactory ContentFactory
        {
            get
            {
                return _ContentFactory ?? (_ContentFactory = EntityFactory.CreateOperate(ContentType) as IContentFactory);
            }
        }

        private IStatisticsFactory _StatisticsFactory;
        /// <summary>统计实体工厂</summary>
        public virtual IStatisticsFactory StatisticsFactory
        {
            get
            {
                return _StatisticsFactory ?? (_StatisticsFactory = EntityFactory.CreateOperate(StatisticsType) as IStatisticsFactory);
            }
        }
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
                        Statistics<TStatistics>.Meta.TableName = Statistics<TStatistics>.Meta.Table.TableName + suffix;
                    }
                }
                else
                {
                    EntityTitle<TTitle>.Meta.TableName = null;
                    EntityCategory<TCategory>.Meta.TableName = null;
                    EntityContent<TContent>.Meta.TableName = null;
                    Statistics<TStatistics>.Meta.TableName = null;
                }
            }
        }

        Int32 IModelProvider.CurrentChannel { get { return CurrentChannel; } set { CurrentChannel = value; } }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using XCode;

namespace NewLife.CMX
{
    /// <summary>信息扩展接口</summary>
    public interface IInfoExtend
    {
        /// <summary>信息编号</summary>
        Int32 InfoID { get; }

        /// <summary>该扩展所对应的信息</summary>
        IInfo Info { get; }
    }

    /// <summary>信息扩展基类</summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class InfoExtend<TEntity> : Entity<TEntity> where TEntity : InfoExtend<TEntity>, IInfoExtend, new()
    {
        /// <summary>来源</summary>
        [XmlIgnore, ScriptIgnore]
        [Map("ExtendID", typeof(Info), "ID")]
        public IInfo Info { get { return Extends.Get(nameof(Info), k => NewLife.CMX.Info.FindByID(this["ExtendID"].ToInt())); } }

        /// <summary>根据标题查找</summary>
        /// <param name="infoid">标题</param>
        /// <returns></returns>
        public static IList<TEntity> FindAllByInfoID(Int32 infoid)
        {
            if (Meta.Count >= 1000)
                return FindAll(nameof(IInfoExtend.InfoID), infoid);
            else // 实体缓存
                return Meta.Cache.Entities.Where(e => e.InfoID == infoid).ToList();
        }
    }
}
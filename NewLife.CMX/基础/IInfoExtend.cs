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
        Int32 InfoID { get; set; }

        /// <summary>该扩展所对应的信息</summary>
        Info Info { get; }

        ///// <summary>根据标题查找</summary>
        ///// <param name="infoid">标题</param>
        ///// <returns></returns>
        //IEntity FindByInfoID(Int32 infoid);
    }

    /// <summary>信息扩展基类</summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class InfoExtend<TEntity> : Entity<TEntity> where TEntity : InfoExtend<TEntity>, IInfoExtend, new()
    {
        /// <summary>来源</summary>
        [XmlIgnore, ScriptIgnore]
        [Map("ExtendID", typeof(Info), "ID")]
        public Info Info => Extends.Get(nameof(Info), k => NewLife.CMX.Info.FindByID(this["ExtendID"].ToInt()));

        /// <summary>根据标题查找</summary>
        /// <param name="infoid">标题</param>
        /// <returns></returns>
        public static TEntity FindByInfoID(Int32 infoid)
        {
            if (Meta.Count < 1000) return Meta.Cache.Find(e => e.InfoID == infoid);

            return Find(nameof(IInfoExtend.InfoID), infoid);
        }

        ///// <summary>根据标题查找</summary>
        ///// <param name="infoid">标题</param>
        ///// <returns></returns>
        //IEntity IInfoExtend.FindByInfoID(Int32 infoid) => FindByInfoID(infoid);
    }
}
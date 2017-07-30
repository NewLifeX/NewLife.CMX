using System;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using XCode;

namespace NewLife.CMX
{
    /// <summary>信息扩展接口</summary>
    public interface IInfoExtend
    {
        //Int32 ExtendID { get; }

        /// <summary>该扩展所对应的信息</summary>
        IInfo Info { get; }
    }

    /// <summary>信息扩展基类</summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class InfoExtend<TEntity> : Entity<TEntity>, IInfoExtend where TEntity : InfoExtend<TEntity>, new()
    {
        [NonSerialized]
        private IInfo _Info;
        /// <summary>该扩展所对应的信息</summary>
        [XmlIgnore, ScriptIgnore]
        [Map("ExtendID", typeof(Info), "ID")]
        public IInfo Info
        {
            get
            {
                var extid = this["ExtendID"].ToInt();
                if (_Info == null && extid > 0 && !Dirtys.ContainsKey("Info"))
                {
                    _Info = NewLife.CMX.Info.FindByID(extid);
                    Dirtys["Info"] = true;
                }
                return _Info;
            }
            set { _Info = value; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using XCode;

namespace NewLife.CMX
{
    public interface IInfoExtend
    {
        //Int32 ExtendID { get; }

        IInfo Info { get; }
    }

    public abstract class InfoExtend<TEntity> : Entity<TEntity>, IInfoExtend where TEntity : InfoExtend<TEntity>, new()
    {
        [NonSerialized]
        private IInfo _Info;
        /// <summary>该扩展所对应的信息</summary>
        [XmlIgnore]
        [BindRelation("ExtendID", false, "Info", "ID")]
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
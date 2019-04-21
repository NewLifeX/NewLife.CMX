/*
 * XCoder v6.1.5422.21756
 * 作者：nnhy/X2
 * 时间：2014-11-09 00:59:57
 * 版权：版权所有 (C) 新生命开发团队 2002~2014
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife.Common;
using NewLife.Log;
using NewLife.Serialization;
using XCode;
using XCode.Membership;

namespace NewLife.CMX
{
    /// <summary>导航</summary>
    public partial class Nav : EntityTree<Nav>
    {
        #region 对象操作﻿
        static Nav()
        {
            if (Setting == null) Setting = new EntityTreeSetting<Nav> { Factory = Meta.Factory };

            Meta.Modules.Add<UserModule>();
            Meta.Modules.Add<TimeModule>();
            Meta.Modules.Add<IPModule>();
        }

        /// <summary>验证导航数据</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
            if (!isNew && !HasDirty) return;

            base.Valid(isNew);
        }

        /// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void InitData()
        {
            base.InitData();

            // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
            // Meta.Count是快速取得表记录数
            if (Meta.Count > 0) return;

            // 需要注意的是，如果该方法调用了其它实体类的首次数据库操作，目标实体类的数据初始化将会在同一个线程完成
            if (XTrace.Debug) XTrace.WriteLine("开始初始化{0}[{1}]数据……", typeof(Nav).Name, Meta.Table.DataTable.DisplayName);

            var fn = "../InitData/{0}.json".F(Meta.TableName).GetFullPath();

            var header = Root.Add("头部");
            header.Add("首页", "/");
            header.Add("关于我们", "/About");

            var footer = Root.Add("尾部");
            var link = footer.Add("友情链接");
            link.Add("新生命团队", "http://www.NewLifeX.com");
            link.Add("开源项目", "https://github.com/NewLifeX");

            footer.Add("社区").Add("用户社区", "/Communicate");
            footer.Add("关于").Add("关于我们", "/About");

            if (XTrace.Debug) XTrace.WriteLine("完成初始化{0}[{1}]数据！", typeof(Nav).Name, Meta.Table.DataTable.DisplayName);
        }
        #endregion

        #region 扩展属性﻿
        /// <summary>分类</summary>
        [XmlIgnore, ScriptIgnore]
        public ICategory Category => Extends.Get(nameof(Category), k => NewLife.CMX.Category.FindByID(CategoryID));

        /// <summary>分类</summary>
        [XmlIgnore, ScriptIgnore]
        [DisplayName("分类")]
        [Map(__.CategoryID, typeof(Category), "ID")]
        public String CategoryName => Category + "";
        #endregion

        #region 扩展查询﻿
        /// <summary>根据ID查找导航</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Nav FindByID(Int32 id)
        {
            if (id <= 0) return null;

            return Meta.Cache.Find(e => e.ID == id);
        }

        /// <summary>根据名称查找导航</summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Nav FindByName(String name)
        {
            if (name.IsNullOrEmpty()) return null;

            return Meta.Cache.Find(e => e.Name == name);
        }
        #endregion

        #region 高级查询
        #endregion

        #region 扩展操作
        #endregion

        #region 业务
        /// <summary>添加导航</summary>
        /// <param name="name"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public Nav Add(String name, String url = null)
        {
            var entity = new Nav()
            {
                ParentID = ID,
                Name = name,
                Url = url,
                Enable = true,

                // 排序，新的在后面
                Sort = Childs.Count + 1
            };
            entity.Insert();

            return entity;
        }
        #endregion
    }
}
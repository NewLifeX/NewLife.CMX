/*
 * XCoder v5.1.4992.36291
 * 作者：nnhy/X
 * 时间：2013-09-01 20:13:59
 * 版权：版权所有 (C) 新生命开发团队 2002~2013
*/
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NewLife.Log;
using XCode;
using XCode.Membership;

namespace NewLife.CMX
{
    /// <summary>频道</summary>
    public partial class Channel : UserTimeEntity<Channel>
    {
        #region 对象操作﻿
        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
            // 这里验证参数范围，建议抛出参数异常，指定参数名，前端用户界面可以捕获参数异常并聚焦到对应的参数输入框
            //if (String.IsNullOrEmpty(Name)) throw new ArgumentNullException(__.Name, _.Name.DisplayName + "无效！");
            //if (!isNew && ID < 1) throw new ArgumentOutOfRangeException(__.ID, _.ID.DisplayName + "必须大于0！");

            // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
            base.Valid(isNew);

            if (String.IsNullOrEmpty(Roles)) AddRole(1);

            if (String.IsNullOrEmpty(DisplayName)) DisplayName = Name;
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
            if (XTrace.Debug) XTrace.WriteLine("开始初始化{0}[{1}]数据……", typeof(Channel).Name, Meta.Table.DataTable.DisplayName);

            Model.Meta.Session.WaitForInitData(3000);
            foreach (var item in Model.Meta.Session.Cache.Entities)
            {
                var entity = new Channel();
                entity.ModelID = item.ID;

                //entity.ListTemplate = String.Format("{0}ModelList", item.Provider.TitleType.Name);
                //entity.FormTemplate = String.Format("{0}ModelContent", item.Provider.TitleType.Name);
                // 考虑到在查询中null与string.empty在构造查询语句时的不同
                entity.Suffix = "";
                entity.Name = item.Name;
                entity.DisplayName = "默认" + (item.DisplayName ?? item.Name);
                entity.AddRole(1);
                entity.Enable = true;
                entity.Save();
            }

            if (XTrace.Debug) XTrace.WriteLine("完成初始化{0}[{1}]数据！", typeof(Channel).Name, Meta.Table.DataTable.DisplayName);
        }
        #endregion

        #region 扩展属性﻿
        /// <summary>模型</summary>
        [DisplayName("模型")]
        [BindRelation("ModelID", false, "Model", "ID")]
        public Model Model { get { return Model.FindByID(ModelID); } }

        /// <summary>模型名称</summary>
        [DisplayName("模型")]
        public String ModelName { get { return Model != null ? (Model.DisplayName ?? Model.Name) : "未命名"; } }
        #endregion

        #region 扩展查询﻿
        /// <summary>根据ID查询</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Channel FindByID(Int32 id)
        {
            if (Meta.Count >= 1000)
                return Find(__.ID, id);
            else
                return Meta.Cache.Entities.Find(__.ID, id);
        }

        /// <summary>根据名称查找</summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Channel FindByName(String name)
        {
            if (name.IsNullOrEmpty()) return null;

            if (Meta.Count >= 1000)
                return Find(__.Name, name);
            else // 实体缓存
                return Meta.Cache.Entities.Find(__.Name, name);
        }

        /// <summary>查找模型下的第一个频道</summary>
        /// <param name="ModelID"></param>
        /// <returns></returns>
        private static Channel FindByModelID(int ModelID)
        {
            if (Meta.Count > 1000)
                return Find(_.ModelID, ModelID);
            else
                return Meta.Cache.Entities.Find(_.ModelID, ModelID);
        }

        /// <summary>
        /// 根据扩展名查找
        /// </summary>
        /// <param name="Suffix"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Channel FindBySuffix(String Suffix)
        {
            if (Meta.Count > 1000)
                return Find(__.Suffix, Suffix);
            else
                return Meta.Cache.Entities.Find(__.Suffix, Suffix);
        }

        /// <summary>查找模型下的频道数</summary>
        /// <param name="modelid"></param>
        /// <returns></returns>
        public static Int32 FindCountByModel(Int32 modelid)
        {
            if (Meta.Count > 1000)
                return FindCount(__.ModelID, modelid);
            else
                return Meta.Cache.Entities.ToList().Count(e => e.ModelID == modelid);
        }

        /// <summary>
        /// 优先使用频道扩展名查询，在没有频道扩展名的前提下再使用模型编号查询
        /// 注意如果频道扩展名为空的情况下，只使用模型编号查询，返回的对象为所有使用该模型的频道中ID最后的一个
        /// </summary>
        /// <param name="Suffix"></param>
        /// <param name="ModelID"></param>
        /// <returns></returns>
        public static Channel FindBySuffixOrModel(String Suffix, Int32 ModelID = 0)
        {
            if (String.IsNullOrEmpty(Suffix) && ModelID == 0) return null;

            if (!String.IsNullOrEmpty(Suffix)) return FindBySuffix(Suffix);

            if (Suffix == null) return FindByModelID(ModelID);
            //Suffix = Suffix == null ? "" : Suffix;

            return FindBySuffixAndModel(Suffix, ModelID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="suffix"></param>
        /// <param name="modelID"></param>
        /// <returns></returns>
        public static Channel FindBySuffixAndModel(String suffix, Int32 modelID)
        {
            //if (Suffix == null || ModelID < 1) return null;

            if (Meta.Count > 1000)
                return Find(new String[] { __.Suffix, __.ModelID }, new Object[] { suffix, modelID });
            else if (suffix.IsNullOrEmpty())
                return Meta.Cache.Entities.Find(e => (e.Suffix.IsNullOrEmpty() && e.ModelID == modelID));
            else
                return Meta.Cache.Entities.Find(e => e.Suffix == suffix & e.ModelID == modelID);

        }

        /// <summary>
        /// 根据扩展名或id查询
        /// </summary>
        /// <returns></returns>
        public static Channel FindBySuffixOrID(String suffix, Int32 id)
        {
            if (String.IsNullOrEmpty(suffix) && id <= 0) return null;

            if (id > 0)
                return FindByID(id);
            else
                return FindBySuffix(suffix);
        }
        #endregion

        #region 高级查询
        #endregion

        #region 扩展操作
        private List<Int32> _roles;
        void InitRoles()
        {
            if (_roles != null) return;

            if (String.IsNullOrEmpty(Roles))
                _roles = new List<Int32>();
            else
            {
                var ss = Roles.Split(",");
                _roles = ss.Select(e => e.ToInt()).Where(e => e != 0).Distinct().ToList();
            }
        }

        /// <summary>是否有指定角色权限</summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public Boolean HasRole(Int32 roleid)
        {
            InitRoles();

            return _roles.Contains(roleid);
        }

        /// <summary>是否有指定角色权限</summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Boolean HasRole(IManageUser user) { return HasRole(user["RoleID"].ToInt()); }

        /// <summary>添加角色</summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public Channel AddRole(Int32 roleid)
        {
            InitRoles();

            if (!_roles.Contains(roleid))
            {
                _roles.Add(roleid);
                _roles.Sort();
                Roles = String.Join(",", _roles.ToArray().Select(e => e.ToString()).ToArray());
            }

            return this;
        }

        /// <summary>移除角色</summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public Channel RemoveRole(Int32 roleid)
        {
            InitRoles();

            if (_roles.Contains(roleid))
            {
                _roles.Remove(roleid);
                Roles = String.Join(",", _roles.ToArray().Select(e => e.ToString()).ToArray());
            }

            return this;
        }
        #endregion

        #region 业务
        /// <summary>当前频道的类别</summary>
        public IList<IEntityCategory> Categories
        {
            get
            {
                var tree = Model.Provider.CategoryFactory.Root;
                return tree.Childs.Cast<IEntityCategory>().ToList();
            }
        }

        /// <summary>所有末端类别</summary>
        public IList<IEntityCategory> AllCategories
        {
            get
            {
                var tree = Model.Provider.CategoryFactory.Root;
                return tree.AllChilds.Cast<IEntityCategory>().Where(e => e.Childs.Count == 0).ToList();
            }
        }

        /// <summary>查找当前频道之下的分类</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEntityCategory FindCategory(Int32 id)
        {
            var fact = Model.Provider.CategoryFactory;
            var cat = fact.FindByID(id);
            return cat;
        }

        /// <summary>查找当前频道之下的分类，如果分类不存在则按照层次创建</summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEntityCategory FindCategory(String name)
        {
            var fact = Model.Provider.CategoryFactory;
            var cat = fact.FindByName(name);
            if (cat == null) cat = fact.FindByPath(name);
            if (cat == null)
            {
                var root = fact.Root;

                foreach (var item in name.Split("/"))
                {
                    cat = fact.FindByName(item);
                    if (cat == null)
                    {
                        cat = fact.Create() as IEntityCategory;
                        cat.ParentID = root.ID;
                        cat.Name = item;
                        cat.ChannelID = ID;
                        cat.Insert();
                    }

                    root = cat;
                }
            }
            return cat;
        }

        /// <summary>根据编码查找分类</summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static IEntityCategory FindCategoryByCode(String code)
        {
            foreach (var chn in FindAllWithCache())
            {
                var fact = chn.Model.Provider.CategoryFactory;
                var cat = fact.FindByCode(code);
                if (cat != null) return cat;
            }

            return null;
        }

        /// <summary>查找主题</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEntityTitle FindTitle(Int32 id)
        {
            var fact = Model.Provider.TitleFactory;
            return fact.FindByID(id);
        }

        ///// <summary>获取标题列表</summary>
        ///// <param name="channelName">频道</param>
        ///// <param name="categoryPath"></param>
        ///// <param name="pageIndex"></param>
        ///// <param name="pageCount"></param>
        ///// <returns></returns>
        //public static IList<IEntityTitle> GetTitles(String channelName, String categoryPath, Int32 pageIndex = 1, Int32 pageCount = 10)
        //{
        //    var provider = ModelProvider.Get(this.GetType());
        //    return provider.TitleFactory.GetTitles(ID, pageIndex, pageCount);
        //}
        #endregion
    }
}
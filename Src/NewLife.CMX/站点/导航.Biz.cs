/*
 * XCoder v6.1.5422.21756
 * 作者：nnhy/X2
 * 时间：2014-11-09 00:59:57
 * 版权：版权所有 (C) 新生命开发团队 2002~2014
*/
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using NewLife.Common;
using NewLife.Log;
using XCode;
using XCode.Membership;
using NewLife.Serialization;

namespace NewLife.CMX
{
    /// <summary>导航</summary>
    public partial class Nav : UserTimeEntityTree<Nav>, IUserInfo, ITimeInfo
    {
        #region 验证数据
        /// <summary>验证导航数据</summary>
        /// <param name="isNew"></param>
        public override void Valid(bool isNew)
        {
            if (!isNew && !HasDirty) return;

            base.Valid(isNew);

            var fs = Meta.FieldNames;

            // 当前登录用户
            var user = ManageProvider.Provider.Current;
            if (user != null)
            {
                if (isNew)
                {
                    SetDirtyItem(__.CreateUserID, user.ID);
                }
                SetDirtyItem(__.UpdateUserID, user.ID);
            }
            if (isNew) SetDirtyItem(__.CreateTime, DateTime.Now);
            SetDirtyItem(__.UpdateTime, DateTime.Now);
        }

        /// <summary>设置脏数据项。如果某个键存在并且数据没有脏，则设置</summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        private void SetDirtyItem(String name, Object value)
        {
            if (Meta.FieldNames.Contains(name) && !Dirtys[name]) SetItem(name, value);
        }
        #endregion

        #region 对象操作﻿
        static Nav()
        {
            if (Setting == null) Setting = new EntityTreeSetting<Nav> { Factory = Meta.Factory };
            Setting.EnableCaching = false;
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

            if (File.Exists(fn))
            {
                if (XTrace.Debug) XTrace.WriteLine("使用数据初始化文件【{0}】初始化{1}[{2}]数据……", fn, typeof(Nav).Name, Meta.Table.DataTable.DisplayName);

                var list = EntityList<Nav>.FromJson(File.ReadAllText(fn));
                //var list = new EntityList<Nav>();
                //list.FromXml(File.ReadAllText(fn));
                var queue = new Queue<Nav>(list);
                while (queue.Count > 0)
                {
                    var item = queue.Dequeue();
                    item.Insert();

                    var childs = item.Childrens;
                    if (childs != null && childs.Count > 0)
                    {
                        foreach (var child in childs)
                        {
                            child.ParentID = item.ID;
                            queue.Enqueue(child);
                        }
                    }
                }
            }
            else
            {
                var header = Root.Add("头部");
                header.Add("首页", "/");
                header.Add("关于我们", "/About");

                var footer = Root.Add("尾部");
                var link = footer.Add("友情链接");
                link.Add("新生命团队", "http://www.NewLifeX.com");
                link.Add("无声物联", "http://www.peacemoon.cn");

                footer.Add("社区").Add("用户社区", "/Communicate");
                footer.Add("关于").Add("关于我们", "/About");

                // 开发模式下输出初始化配置
                if (SysConfig.Current.Develop)
                {
                    var list = new EntityList<Nav>();
                    list.Add(header);
                    list.Add(footer);
                    File.WriteAllText(fn.EnsureDirectory(), list.ToJson(true));
                }
            }

            if (XTrace.Debug) XTrace.WriteLine("完成初始化{0}[{1}]数据！", typeof(Nav).Name, Meta.Table.DataTable.DisplayName);
        }

        /// <summary>子孙节点</summary>
        public EntityList<Nav> Childrens { get; set; }
        #endregion

        #region 扩展属性﻿
        private IModel _Model;
        /// <summary>模型</summary>
        [XmlIgnore]
        [BindRelation("ModelID", false, "Model", "ID")]
        public IModel Model
        {
            get
            {
                if (_Model == null && ModelID > 0 && !Dirtys.ContainsKey("Model"))
                {
                    _Model = ModelX.FindByID(ModelID);
                    Dirtys["Model"] = true;
                }
                return _Model;
            }
            set { _Model = value; }
        }

        /// <summary>模型名称</summary>
        [XmlIgnore]
        [DisplayName("模型名称")]
        public String ModelName { get { return Model + ""; } }
        #endregion

        #region 扩展查询﻿
        /// <summary>根据ID查找导航</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Nav FindByID(Int32 id)
        {
            return Meta.Cache.Entities.Find(__.ID, id);
        }

        /// <summary>根据名称查找导航</summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Nav FindByName(String name)
        {
            return Meta.Cache.Entities.Find(__.Name, name);
        }
        #endregion

        #region 高级查询
        // 以下为自定义高级查询的例子

        ///// <summary>
        ///// 查询满足条件的记录集，分页、排序
        ///// </summary>
        ///// <param name="key">关键字</param>
        ///// <param name="orderClause">排序，不带Order By</param>
        ///// <param name="startRowIndex">开始行，0表示第一行</param>
        ///// <param name="maximumRows">最大返回行数，0表示所有行</param>
        ///// <returns>实体集</returns>
        //[DataObjectMethod(DataObjectMethodType.Select, true)]
        //public static EntityList<Nav> Search(String key, String orderClause, Int32 startRowIndex, Int32 maximumRows)
        //{
        //    return FindAll(SearchWhere(key), orderClause, null, startRowIndex, maximumRows);
        //}

        ///// <summary>
        ///// 查询满足条件的记录总数，分页和排序无效，带参数是因为ObjectDataSource要求它跟Search统一
        ///// </summary>
        ///// <param name="key">关键字</param>
        ///// <param name="orderClause">排序，不带Order By</param>
        ///// <param name="startRowIndex">开始行，0表示第一行</param>
        ///// <param name="maximumRows">最大返回行数，0表示所有行</param>
        ///// <returns>记录数</returns>
        //public static Int32 SearchCount(String key, String orderClause, Int32 startRowIndex, Int32 maximumRows)
        //{
        //    return FindCount(SearchWhere(key), null, null, 0, 0);
        //}

        /// <summary>构造搜索条件</summary>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        private static String SearchWhere(String key)
        {
            // WhereExpression重载&和|运算符，作为And和Or的替代
            // SearchWhereByKeys系列方法用于构建针对字符串字段的模糊搜索
            var exp = SearchWhereByKeys(key, null);

            // 以下仅为演示，Field（继承自FieldItem）重载了==、!=、>、<、>=、<=等运算符（第4行）
            //if (userid > 0) exp &= _.OperatorID == userid;
            //if (isSign != null) exp &= _.IsSign == isSign.Value;
            //if (start > DateTime.MinValue) exp &= _.OccurTime >= start;
            //if (end > DateTime.MinValue) exp &= _.OccurTime < end.AddDays(1).Date;

            return exp;
        }
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
            var entity = new Nav();
            entity.ParentID = ID;
            entity.Name = name;
            entity.Url = url;
            entity.Enable = true;

            // 排序，新的在后面
            entity.Sort = this.Childs.Count + 1;

            entity.Insert();

            return entity;
        }
        #endregion
    }
}
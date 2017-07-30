/*
 * XCoder v6.6.5879.6460
 * 作者：Stone/X
 * 时间：2016-02-06 17:22:55
 * 版权：版权所有 (C) 新生命开发团队 2002~2016
*/
using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife.Data;
using NewLife.Model;
using XCode;
using XCode.Cache;
using XCode.Membership;

namespace NewLife.CMX
{
    /// <summary>内容</summary>
    [ModelCheckMode(ModelCheckModes.CheckTableWhenFirstUse)]
    public class Content : Content<Content> { }

    /// <summary>内容</summary>
    public partial class Content<TEntity> : Entity<TEntity> where TEntity : Content<TEntity>, new()
    {
        #region 对象操作
        /// <summary>根据ParentID缓存最后版本</summary>
        static SingleEntityCache<Int32, TEntity> _cache;

        static Content()
        {
            // 用于引发基类的静态构造函数，所有层次的泛型实体类都应该有一个
            var entity = new TEntity();

            _cache = new SingleEntityCache<int, TEntity>();
            _cache.FindKeyMethod = id =>
            {
                var list = FindAll(_.InfoID == id, _.Version.Desc(), null, 0, 1);
                return list.Count > 0 ? list[0] : null;
            };

            Meta.Modules.Add<UserModule>();
            Meta.Modules.Add<TimeModule>();
            Meta.Modules.Add<IPModule>();
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;

            // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
            base.Valid(isNew);
        }

        /// <summary>不允许修改</summary>
        /// <returns></returns>
        public override int Update()
        {
            // 重载非OnXXX版本是为了让实体类内部可以直接调用OnXXX越过这里的检查
            throw new XException(Meta.Table.DataTable.DisplayName + "不允许修改！");
            //return base.Update();
        }

        /// <summary>不允许修改</summary>
        /// <returns></returns>
        public override int Delete()
        {
            // 重载非OnXXX版本是为了让实体类内部可以直接调用OnXXX越过这里的检查
            throw new XException(Meta.Table.DataTable.DisplayName + "不允许删除！");
            //return base.Delete();
        }
        #endregion

        #region 扩展属性
        /// <summary>创建人</summary>
        [XmlIgnore, ScriptIgnore]
        [DisplayName("创建人")]
        public IManageUser CreateUser { get { return Extends.Get(nameof(CreateUser), k => ManageProvider.Provider.FindByID(CreateUserID)); } }

        /// <summary>创建人名称</summary>
        [XmlIgnore, ScriptIgnore]
        [DisplayName("创建人")]
        [Map(__.CreateUserID)]
        public String CreateUserName { get { return CreateUser + ""; } }
        #endregion

        #region 扩展查询
        /// <summary>根据ID查询</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static TEntity FindByID(Int32 id)
        {
            if (Meta.Count >= 1000)
                return Meta.SingleCache[id];
            else
                return Meta.Cache.Entities.Find(__.ID, id);
        }

        /// <summary>根据主题查找</summary>
        /// <param name="parentid">主题</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EntityList<TEntity> FindAllByParentID(Int32 parentid)
        {
            if (Meta.Count >= 1000)
                return FindAll(__.InfoID, parentid);
            else // 实体缓存
                return Meta.Cache.Entities.FindAll(__.InfoID, parentid);
        }

        /// <summary>根据主题、版本查找</summary>
        /// <param name="parentid">主题</param>
        /// <param name="version">版本</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static TEntity FindByParentIDAndVersion(Int32 parentid, Int32 version)
        {
            if (Meta.Count >= 1000)
                return Find(new String[] { __.InfoID, __.Version }, new Object[] { parentid, version });
            else // 实体缓存
                return Meta.Cache.Entities.Find(e => e.InfoID == parentid && e.Version == version);
        }

        /// <summary>根据ParentID查询最后版本</summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static TEntity FindLastByParentID(Int32 parentid)
        {
            if (Meta.Count >= 1000)
                return _cache[parentid];
            else
                return Meta.Cache.Entities.FindAll(__.InfoID, parentid).Sort(__.Version, true).ToList().FirstOrDefault();
        }
        #endregion

        #region 高级查询
        // 以下为自定义高级查询的例子

        /// <summary>查询满足条件的记录集，分页、排序</summary>
        /// <param name="userid">用户编号</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="key">关键字</param>
        /// <param name="param">分页排序参数，同时返回满足条件的总记录数</param>
        /// <returns>实体集</returns>
        public static EntityList<TEntity> Search(Int32 userid, DateTime start, DateTime end, String key, PageParameter param)
        {
            // WhereExpression重载&和|运算符，作为And和Or的替代
            // SearchWhereByKeys系列方法用于构建针对字符串字段的模糊搜索，第二个参数可指定要搜索的字段
            var exp = SearchWhereByKeys(key, null, null);

            // 以下仅为演示，Field（继承自FieldItem）重载了==、!=、>、<、>=、<=等运算符
            //if (userid > 0) exp &= _.OperatorID == userid;
            //if (isSign != null) exp &= _.IsSign == isSign.Value;
            //exp &= _.OccurTime.Between(start, end); // 大于等于start，小于end，当start/end大于MinValue时有效

            return FindAll(exp, param);
        }
        #endregion

        #region 扩展操作
        /// <summary>根据父级编号删除对应内容</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Int32 DeleteByParentID(Int32 id)
        {
            var rs = 0;
            var list = FindAllByParentID(id);
            foreach (var item in list)
            {
                rs += item.OnDelete();
            }
            return rs;
        }
        #endregion

        #region 业务
        #endregion
    }
}
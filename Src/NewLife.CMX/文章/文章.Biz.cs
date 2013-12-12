/*
 * XCoder v5.1.4992.36291
 * 作者：nnhy/X
 * 时间：2013-09-01 20:13:59
 * 版权：版权所有 (C) 新生命开发团队 2002~2013
*/
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using NewLife.CMX.ModelBase;
using NewLife.CMX.Tool;
using NewLife.CommonEntity;
using NewLife.Log;
using NewLife.Web;
using XCode;
using XCode.Configuration;

namespace NewLife.CMX
{
    /// <summary>文章</summary>
    public partial class Article : ModelEntityBase<Article>
    {
        #region 对象操作﻿

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
            // 这里验证参数范围，建议抛出参数异常，指定参数名，前端用户界面可以捕获参数异常并聚焦到对应的参数输入框
            //if (String.IsNullOrEmpty(Name)) throw new ArgumentNullException(_.Name, _.Name.DisplayName + "无效！");
            //if (!isNew && ID < 1) throw new ArgumentOutOfRangeException(_.ID, _.ID.DisplayName + "必须大于0！");

            // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
            base.Valid(isNew);

            // 在新插入数据或者修改了指定字段时进行唯一性验证，CheckExist内部抛出参数异常
            //if (isNew || Dirtys[__.Name]) CheckExist(__.Name);

            if (isNew && !Dirtys[__.CreateTime])
            {
                CreateTime = DateTime.Now;
                CreateUserID = Admin.Current.ID;
                CreateUserName = Admin.Current.DisplayName;
            }
            //过滤应更新点击而导致的数据验证
            if (!Dirtys[__.UpdateTime] && Admin.Current != null && !(Dirtys.Count == 1 && Dirtys[_.Hits]))
            {
                UpdateTime = DateTime.Now;
                UpdateUserID = Admin.Current.ID;
                UpdateUserName = Admin.Current.DisplayName;
            }
        }

        ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected override void InitData()
        //{
        //    base.InitData();

        //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        //    // Meta.Count是快速取得表记录数
        //    if (Meta.Count > 0) return;

        //    // 需要注意的是，如果该方法调用了其它实体类的首次数据库操作，目标实体类的数据初始化将会在同一个线程完成
        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化{0}[{1}]数据……", typeof(Article).Name, Meta.Table.DataTable.DisplayName);

        //    var entity = new Article();
        //    entity.CategoryID = 0;
        //    entity.Title = "abc";
        //    entity.Version = 0;
        //    entity.Hits = 0;
        //    entity.StatisticsID = 0;
        //    entity.CreateUserID = 0;
        //    entity.CreateUserName = "abc";
        //    entity.CreateTime = DateTime.Now;
        //    entity.UpdateUserID = 0;
        //    entity.UpdateUserName = "abc";
        //    entity.UpdateTime = DateTime.Now;
        //    entity.Remark = "abc";
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化{0}[{1}]数据！", typeof(Article).Name, Meta.Table.DataTable.DisplayName);
        //}


        ///// <summary>已重载。基类先调用Valid(true)验证数据，然后在事务保护内调用OnInsert</summary>
        ///// <returns></returns>
        //public override Int32 Insert()
        //{
        //    return base.Insert();
        //}

        /// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        /// <returns></returns>
        protected override Int32 OnInsert()
        {
            Version += 1;

            Int32 num = base.OnInsert();

            HelperTool.SaveModelContent(typeof(ArticleContent), Version, ChannelSuffix, this, null);

            return num;
        }

        /// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        protected override int OnUpdate()
        {
            //过滤只更新点击次数的情况
            if (Admin.Current != null && !(Dirtys.Count == 1 && Dirtys[_.Hits]))
            {
                Version += 1;

                HelperTool.SaveModelContent(typeof(ArticleContent), Version, ChannelSuffix, this, null);
            }
            return base.OnUpdate();
        }


        #endregion

        #region 扩展属性﻿
        public static String ChannelSuffix;

        private Channel _Channel;
        /// <summary>频道</summary>
        public Channel Channel
        {
            get
            {
                if (_Channel == null && ChannelSuffix != null && !Dirtys.ContainsKey("Channel"))
                {
                    _Channel = Channel.FindBySuffix(ChannelSuffix);
                    Dirtys["Channel"] = true;
                }
                return _Channel;
            }
            set { _Channel = value; }
        }

        private String _ChannelName;
        /// <summary>频道名</summary>
        public String ChannelName
        {
            get
            {
                if (!Dirtys.ContainsKey("ChannelName"))
                {
                    _ChannelName = Channel == null ? "" : Channel.Name;
                    Dirtys["ChannelName"] = true;
                }
                return _ChannelName;
            }
            set { _ChannelName = value; }
        }

        private ArticleContent _ArticleContent;
        /// <summary></summary>
        public ArticleContent ArticleContent
        {
            get
            {
                try
                {
                    if (_ArticleContent == null && !Dirtys.ContainsKey("ArticleContent"))
                    {
                        ArticleContent.Meta.TableName = "";
                        ArticleContent.Meta.TableName += ChannelSuffix;
                        _ArticleContent = ArticleContent.FindByParentIDAndVersion(ID, Version);

                        if (_ArticleContent == null)
                        {
                            _ArticleContent = new ArticleContent();
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    ArticleContent.Meta.TableName = "";
                }

                return _ArticleContent;
            }
            set { _ArticleContent = value; }
        }

        private String _ConentTxt;
        /// <summary></summary>
        public String ConentTxt
        {
            get
            {
                if (_ConentTxt == null && !Dirtys.ContainsKey("ConentTxt"))
                {
                    _ConentTxt = ArticleContent.Content ?? "";
                    //_ConentTxt = "";
                    Dirtys["ConentTxt"] = true;
                }
                return _ConentTxt;
            }
            set
            {
                _ConentTxt = value;
            }
        }
        #endregion

        #region 扩展查询﻿
        /// <summary>根据ID查询</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Article FindByID(Int32 id)
        {
            if (Meta.Count >= 1000)
                return Meta.SingleCache[id];
            else
                return Meta.Cache.Entities.Find(__.ID, id);
        }

        /// <summary>根据分类查找</summary>
        /// <param name="categoryid">分类</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EntityList<Article> FindAllByCategoryID(Int32 categoryid)
        {
            if (Meta.Count >= 1000)
                return FindAll(_.CategoryID, categoryid);
            else // 实体缓存
                return Meta.Cache.Entities.FindAll(_.CategoryID, categoryid);
        }
        #endregion

        #region 高级查询
        // 以下为自定义高级查询的例子

        /// <summary>
        /// 查询满足条件的记录集，分页、排序
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="orderClause">排序，不带Order By</param>
        /// <param name="startRowIndex">开始行，0表示第一行</param>
        /// <param name="maximumRows">最大返回行数，0表示所有行</param>
        /// <returns>实体集</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static EntityList<Article> Search(String key, Int32 CategoryID, String orderClause, Int32 startRowIndex, Int32 maximumRows)
        {
            return FindAll(SearchWhere(key, CategoryID), orderClause, null, startRowIndex, maximumRows);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="orderClause"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static EntityList<Article> Search(Int32[] CategoryID, String orderClause, Int32 startRowIndex, Int32 maximumRows)
        {
            return FindAll(SearchWhere(CategoryID), orderClause, null, startRowIndex, maximumRows);
        }
        /// <summary>
        /// 查询满足条件的记录总数，分页和排序无效，带参数是因为ObjectDataSource要求它跟Search统一
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="orderClause">排序，不带Order By</param>
        /// <param name="startRowIndex">开始行，0表示第一行</param>
        /// <param name="maximumRows">最大返回行数，0表示所有行</param>
        /// <returns>记录数</returns>
        public static Int32 SearchCount(String key, Int32 CategoryID, String orderClause, Int32 startRowIndex, Int32 maximumRows)
        {
            return FindCount(SearchWhere(key, CategoryID), null, null, 0, 0);
        }


        public static Int32 SearchCount(Int32[] CategoryID, String orderClause, Int32 startRowIndex, Int32 maximumRows)
        {
            return FindCount(SearchWhere(CategoryID), null, null, 0, 0);
        }
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

        private static String SearchWhere(String key, Int32 CategoryID)
        {
            var exp = SearchWhereByKeys(key, null);

            exp &= _.CategoryID == CategoryID;

            return exp;
        }

        private static String SearchWhere(Int32[] CategoryID)
        {
            //String[] str = Array.ConvertAll(CategoryID, e => e.ToString());
            //String array = String.Join(",", str);
            //var exp = SearchWhereByKeys(null, null);
            //exp=SearchWhere
            var exp = new WhereExpression();
            exp &= _.CategoryID.In(CategoryID);

            return exp;
        }
        #endregion

        #region 扩展操作
        #endregion

        #region 业务
        ///// <summary>
        ///// 保存文章内容
        ///// </summary>
        //private void SaveContent(Int32 version)
        //{
        //    IEntityOperate ieo = EntityFactory.CreateOperate(typeof(ArticleContent));
        //    ieo.TableName += ChannelSuffix;

        //    ArticleContent ac = ieo.Create() as ArticleContent;

        //    try
        //    {
        //        ac.ParentID = ID;
        //        ac.Content = ArticleContent.Content;
        //        ac.Title = Title;

        //        ac.Version = version;

        //        ac.Save();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        ieo.TableName = "";
        //    }
        //}

        /// <summary>
        /// 更新点击次数
        /// </summary>
        /// <param name="Suffix"></param>
        /// <param name="ParentID"></param>
        public static void UpdateClickHit(string Suffix, Int32 ParentID)
        {
            try
            {
                Meta.TableName += Suffix;
                var entity = FindByID(ParentID);

                entity.Hits += 1;
                entity.Save();
            }
            catch (Exception ex)
            {
                XTrace.WriteLine(ex.Message);
            }
            finally
            {
                Meta.TableName = "";
            }
        }
        #endregion
    }
}
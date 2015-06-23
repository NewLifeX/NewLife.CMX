/*
 * XCoder v5.1.4992.36291
 * 作者：nnhy/X
 * 时间：2013-09-01 20:13:59
 * 版权：版权所有 (C) 新生命开发团队 2002~2013
*/
﻿using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using NewLife.CMX.Config;
using NewLife.CMX.Editor;
using NewLife.Log;
using XCode;

namespace NewLife.CMX
{
    /// <summary>文章</summary>
    public partial class Article : EntityTitle<Article, ArticleCategory, ArticleContent, ArticleStatistics>
    {
        #region 对象操作﻿
        public override void Valid(bool isNew)
        {
            base.Valid(isNew);

            if (!Dirtys[__.PublishTime]) PublishTime = DateTime.Now;
        }
        #endregion

        #region 扩展属性﻿
        [NonSerialized]
        private Source _Source;
        /// <summary>该文章所对应的来源</summary>
        [XmlIgnore]
        public Source Source
        {
            get
            {
                if (_Source == null && !Dirtys.ContainsKey("Source"))
                {
                    _Source = Source.FindByID(SourceID);
                    if (_Source == null)
                    {
                        _Source = new Source();
                        _Source.Name = "本站";
                        _Source.Url = "/";
                    }

                    Dirtys["Source"] = true;
                }
                return _Source;
            }
            set { _Source = value; }
        }

        /// <summary>该文章所对应的来源名称</summary>
        [XmlIgnore]
        public String SourceName { get { return Source != null ? Source.Name : String.Empty; } }

        private String _FirstImagePath;
        /// <summary>图片路径</summary>
        public String FirstImagePath
        {
            get
            {
                if (String.IsNullOrEmpty(_FirstImagePath) && !String.IsNullOrEmpty(ContentText) && !Dirtys.ContainsKey("FirstImagePath"))
                {
                    //获取文章中的第一张照片
                    Regex r = new Regex(@"<img\ssrc=""([^>]*)file=([^>=""]*)""(.*)/>");
                    Match m = r.Match(ContentText);
                    _FirstImagePath = m.Groups[2].ToString();

                    _FirstImagePath = _FirstImagePath.StartsWith("~/update") ? _FirstImagePath : UEditorConfig.Current.UploadPath + _FirstImagePath;
                    //FirstImagePath = _FirstImagePath.Replace("~", CMXConfigBase.Current.CurrentRootPath);
                    Dirtys["FirstImagePath"] = true;
                }
                return _FirstImagePath;
            }
            set { _FirstImagePath = value; }
        }
        #endregion

        #region 扩展查询﻿
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
            if (Meta.Count < 1000 && key.IsNullOrWhiteSpace()) return FindAllByCategoryID(CategoryID).Page(startRowIndex, maximumRows);

            return FindAll(SearchWhere(key, CategoryID), orderClause, null, startRowIndex, maximumRows);
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
            if (Meta.Count < 1000 && key.IsNullOrWhiteSpace()) return FindAllByCategoryID(CategoryID).Count;

            return FindCount(SearchWhere(key, CategoryID), null, null, 0, 0);
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
            if (Meta.Count < 1000) return Meta.Cache.Entities.FindAll(e => Array.IndexOf(CategoryID, e.CategoryID) >= 0).Page(startRowIndex, maximumRows);

            return FindAll(SearchWhere(CategoryID), orderClause, null, startRowIndex, maximumRows);
        }

        public static Int32 SearchCount(Int32[] CategoryID, String orderClause, Int32 startRowIndex, Int32 maximumRows)
        {
            if (Meta.Count < 1000) return Meta.Cache.Entities.FindAll(e => Array.IndexOf(CategoryID, e.CategoryID) >= 0).Count;

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
            var entity = FindByID(ParentID);

            //entity.Hits++;
            //entity.Save();
        }

        /// <summary>
        /// 获取幻灯片实体列表
        /// </summary>
        /// <param name="take"></param>
        /// <returns></returns>
        public static EntityList<Article> GetSlides(Int32 take)
        {
            if (take <= 0) throw new ArgumentOutOfRangeException("take", "必须大于0!");
            //var provider = ModelProvider.Get<TEntity>();
            //var model = Model.FindByName(provider.Name);
            var where = _.Slide == true;
            var order = _.ID.Desc();
            return FindAll(where, order, null, 0, take);
        }
        /// <summary>
        /// 获取推荐实体列表
        /// </summary>
        /// <param name="take"></param>
        /// <returns></returns>
        public static EntityList<Article> GetRecommends(Int32 take)
        {
            if (take <= 0) throw new ArgumentOutOfRangeException("take", "必须大于0!");
            var where = _.Recommend == true;
            var order = _.ID.Desc();
            return FindAll(where, order, null, 0, take);
        }
        #endregion
    }
}
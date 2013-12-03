/*
 * XCoder v5.1.4992.36291
 * 作者：nnhy/X
 * 时间：2013-09-01 20:13:59
 * 版权：版权所有 (C) 新生命开发团队 2002~2013
*/
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// <summary>产品</summary>
    public partial class Product : ModelEntityBase<Product>
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

            // 货币保留6位小数
            if (Dirtys[__.Price]) Price = Math.Round(Price, 6);
            if (isNew && !Dirtys[__.CreateTime])
            {
                CreateTime = DateTime.Now;
                CreateUserID = Admin.Current.ID;
                CreateUserName = Admin.Current.DisplayName;
            }
            if (!Dirtys[__.UpdateTime])
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
        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化{0}[{1}]数据……", typeof(Product).Name, Meta.Table.DataTable.DisplayName);

        //    var entity = new Product();
        //    entity.CategoryID = 0;
        //    entity.Title = "abc";
        //    entity.Version = 0;
        //    entity.Price = 0;
        //    entity.StatisticsID = 0;
        //    entity.CreateUserID = 0;
        //    entity.CreateUserName = "abc";
        //    entity.CreateTime = DateTime.Now;
        //    entity.UpdateUserID = 0;
        //    entity.UpdateUserName = "abc";
        //    entity.UpdateTime = DateTime.Now;
        //    entity.Remark = "abc";
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化{0}[{1}]数据！", typeof(Product).Name, Meta.Table.DataTable.DisplayName);
        //}


        ///// <summary>已重载。基类先调用Valid(true)验证数据，然后在事务保护内调用OnInsert</summary>
        ///// <returns></returns>
        //public override Int32 Insert()
        //{
        //    return base.Insert();
        //}

        ///// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        ///// <returns></returns>
        //protected override Int32 OnInsert()
        //{
        //    return base.OnInsert();
        //}

        /// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        /// <returns></returns>
        protected override Int32 OnInsert()
        {
            Version += 1;

            Int32 num = base.OnInsert();

            //SaveContent(Version);
            HelperTool.SaveModelProductContent(typeof(ProductContent), Version, ChannelSuffix, this, null);

            return num;
        }

        /// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        protected override int OnUpdate()
        {
            Version += 1;

            //SaveContent(Version);
            HelperTool.SaveModelProductContent(typeof(ProductContent), Version, ChannelSuffix, this, null);

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
                if (_ChannelName == null && !Dirtys.ContainsKey("ChannelName"))
                {
                    _ChannelName = Channel == null ? "" : Channel.Name;
                    Dirtys["ChannelName"] = true;
                }
                return _ChannelName;
            }
            set { _ChannelName = value; }
        }

        private ProductContent _ProductContent;
        /// <summary></summary>
        public ProductContent ProductContent
        {
            get
            {
                try
                {
                    if (_ProductContent == null && !Dirtys.ContainsKey("ProductContent"))
                    {
                        ProductContent.Meta.TableName = "";
                        ProductContent.Meta.TableName += ChannelSuffix;

                        _ProductContent = ProductContent.FindByParentIDAndVersion(ID, Version);

                        if (_ProductContent == null)
                        {
                            _ProductContent = new ProductContent();
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    ProductContent.Meta.TableName = "";
                }
                return _ProductContent;
            }
            set { _ProductContent = value; }
        }

        private String _ConentTxt;
        /// <summary></summary>
        public String ConentTxt
        {
            get
            {
                if (_ConentTxt == null && !Dirtys.ContainsKey("ConentTxt"))
                {
                    _ConentTxt = ProductContent.Content ?? "";
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

        private String _ProductGG;
        /// <summary>规格参数</summary>
        public String ProductGG
        {
            get
            {
                if (_ProductGG == null && !Dirtys.ContainsKey("ProductGG"))
                {
                    _ProductGG = ProductContent.Specification ?? "";
                    Dirtys["ProductGG"] = true;
                }
                return _ProductGG;
            }
            set { _ProductGG = value; }
        }

        private String _ProductTD;
        /// <summary>产品特点</summary>
        public String ProductTD
        {
            get
            {
                if (_ProductTD == null && !Dirtys.ContainsKey("ProductTD"))
                {
                    _ProductTD = ProductContent.Feature ?? "";
                    Dirtys.ContainsKey("ProductTD");
                }
                return _ProductTD;
            }
            set { _ProductTD = value; }
        }

        private String _ProductYY;
        /// <summary>推荐应用</summary>
        public String ProductYY
        {
            get
            {
                if (_ProductYY == null && !Dirtys.ContainsKey("ProductYY"))
                {
                    _ProductYY = ProductContent.App ?? "";
                    Dirtys["ProductYY"] = true;
                }
                return _ProductYY;
            }
            set { _ProductYY = value; }
        }

        private String _ProductPJ;
        /// <summary>相关配件</summary>
        public String ProductPJ
        {
            get
            {
                if (_ProductPJ == null && !Dirtys.ContainsKey("ProductPJ"))
                {
                    _ProductPJ = ProductContent.Fitting ?? "";
                    Dirtys["ProductPJ"] = true;
                }
                return _ProductPJ;
            }
            set { _ProductPJ = value; }
        }

        private String _ProductSP;
        /// <summary>产品视频</summary>
        public String ProductSP
        {
            get
            {
                if (_ProductSP == null && !Dirtys.ContainsKey("ProductSP"))
                {
                    _ProductSP = ProductContent.Video ?? "";
                    Dirtys["ProductSP"] = true;
                }
                return _ProductSP;
            }
            set { _ProductSP = value; }
        }
        #endregion

        #region 扩展查询﻿
        /// <summary>根据分类查找</summary>
        /// <param name="categoryid">分类</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EntityList<Product> FindAllByCategoryID(Int32 categoryid)
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
        public static EntityList<Product> Search(String key, Int32 CategoryID, String orderClause, Int32 startRowIndex, Int32 maximumRows)
        {
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
            return FindCount(SearchWhere(key, CategoryID), null, null, 0, 0);
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

        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="key"></param>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        private static String SearchWhere(String key, Int32 CategoryID)
        {
            // WhereExpression重载&和|运算符，作为And和Or的替代
            // SearchWhereByKeys系列方法用于构建针对字符串字段的模糊搜索
            var exp = SearchWhereByKeys(key, null);

            // 以下仅为演示，Field（继承自FieldItem）重载了==、!=、>、<、>=、<=等运算符（第4行）
            //if (userid > 0) exp &= _.OperatorID == userid;
            //if (isSign != null) exp &= _.IsSign == isSign.Value;
            //if (start > DateTime.MinValue) exp &= _.OccurTime >= start;
            //if (end > DateTime.MinValue) exp &= _.OccurTime < end.AddDays(1).Date;
            exp &= _.CategoryID == CategoryID;
            return exp;
        }
        #endregion

        #region 扩展操作
        #endregion

        #region 业务
        #endregion
    }
}
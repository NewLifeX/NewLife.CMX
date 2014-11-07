/*
 * XCoder v5.1.4992.36291
 * 作者：nnhy/X
 * 时间：2013-09-01 20:13:59
 * 版权：版权所有 (C) 新生命开发团队 2002~2013
*/
﻿using System;
using System.ComponentModel;
using XCode;

namespace NewLife.CMX
{
    /// <summary>产品</summary>
    public partial class Product : EntityTitle<Product, ProductCategory, ProductContent, ProductStatistics>
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

            // 在新插入数据或者修改了指定字段时进行唯一性验证，CheckExist内部抛出参数异常
            //if (isNew || Dirtys[__.Name]) CheckExist(__.Name);

            // 货币保留6位小数
            if (Dirtys[__.Price]) Price = Math.Round(Price, 6);
        }
        #endregion

        #region 扩展属性﻿
        //private String _ConentTxt;
        ///// <summary>产品内容</summary>
        //public String ConentTxt
        //{
        //    get
        //    {
        //        if (_ConentTxt == null && !Dirtys.ContainsKey("ConentTxt"))
        //        {
        //            _ConentTxt = Content.Content ?? "";
        //            //_ConentTxt = "";
        //            Dirtys["ConentTxt"] = true;
        //        }
        //        return _ConentTxt;
        //    }
        //    set
        //    {
        //        _ConentTxt = value;
        //    }
        //}

        private String _ProductGG;
        /// <summary>规格参数</summary>
        public String ProductGG
        {
            get
            {
                if (_ProductGG == null && !Dirtys.ContainsKey("ProductGG"))
                {
                    _ProductGG = Content.Specification ?? "";
                    Dirtys["ProductGG"] = true;
                }
                return _ProductGG;
            }
            set { _ProductGG = value; Content.Specification = value; }
        }

        private String _ProductTD;
        /// <summary>产品特点</summary>
        public String ProductTD
        {
            get
            {
                if (_ProductTD == null && !Dirtys.ContainsKey("ProductTD"))
                {
                    _ProductTD = Content.Feature ?? "";
                    Dirtys.ContainsKey("ProductTD");
                }
                return _ProductTD;
            }
            set { _ProductTD = value; Content.Feature = value; }
        }

        private String _ProductYY;
        /// <summary>推荐应用</summary>
        public String ProductYY
        {
            get
            {
                if (_ProductYY == null && !Dirtys.ContainsKey("ProductYY"))
                {
                    _ProductYY = Content.App ?? "";
                    Dirtys["ProductYY"] = true;
                }
                return _ProductYY;
            }
            set { _ProductYY = value; Content.App = value; }
        }

        private String _ProductPJ;
        /// <summary>相关配件</summary>
        public String ProductPJ
        {
            get
            {
                if (_ProductPJ == null && !Dirtys.ContainsKey("ProductPJ"))
                {
                    _ProductPJ = Content.Fitting ?? "";
                    Dirtys["ProductPJ"] = true;
                }
                return _ProductPJ;
            }
            set { _ProductPJ = value; Content.Fitting = value; }
        }

        private String _ProductSP;
        /// <summary>产品视频</summary>
        public String ProductSP
        {
            get
            {
                if (_ProductSP == null && !Dirtys.ContainsKey("ProductSP"))
                {
                    _ProductSP = Content.Video ?? "";
                    Dirtys["ProductSP"] = true;
                }
                return _ProductSP;
            }
            set { _ProductSP = value; Content.Video = value; }
        }
        #endregion

        #region 扩展查询﻿
        #endregion

        #region 高级查询
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
/*
 * XCoder v6.6.5879.6460
 * 作者：Stone/X
 * 时间：2016-02-06 19:24:23
 * 版权：版权所有 (C) 新生命开发团队 2002~2016
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using XCode;

namespace NewLife.CMX
{
    /// <summary>图册</summary>
    public partial class Photo : InfoExtend<Photo>, IInfoExtend
    {
        #region 对象操作
        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
			// 如果没有脏数据，则不需要进行任何处理
			if (!HasDirty) return;

            // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
            base.Valid(isNew);
        }
        #endregion

        #region 扩展属性
        #endregion

        #region 扩展查询
        #endregion

        #region 高级查询
        #endregion

        #region 扩展操作
        #endregion

        #region 业务
        #endregion
    }
}
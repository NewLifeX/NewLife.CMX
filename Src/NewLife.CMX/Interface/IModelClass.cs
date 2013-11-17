using System;
using System.Collections.Generic;
using System.Text;
using NewLife.Reflection;
using System.Linq;
using XCode;

namespace NewLife.CMX.Interface
{
    public interface IModelClass
    {
        /// <summary>类名</summary>
        String Name { get; }

        /// <summary>
        /// 获取模型类路径
        /// </summary>
        /// <returns></returns>
        String GetClassPath();

        /// <summary>分类模型类名称</summary>
        String ClassCategoryName { get; }

        /// <summary>模型分类路径</summary>
        String ClassCategoryPath { get; }

        /// <summary>
        /// 获取模型分类路径
        /// </summary>
        /// <returns></returns>
        String GetClassCategoryPath();
    }
}

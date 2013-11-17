using System;
using System.Collections.Generic;
using System.Text;
using NewLife.CMX.Interface;
using XCode;

namespace NewLife.CMX
{
    public abstract class ModelCategoryEntity<T> : EntityTree<T>,IModelCategory where T : ModelCategoryEntity<T>, new()
    {
        /// <summary>
        /// 查询子类以及子类的ID如果子类不是最终类，返回的时候ID会被改为负数
        /// </summary>
        /// <param name="parentKey"></param>
        /// <returns></returns>
        public static Dictionary<String, String> FindChildNameAndIDByNoParent(Int32 parentKey)
        {
            var entity = Meta.Factory.Default as T;

            var list = new EntityList<T>();

            list = FindAllChildsNoParent(parentKey);

            Dictionary<String, String> dic = new Dictionary<String, String>();

            foreach (T item in list)
            {
                if (Convert.ToBoolean(item["IsEnd"]))
                    dic.Add(item["ID"].ToString(), item.TreeNodeName);
                else
                    dic.Add("-" + item["ID"].ToString(), item.TreeNodeName);
            }

            return dic;
        }
    }
}

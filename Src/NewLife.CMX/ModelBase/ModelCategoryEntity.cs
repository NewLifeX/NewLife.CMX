using System;
using System.Collections.Generic;
using XCode;

namespace NewLife.CMX
{
    /// <summary>模型分类基类</summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ModelCategoryEntity<T> : EntityTree<T>/*, IModelCategory*/ where T : ModelCategoryEntity<T>, new()
    {
        //public abstract Boolean IsEnd { get; set; }

        /// <summary>查询所有子孙类以及子孙类的ID如果子类不是最终类，返回的时候ID会被改为负数</summary>
        /// <param name="parentKey"></param>
        /// <returns></returns>
        public static Dictionary<String, String> FindAllChildsNameAndIDByNoParent(Int32 parentKey)
        {
            var entity = Meta.Factory.Default as T;
            var list = FindAllChildsNoParent(parentKey);
            var dic = new Dictionary<String, String>();

            foreach (T item in list)
            {
                if (Convert.ToBoolean(item["IsEnd"]))
                    dic.Add(item["ID"].ToString(), item.TreeNodeName);
                else
                    dic.Add("-" + item["ID"].ToString(), item.TreeNodeName);
            }

            return dic;
        }

        /// <summary>查询子类以及子类的ID如果子类不是最终类，返回的时候ID会被改为负数</summary>
        /// <param name="parentKey"></param>
        /// <returns></returns>
        public static Dictionary<String, String> FindChildNameAndIDByNoParent(Int32 parentKey, Int32 deepth)
        {
            var entity = Meta.Factory.Default as T;
            var list = FindAllChildsNoParent(parentKey);
            var dic = new Dictionary<string, string>();

            foreach (T item in list)
            {
                if (item.Deepth > deepth) continue;

                if (Convert.ToBoolean(item["IsEnd"]))
                    dic.Add(item["ID"].ToString(), item.TreeNodeName);
                else
                    dic.Add("-" + item["ID"].ToString(), item.TreeNodeName);
            }
            return dic;
        }

        /// <summary>查询所有不是终节点的节点</summary>
        /// <param name="parentkey"></param>
        /// <returns></returns>
        public static EntityList<T> FindAllByNoEnd(Int32 parentkey)
        {
            var entitylist = FindAllChildsByParent(parentkey);
            entitylist.RemoveAll(e => (Boolean)e["IsEnd"]);
            return entitylist;
        }
    }
}
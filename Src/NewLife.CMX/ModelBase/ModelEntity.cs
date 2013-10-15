using System;
using System.Collections.Generic;
using System.Text;
using NewLife.Reflection;
using XCode;
using NewLife.Linq;

namespace NewLife.CMX.ModelBase
{
    public abstract class ModelEntityBase<T> : Entity<T>, IModelClass where T : ModelEntityBase<T>, new()
    {
        #region 构造方法
        /// <summary></summary>
        public ModelEntityBase()
        {
            Name = this.GetType().Name;
        }
        #endregion

        #region 属性
        private String _Name;
        /// <summary>名称</summary>
        public String Name { get { return _Name; } set { _Name = value; } }

        private String _ClassName;
        /// <summary>模型类名</summary>
        public String ClassName { get { return _ClassName; } set { _ClassName = value; } }

        private String _ClassPath;
        /// <summary>模型类路径</summary>
        public String ClassPath { get { return _ClassPath; } set { _ClassPath = value; } }

        private String _ClassCategoryName;
        /// <summary>模型分类名</summary>
        public String ClassCategoryName { get { return _ClassCategoryName; } set { _ClassCategoryName = value; } }

        private String _ClassCategoryPath;
        /// <summary>模型分类路径</summary>
        public String ClassCategoryPath { get { return _ClassCategoryPath; } set { _ClassCategoryPath = value; } }
        #endregion

        #region 接口方法
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string GetClassPath()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string GetClassCategoryPath()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 静态获取所有模型类
        /// <summary>
        /// 获取所有模型类
        /// </summary>
        /// <returns></returns>
        public static List<IModel> GetAllModel()
        {
            return AssemblyX.FindAllPlugins(typeof(IModel), true).Select(t => TypeX.CreateInstance(t) as IModel).ToList();
        }
        #endregion
    }
}

using System;
using System.ComponentModel;
using XCode;
using XCode.DataAccessLayer;
using NewLife.Log;

namespace NewLife.CMX.CMS
{
    [BindTable("SysModel", Description = "系统模型", ConnName = "CMX", DbType = DatabaseType.SqlServer)]
    public class SysModel : BaseInfo<SysModel>
    {
        #region 属性
        private Int32 _Sort;
        /// <summary>排序</summary>
        [DisplayName("排序")]
        [Description("排序")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(1, "Sort", "排序", null, "int", 10, 0, false)]
        public Int32 Sort
        {
            get { return _Sort; }
            set { if (OnPropertyChanging("Sort", value)) { _Sort = value; OnPropertyChanged("Sort"); } }
        }

        private String _ListUrl;
        /// <summary>列表页面</summary>
        [DisplayName("列表页面")]
        [Description("列表页面")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn(3, "ListUrl", "列表页面", null, "nvarchar(250)", 0, 0, true)]
        public virtual String ListUrl
        {
            get { return _ListUrl; }
            set { if (OnPropertyChanging("ListUrl", value)) { _ListUrl = value; OnPropertyChanged("ListUrl"); } }
        }

        private String _ViewUrl;
        /// <summary>编辑页面</summary>
        [DisplayName("编辑页面")]
        [Description("编辑页面")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn(3, "ViewUrl", "编辑页面", null, "nvarchar(250)", 0, 0, true)]
        public virtual String ViewUrl
        {
            get { return _ViewUrl; }
            set { if (OnPropertyChanging("ViewUrl", value)) { _ViewUrl = value; OnPropertyChanged("ViewUrl"); } }
        }

        private Boolean _IsSys;
        /// <summary>系统模型</summary>
        [Description("系统模型")]
        [DataObjectField(false, false, true, 1)]
        [BindColumn(10, "IsSys", "系统模型", "", "bit", 0, 0, false)]
        public Boolean IsSys
        {
            get { return _IsSys; }
            set { if (OnPropertyChanging("IsSys", value)) { _IsSys = value; OnPropertyChanged("IsSys"); } }
        }
        #endregion

        #region 初始化
        protected override void InitData()
        {
            base.InitData();

            if (Meta.Count > 0) return;

            Meta.BeginTrans();
            try
            {
                Add("文章模型", "ArticleList.aspx", "ArticleForm.aspx", true);
                Add("商品模型", "CommodityList.aspx", "CommodityForm.aspx", true);
                Add("下载模型", "DownList.aspx", "DownForm.aspx", true);
                Add("单页模型", "ContentForm.aspx", "ContentForm.aspx", true);

                Meta.Commit();
            }
            catch (Exception ex)
            {
                XTrace.WriteException(ex);
                Meta.Rollback();
            }
        }

        protected override int OnDelete()
        {
            if (IsSys) throw new Exception("系统模型不能删除！");

            return base.OnDelete();
        }
        #endregion

        #region 业务
        /// <summary>增加系统模型</summary>
        /// <param name="name">模型名字</param>
        /// <param name="_ListUrl">列表页</param>
        /// <param name="_ViewUrl">详细页</param>
        /// <param name="_IsSys">是否系统模型</param>
        /// <returns></returns>
        public static SysModel Add(String name, String _ListUrl, String _ViewUrl, Boolean _IsSys)
        {
            var entity = SysModel.FindByName(name);
            if (entity != null) return entity;

            entity = new SysModel();
            entity.Name = name;
            entity.ListUrl = _ListUrl;
            entity.ViewUrl = _ViewUrl;
            entity.IsSys = _IsSys;
            entity.Save();

            return entity;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using XTemplate.Templating;
using NewLife.CMX.Templates;
using XCode;

namespace NewLife.CMX
{
    /// <summary>列表模版基类。模版生成类继承于此类</summary>
    public class TextListPage : PageBase<Text>
    {
        #region 属性
        private List<Text> _Entities;
        /// <summary>实体对象集合</summary>
        public List<Text> Entities { get { return _Entities; } set { _Entities = value; } }

        private Text[] _EntitiesArray;
        /// <summary>实际数组</summary>
        public Text[] EntitiesArray { get { return _EntitiesArray; } set { _EntitiesArray = value; } }

        private TextCategory _Category;
        /// <summary>分类</summary>
        public TextCategory Category
        {
            get
            {
                if (_Category == null && CategoryID > 0) _Category = TextCategory.FindByID(CategoryID);

                return _Category;
            }
            set { _Category = value; }
        }

        private Int32 _CategoryID;
        /// <summary>分类编号</summary>
        public Int32 CategoryID { get { return _CategoryID; } set { _CategoryID = value; } }

        private Int32 _CurrentChannelID;
        /// <summary>当前频道编号</summary>
        public Int32 CurrentChannelID { get { return _CurrentChannelID; } set { _CurrentChannelID = value; } }

        private String _ChannelName;
        /// <summary>当前频道名称</summary>
        public String ChannelName { get { return _ChannelName; } set { _ChannelName = value; } }

        private String _ChannelSuffix;
        /// <summary>当前频道扩展名称</summary>
        public String ChannelSuffix { get { return _ChannelSuffix; } set { _ChannelSuffix = value; } }

        private Int32 _PageCount = 10;
        /// <summary>页面记录数</summary>
        public Int32 PageCount { get { return _PageCount; } set { _PageCount = value; } }

        private Int32 _CurrentPageNo = 1;
        /// <summary>当前页码</summary>
        public Int32 CurrentPageNo { get { return _CurrentPageNo; } set { _CurrentPageNo = value; } }

        private String _BeforePage;
        /// <summary>前页</summary>
        public String BeforePage { get { return _BeforePage; } set { _BeforePage = value; } }

        private String _NextPage;
        /// <summary>后页</summary>
        public String NextPage { get { return _NextPage; } set { _NextPage = value; } }

        private String _Order = "desc";
        /// <summary>排序</summary>
        public String Order { get { return _Order; } set { _Order = value; } }

        private Boolean _IsFillArray = false;
        /// <summary>是否填充数组(有些特殊的情况需要把结果集合转换成数组，
        /// 但是实际数据量不满足要求，需要填充空白数据)</summary>
        public Boolean IsFillArray { get { return _IsFillArray; } set { _IsFillArray = value; } }
        #endregion

        #region 初始化
        /// <summary>初始化</summary>
        public TextListPage()
        {
            //初始化
            Provider.CurrentChannel = 0;
        }

        /// <summary>初始化并设置当前频道</summary>
        public void init()
        {
            //初始化
            Provider.CurrentChannel = 0;

            var c = Channel.FindBySuffixOrID(ChannelSuffix, CurrentChannelID);

            if (c != null) Provider.CurrentChannel = c.ID;

            DataBind();
        }

        /// <summary>数据绑定</summary>
        void DataBind()
        {
            //加载全部数据
            FindByParams();

            GetEntitiesArrayByPageCount();
        }
        #endregion

        #region 业务方法
        /// <summary>
        /// 获取指定数量的记录,
        /// </summary>
        /// <returns>
        /// 返回集合数组，如果实际的数据量小于PageCount，自动补全需要的数量，PageCount==0返回null
        /// </returns>
        public void GetEntitiesArrayByPageCount()
        {
            if (PageCount == 0) return;

            if (IsFillArray)
            {
                if (Entities.Count < PageCount)
                {
                    for (int i = 0; i < PageCount - Entities.Count; i++)
                    {
                        Entities.Add(new Text());
                    }
                }
                else
                    Entities.RemoveAt(PageCount);
            }

            EntitiesArray = Entities.ToArray();
        }

        /// <summary>
        /// 加载全部数据
        /// </summary>
        /// <returns></returns>
        public void FindByParams()
        {
            //Entities = Article.FindAllWithCache();
            Entities = Text.FindAll(new WhereExpression(), " ID " + Order, null, (CurrentPageNo - 1) * PageCount, PageCount);

            if (CategoryID > 0) Entities = Entities.FindAll(e => e.CategoryID == CategoryID);
        }
        #endregion
    }

    /// <summary>模版基类。模版生成类继承于此类</summary>
    public class TextPage : PageBase<Text>
    {
        #region 属性
        private Text _Entity;
        /// <summary>实体对象</summary>
        public Text Entity { get { return _Entity; } set { _Entity = value; } }

        private Int32 _EntityID;
        /// <summary>实体编号</summary>
        public Int32 EntityID { get { return _EntityID; } set { _EntityID = value; } }

        private Int32 _CurrentChannelID;
        /// <summary>当前频道编号</summary>
        public Int32 CurrentChannelID { get { return _CurrentChannelID; } set { _CurrentChannelID = value; } }

        private String _ChannelName;
        /// <summary>频道名称</summary>
        public String ChannelName { get { return _ChannelName; } set { _ChannelName = value; } }

        private String _ChannelSuffix;
        /// <summary>频道扩展名</summary>
        public String ChannelSuffix { get { return _ChannelSuffix; } set { _ChannelSuffix = value; } }
        #endregion

        #region 页面初始化
        /// <summary>构造方法</summary>
        public TextPage()
        {
            Provider.CurrentChannel = 0;
        }

        /// <summary>初始化</summary>
        public void Init()
        {
            var c = Channel.FindBySuffixOrID(ChannelSuffix, CurrentChannelID);

            if (c != null) Provider.CurrentChannel = c.ID;

            DataBind();
        }

        /// <summary>数据绑定</summary>
        void DataBind()
        {
            if (EntityID > 0) Entity = Text.FindByID(EntityID);
        }
        #endregion
    }
}
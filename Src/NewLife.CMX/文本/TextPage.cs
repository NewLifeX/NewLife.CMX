using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using XTemplate.Templating;
using NewLife.CMX.Templates;

namespace NewLife.CMX
{
    /// <summary>列表模版基类。模版生成类继承于此类</summary>
    public class TextListPage : PageBase
    {
    }

    /// <summary>模版基类。模版生成类继承于此类</summary>
    public class TextPage : PageBase
    {
        #region 属性
        private Text _Entity;
        /// <summary>实体对象</summary>
        public Text Entity { get { return _Entity; } set { _Entity = value; } }
        #endregion
    }
}
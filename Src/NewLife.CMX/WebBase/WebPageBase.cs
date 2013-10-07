using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using XUrlRewrite.Helper;

namespace NewLife.CMX.WebBase
{
    public class WebPageBase : Page
    {
        protected override void InitializeCulture()
        {
            base.InitializeCulture();

            Page.Load += delegate(object sender, EventArgs e)
            {
                if (RewriteHelper != null && Page.Form != null)
                    Page.Form.Action = String.IsNullOrEmpty(RewriteHelper.FormAction) ? Page.Request.Path : RewriteHelper.FormAction;
            };
        }

        /// <summary>
        /// 重写地址辅助工具,用于得到真实的地址信息,以及将相对于模板目录的路径转换为Web访问可用的Url
        /// </summary>
        private RewriteHelper _RewriteHelper;
        /// <summary>
        /// 重写地址辅助工具,用于得到真实的地址信息,以及将相对于模板目录的路径转换为Web访问可用的Url
        /// </summary>
        protected RewriteHelper RewriteHelper
        {
            get
            {
                if (_RewriteHelper == null)
                {
                    _RewriteHelper = RewriteHelper.Current;
                }
                return _RewriteHelper;
            }
        }
    }
}

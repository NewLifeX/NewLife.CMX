using System;
using System.Collections.Generic;
using System.Text;
using NewLife.CMX.Config;
using NewLife.CMX.TemplateEngine;

namespace NewLife.CMX.Web.ModeProcess
{
    public class LeftMenuContent
    {


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public String GetContent()
        {
            CMXEngine engine = new CMXEngine(TemplateConfig.Current);
            String content = engine.Render(TemplateConfig.Current.LeftAddress);

            return content;
        }
    }
}

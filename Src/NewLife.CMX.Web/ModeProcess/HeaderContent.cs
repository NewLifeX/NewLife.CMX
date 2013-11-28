using System;
using System.Collections.Generic;
using System.Text;
using NewLife.CMX.Config;
using NewLife.CMX.TemplateEngine;

namespace NewLife.CMX.Web
{
    public class HeaderContent
    {
        public static String GetContent()
        {
            CMXEngine engine = new CMXEngine(TemplateConfig.Current);
            String content = engine.Render( "Header.html");

            return content;
        }
    }
}

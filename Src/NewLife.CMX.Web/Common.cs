using System;
using System.Collections.Generic;
using System.Text;
using NewLife.CMX.Config;
using NewLife.CMX.TemplateEngine;

namespace NewLife.CMX.Web
{
    public class Common : ICommon
    {
        private String _Address;
        /// <summary></summary>
        public String Address { get { return _Address; } set { _Address = value; } }

        public string Process()
        {
            try
            {
                CMXEngine engine = new CMXEngine(TemplateConfig.Current);
                String content = engine.Render(Address + ".html");

                return content;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.CMX.Config;
using NewLife.CMX.TemplateEngine;
using NewLife.CommonEntity;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void model_Click(object sender, EventArgs e)
    {
        CMXEngine engine = new CMXEngine(TemplateConfig.Current);

        //engine.RenderAll();
    }
}
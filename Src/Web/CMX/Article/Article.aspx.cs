using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.CMX;
using NewLife.Web;

public partial class CMX_Article : MyModelEntityList<Article>
{
    //String channelsuffix;
    //Int32 channelid;

    //protected override void OnInit(EventArgs e)
    //{
    //    channelsuffix = Request["Channel"];
    //    Channel c = Channel.FindBySuffix(channelsuffix);

    //    if(c==null) 

    //    Article.Meta.TableName = "";
    //    Article.Meta.TableName += c.Suffix;

    //    base.OnInit(e);
    //}

    //protected override void OnPreRenderComplete(EventArgs e)
    //{
    //    base.OnPreRenderComplete(e);

    //    Article.Meta.TableName = "";
    //}

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ods_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        Int32 cid = 0;

        Int32.TryParse(Request["CategoryID"], out cid);

        e.InputParameters["CategoryID"] = cid;
    }
}
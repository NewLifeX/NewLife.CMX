using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.CMX;
using NewLife.Web;

public partial class CMX_Article : MyModelEntityList<Article>
{
    String channelsuffix;
    Int32 channelid;

    protected override void OnInit(EventArgs e)
    {
        Channel c;

        channelid = WebHelper.RequestInt("Channel");

        if (channelid == 0)
        {
            channelsuffix = Request["Channel"];
            c = Channel.FindBySuffix(channelsuffix);
        }
        else
        {
            c = Channel.FindByID(channelid);
        }

        Article.Meta.TableName += c.Suffix;

        base.OnInit(e);
    }

    protected override void OnPreRenderComplete(EventArgs e)
    {
        base.OnPreRenderComplete(e);

        Article.Meta.TableName = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
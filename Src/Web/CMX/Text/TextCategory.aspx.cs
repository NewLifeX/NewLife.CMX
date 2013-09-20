using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.CMX;
using NewLife.Web;

public partial class CMX_TextCategory : MyModelEntityList<TextCategory>
{

    private String _suffix;

    public String Suffix
    {
        get
        {
            if (_suffix == null)
                _suffix = Request["Channel"];
            return _suffix;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        Channel c = Channel.FindBySuffix(Suffix);

        if (c == null)
        {
            //存在BUG当无法获取频道信息的时候会默认查询基础表
            WebHelper.Alert("未知频道！");
        }

        ArticleCategory.Meta.TableName += c.Suffix;

        base.OnInit(e);
    }

    protected override void OnSaveStateComplete(EventArgs e)
    {

        base.OnSaveStateComplete(e);
        ArticleCategory.Meta.TableName = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }
}
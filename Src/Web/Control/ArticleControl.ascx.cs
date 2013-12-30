using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.CMX;
using NewLife.Log;
using XCode;

public partial class Control_ArticleControl : System.Web.UI.UserControl
{
    private String _ChannelSuffix;
    /// <summary></summary>
    public String ChannelSuffix
    {
        get { return _ChannelSuffix; }
        set { _ChannelSuffix = value; }
    }

    private Int32 _Count;
    /// <summary></summary>
    public Int32 Count
    {
        get { return _Count; }
        set { _Count = value; }
    }

    private String _CategoryName;
    /// <summary></summary>
    public String CategoryName
    {
        get { return _CategoryName; }
        set { _CategoryName = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Channel chn = Channel.FindBySuffix(ChannelSuffix);
        Type type = chn.Model.Provider.CategoryType;
        IEntityOperate eop = EntityFactory.CreateOperate(type);
        try
        {
            eop.TableName += ChannelSuffix;
            Content.DataSource = eop.FindAll("CategoryName='" + CategoryName + "'", null, null, 0, Count);
            Content.DataBind();
        }
        catch (Exception ex)
        {
            XTrace.WriteException(ex);
        }
        finally
        {
            eop.TableName = "";
        }
    }
}
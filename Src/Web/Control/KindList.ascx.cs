using System;
using System.Reflection;
using NewLife.CMX;
using XCode;

public partial class Control_KindList : System.Web.UI.UserControl
{
    private String _ChannelSuffix;
    /// <summary></summary>
    public String ChannelSuffix
    {
        get { return _ChannelSuffix; }
        set { _ChannelSuffix = value; }
    }

    private Int32 _CategoryID;
    /// <summary></summary>
    public Int32 CategoryID
    {
        get { return _CategoryID; }
        set { _CategoryID = value; }
    }

    private Object _Scoure;
    /// <summary></summary>
    public Object Scoure
    {
        get { return _Scoure; }
        set { _Scoure = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Scoure != null)
        //{
        //    kindrepeater.DataSource = Scoure;
        //    kindrepeater.DataBind();
        //}
        //else
        //{
        Channel chn = Channel.FindBySuffix(ChannelSuffix);

        IEntityOperate eop = EntityFactory.CreateOperate(chn.Model.Provider.TitleType);
        eop.TableName += ChannelSuffix;

        Object CategoryDic = eop.EntityType.BaseType.BaseType.BaseType.InvokeMember("FindAllChildsByParent", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, null, new Object[] { CategoryID });

        kindrepeater.DataSource = CategoryDic;
        kindrepeater.DataBind();
        //}
    }
}
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
        Channel c = Channel.FindBySuffix(ChannelSuffix);

        IEntityOperate ieo = EntityFactory.CreateOperate(c.Model.ClassName);
        ieo.TableName += ChannelSuffix;
        Type t = ieo.Create(false).GetType();

        Object CategoryDic = t.BaseType.BaseType.BaseType.InvokeMember("FindAllChildsByParent", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, null, new Object[] { CategoryID });

        kindrepeater.DataSource = CategoryDic;
        kindrepeater.DataBind();
        //}
    }
}
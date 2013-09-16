using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.Log;
using NewLife.Web;
using NewLife.CMX;
using XCode;

public partial class CMX_ChannelForm : MyEntityForm<Channel>
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        EntityList<Model> entitylist = Model.FindAll(Model._.Enable, true);
        if (entitylist == null || entitylist.Count <= 0)
        {
            frmModelID.Items.Add(new ListItem("请先添加模板", "0"));
        }

        frmModelID.DataSource = entitylist;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ManagerPage.SetFormScript(true);

        EntityForm.OnValid += EntityForm_OnValid;
    }

    void EntityForm_OnValid(object sender, NewLife.CommonEntity.EntityFormEventArgs e)
    {
        if (Entity.ModelID == 0)
        {
            e.Cancel = true;
            WebHelper.Alert("请选择模型！");
        }
    }
}
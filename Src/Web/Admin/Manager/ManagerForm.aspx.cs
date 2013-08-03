using NewLife.CommonEntity;
using NewLife.Security;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Manager_ManagerForm : MyEntityForm
{
    /// <summary>实体类型</summary>
    public override Type EntityType { get { return CommonManageProvider.Provider.AdminstratorType; } set { base.EntityType = value; } }
 
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        EntityForm.OnSetForm += EntityForm_OnSetForm;
        EntityForm.OnGetForm += EntityForm_OnGetForm;
    }

    void EntityForm_OnSetForm(object sender, EntityFormEventArgs e)
    {
        frmPassword.Text = null;
    }

    void EntityForm_OnGetForm(object sender, EntityFormEventArgs e)
    {
        if (!String.IsNullOrEmpty(frmPassword.Text) && frmPassword.Text== frmPasswordRe.Text) EntityForm.Entity.SetItem("Password", DataHelper.Hash(frmPassword.Text));
    }

    protected override void OnInitComplete(EventArgs e)
    {
        base.OnInitComplete(e);
        ods.DataObjectTypeName = ods.TypeName = CommonManageProvider.Provider.RoleType.FullName;
    }
}
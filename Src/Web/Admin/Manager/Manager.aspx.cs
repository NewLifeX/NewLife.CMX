using NewLife.CommonEntity;
using NewLife.Reflection;
using NewLife.Web;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XCode;

public partial class Admin_Manager_Manager : MyEntityList
{
    /// <summary>实体类型</summary>
    public override Type EntityType { get { return CommonManageProvider.Provider.AdminstratorType; } set { base.EntityType = value; } }

    IEntityOperate RoleFactory { get { return EntityFactory.CreateOperate(CommonManageProvider.Provider.RoleType); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        Type type = CommonManageProvider.Provider.RoleType;
        odsRole.TypeName = type.FullName;
        odsRole.DataObjectTypeName = type.FullName;
    }

    protected void btnEnable_Click(object sender, EventArgs e) { EnableOrDisable(true); }

    protected void btnDisable_Click(object sender, EventArgs e) { EnableOrDisable(false); }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DoBatch("删除", delegate(IAdministrator admin)
        {
            if (admin.Name == "admin") return false;

            (admin as IEntity).Delete();

            return false;
        });
    }

    void EnableOrDisable(Boolean isenable)
    {
        DoBatch(isenable ? "启用" : "禁用", delegate(IAdministrator admin)
        {
            if (admin.IsEnable != isenable)
            {
                admin.IsEnable = isenable;
                return true;
            }
            return false;
        });
    }
    void DoBatch(String action, Func<IAdministrator, Boolean> callback)
    {
        Int32[] vs = gvExt.SelectedIntValues;
        if (vs == null || vs.Length < 1) return;
        Int32 n = 0;
        IEntityOperate eop = EntityFactory.CreateOperate(EntityType);
        eop.BeginTransaction();
        try
        {
            foreach (Int32 item in vs)
            {
                IEntity entity = eop.FindByKey(item);
                IAdministrator admin = entity as IAdministrator;
                if (admin != null && callback(admin))
                {
                    entity.Save();
                    n++;
                }
            }

            eop.Commit();

            WebHelper.Alert("成功" + action + n + "个管理员！");
        }
        catch (Exception ex)
        {
            eop.Rollback();

            WebHelper.Alert("操作失败！" + ex.Message);
        }
        if (n > 0) gv.DataBind();
    }


}
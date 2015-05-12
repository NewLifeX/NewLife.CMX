using System;
using NewLife.Web;
using XCode;
using XCode.Membership;

public partial class Admin_Manager_Manager : MyEntityList
{
    /// <summary>实体类型</summary>
    public override Type EntityType { get { return ManageProvider.Provider.UserType; } set { base.EntityType = value; } }

    IEntityOperate RoleFactory { get { return EntityFactory.CreateOperate(ManageProvider.Provider.GetService<IRole>().GetType()); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        Type type = ManageProvider.Provider.GetService<IRole>().GetType();
        odsRole.TypeName = type.FullName;
        odsRole.DataObjectTypeName = type.FullName;
    }

    protected void btnEnable_Click(object sender, EventArgs e) { EnableOrDisable(true); }

    protected void btnDisable_Click(object sender, EventArgs e) { EnableOrDisable(false); }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DoBatch("删除", delegate(IUser admin)
        {
            if (admin.Name == "admin") return false;

            (admin as IEntity).Delete();

            return false;
        });
    }

    void EnableOrDisable(Boolean isenable)
    {
        DoBatch(isenable ? "启用" : "禁用", delegate(IUser admin)
        {
            if (admin.Enable != isenable)
            {
                admin.Enable = isenable;
                return true;
            }
            return false;
        });
    }
    void DoBatch(String action, Func<IUser, Boolean> callback)
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
                IUser admin = entity as IUser;
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
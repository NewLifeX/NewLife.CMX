﻿using System;
using System.Web.UI;
using NewLife.CMX;
using NewLife.CommonEntity;
using NewLife.Web;
using XCode;
using XCode.Membership;

/// <summary>实体表单页面基类</summary>
public class MyModelEntityForm : Page
{
    #region 管理页控制器
    private Type _EntityType;
    /// <summary>实体类</summary>
    public virtual Type EntityType { get { return _EntityType; } set { _EntityType = value; } }

    /// <summary>管理页控制器</summary>
    protected IManagePage Manager;

    /// <summary>表单控制器</summary>
    protected IEntityForm EntityForm;

    /// <summary>管理员</summary>
    protected IUser CurrentManage
    {
        get
        {
            return ManageProvider.Provider.Current as IUser;
        }
    }

    protected override void OnPreInit(EventArgs e)
    {
        //if (CurrentManage == null || CurrentManage.RoleID == 2)
        //{//前台用户直接跳出
        //    Response.Redirect("/Manager/Login.aspx");
        //}

        // 让页面管理器先注册，因为页面管理器要控制权限
        Manager = ManageProvider.Provider.GetService<IManagePage>().Init(this, EntityType);
        Manager.ValidatePermission = false;//关闭页面权限检查
        EntityForm = ManageProvider.Provider.GetService<IEntityForm>().Init(this, EntityType);
        EntityForm.OnSaveSuccess += EntityForm_OnSaveSuccess;
        EntityForm.OnSaveFailure += EntityForm_OnSaveFailure;
        base.OnPreInit(e);
    }
    void EntityForm_OnSaveFailure(object sender, EntityFormEventArgs e)
    {
        e.Cancel = true;
        WebHelper.Alert(e.Error.Message);

    }
    void EntityForm_OnSaveSuccess(object sender, EntityFormEventArgs e)
    {
        e.Cancel = true;
        //由于原先框架中的API属性不存在
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", @"alert('成功');
        //var api = frameElement.api;api.reload();", true);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", @"alert('成功');$(frameElement).attr('src',$(frameElement).attr('src'));", true);

    }
    public void CloseWindows()
    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", @"alert('成功！');
        //var api = frameElement.api;api.reload();", true);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", @"alert('成功!');$(frameElement).attr('src',$(frameElement).attr('src'));", true);
    }
    #endregion
}

/// <summary>实体表单页面基类</summary>
public class MyModelEntityForm<TEntity> : MyModelEntityForm where TEntity : Entity<TEntity>, new()
{
    /// <summary>实体类</summary>
    public override Type EntityType { get { return base.EntityType ?? (base.EntityType = typeof(TEntity)); } set { base.EntityType = value; } }

    /// <summary>实体</summary>
    public virtual TEntity Entity { get { return EntityForm == null ? null : EntityForm.Entity as TEntity; } set { if (EntityForm != null) EntityForm.Entity = value; } }

    protected override void OnInit(EventArgs e)
    {
        IModelProvider provider = ModelProvider.Get<TEntity>();
        //初始化
        provider.CurrentChannel = 0;
        try
        {
            Channel chn = Channel.FindByID(WebHelper.RequestInt("ChannelID"));

            if (chn == null) throw new Exception("未知频道");
            if (provider != null) provider.CurrentChannel = chn.ID;

            base.OnInit(e);
        }
        catch (Exception)
        {
            provider.CurrentChannel = 0;
            throw;
        }
    }

    //protected override void OnSaveStateComplete(EventArgs e)
    //{
    //    base.OnSaveStateComplete(e);
    //    //EntityFactory.CreateOperate(EntityType).TableName = "";
    //    Model.FindProvider(EntityType).CurrentChannel = 0;
    //}



    //protected override void OnUnload(EventArgs e)
    //{
    //    //EntityFactory.CreateOperate(EntityType).TableName = "";
    //    Model.FindProvider(EntityType).CurrentChannel = 0;
    //    base.OnUnload(e);
    //}
}
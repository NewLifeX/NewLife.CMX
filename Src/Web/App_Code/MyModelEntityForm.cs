﻿using System;
using System.Web.UI;
using NewLife.CommonEntity;
using XCode;
using NewLife.Web;
using NewLife.CMX;
using NewLife.Reflection;

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
    protected IAdministrator CurrentManage
    {
        get
        {
            return ManageProvider.Provider.Current as IAdministrator;
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
        try
        {
            //Channel c = Channel.FindBySuffix(Request["Channel"]);
            Channel c = Channel.FindBySuffixOrModel(Request["Channel"], WebHelper.RequestInt("ModelID"));

            if (c == null) throw new Exception("未知频道");
            //在某些不知名的情况下会出现表名没有被恢复的情况，所以添加预先重置连接名
            EntityFactory.CreateOperate(EntityType).TableName = "";
            EntityFactory.CreateOperate(EntityType).TableName += c.Suffix;

            if (EntityType.BaseType.GetGenericTypeDefinition() != typeof(EntityTree<>) && EntityType.BaseType.GetGenericTypeDefinition() != typeof(EntityCategory<>))
            {
                FieldInfoX mix = FieldInfoX.Create(EntityType, "ChannelSuffix");

                mix.SetValue(c.Suffix);
            }

            base.OnInit(e);
        }
        //finally
        //{
        //    EntityFactory.CreateOperate(EntityType).TableName = "";
        //}
        catch (Exception)
        {
            EntityFactory.CreateOperate(EntityType).TableName = "";
            throw;
        }
    }

    //protected override void OnSaveStateComplete(EventArgs e)
    //{
    //    base.OnSaveStateComplete(e);
    //    EntityFactory.CreateOperate(EntityType).TableName = "";
    //}

    //protected override void OnUnload(EventArgs e)
    //{
    //    EntityFactory.CreateOperate(EntityType).TableName = "";
    //    base.OnUnload(e);
    //}
}
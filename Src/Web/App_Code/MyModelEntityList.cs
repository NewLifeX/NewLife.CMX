﻿﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using NewLife.CMX;
using NewLife.CommonEntity;
using NewLife.Web;
using XCode;
using XControl;

/// <summary>实体列表页面基类</summary>
public abstract class MyModelEntityList : Page
{
    #region 管理页控制器
    private Type _EntityType;
    /// <summary>实体类</summary>
    public virtual Type EntityType { get { return _EntityType; } set { _EntityType = value; } }

    /// <summary>管理页控制器</summary>
    protected IManagePage Manager;

    /// <summary>管理员</summary>
    protected IAdministrator CurrentManage { get { return ManageProvider.Provider.Current as IAdministrator; } }

    // 如果不写日志，就不要拦截异常，否则压根就不知道哪里出错！
    //protected override void OnInit(EventArgs e)
    //{
    //    if (System.Web.HttpContext.Current != null)
    //    {
    //        base.OnInit(e);
    //        this.Error += new System.EventHandler(this.Page_Error);
    //    }
    //}
    //protected virtual void Page_Error(object sender, System.EventArgs e)
    //{
    //    // Exception ex = Server.GetLastError();
    //    WebHelper.AlertAndEnd("系统错误！");
    //    Server.ClearError();
    //}

    protected override void OnPreInit(EventArgs e)
    {
        //if (CurrentManage == null || CurrentManage.RoleID == 2)
        //{//前台用户直接跳出
        //    Response.Redirect("/Manager/Login.aspx");
        //}
        Manager = ManageProvider.Provider.GetService<IManagePage>().Init(this, EntityType);
        Manager.ValidatePermission = false;//关闭页面权限检查
        base.OnPreInit(e);
    }
    #endregion

    protected override void OnPreLoad(EventArgs e)
    {
        base.OnPreLoad(e);

        AutoAddParamForAdd();
    }

    /// <summary>自动为添加按钮附加参数，Request中，只要是当前实体成员的参数，否附加上去</summary>
    void AutoAddParamForAdd()
    {
        if (Request.QueryString == null || Request.QueryString.Count < 1) return;
        if (EntityType == null) return;

        LinkBox lb = ControlHelper.FindControlByField<LinkBox>(Page, "lbAdd");
        if (lb == null) return;

        StringBuilder sb = new StringBuilder();
        IEntityOperate op = EntityFactory.CreateOperate(EntityType);
        HashSet<String> hs = new HashSet<string>(op.FieldNames, StringComparer.OrdinalIgnoreCase);
        foreach (String item in Request.QueryString.Keys)
        {
            // 仅接受实体类成员
            if (!hs.Contains(item)) continue;

            if (sb.Length > 0) sb.Append("&");
            sb.AppendFormat("{0}={1}", item, Request.QueryString[item]);
        }
        if (sb.Length > 0)
        {
            if (!lb.Url.Contains("?"))
                lb.Url += "?" + sb;
            else
                lb.Url += "&" + sb;
        }
    }
}

/// <summary>实体列表页面基类</summary>
public class MyModelEntityList<TEntity> : MyModelEntityList where TEntity : Entity<TEntity>, new()
{
    /// <summary>实体类</summary>
    public override Type EntityType { get { return base.EntityType ?? (base.EntityType = typeof(TEntity)); } set { base.EntityType = value; } }

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
    //    Model.FindProvider(EntityType).CurrentChannel = 0;
    //}

    //protected override void OnUnload(EventArgs e)
    //{
    //    Model.FindProvider(EntityType).CurrentChannel = 0;
    //    base.OnUnload(e);
    //}
}

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.Log;
using NewLife.Web;
using NewLife.CMX;

public partial class CMX_TextCategoryForm : MyModelEntityForm<TextCategory>
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        //只有新添加数据才可以设置是否最终分类
        //任意一个分类的子分类不允许 既有最终分类 又有不是最终分类的情况
        if (!EntityForm.IsNew || Entity.Childs.Count > 0) frmIsEnd.Enabled = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ManagerPage.SetFormScript(true);
    }
}
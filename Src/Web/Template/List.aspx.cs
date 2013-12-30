using System;
using NewLife.CMX;
using NewLife.CMX.Config;
using NewLife.CMX.WebBase;
using NewLife.Reflection;
using NewLife.Web;

public partial class Template_List : WebPageBase
{
    /// <summary>ID</summary>
    private Int32 CategoryID
    {
        get { return WebHelper.RequestInt("CategoryID"); }
    }

    /// <summary>类编码</summary>
    private String Suffix
    {
        get { return Request["Suffix"]; }
    }

    /// <summary>请求地址</summary>
    private String Address
    {
        get
        {
            String ad = Request["Address"];

            if (string.IsNullOrEmpty(ad))
            {
                ad = C.FormTemplate;
                ad = ad.Substring(0, ad.IndexOf('.'));
            }
            return ad;
        }
    }

    /// <summary>分页索引</summary>
    private Int32 PageIndex
    {
        get
        {
            Int32 i = WebHelper.RequestInt("Pageindex");
            //设置查询开始记录数，索引从0开始
            //i = i > 0 ? i - 1 : 0;
            if (i == 0) i = 1;
            return i;
        }
    }

    /// <summary>记录数</summary>
    private Int32 RecordNum
    {
        get
        {
            Int32 i = WebHelper.RequestInt("RecordNum");
            if (i == 0) i = TemplateConfig.Current.RecordNum;
            return i;
        }
    }

    private Channel _C;
    /// <summary>频道</summary>
    private Channel C
    {
        get
        {
            if (_C == null & !String.IsNullOrEmpty(Suffix))
            {
                _C = Channel.FindBySuffix(Suffix);
            }

            if (_C == null)
            {
                Err("未确定的频道！");
            }
            else if (String.IsNullOrEmpty(_C.ListTemplate))
            {
                Err("未绑定模版！");
            }

            return _C;
        }
        set { _C = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        String content = "";
        //try
        //{
        TypeX type = TypeX.GetType("NewLife.CMX.Web." + Address);
        IModeList iml = type.CreateInstance() as IModeList;
        //Dictionary<String, Object> dic = GetQueryDic();
        iml.ChannelID = C.ID;
        //iml.Suffix = Suffix;
        iml.Address = Address;
        iml.CategoryID = CategoryID;
        iml.Pageindex = PageIndex;
        iml.RecordNum = RecordNum;
        content = iml.Process();
        //}
        //catch (ThreadAbortException)
        //{
        //    Response.Redirect(CMXConfigBase.Current.CurrentRootPath + "/Index.html");
        //}
        //catch (Exception ex)
        //{
        //    // 不要随便拦截异常而无所作为，会害死人的
        //    XTrace.WriteException(ex);

        //    //Err("编译出错！");
        //    Err(ex.Message);
        //}

        Response.Write(content);
    }

    private void Err(String Msg)
    {
        Response.Clear();
        Response.Write(Msg);
        Response.End();
    }
}
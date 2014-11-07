using System;
using System.Web.UI;
using NewLife.CMX;
using NewLife.CMX.Config;

using NewLife.Reflection;
using NewLife.Web;

public partial class Template_List : Page
{
    /// <summary>ID</summary>
    private Int32 CategoryID { get { return WebHelper.RequestInt("CategoryID"); } }

    /// <summary>类编码</summary>
    private String Suffix
    {
        get
        {
            String str = Request["Suffix"];
            if (str == "$") str = "";
            return str;
        }
    }

    /// <summary>模型缩写</summary>
    private String ModelShortName
    {
        get
        {
            String str = Request["ModelSN"];
            if (str == null || str == "$") str = String.Empty;
            return str;
        }
    }

    /// <summary>请求地址</summary>
    private String Address
    {
        get
        {
            String ad = Request["Address"];

            //if (string.IsNullOrEmpty(ad) || ad == "$") ad = C.ListTemplate;
            //ad = ad.Substring(0, ad.IndexOf('.'));
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
            //if (i == 0) i = TemplateConfig.Current.RecordNum;
            return i;
        }
    }

    private Channel _C;
    /// <summary>频道</summary>
    private Channel C
    {
        get
        {
            if (_C == null)
                _C = Channel.FindBySuffixOrModelShortName(Suffix, ModelShortName);

            if (_C == null)
                Err("未确定的频道！");
            else if (String.IsNullOrEmpty(_C.ListTemplate))
                Err("未绑定模版！");

            return _C;
        }
        set { _C = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //String content = "";
        //try
        //{
        TypeX type = TypeX.GetType("NewLife.CMX.Web." + C.ListTemplate);
        IModeList iml = type.CreateInstance() as IModeList;
        //Dictionary<String, Object> dic = GetQueryDic();
        iml.ChannelID = C.ID;
        //iml.Suffix = Suffix;
        iml.ModelShortName = ModelShortName;
        iml.Address = Address;
        iml.CategoryID = CategoryID;
        iml.Pageindex = PageIndex;
        iml.RecordNum = RecordNum;
        String content = iml.Process();
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
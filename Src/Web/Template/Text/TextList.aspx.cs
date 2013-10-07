using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.CMX;
using NewLife.Log;
using NewLife.Web;
using XCode;

public partial class Template_Text_TextList : System.Web.UI.Page
{
    public String Suffix { get { return Request["Suffix"]; } }

    public Int32 CategoryID { get { return WebHelper.RequestInt("CategoryID"); } }

    public TextCategory Category;

    public EntityList<Text> ListText = new EntityList<Text>();

    protected override void OnInit(EventArgs e)
    {
        try
        {
            TextCategory.Meta.TableName += Suffix;
            Text.Meta.TableName += Suffix;

            Category = TextCategory.FindByID(CategoryID);

            if (Category.IsEnd)
            {
                ListText.AddRange(GetTextList(CategoryID));
            }
            else
            {
                List<TextCategory> listcategory = Category.AllChilds;

                foreach (TextCategory item in listcategory)
                {
                    if (item.IsEnd)
                    {
                        ListText.AddRange(GetTextList(item.ID));
                    }
                }
            }
            base.OnInit(e);
        }
        catch (Exception ex)
        {
            WebHelper.Alert("信息异常，请联系管理员！");
            XTrace.WriteLine(ex.Message);
            return;
        }
        finally
        {
            TextCategory.Meta.TableName = "";
            Text.Meta.TableName = "";
        }
    }

    private EntityList<Text> GetTextList(int categoryid)
    {
        EntityList<Text> Texts = new EntityList<Text>();

        foreach (Text item in Text.FindAllByCategoryID(categoryid))
        {
            Texts.Add(item);
        }

        return Texts;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        frmtext.DataSource = ListText;
        frmtext.DataBind();
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.CMX;
using NewLife.Xml;

public partial class Admin_Settings_Sys_Config : MyEntityList
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadConfig();
    }
    void LoadConfig()
    {
        SiteConfig sc = SiteConfig.Current;
        webname.Text = sc.Name;
        webcompany.Text = sc.Company;
        webtel.Text = sc.Tel;
        webfax.Text = sc.Fax;
        webmail.Text = sc.Mail;
        webcrod.Text = sc.Crod;
        webtitle.Text = sc.Title;
        webkeyword.Text = sc.KeyWord;
        webdescription.Text = sc.Description;
        webcopyright.Text = sc.CopyRight;

        AttachConfig ac = AttachConfig.Current;
        attachpath.Text = ac.Path;
        attachextension.Text = ac.Extension;
        attachsave.Text = ac.SaveMode.ToString();
        attachfilesize.Text = ac.FileSize.ToString();
        attachimgsize.Text = ac.SiteAttachimgsize.ToString();
        attachimgmaxheight.Text = ac.ImgMaxHeight.ToString();
        attachimgmaxwidth.Text = ac.ImgMaxWidth.ToString();
        thumbnailheight.Text = ac.ThumbnailHeight.ToString();
        thumbnailwidth.Text = ac.ThumbnailWidth.ToString();
        watermarktype.Text = ac.WatermarkType.ToString();
        watermarkposition.Text = ac.WatermarkPosition.ToString();
        watermarkimgquality.Text = ac.WatermarkImgQuality.ToString();
        watermarkpic.Text = ac.WatermarkPic;
        watermarktransparency.Text = ac.WatermarkTransparency.ToString();
        watermarktext.Text = ac.WatermarkText;
        watermarkfont.Text = ac.WatermarkFont;
        watermarkfontsize.Text = ac.WatermarkFontSize.ToString();

        SysConfig ssc = SysConfig.Current;
        webpath.Text = ssc.Path;
        webmanagepath.Text = ssc.ManagePath;
        staticstatus.SelectedIndex = ssc.StaticStatus;
        staticextension.Text = ssc.StaticExtension;
        memberstatus.SelectedIndex = ssc.SiteMemberStatus;
        commentstatus.SelectedIndex = ssc.CommentStatus;
        logstatus.SelectedIndex = ssc.LogStatus;
        webstatus.SelectedIndex = ssc.WebStatus;
        webclosereason.Text = ssc.CloseReason;
        webcountcode.Text = ssc.CountCode;
        MailConfig mc = MailConfig.Current;
        emailstmp.Text = mc.Stmp;
        emailport.Text = mc.Port.ToString();
        emailfrom.Text = mc.From;
        emailusername.Text = mc.Username;
        emailpassword.Text = mc.Password;
        emailnickname.Text = mc.Nickname;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SiteConfig sc = SiteConfig.Current;
        sc.Name = webname.Text.Trim();
        sc.Company = webcompany.Text.Trim();
        sc.Tel = webtel.Text.Trim();
        sc.Fax = webfax.Text.Trim();
        sc.Mail = webmail.Text.Trim();
        sc.Crod = webcrod.Text.Trim();
        sc.Title = webtitle.Text.Trim();
        sc.KeyWord = webkeyword.Text.Trim();
        sc.Description = webdescription.Text.Trim();
        sc.CopyRight = webcopyright.Text.Trim();
        sc.Save();

        AttachConfig ac = AttachConfig.Current;
        ac.Path = attachpath.Text.Trim();
        ac.Extension = attachextension.Text.Trim();
        ac.SaveMode = attachsave.SelectedIndex;
        ac.FileSize = Convert.ToInt32(attachfilesize.Text);
        ac.SiteAttachimgsize = Convert.ToInt32(attachimgsize.Text.Trim());
        ac.ImgMaxHeight = Convert.ToInt32(attachimgmaxheight.Text.Trim());
        ac.ImgMaxWidth = Convert.ToInt32(attachimgmaxwidth.Text.Trim());
        ac.ThumbnailHeight = Convert.ToInt32(thumbnailheight.Text.Trim());
        ac.ThumbnailWidth= Convert.ToInt32(thumbnailwidth.Text.Trim());
        ac.WatermarkType = Convert.ToInt32(watermarktype.Text.Trim());
        ac.WatermarkPosition = Convert.ToInt32(watermarkposition.Text.Trim());
        ac.WatermarkImgQuality = Convert.ToInt32(watermarkimgquality.Text.Trim());
        ac.WatermarkPic = watermarkpic.Text.Trim();
        ac.WatermarkTransparency = Convert.ToInt32(watermarktransparency.Text.Trim());
        ac.WatermarkText = watermarktext.Text.Trim();
        ac.WatermarkFont = watermarkfont.Text.Trim();
        ac.WatermarkFontSize = Convert.ToInt32(watermarkfontsize.Text.Trim());
        ac.Save();
        
        SysConfig ssc = SysConfig.Current;
        ssc.Path = webpath.Text.Trim();
        ssc.ManagePath = webmanagepath.Text.Trim();
        ssc.StaticStatus = Convert.ToInt32(staticstatus.Text.Trim());
        ssc.StaticExtension = staticextension.Text.Trim();
        ssc.SiteMemberStatus = memberstatus.SelectedIndex;
        ssc.CommentStatus = commentstatus.SelectedIndex;
        ssc.LogStatus = logstatus.SelectedIndex;
        ssc.WebStatus = webstatus.SelectedIndex;
        ssc.CloseReason = webclosereason.Text.Trim();
        ssc.CountCode = webcountcode.Text.Trim();
        ssc.Save();

        MailConfig mc = MailConfig.Current;
        mc.Stmp = emailstmp.Text.Trim();
        mc.Port = Convert.ToInt32(emailport.Text.Trim());
        mc.From = emailfrom.Text.Trim();
        mc.Username = emailusername.Text.Trim();
        mc.Password = emailpassword.Text.Trim();
        mc.Nickname = emailnickname.Text.Trim();
        mc.Save();
    }
}
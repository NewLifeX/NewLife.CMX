<%@ Page Title="系统设置" Language="C#" AutoEventWireup="true" CodeFile="Sys_Config.aspx.cs" Inherits="Admin_Settings_Sys_Config" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统参数设置</title>
    <link href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../images/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/messages_cn.js"></script>
    <script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
    <script type="text/javascript" src="../../scripts/dtcms/function.js"></script>
    <script type="text/javascript">
        //表单验证
        $(function () {
            $("#form1").validate({
                invalidHandler: function (e, validator) {
                    parent.jsprint("有 " + validator.numberOfInvalids() + " 项填写有误，请检查！", "", "Warning");
                },
                errorPlacement: function (lable, element) {
                    //可见元素显示错误提示
                    if (element.parents(".tab_con").css('display') != 'none') {
                        element.ligerTip({ content: lable.html(), appendIdTo: lable });
                    }
                },
                success: function (lable) {
                    lable.ligerHideTip();
                }
            });
        });
    </script>
</head>
<body class="mainbody">

    <form id="form1" runat="server">
        <div class="navigation">首页 &gt; 系统管理 &gt; 系统参数设置</div>
        <div id="contentTab">
            <ul class="tab_nav">
                <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">网站基本信息</a></li>
                <li><a onclick="tabs('#contentTab',1);" href="javascript:void(0);">功能权限配置</a></li>
                <li><a onclick="tabs('#contentTab',2);" href="javascript:void(0);">邮件发送配置</a></li>
                <li><a onclick="tabs('#contentTab',3);" href="javascript:void(0);">附件配置</a></li>
                <li><a onclick="tabs('#contentTab',4);" href="javascript:void(0);">网站配置</a></li>
            </ul>

            <div class="tab_con" style="display: block;">
                <table class="form_table">
                    <col width="180px">
                    <col>
                    <tbody>
                        <tr>
                            <th>站点名称：</th>
                            <td>
                                <asp:TextBox ID="webname" runat="server" CssClass="txtInput normal required" MaxLength="100"></asp:TextBox><label>*</label></td>
                        </tr>
                        <tr>
                            <th>公司名称：</th>
                            <td>
                                <asp:TextBox ID="webcompany" runat="server" CssClass="txtInput normal required" MaxLength="100"></asp:TextBox><label>*</label></td>
                        </tr>
                        <tr>
                            <th>网站域名：</th>
                            <td>
                                <asp:TextBox ID="weburl" runat="server" CssClass="txtInput normal required url" MaxLength="250"></asp:TextBox><label>*以“http://”开头</label></td>
                        </tr>
                        <tr>
                            <th>联系电话：</th>
                            <td>
                                <asp:TextBox ID="webtel" runat="server" CssClass="txtInput normal" MaxLength="50"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th>传真号码：</th>
                            <td>
                                <asp:TextBox ID="webfax" runat="server" CssClass="txtInput normal" MaxLength="50"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th>管理员邮箱：</th>
                            <td>
                                <asp:TextBox ID="webmail" runat="server" CssClass="txtInput normal email" MaxLength="100"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th>网站备案号：</th>
                            <td>
                                <asp:TextBox ID="webcrod" runat="server" CssClass="txtInput normal" MaxLength="50"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th>首页标题(SEO)：</th>
                            <td>
                                <asp:TextBox ID="webtitle" runat="server" CssClass="txtInput normal required" MaxLength="250" Style="width: 350px;"></asp:TextBox><label>*自定义的首页标题</label></td>
                        </tr>
                        <tr>
                            <th>页面关健词(SEO)：</th>
                            <td>
                                <asp:TextBox ID="webkeyword" runat="server" CssClass="txtInput" MaxLength="250" Style="width: 350px;"></asp:TextBox>
                                <label>页面关键词(keyword)</label></td>
                        </tr>
                        <tr>
                            <th>页面描述(SEO)：</th>
                            <td>
                                <asp:TextBox ID="webdescription" runat="server" MaxLength="250" TextMode="MultiLine" CssClass="small"></asp:TextBox>
                                <label>页面描述(description)</label></td>
                        </tr>
                        <tr>
                            <th>网站版权信息：</th>
                            <td>
                                <asp:TextBox ID="webcopyright" runat="server" MaxLength="500" TextMode="MultiLine" CssClass="small"></asp:TextBox><label>支持HTML格式</label></td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <div class="tab_con">
                <table class="form_table">
                    <col width="180px">
                    <col>
                    <tbody>
                        <tr>
                            <th>网站安装目录：</th>
                            <td>
                                <asp:TextBox ID="webpath" runat="server" CssClass="txtInput normal required" MaxLength="100">/</asp:TextBox><label>*根目录下，输入“/”；如：http://abc.com/web，输入“web/”</label></td>
                        </tr>
                        <tr>
                            <th>网站管理目录：</th>
                            <td>
                                <asp:TextBox ID="webmanagepath" runat="server" CssClass="txtInput normal required" minlength="2" MaxLength="100">admin</asp:TextBox><label>*默认是admin，如已经更改，请输入目录名</label></td>
                        </tr>
                        <tr>
                            <th>URL重写开关：</th>
                            <td>
                                <asp:RadioButtonList ID="staticstatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Selected="True" Value="0">关闭</asp:ListItem>
                                    <asp:ListItem Value="1">伪URL重写</asp:ListItem>
                                </asp:RadioButtonList>
                                <label>(<a href="url_rewrite_list.aspx">编辑伪静态url替换规则</a>)</label>
                            </td>
                        </tr>
                        <tr>
                            <th>静态URL后缀：</th>
                            <td>
                                <asp:TextBox ID="staticextension" runat="server" CssClass="txtInput small required" minlength="2" MaxLength="100"></asp:TextBox><label>*扩展名，不包括“.”，如：aspx、html</label></td>
                        </tr>
                        <tr>
                            <th>开启会员功能：</th>
                            <td>
                                <asp:RadioButtonList ID="memberstatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Value="0">关闭</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="1">开启</asp:ListItem>
                                </asp:RadioButtonList>
                                <label></label>
                            </td>
                        </tr>
                        <tr>
                            <th>开启评论审核：</th>
                            <td>
                                <asp:RadioButtonList ID="commentstatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Selected="True" Value="0">关闭</asp:ListItem>
                                    <asp:ListItem Value="1">开启</asp:ListItem>
                                </asp:RadioButtonList>
                                <label></label>
                            </td>
                        </tr>
                        <tr>
                            <th>后台管理日志：</th>
                            <td>
                                <asp:RadioButtonList ID="logstatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Selected="True" Value="0">关闭</asp:ListItem>
                                    <asp:ListItem Value="1">开启</asp:ListItem>
                                </asp:RadioButtonList>
                                <label></label>
                            </td>
                        </tr>
                        <tr>
                            <th>是否关闭网站：</th>
                            <td>
                                <asp:RadioButtonList ID="webstatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Value="0">关闭</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="1">开启</asp:ListItem>
                                </asp:RadioButtonList>
                                <label></label>
                            </td>
                        </tr>
                        <tr>
                            <th>关闭原因描述：</th>
                            <td>
                                <asp:TextBox ID="webclosereason" runat="server" MaxLength="500" TextMode="MultiLine" CssClass="small"></asp:TextBox><label>支持HTML格式</label></td>
                        </tr>
                        <tr>
                            <th>网站统计代码：</th>
                            <td>
                                <asp:TextBox ID="webcountcode" runat="server" MaxLength="500" TextMode="MultiLine" CssClass="small"></asp:TextBox><label>支持HTML格式</label></td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <div class="tab_con">
                <table class="form_table">
                    <col width="180px">
                    <col>
                    <tbody>
                        <tr>
                            <th>STMP服务器：</th>
                            <td>
                                <asp:TextBox ID="emailstmp" runat="server" CssClass="txtInput normal required" MaxLength="100"></asp:TextBox><label>*发送邮件的SMTP服务器地址</label></td>
                        </tr>
                        <tr>
                            <th>SMTP端口：</th>
                            <td>
                                <asp:TextBox ID="emailport" runat="server" CssClass="txtInput small required digits" MaxLength="10">25</asp:TextBox><label>*SMTP服务器的端口</label></td>
                        </tr>
                        <tr>
                            <th>发件人地址：</th>
                            <td>
                                <asp:TextBox ID="emailfrom" runat="server" CssClass="txtInput normal required" MaxLength="100"></asp:TextBox><label>*</label></td>
                        </tr>
                        <tr>
                            <th>邮箱账号：</th>
                            <td>
                                <asp:TextBox ID="emailusername" runat="server" CssClass="txtInput normal required" MaxLength="100"></asp:TextBox><label>*</label></td>
                        </tr>
                        <tr>
                            <th>邮箱密码：</th>
                            <td>
                                <asp:TextBox ID="emailpassword" runat="server" CssClass="txtInput normal required" MaxLength="100" TextMode="Password"></asp:TextBox><label>*</label></td>
                        </tr>
                        <tr>
                            <th>发件人昵称：</th>
                            <td>
                                <asp:TextBox ID="emailnickname" runat="server" CssClass="txtInput normal required" MaxLength="100"></asp:TextBox><label>*显示发件人的昵称</label></td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <div class="tab_con">
                <table class="form_table">
                    <col width="180px">
                    <col>
                    <tbody>
                        <tr>
                            <th>附件上传目录：</th>
                            <td>
                                <asp:TextBox ID="attachpath" runat="server" CssClass="txtInput normal required" minlength="2" MaxLength="100">upload</asp:TextBox><label>*上传图片或附件的目录，自动创建在网站根目录下</label></td>
                        </tr>
                        <tr>
                            <th>附件上传类型：</th>
                            <td>
                                <asp:TextBox ID="attachextension" runat="server" CssClass="txtInput normal required" MaxLength="250"></asp:TextBox><label>*以英文的逗号分隔开，如：“jpg,gif,rar”</label></td>
                        </tr>
                        <tr>
                            <th>附件保存方式：</th>
                            <td>
                                <asp:DropDownList ID="attachsave" runat="server" CssClass="select2">
                                    <asp:ListItem Value="1">按年月日每天一个目录</asp:ListItem>
                                    <asp:ListItem Value="2">按年月/日/存入不同目录</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>文件上传大小：</th>
                            <td>
                                <asp:TextBox ID="attachfilesize" runat="server" CssClass="txtInput small required number" MaxLength="10"></asp:TextBox>KB<label>*超过设置的文件大小不予上传，0不限制</label></td>
                        </tr>
                        <tr>
                            <th>图片上传大小：</th>
                            <td>
                                <asp:TextBox ID="attachimgsize" runat="server" CssClass="txtInput small required number" MaxLength="10"></asp:TextBox>KB<label>*超过设置的图片大小不予上传，0不限制</label></td>
                        </tr>
                        <tr>
                            <th>图片最大尺寸：</th>
                            <td>
                                <asp:TextBox ID="attachimgmaxheight" runat="server" CssClass="txtInput small2 required digits" MaxLength="10">0</asp:TextBox>×
                    <asp:TextBox ID="attachimgmaxwidth" runat="server" CssClass="txtInput small2 required digits" MaxLength="10">0</asp:TextBox>px
                    <label>*设置图片高和宽，超出自动裁剪，0为不受限制</label>
                            </td>
                        </tr>
                        <tr>
                            <th>生成缩略图大小：</th>
                            <td>
                                <asp:TextBox ID="thumbnailheight" runat="server" CssClass="txtInput small2 required digits" MaxLength="10">0</asp:TextBox>×
                    <asp:TextBox ID="thumbnailwidth" runat="server" CssClass="txtInput small2 required digits" MaxLength="10">0</asp:TextBox>px
                    <label>*图片生成缩略图高和宽，0为不生成</label>
                            </td>
                        </tr>
                        <tr>
                            <th>图片水印类型：</th>
                            <td>
                                <asp:RadioButtonList ID="watermarktype" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Value="0" Selected="True">关闭水印 </asp:ListItem>
                                    <asp:ListItem Value="1">文字水印 </asp:ListItem>
                                    <asp:ListItem Value="2">图片水印 </asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <th>图片水印位置：</th>
                            <td>
                                <asp:RadioButtonList ID="watermarkposition" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Value="1">左上 </asp:ListItem>
                                    <asp:ListItem Value="2">中上 </asp:ListItem>
                                    <asp:ListItem Value="3">右上 </asp:ListItem>
                                    <asp:ListItem Value="4">左中 </asp:ListItem>
                                    <asp:ListItem Value="5">居中 </asp:ListItem>
                                    <asp:ListItem Value="6">右中 </asp:ListItem>
                                    <asp:ListItem Value="7">左下 </asp:ListItem>
                                    <asp:ListItem Value="8">中下 </asp:ListItem>
                                    <asp:ListItem Value="9" Selected="True">右下 </asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <th>图片生成质量：</th>
                            <td>
                                <asp:TextBox ID="watermarkimgquality" runat="server" CssClass="txtInput small required digits" MaxLength="3">80</asp:TextBox><label>*只适用于加水印的jpeg格式图片.取值范围 0-100, 0质量最低, 100质量最高, 默认80</label></td>
                        </tr>
                        <tr>
                            <th>图片水印文件：</th>
                            <td>
                                <asp:TextBox ID="watermarkpic" runat="server" CssClass="txtInput normal required" MaxLength="100">watermark.png</asp:TextBox><label>*需存放在站点目录下，如图片不存在将使用文字水印</label></td>
                        </tr>
                        <tr>
                            <th>水印透明度：</th>
                            <td>
                                <asp:TextBox ID="watermarktransparency" runat="server" CssClass="txtInput small required digits" MaxLength="2" max="10">5</asp:TextBox><label>*取值范围1--10 (10为不透明)</label></td>
                        </tr>
                        <tr>
                            <th>水印文字：</th>
                            <td>
                                <asp:TextBox ID="watermarktext" runat="server" CssClass="txtInput normal required" MaxLength="100"></asp:TextBox><label>*文字水印的内容</label></td>
                        </tr>
                        <tr>
                            <th>文字字体：</th>
                            <td>
                                <asp:DropDownList ID="watermarkfont" runat="server" CssClass="select2">
                                    <asp:ListItem Value="Arial">Arial</asp:ListItem>
                                    <asp:ListItem Value="Arial Black">Arial Black</asp:ListItem>
                                    <asp:ListItem Value="Batang">Batang</asp:ListItem>
                                    <asp:ListItem Value="BatangChe">BatangChe</asp:ListItem>
                                    <asp:ListItem Value="Comic Sans MS">Comic Sans MS</asp:ListItem>
                                    <asp:ListItem Value="Courier New">Courier New</asp:ListItem>
                                    <asp:ListItem Value="Dotum">Dotum</asp:ListItem>
                                    <asp:ListItem Value="DotumChe">DotumChe</asp:ListItem>
                                    <asp:ListItem Value="Estrangelo Edessa">Estrangelo Edessa</asp:ListItem>
                                    <asp:ListItem Value="Franklin Gothic Medium">Franklin Gothic Medium</asp:ListItem>
                                    <asp:ListItem Value="Gautami">Gautami</asp:ListItem>
                                    <asp:ListItem Value="Georgia">Georgia</asp:ListItem>
                                    <asp:ListItem Value="Gulim">Gulim</asp:ListItem>
                                    <asp:ListItem Value="GulimChe">GulimChe</asp:ListItem>
                                    <asp:ListItem Value="Gungsuh">Gungsuh</asp:ListItem>
                                    <asp:ListItem Value="GungsuhChe">GungsuhChe</asp:ListItem>
                                    <asp:ListItem Value="Impact">Impact</asp:ListItem>
                                    <asp:ListItem Value="Latha">Latha</asp:ListItem>
                                    <asp:ListItem Value="Lucida Console">Lucida Console</asp:ListItem>
                                    <asp:ListItem Value="Lucida Sans Unicode">Lucida Sans Unicode</asp:ListItem>
                                    <asp:ListItem Value="Mangal">Mangal</asp:ListItem>
                                    <asp:ListItem Value="Marlett">Marlett</asp:ListItem>
                                    <asp:ListItem Value="Microsoft Sans Serif">Microsoft Sans Serif</asp:ListItem>
                                    <asp:ListItem Value="MingLiU">MingLiU</asp:ListItem>
                                    <asp:ListItem Value="MS Gothic">MS Gothic</asp:ListItem>
                                    <asp:ListItem Value="MS Mincho">MS Mincho</asp:ListItem>
                                    <asp:ListItem Value="MS PGothic">MS PGothic</asp:ListItem>
                                    <asp:ListItem Value="MS PMincho">MS PMincho</asp:ListItem>
                                    <asp:ListItem Value="MS UI Gothic">MS UI Gothic</asp:ListItem>
                                    <asp:ListItem Value="MV Boli">MV Boli</asp:ListItem>
                                    <asp:ListItem Value="Palatino Linotype">Palatino Linotype</asp:ListItem>
                                    <asp:ListItem Value="PMingLiU">PMingLiU</asp:ListItem>
                                    <asp:ListItem Value="Raavi">Raavi</asp:ListItem>
                                    <asp:ListItem Value="Shruti">Shruti</asp:ListItem>
                                    <asp:ListItem Value="Sylfaen">Sylfaen</asp:ListItem>
                                    <asp:ListItem Value="Symbol">Symbol</asp:ListItem>
                                    <asp:ListItem Value="Tahoma" Selected="selected">Tahoma</asp:ListItem>
                                    <asp:ListItem Value="Times New Roman">Times New Roman</asp:ListItem>
                                    <asp:ListItem Value="Trebuchet MS">Trebuchet MS</asp:ListItem>
                                    <asp:ListItem Value="Tunga">Tunga</asp:ListItem>
                                    <asp:ListItem Value="Verdana">Verdana</asp:ListItem>
                                    <asp:ListItem Value="Webdings">Webdings</asp:ListItem>
                                    <asp:ListItem Value="Wingdings">Wingdings</asp:ListItem>
                                    <asp:ListItem Value="仿宋_GB2312">仿宋_GB2312</asp:ListItem>
                                    <asp:ListItem Value="宋体">宋体</asp:ListItem>
                                    <asp:ListItem Value="新宋体">新宋体</asp:ListItem>
                                    <asp:ListItem Value="楷体_GB2312">楷体_GB2312</asp:ListItem>
                                    <asp:ListItem Value="黑体">黑体</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="watermarkfontsize" runat="server" CssClass="txtInput small2 required digits" MaxLength="10">12</asp:TextBox>px
                    <label>*文字水印的字体和大小</label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="tab_con">
                <table class="form_table">
                    <col width="180px">
                    <col>
                    <tbody>
                        <tr>
                            <th>首页地址：</th>
                            <td>
                                <asp:TextBox ID="wsindex" runat="server" CssClass="txtInput normal required" MaxLength="100"></asp:TextBox><label>*设置首页地址</label></td>
                        </tr>
                        <tr>
                            <th>联系我们：</th>
                            <td>
                                <asp:TextBox ID="wscontact" runat="server" CssClass="txtInput normal required" MaxLength="100"></asp:TextBox><label>*</label></td>
                        </tr>
                        <tr>
                            <th>论坛地址：</th>
                            <td>
                                <asp:TextBox ID="wsforum" runat="server" CssClass="txtInput normal required" MaxLength="100"></asp:TextBox><label></label></td>
                        </tr>
                        <tr>
                            <th>商城地址：</th>
                            <td>
                                <asp:TextBox ID="wsshop" runat="server" CssClass="txtInput normal required" MaxLength="100"></asp:TextBox><label></label></td>
                        </tr>
                        <tr>
                            <th>版权：</th>
                            <td>
                                <asp:TextBox ID="wscopyright" runat="server" CssClass="txtInput normal required" MaxLength="100"></asp:TextBox><label></label></td>
                        </tr>
                        <tr>
                            <th>公司地址：</th>
                            <td>
                                <asp:TextBox ID="wsaddress" runat="server" CssClass="txtInput normal required" MaxLength="100"></asp:TextBox><label></label></td>
                        </tr>
                        <tr>
                            <th>联系电话：</th>
                            <td>
                                <asp:TextBox ID="wstel" runat="server" CssClass="txtInput normal required" MaxLength="100"></asp:TextBox><label></label></td>
                        </tr>
                        <tr>
                            <th>备案信息：</th>
                            <td>
                                <asp:TextBox ID="wsicp" runat="server" CssClass="txtInput normal required" MaxLength="100"></asp:TextBox><label></label></td>
                        </tr>
                        <tr>
                            <th>标题：</th>
                            <td>
                                <asp:TextBox ID="wstitle" runat="server" CssClass="txtInput normal required" MaxLength="100"></asp:TextBox><label></label></td>
                        </tr>
                        <tr>
                            <th>META关键字：</th>
                            <td>
                                <asp:TextBox ID="wskeywords" runat="server" CssClass="txtInput normal required" MaxLength="100"></asp:TextBox><label></label></td>
                        </tr>
                        <tr>
                            <th>META描述：</th>
                            <td>
                                <asp:TextBox ID="wsdescription" runat="server" CssClass="txtInput normal required" MaxLength="100"></asp:TextBox><label></label></td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <div class="foot_btn_box">
                <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btnSubmit" OnClick="btnSubmit_Click" />
                &nbsp;<input name="重置" type="reset" class="btnSubmit" value="重 置" />
            </div>
        </div>
    </form>
</body>
</html>

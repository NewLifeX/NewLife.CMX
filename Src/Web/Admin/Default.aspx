<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>

<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=NewLife.CMX.SiteConfig.Current.Name %> - 后台管理</title>
    <link href="../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="images/style.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="../scripts/ui/js/ligerBuild.min.js" type="text/javascript"></script>
    <script src="../scripts/dtcms/function.js" type="text/javascript"></script>

    <script type="text/javascript">
        var tab = null;
        var accordion = null;
        var tree = null;
        $(function () {
            //页面布局
            $("#global_layout").ligerLayout({ leftWidth: 180, height: '100%', topHeight: 65, bottomHeight: 24, allowTopResize: false, allowBottomResize: false, allowLeftCollapse: true, onHeightChanged: f_heightChanged });

            var height = $(".l-layout-center").height();

            //Tab
            $("#framecenter").ligerTab({ height: height });

            //左边导航面板
            $("#global_left_nav").ligerAccordion({ height: height - 25, speed: null });

            $(".l-link").hover(function () {
                $(this).addClass("l-link-over");
            }, function () {
                $(this).removeClass("l-link-over");
            });

            //设置频道菜单
            //$("#global_channel_tree").ligerTree({
            //    url: '/Admin/ajax.ashx?action=channel',
            //    checkbox: false,
            //    nodeWidth: 112,
            //    //attribute: ['nodename', 'url'],
            //    onSelect: function (node) {
            //        if (!node.data.url) return;
            //        var tabid = $(node.target).attr("tabid");
            //        if (!tabid) {
            //            tabid = new Date().getTime();
            //            $(node.target).attr("tabid", tabid)
            //        }
            //        f_addTab("my", node.data.text, node.data.url);
            //    }
            //});

            //加载插件菜单
            //loadPluginsNav();

            //快捷菜单
            var menu = $.ligerMenu({
                width: 120, items:
                [
                    { text: '管理首页', click: itemclick },
                    { text: '修改密码', click: itemclick },
                    { line: true },
                    { text: '关闭菜单', click: itemclick }
                ]
            });
            $("#tab-tools-nav").bind("click", function () {
                var offset = $(this).offset(); //取得事件对象的位置
                menu.show({ top: offset.top + 27, left: offset.left - 120 });
                return false;
            });

            tab = $("#framecenter").ligerGetTabManager();
            accordion = $("#global_left_nav").ligerGetAccordionManager();
            //tree = $("#global_channel_tree").ligerGetTreeManager();
            //tree.expandAll(); //默认展开所有节点
            $("#pageloading_bg,#pageloading").hide();
        });

        //快捷菜单回调函数
        function itemclick(item) {
            switch (item.text) {
                case "管理首页":
                    f_addTab('home', '管理中心', 'center.aspx');
                    break;
                case "快捷导航":
                    //调用函数
                    break;
                case "修改密码":
                    f_addTab('manager_pwd', '修改密码', 'manager/manager_pwd.aspx');
                    break;
                default:
                    //关闭窗口
                    break;
            }
        }
        function f_heightChanged(options) {
            if (tab)
                tab.addHeight(options.diff);
            if (accordion && options.middleHeight - 24 > 0)
                accordion.setHeight(options.middleHeight - 24);
        }
        //添加Tab，可传3个参数
        function f_addTab(tabid, text, url, iconcss) {
            if (arguments.length == 4) {
                tab.addTabItem({ tabid: tabid, text: text, url: url, iconcss: iconcss });
            } else {
                tab.addTabItem({ tabid: tabid, text: text, url: url });
            }
        }
        //提示Dialog并关闭Tab
        function f_errorTab(tit, msg) {
            $.ligerDialog.open({
                isDrag: false,
                allowClose: false,
                type: 'error',
                title: tit,
                content: msg,
                buttons: [{
                    text: '确定',
                    onclick: function (item, dialog, index) {
                        //查找当前iframe名称
                        var itemiframe = "#framecenter .l-tab-content .l-tab-content-item";
                        var curriframe = "";
                        $(itemiframe).each(function () {
                            if ($(this).css("display") != "none") {
                                curriframe = $(this).attr("tabid");
                                return false;
                            }
                        });
                        if (curriframe != "") {
                            tab.removeTabItem(curriframe);
                            dialog.close();
                        }
                    }
                }]
            });
        }
    </script>
</head>
<body style="padding: 0px;">
    <form id="form1" runat="server">
        <div class="pageloading_bg" id="pageloading_bg"></div>
        <div id="pageloading">数据加载中，请稍等...</div>
        <div id="global_layout" class="layout" style="width: 100%">
            <!--头部-->
            <div position="top" class="header">
                <div class="header_box">
                    <div class="header_right">
                        <span><b><%=admin.DisplayName %>（<%=admin.RoleName %>）</b>您好，欢迎光临</span>
                        <br />
                        <a href="javascript:f_addTab('home','管理中心','center.aspx')">管理中心</a> | 
                    <a target="_blank" href="#">预览网站</a> | 
                    <asp:LinkButton ID="lbtnExit" runat="server" onclick="lbtnExit_Click">安全退出</asp:LinkButton>
                    </div>
                    <a class="logo">Logo</a>
                </div>
            </div>
            <!--左边-->
<%--            <div position="left" title="管理菜单" id="global_left_nav">
                <div title="频道管理" iconcss="menu-icon-model" class="l-scroll">
                    <ul id="global_channel_tree" style="margin-top: 3px;">
                    </ul>
                </div>
                <div title="会员管理" iconcss="menu-icon-member">
                    <ul class="nlist">
                    </ul>
                </div>

                <div title="插件管理" iconcss="menu-icon-plugins">
                    <ul id="global_plugins" class="nlist">
                    </ul>
                </div>
                <div title="控制面板" iconcss="menu-icon-setting">
                    <ul class="nlist">
                        <li><a class="l-link" href="javascript:f_addTab('sys_config','系统参数设置','Settings/Sys_Config.aspx')">系统参数设置</a></li>
                        <li><a class="l-link" href="javascript:f_addTab('Manager','系统用户管理','Manager/Manager.aspx')">系统用户管理</a></li>
                        <li><a class="l-link" href="javascript:f_addTab('Menu','系统菜单管理','Manager/Menu.aspx')">系统菜单管理</a></li>
                        <li><a class="l-link" href="javascript:f_addTab('Log','系统日志管理','Manager/Log.aspx')">系统日志管理</a></li>
                        <li><a class="l-link" href="javascript:f_addTab('SysModel','系统模型管理','Manager/SysModel.aspx')">系统模型管理</a></li>

                    </ul>
                </div>
            </div>--%>
            <Custom:LeftMenu id="leftmenu" runat="server"/>
            <div position="center" id="framecenter" toolsid="tab-tools-nav">
                <div tabid="home" title="管理中心" iconcss="tab-icon-home" style="height: 300px">
                    <iframe frameborder="0" name="sysMain" src="Main.aspx"></iframe>
                </div>
            </div>
            <div position="bottom" class="footer">
                <div class="copyright"><label runat="server" id="copyright"></label></div>
            </div>
        </div>
    </form>
</body>
</html>

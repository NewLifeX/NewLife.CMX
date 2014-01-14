<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductShow.aspx.cs" Inherits="templates_childwebs_ProductShow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../style/djComm.css" rel="stylesheet" type="text/css" />
    <link href="../../style/djConts.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $(".productList li").each(function (i) {
                if (i % 4 == "0") {
                    $(this).css("margin-left","20px");
                }
            });
        });

    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="top">
        <p>
            欢迎光临，现在是2013-12-20 11：01：30
        </p>
    </div>
    <div class="head">
        <div class="headcont">
            <a href="index.aspx">
                <img src="../../images/djimages/logo.png" />
            </a>
            <input type="text" class="txt" /><input type="button" class="btn" />
        </div>
    </div>
    <div class="outmenu">
        <div class="nav">
            <div class="menu">
                <ul>
                    <li><a class="hide" href="Index.aspx">网站首页</a>
                        <!--[if lte IE 6]>
<a href="../menu/index.html">DEMOS
<table><tr><td>
<![endif]-->
                        <ul>
                            <li><a href="Index.aspx">子菜单</a></li>
                            <li><a href="Index.aspx">子菜单</a></li>
                            <li><a href="Index.aspx">子菜单</a></li>
                        </ul>
                        <!--[if lte IE 6]>
</td></tr></table>
</a>
<![endif]-->
                    </li>
                    <li><a class="hide" href="Index.aspx">产品展示</a>
                        <!--[if lte IE 6]>
<a href="index.html">MENUS
<table><tr><td>
<![endif]-->
                        <ul>
                            <li><a href="Index.aspx">子菜单</a></li>
                            <li><a href="Index.aspx">子菜单</a></li>
                            <li><a href="Index.aspx">子菜单</a></li>
                        </ul>
                        <!--[if lte IE 6]>
</td></tr></table>
</a>
<![endif]-->
                    </li>
                    <li><a class="hide" href="Index.aspx">案例演示</a>
                        <!--[if lte IE 6]>
<a href="../layouts/index.html">LAYOUTS
<table><tr><td>
<![endif]-->
                        <ul>
                            <li><a href="Index.aspx">子菜单</a></li>
                            <li><a href="Index.aspx">子菜单</a></li>
                            <li><a href="Index.aspx">子菜单</a></li>
                        </ul>
                        <!--[if lte IE 6]>
</td></tr></table>
</a>
<![endif]-->
                    </li>
                    <li><a class="hide" href="Index.aspx">客服服务</a>
                        <!--[if lte IE 6]>
<a href="../boxes/index.html">BOXES
<table><tr><td>
<![endif]-->
                        <ul>
                            <li><a href="Index.aspx">子菜单</a></li>
                            <li><a href="Index.aspx">子菜单</a></li>
                            <li><a href="Index.aspx">子菜单</a></li>
                        </ul>
                        <!--[if lte IE 6]>
</td></tr></table>
</a>
<![endif]-->
                    </li>
                    <li><a class="hide" href="Index.aspx">咨询中心</a>
                        <!--[if lte IE 6]>
<a href="../mozilla/index.html">MOZILLA
<table><tr><td>
<![endif]-->
                        <ul>
                            <li><a href="Index.aspx">子菜单</a></li>
                            <li><a href="Index.aspx">子菜单</a></li>
                            <li><a href="Index.aspx">子菜单</a></li>
                        </ul>
                        <!--[if lte IE 6]>
</td></tr></table>
</a>
<![endif]-->
                    </li>
                    <li><a class="hide" href="Index.aspx">技术论坛</a>
                        <!--[if lte IE 6]>
<a href="../ie/index.html">EXPLORER
<table><tr><td>
<![endif]-->
                        <ul>
                            <li><a href="Index.aspx">子菜单</a></li>
                            <li><a href="Index.aspx">子菜单</a></li>
                            <li><a href="Index.aspx">子菜单</a></li>
                        </ul>
                    </li>
                    <!--[if lte IE 6]>
</td></tr></table>
</a>
<![endif]-->
                </ul>
                <!-- clear the floats if required -->
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
    <div class="swf2">
    </div>
    <div class="mid">
        <div class="left left2">
            <p>
                案例分类</p>
            <ul>
                <li><a href="#">混合管</a> </li>
                <li><a href="#">点胶机</a> </li>
                <li><a href="#">点胶针头</a> </li>
                <li><a href="#">AB胶筒</a> </li>
                <li><a href="#">点胶阀门</a> </li>
                <li><a href="#">点胶针筒</a> </li>
                <li><a href="#">AB胶枪</a> </li>
                <li><a href="#">针筒适配器</a> </li>
                <li><a href="#">压力桶</a> </li>
                <li><a href="#">各种料管</a> </li>
                <li><a href="#">非标产品定制</a> </li>
            </ul>
            <p>
                联系我们</p>
            <span>地址：广东省东莞市高埗镇冼沙二上坊广场北路宝源工业园</span> <span>电话：0769-23107897</span> <span>传真：0769-23102536
            </span>
        </div>
        <div class="right">
            <p class="ptitle">
                产品展示 产品目录
            </p>
            <div class="productList">
                <ul>
                    <li>
                        <img src="../../images/djimages/te.gif" />
                        <p>
                            <a>YWS系列</a></p>
                    </li>
                    <li>
                        <img src="../../images/djimages/te.gif" />
                        <p>
                            <a>YWS系列</a></p>
                    </li>
                    <li>
                        <img src="../../images/djimages/te.gif" />
                        <p>
                            <a>YWS系列</a></p>
                    </li>
                    <li>
                        <img src="../../images/djimages/te.gif" />
                        <p>
                            <a>YWS系列</a></p>
                    </li>
                    <li>
                        <img src="../../images/djimages/te.gif" />
                        <p>
                            <a>YWS系列</a></p>
                    </li>
                    <li>
                        <img src="../../images/djimages/te.gif" />
                        <p>
                            <a>YWS系列</a></p>
                    </li>
                    <li>
                        <img src="../../images/djimages/te.gif" />
                        <p>
                            <a>YWS系列</a></p>
                    </li>
                    <li>
                        <img src="../../images/djimages/te.gif" />
                        <p>
                            <a>YWS系列</a></p>
                    </li>
                    <li>
                        <img src="../../images/djimages/te.gif" />
                        <p>
                            <a>YWS系列</a></p>
                    </li>
                    <li>
                        <img src="../../images/djimages/te.gif" />
                        <p>
                            <a>YWS系列</a></p>
                    </li>
                    <li>
                        <img src="../../images/djimages/te.gif" />
                        <p>
                            <a>YWS系列</a></p>
                    </li>
                    <li>
                        <img src="../../images/djimages/te.gif" />
                        <p>
                            <a>YWS系列</a></p>
                    </li>
                    <li>
                        <img src="../../images/djimages/te.gif" />
                        <p>
                            <a>YWS系列</a></p>
                    </li>
                    <li>
                        <img src="../../images/djimages/te.gif" />
                        <p>
                            <a>YWS系列</a></p>
                    </li>
                    <li>
                        <img src="../../images/djimages/te.gif" />
                        <p>
                            <a>YWS系列</a></p>
                    </li>
                    <li>
                        <img src="../../images/djimages/te.gif" />
                        <p>
                            <a>YWS系列</a></p>
                    </li>
               
                
                </ul>
                <div class="page">
                    <a>首页</a> <a>上一页</a> <a>第1页</a> <a>下一页</a> <a>尾页</a>
                </div>
            </div>
        </div>
    </div>
    <div class="foot">
        <ul>
            <li><a href="Index.aspx">返回首页 </a><span>|</span></li>
            <li><a href="Index.aspx">联系方式 </a><span>|</span></li>
            <li><a href="Index.aspx">咨询中心 </a><span>|</span></li>
            <li><a href="Index.aspx">技术咨询 </a><span>|</span></li>
            <li><a href="Index.aspx">案例演示 </a></li>
        </ul>
        <p>
            版权所有 © 2008-2012 东莞市月无声电子设备有限公司 粤ICP备09218017号</p>
    </div>
    </form>
</body>
</html>
